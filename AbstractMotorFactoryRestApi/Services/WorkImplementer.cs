using AbstractMotorFactoryServiceDAL.BindingModels;
using AbstractMotorFactoryServiceDAL.Interfaces;
using System;
using System.Threading;

namespace AbstractMotorFactoryRestApi.Services
{
    public class WorkImplementer
    {
        private readonly ICoreService _service;
        private readonly IImplementerService _serviceImplementer;
        private readonly int _implementerId;
        private readonly int _orderId;
        
        static Semaphore _sem = new Semaphore(3, 3);

        Thread myThread;

        public WorkImplementer(ICoreService service, IImplementerService serviceImplementer, int implementerId, int orderId)
        {
            _service = service;
            _serviceImplementer = serviceImplementer;
            _implementerId = implementerId;
            _orderId = orderId;
            try
            {
                _service.TakeOrderInWork(new ProductionBindingModel
                {
                    Id = _orderId,
                    ImplementerId = _implementerId
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            myThread = new Thread(Work);
            myThread.Start();
        }

        public void Work()
        {
            try
            {
                _sem.WaitOne();
                Thread.Sleep(10000);
                _service.FinishOrder(new ProductionBindingModel
                {
                    Id = _orderId
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                _sem.Release();
            }
        }
    }
}