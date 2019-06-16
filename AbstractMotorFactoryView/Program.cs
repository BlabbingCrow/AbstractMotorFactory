using AbstractMotorFactoryServiceDAL.Interfaces;
using AbstractMotorFactoryServiceImplementList.Implementations;
using System;
using System.Windows.Forms;
using Unity;
using Unity.Lifetime;

namespace AbstractMotorFactoryView
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var container = BuildUnityContainer();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(container.Resolve<FormMain>());
        }

        public static IUnityContainer BuildUnityContainer()
        {
            var currentContainer = new UnityContainer();
            currentContainer.RegisterType<ICustomerService, CustomerServiceList>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IDetailService, DetailServiceList>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IEngineService, EngineServiceList>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ICoreService, CoreServiceList>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IStorageService, StorageServiceList>(new HierarchicalLifetimeManager());
            return currentContainer;
        }
    }
}
