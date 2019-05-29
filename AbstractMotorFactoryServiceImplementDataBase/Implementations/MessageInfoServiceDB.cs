﻿using AbstractMotorFactoryModel;
using AbstractMotorFactoryServiceDAL.BindingModels;
using AbstractMotorFactoryServiceDAL.Interfaces;
using AbstractMotorFactoryServiceDAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text.RegularExpressions;

namespace AbstractMotorFactoryServiceImplementDataBase.Implementations
{
    public class MessageInfoServiceDB : IMessageInfoService
    {
        private AbstractDbContext context;

        public MessageInfoServiceDB(AbstractDbContext context)
        {
            this.context = context;
        }

        public List<MessageInfoViewModel> GetList()
        {
            List<MessageInfoViewModel> result = context.MessageInfos.Where(rec => !rec.CustomerId.HasValue).Select(rec => new MessageInfoViewModel
            {
                MessageId = rec.MessageId,
                ClientName = rec.FromMailAddress,
                DateDelivery = rec.DateDelivery,
                Subject = rec.Subject,
                Body = rec.Body
            })
            .ToList();
            return result;
        }

        public MessageInfoViewModel GetElement(int id)
        {
            MessageInfo element = context.MessageInfos.Include(rec => rec.Customer).FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new MessageInfoViewModel
                {
                    MessageId = element.MessageId,
                    ClientName = element.Customer.CustomerFIO,
                    DateDelivery = element.DateDelivery,
                    Subject = element.Subject,
                    Body = element.Body
                };
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(MessageInfoBindingModel model)
        {
            MessageInfo element = context.MessageInfos.FirstOrDefault(rec =>rec.MessageId == model.MessageId);
            if (element != null)
            {
                return;
            }
            var message = new MessageInfo
            {
                MessageId = model.MessageId,
                FromMailAddress = model.FromMailAddress,
                DateDelivery = model.DateDelivery,
                Subject = model.Subject,
                Body = model.Body
            };
            var mailAddress = Regex.Match(model.FromMailAddress, @"(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9az])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))");
            if (mailAddress.Success)
            {
                var client = context.Customers.FirstOrDefault(rec => rec.Mail == mailAddress.Value);
                if (client != null)
                {
                    message.CustomerId = client.Id;
                }
            }
            context.MessageInfos.Add(message);
            context.SaveChanges();
        }
    }
}
