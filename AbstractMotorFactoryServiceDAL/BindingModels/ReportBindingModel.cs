using System;

namespace AbstractMotorFactoryServiceDAL.BindingModels
{
    public class ReportBindingModel
    {
        public string FileName { get; set; }

        public DateTime? TimeFrom { get; set; }

        public DateTime? TimeTo { get; set; }
    }
}
