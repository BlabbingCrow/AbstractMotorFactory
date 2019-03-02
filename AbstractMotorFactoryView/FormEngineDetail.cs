using AbstractMotorFactoryServiceDAL.Interfaces;
using AbstractMotorFactoryServiceDAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Unity;

namespace AbstractMotorFactoryView
{
    public partial class FormEngineDetail : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        public EngineDetailViewModel Model
        {
            set { model = value; }
            get { return model; }
        }

        private readonly IDetailService service;

        private EngineDetailViewModel model;

        public FormEngineDetail(IDetailService service)
        {
            InitializeComponent();
            this.service = service;
        }

        private void FormEngineDetail_Load(object sender, EventArgs e)
        {
            try
            {
                List<DetailViewModel> list = service.GetList();
                if (list != null)
                {
                    comboBox1.DisplayMember = "DetailName";
                    comboBox1.ValueMember = "Id";
                    comboBox1.DataSource = list;
                    comboBox1.SelectedItem = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (model != null)
            {
                comboBox1.Enabled = false;
                comboBox1.SelectedValue = model.DetailId;
                textBox1.Text = model.Number.ToString();
            }
        }


        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Заполните поле Количество", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBox1.SelectedValue == null)
            {
                MessageBox.Show("Выберите компонент", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                if (model == null)
                {
                    model = new EngineDetailViewModel
                    {
                        DetailId = Convert.ToInt32(comboBox1.SelectedValue),
                        DetailName = comboBox1.Text,
                        Number = Convert.ToInt32(textBox1.Text)
                    };
                }
                else
                {
                    model.Number = Convert.ToInt32(textBox1.Text);
                }
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
