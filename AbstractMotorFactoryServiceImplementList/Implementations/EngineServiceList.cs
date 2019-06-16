using AbstractMotorFactoryModel;
using AbstractMotorFactoryServiceDAL.BindingModels;
using AbstractMotorFactoryServiceDAL.Interfaces;
using AbstractMotorFactoryServiceDAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AbstractMotorFactoryServiceImplementList.Implementations
{
    public class EngineServiceList : IEngineService
    {
        private DataListSingleton source;

        public EngineServiceList()
        {
            source = DataListSingleton.GetInstance();
        }

        public List<EngineViewModel> GetList()
        {
            List<EngineViewModel> result = source.Engines
                .Select(rec => new EngineViewModel
                {
                    Id = rec.Id,
                    EngineName = rec.EngineName,
                    Cost = rec.Cost,
                    EngineDetails = source.EngineDetails
                        .Where(recED => recED.EngineId == rec.Id)
                        .Select(recED => new EngineDetailViewModel
                        {
                            Id = recED.Id,
                            EngineId = recED.EngineId,
                            DetailId = recED.DetailId,
                            DetailName = source.Details.FirstOrDefault(recD =>
                            recD.Id == recED.DetailId)?.DetailName,
                            Number = recED.Number
                        })
                        .ToList()
                })
                .ToList();
            return result;
        }

        public EngineViewModel GetElement(int id)
        {
            Engine element = source.Engines.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new EngineViewModel
                {
                    Id = element.Id,
                    EngineName = element.EngineName,
                    Cost = element.Cost,
                    EngineDetails = source.EngineDetails
                        .Where(recED => recED.EngineId == element.Id)
                        .Select(recED => new EngineDetailViewModel
                        {
                            Id = recED.Id,
                            EngineId = recED.EngineId,
                            DetailId = recED.DetailId,
                            DetailName = source.Details.FirstOrDefault(recD =>
                            recD.Id == recED.DetailId)?.DetailName,
                            Number = recED.Number
                        })
                        .ToList()
                };
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(EngineBindingModel model)
        {
            Engine element = source.Engines.FirstOrDefault(rec => rec.EngineName == model.EngineName);
            if (element != null)
            {
                throw new Exception("Уже есть двигатель с таким названием");
            }
            int maxId = source.Engines.Count > 0 ? source.Engines.Max(rec => rec.Id) : 0;
            source.Engines.Add(new Engine
            {
                Id = maxId + 1,
                EngineName = model.EngineName,
                Cost = model.Cost
            });

            int maxEDId = source.EngineDetails.Count > 0 ? source.EngineDetails.Max(rec => rec.Id) : 0;

            var groupDetails = model.EngineDetails
                                        .GroupBy(rec => rec.DetailId)
                                        .Select(rec => new
                                        {
                                            DetailId = rec.Key,
                                            Number = rec.Sum(r => r.Number)
                                        });

            foreach (var groupComponent in groupDetails)
            {
                source.EngineDetails.Add(new EngineDetail
                {
                    Id = ++maxEDId,
                    EngineId = maxId + 1,
                    DetailId = groupComponent.DetailId,
                    Number = groupComponent.Number
                });
            }
        }

        public void UpdElement(EngineBindingModel model)
        {
            Engine element = source.Engines.FirstOrDefault(rec => rec.EngineName == model.EngineName && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть изделие с таким названием");
            }
            element = source.Engines.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.EngineName = model.EngineName;
            element.Cost = model.Cost;
            int maxPCId = source.EngineDetails.Count > 0 ? source.EngineDetails.Max(rec => rec.Id) : 0;

            var compIds = model.EngineDetails.Select(rec => rec.DetailId).Distinct();
            var updateDetails = source.EngineDetails.Where(rec => rec.EngineId == model.Id && compIds.Contains(rec.DetailId));
            foreach (var updateDetail in updateDetails)
            {
                updateDetail.Number = model.EngineDetails.FirstOrDefault(rec =>
                    rec.Id == updateDetail.Id).Number;
            }
            source.EngineDetails.RemoveAll(rec => rec.EngineId == model.Id &&
                !compIds.Contains(rec.DetailId));

            var groupDetails = model.EngineDetails
                                        .Where(rec => rec.Id == 0)
                                        .GroupBy(rec => rec.DetailId)
                                        .Select(rec => new
                                        {
                                            ComponentId = rec.Key,
                                            Number = rec.Sum(r => r.Number)
                                        });
            foreach (var groupDetail in groupDetails)
            {
                EngineDetail elementPC = source.EngineDetails.FirstOrDefault(rec
                    => rec.EngineId == model.Id && rec.DetailId == groupDetail.ComponentId);
                if (elementPC != null)
                {
                    elementPC.Number += groupDetail.Number;
                }
                else
                {
                    source.EngineDetails.Add(new EngineDetail
                    {
                        Id = ++maxPCId,
                        EngineId = model.Id,
                        DetailId = groupDetail.ComponentId,
                        Number = groupDetail.Number
                    });
                }
            }
        }

        public void DelElement(int id)
        {
            Engine element = source.Engines.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                source.EngineDetails.RemoveAll(rec => rec.EngineId == id);
                source.Engines.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}
