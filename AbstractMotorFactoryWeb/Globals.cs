using AbstractMotorFactoryServiceDAL.Interfaces;
using AbstractMotorFactoryServiceImplementDataBase;
using AbstractMotorFactoryServiceImplementDataBase.Implementations;

namespace AbstractMotorFactoryWeb
{
    public static class Globals
    {
        public static AbstractDbContext DbContext { get; } = new AbstractDbContext();

        public static ICustomerService CustomerService { get; } = new CustomerServiceDB(DbContext);

        public static IDetailService DetailService { get; } = new DetailServiceDB(DbContext);

        public static IEngineService EngineService { get; } = new EngineServiceDB(DbContext);

        public static ICoreService CoreService { get; } = new CoreServiceDB(DbContext);

        public static IStorageService StorageService { get; } = new StorageServiceDB(DbContext);
    }
}