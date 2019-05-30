using AbstractMotorFactoryModel;
using AbstractMotorFactoryServiceDAL.BindingModels;
using AbstractMotorFactoryServiceDAL.Interfaces;
using AbstractMotorFactoryServiceDAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.SqlServer;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;

namespace AbstractMotorFactoryServiceImplementDataBase.Implementations
{
    public class CoreServiceDB : ICoreService
    {
        private AbstractDbContext context;

        public CoreServiceDB(AbstractDbContext context)
        {
            this.context = context;
        }
        public List<ProductionViewModel> GetList()
        {
            List<ProductionViewModel> result = context.Productions.Select(rec => new ProductionViewModel
            {
                Id = rec.Id,
                CustomerId = rec.CustomerId,
                EngineId = rec.EngineId,
                ImplementerId = rec.ImplementerId,
                TimeCreate = SqlFunctions.DateName("dd", rec.TimeCreate) + " " +
                    SqlFunctions.DateName("mm", rec.TimeCreate) + " " +
                    SqlFunctions.DateName("yyyy", rec.TimeCreate),
                TimeImplement = rec.TimeImplement == null ? "" :
                    SqlFunctions.DateName("dd", rec.TimeImplement.Value) + " " +
                    SqlFunctions.DateName("mm", rec.TimeImplement.Value) + " " +
                    SqlFunctions.DateName("yyyy", rec.TimeImplement.Value),
                State = rec.State.ToString(),
                Number = rec.Number,
                Amount = rec.Amount,
                CustomerFIO = rec.Customer.CustomerFIO,
                EngineName = rec.Engine.EngineName,
                ImplementerName = rec.Implementer.ImplementerFIO
            })
            .ToList();
            return result;
        }

        public List<ProductionViewModel> GetFreeOrders()
        {
            List<ProductionViewModel> result = context.Productions
            .Where(x => x.State == ProductionStatus.Принят || x.State == ProductionStatus.НедостаточноРесурсов)
            .Select(rec => new ProductionViewModel
            {
                Id = rec.Id
            })
            .ToList();
            return result;
        }

        public void CreateOrder(ProductionBindingModel model)
        {
            var production = new Production
            {
                CustomerId = model.CustomerId,
                EngineId = model.EngineId,
                TimeCreate = DateTime.Now,
                Number = model.Number,
                Amount = model.Amount,
                State = ProductionStatus.Принят
            };
            context.Productions.Add(production);
            context.SaveChanges();

            var customer = context.Customers.FirstOrDefault(x => x.Id == model.CustomerId);
            SendEmail(customer.Mail, "Оповещение по заказам", string.Format("Заказ №{0} от {1} создан успешно", production.Id, production.TimeCreate.ToShortDateString()));
        }

        public void TakeOrderInWork(ProductionBindingModel model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                Production element = context.Productions.FirstOrDefault(rec => rec.Id == model.Id);
                try
                {
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                    if (element.State != ProductionStatus.Принят && element.State != ProductionStatus.НедостаточноРесурсов)
                    {
                        throw new Exception("Заказ не в статусе \"Принят\"");
                    }

                    var engineDetails = context.EngineDetails.Include(rec => rec.Detail).Where(rec => rec.EngineId == element.EngineId).ToList();
                    foreach (var productComponent in engineDetails)
                    {
                        int NumberOnStocks = productComponent.Number * element.Number;
                        var storageComponents = context.StorageDetails.Where(rec => rec.DetailId == productComponent.DetailId).ToList();
                        foreach (var stockComponent in storageComponents)
                        {
                            if (stockComponent.Number >= NumberOnStocks)
                            {
                                stockComponent.Number -= NumberOnStocks;
                                NumberOnStocks = 0;
                                context.SaveChanges();
                                break;
                            }
                            else
                            {
                                NumberOnStocks -= stockComponent.Number;
                                stockComponent.Number = 0;
                                context.SaveChanges();
                            }
                        }
                        if (NumberOnStocks > 0)
                        {
                            throw new Exception("Не достаточно компонента " + productComponent.Detail.DetailName + " требуется " + productComponent.Number + ", нехватает " + NumberOnStocks);
                        }
                    }
                    element.TimeImplement = DateTime.Now;
                    element.State = ProductionStatus.Выполняется;
                    element.ImplementerId = model.ImplementerId;
                    context.SaveChanges();
                    SendEmail(element.Customer.Mail, "Оповещение по заказам", string.Format("Заказ №{0} от {1} передеан в работу", element.Id, element.TimeCreate.ToShortDateString()));
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    element.State = ProductionStatus.НедостаточноРесурсов;
                    context.SaveChanges();
                    transaction.Commit();
                    throw;
                }
            }
        }

        public void FinishOrder(ProductionBindingModel model)
        {
            Production element = context.Productions.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            if (element.State != ProductionStatus.Выполняется)
            {
                throw new Exception("Заказ не в статусе \"Выполняется\"");
            }
            element.State = ProductionStatus.Готов;
            context.SaveChanges();
            SendEmail(element.Customer.Mail, "Оповещение по заказам", string.Format("Заказ №{0} от {1} передан на оплату", element.Id, element.TimeCreate.ToShortDateString()));
        }

        public void PayOrder(ProductionBindingModel model)
        {
            Production element = context.Productions.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            if (element.State != ProductionStatus.Готов)
            {
                throw new Exception("Заказ не в статусе \"Готов\"");
            }
            element.State = ProductionStatus.Оплачен;
            context.SaveChanges();
            SendEmail(element.Customer.Mail, "Оповещение по заказам", string.Format("Заказ №{0} от {1} оплачен успешно", element.Id, element.TimeCreate.ToShortDateString()));
        }

        public void PutDetailOnStorage(StorageDetailBindingModel model)
        {
            StorageDetail element = context.StorageDetails.FirstOrDefault(rec => rec.StorageId == model.StorageId && rec.DetailId == model.DetailId);
            if (element != null)
            {
                element.Number += model.Number;
            }
            else
            {
                context.StorageDetails.Add(new StorageDetail
                {
                    StorageId = model.StorageId,
                    DetailId = model.DetailId,
                    Number = model.Number
                });
            }
            context.SaveChanges();
        }

        private void SendEmail(string mailAddress, string subject, string text)
        {
            MailMessage objMailMessage = new MailMessage();
            SmtpClient objSmtpClient = null;
            try
            {
                objMailMessage.From = new MailAddress(ConfigurationManager.AppSettings["MailLogin"]);
                objMailMessage.To.Add(new MailAddress(mailAddress));
                objMailMessage.Subject = subject;
                objMailMessage.Body = text;
                objMailMessage.SubjectEncoding = System.Text.Encoding.UTF8;
                objMailMessage.BodyEncoding = System.Text.Encoding.UTF8;
                objSmtpClient = new SmtpClient("smtp.gmail.com", 587);
                objSmtpClient.UseDefaultCredentials = false;
                objSmtpClient.EnableSsl = true;
                objSmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                objSmtpClient.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["MailLogin"], ConfigurationManager.AppSettings["MailPassword"]);
                objSmtpClient.Send(objMailMessage);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objMailMessage = null;
                objSmtpClient = null;
            }
        }

    }
}
