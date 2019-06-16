using AbstractMotorFactoryModel;
using AbstractMotorFactoryServiceDAL.BindingModels;
using AbstractMotorFactoryServiceDAL.Interfaces;
using AbstractMotorFactoryServiceDAL.ViewModels;
using System;
using System.Collections.Generic;

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
            List<EngineViewModel> result = new List<EngineViewModel>();
            for (int i = 0; i < source.Engines.Count; ++i)
            {
                List<EngineDetailViewModel> engineDetails = new List<EngineDetailViewModel> ();
                for (int j = 0; j < source.EngineDetails.Count; ++j)
                {
                    if (source.EngineDetails[j].EngineId == source.Engines[i].Id)
                    {
                        string detailName = string.Empty;
                        for (int k = 0; k < source.Details.Count; ++k)
                        {
                            if (source.EngineDetails[j].DetailId == source.Details[k].Id)
                            {
                                detailName = source.Details[k].DetailName;
                                break;
                            }
                        }
                        engineDetails.Add(new EngineDetailViewModel
                        {
                            Id = source.EngineDetails[j].Id,
                            EngineId = source.EngineDetails[j].EngineId,
                            DetailId = source.EngineDetails[j].DetailId,
                            DetailName = detailName,
                            Number = source.EngineDetails[j].Number
                        });
                    }
                }
                result.Add(new EngineViewModel
                {
                    Id = source.Engines[i].Id,
                    EngineName = source.Engines[i].EngineName,
                    Cost = source.Engines[i].Cost,
                    EngineDetails = engineDetails
                });
            }
            return result;
        }

        public EngineViewModel GetElement(int id)
        {
            for (int i = 0; i < source.Engines.Count; ++i)
            {
                List<EngineDetailViewModel> engineDetails = new List<EngineDetailViewModel>();
                for (int j = 0; j < source.EngineDetails.Count; ++j)
                {
                    if (source.EngineDetails[j].EngineId == source.Engines[i].Id)
                    {
                        string detailName = string.Empty;
                        for (int k = 0; k < source.Engines.Count; ++k)
                        {
                            if (source.EngineDetails[j].DetailId == source.Details[k].Id)
                            {
                                detailName = source.Details[k].DetailName;
                                break;
                            }
                        }
                        engineDetails.Add(new EngineDetailViewModel
                        {
                            Id = source.EngineDetails[j].Id,
                            EngineId = source.EngineDetails[j].EngineId,
                            DetailId = source.EngineDetails[j].DetailId,
                            DetailName = detailName,
                            Number = source.EngineDetails[j].Number
                        });
                    }
                }
                if (source.Engines[i].Id == id)
                {
                    return new EngineViewModel
                    {
                        Id = source.Engines[i].Id,
                        EngineName = source.Engines[i].EngineName,
                        Cost = source.Engines[i].Cost,
                        EngineDetails = engineDetails
                    };
                }
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(EngineBindingModel model)
        {
            int maxId = 0;
            for (int i = 0; i < source.Engines.Count; ++i)
            {
                if (source.Engines[i].Id > maxId)
                {
                    maxId = source.Engines[i].Id;
                }
                if (source.Engines[i].EngineName == model.EngineName)
                {
                    throw new Exception("Уже есть изделие с таким названием");
                }
            }
            source.Engines.Add(new Engine
            {
                Id = maxId + 1,
                EngineName = model.EngineName,
                Cost = model.Cost
            });
            int maxPCId = 0;
            for (int i = 0; i < source.EngineDetails.Count; ++i)
            {
                if (source.EngineDetails[i].Id > maxPCId)
                {
                    maxPCId = source.EngineDetails[i].Id;
                }
            }
            for (int i = 0; i < model.EngineDetails.Count; ++i)
            {
                for (int j = i + 1; j < model.EngineDetails.Count; ++j)
                {
                    if (model.EngineDetails[i].DetailId ==
                    model.EngineDetails[j].DetailId)
                    {
                        model.EngineDetails[i].Number += model.EngineDetails[j].Number;
                        model.EngineDetails.RemoveAt(j--);
                    }
                }
            }
            for (int i = 0; i < model.EngineDetails.Count; ++i)
            {
                source.EngineDetails.Add(new EngineDetail
                {
                    Id = ++maxPCId,
                    EngineId = maxId + 1,
                    DetailId = model.EngineDetails[i].DetailId,
                    Number = model.EngineDetails[i].Number
                });
            }
        }

        public void UpdElement(EngineBindingModel model)
        {
            int index = -1;
            for (int i = 0; i < source.Engines.Count; ++i)
            {
                if (source.Engines[i].Id == model.Id)
                {
                    index = i;
                }
                if (source.Engines[i].EngineName == model.EngineName &&
                source.Engines[i].Id != model.Id)
                {
                    throw new Exception("Уже есть изделие с таким названием");
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            source.Engines[index].EngineName = model.EngineName;
            source.Engines[index].Cost = model.Cost;
            int maxPCId = 0;
            for (int i = 0; i < source.EngineDetails.Count; ++i)
            {
                if (source.EngineDetails[i].Id > maxPCId)
                {
                    maxPCId = source.EngineDetails[i].Id;
                }
            }
            // обновляем существуюущие компоненты
            for (int i = 0; i < source.EngineDetails.Count; ++i)
            {
                if (source.EngineDetails[i].EngineId == model.Id)
                {
                    bool flag = true;
                    for (int j = 0; j < model.EngineDetails.Count; ++j)
                    {
                        // если встретили, то изменяем количество
                        if (source.EngineDetails[i].Id ==
                       model.EngineDetails[j].Id)
                        {
                            source.EngineDetails[i].Number = model.EngineDetails[j].Number;
                            flag = false;
                            break;
                        }
                    }
                    // если не встретили, то удаляем
                    if (flag)
                    {
                        source.EngineDetails.RemoveAt(i--);
                    }
                }
            }
            // новые записи
            for (int i = 0; i < model.EngineDetails.Count; ++i)
            {
                if (model.EngineDetails[i].Id == 0)
                {
                    // ищем дубли
                    for (int j = 0; j < source.EngineDetails.Count; ++j)
                    {
                        if (source.EngineDetails[j].EngineId == model.Id && source.EngineDetails[j].DetailId == model.EngineDetails[i].DetailId)
                        {
                            source.EngineDetails[j].Number += model.EngineDetails[i].Number;
                            model.EngineDetails[i].Id = source.EngineDetails[j].Id;
                            break;
                        }
                    }
                    // если не нашли дубли, то новая запись
                    if (model.EngineDetails[i].Id == 0)
                    {
                        source.EngineDetails.Add(new EngineDetail
                        {
                            Id = ++maxPCId,
                            EngineId = model.Id,
                            DetailId = model.EngineDetails[i].DetailId,
                            Number = model.EngineDetails[i].Number
                        });
                    }
                }
            }
        }

        public void DelElement(int id)
        {
            // удаяем записи по компонентам при удалении изделия
            for (int i = 0; i < source.EngineDetails.Count; ++i)
            {
                if (source.EngineDetails[i].EngineId == id)
                {
                    source.EngineDetails.RemoveAt(i--);
                }
            }
            for (int i = 0; i < source.Engines.Count; ++i)
            {
                if (source.Engines[i].Id == id)
                {
                    source.Engines.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
    }
}
