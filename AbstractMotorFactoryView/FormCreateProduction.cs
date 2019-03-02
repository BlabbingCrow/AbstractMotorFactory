using AbstractMotorFactoryServiceDAL.BindingModels;
using AbstractMotorFactoryServiceDAL.Interfaces;
using AbstractMotorFactoryServiceDAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Unity;

namespace AbstractMotorFactoryView
{
    public partial class FormCreateProduction : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly ICustomerService serviceCus;

        private readonly IEngineService serviceEng;

        private readonly ICoreService serviceCor;


        public FormCreateProduction(ICustomerService serviceCus, IEngineService serviceEng, ICoreService serviceCor)
        {
            InitializeComponent();
            this.serviceCus = serviceCus;
            this.serviceEng = serviceEng;
            this.serviceCor = serviceCor;
        }

        private void FormCreateProduction_Load(object sender, EventArgs e)
        {
            try
            {
                List<CustomerViewModel> listCus = serviceCus.GetList();
                if (listCus != null)
                {
                    comboBoxCustomer.DisplayMember = "CustomerFIO";
                    comboBoxCustomer.ValueMember = "Id";
                    comboBoxCustomer.DataSource = listCus;
                    comboBoxCustomer.SelectedItem = null;
                }
                List<EngineViewModel> listEng = serviceEng.GetList();
                if (listEng != null)
                {
                    comboBoxEngine.DisplayMember = "EngineName";
                    comboBoxEngine.ValueMember = "Id";
                    comboBoxEngine.DataSource = listEng;
                    comboBoxEngine.SelectedItem = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CalcSum()
        {
            Console.WriteLine("CalcSum");
            if (comboBoxEngine.SelectedValue != null && !string.IsNullOrEmpty(textBoxNumber.Text))
            {
                Console.WriteLine("CalcSum in if");
                try
                {
                    int id = Convert.ToInt32(comboBoxEngine.SelectedValue);
                    EngineViewModel engine = serviceEng.GetElement(id);
                    int count = Convert.ToInt32(textBoxNumber.Text);
                    Console.WriteLine(Convert.ToInt32(engine.Cost));
                    Console.WriteLine(Convert.ToInt32(textBoxNumber.Text));
                    textBoxSum.Text = (count * engine.Cost).ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void textBoxNum_TextChanged(object sender, EventArgs e)
        {
            CalcSum();
        }

        private void comboBoxEngine_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalcSum();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxNumber.Text))
            {
                MessageBox.Show("Заполните поле Количество", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxCustomer.SelectedValue == null)
            {
                MessageBox.Show("Выберите клиента", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxEngine.SelectedValue == null)
            {
                MessageBox.Show("Выберите изделие", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                serviceCor.CreateOrder(new ProductionBindingModel
                {
                    CustomerId = Convert.ToInt32(comboBoxCustomer.SelectedValue),
                    EngineId = Convert.ToInt32(comboBoxEngine.SelectedValue),
                    Number = Convert.ToInt32(textBoxNumber.Text),
                    Amount = Convert.ToInt32(textBoxSum.Text)
                });
                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
