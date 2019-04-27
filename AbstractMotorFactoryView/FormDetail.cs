using AbstractMotorFactoryServiceDAL.BindingModels;
using AbstractMotorFactoryServiceDAL.Interfaces;
using AbstractMotorFactoryServiceDAL.ViewModels;
using System;
using System.Windows.Forms;

namespace AbstractMotorFactoryView
{
    public partial class FormDetail : Form
    {
        public int Id { set { id = value; } }

        private int? id;

        public FormDetail()
        {
            InitializeComponent();
        }

        private void FormClient_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    DetailViewModel view = APIClient.GetRequest<DetailViewModel>("api/Detail/Get/" + id.Value);
                    if (view != null)
                    {
                        textBox1.Text = view.DetailName;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Заполните ФИО", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                if (id.HasValue)
                {
                    APIClient.PostRequest<DetailBindingModel, bool>("api/Detail/UpdElement", new DetailBindingModel
                    {
                        Id = id.Value,
                        DetailName = textBox1.Text
                    });
                }
                else
                {
                    APIClient.PostRequest<DetailBindingModel, bool>("api/Detail/AddElement", new DetailBindingModel
                    {
                        DetailName = textBox1.Text
                    });
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
