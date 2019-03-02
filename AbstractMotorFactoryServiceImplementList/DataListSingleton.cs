using AbstractMotorFactoryModel;
using System.Collections.Generic;

namespace AbstractMotorFactoryServiceImplementList
{
    class DataListSingleton
    {
        private static DataListSingleton instance;

        public List<Customer> Customers { get; set; }

        public List<Detail> Details { get; set; }

        public List<Production> Productions { get; set; }

        public List<Engine> Engines { get; set; }

        public List<EngineDetail> EngineDetails { get; set; }

        private DataListSingleton()
        {
            Customers = new List<Customer>();

            Details = new List<Detail>();

            Productions = new List<Production>();

            Engines = new List<Engine>();

            EngineDetails = new List<EngineDetail>();
        }

        public static DataListSingleton GetInstance()
        {
            if (instance == null)
            {
                instance = new DataListSingleton();
            }
            return instance;
        }
    }
}
