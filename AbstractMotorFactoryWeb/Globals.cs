using AbstractMotorFactoryServiceDAL.Interfaces;
using AbstractMotorFactoryServiceImplementList.Implementations;

namespace AbstractMotorFactoryWeb
{
    public static class Globals
    {

        public static ICustomerService CustomerService { get; } = new CustomerServiceList();

        public static IDetailService DetailService { get; } = new DetailServiceList();

        public static IEngineService EngineService { get; } = new EngineServiceList();

        public static ICoreService CoreService { get; } = new CoreServiceList();

    }
}