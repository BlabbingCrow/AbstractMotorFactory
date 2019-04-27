using AbstractMotorFactoryServiceDAL.BindingModels;
using AbstractMotorFactoryServiceDAL.Interfaces;
using AbstractMotorFactoryServiceDAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AbstractMotorFactoryView
{
    public partial class FormEngine : Form
    {
        public int Id { set { id = value; } }

        private int? id;

        private List<EngineDetailViewModel> productComponents;


        public FormEngine()
        {
            InitializeComponent();
        }

        private void FormEngine_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    EngineViewModel view = APIClient.GetRequest<EngineViewModel>("api/Engine/Get/" + id.Value);
                    if (view != null)
                    {
                        textBox1.Text = view.EngineName;
                        textBox2.Text = view.Cost.ToString();
                        productComponents = view.EngineDetails;
                        LoadData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                }
            }
            else
            {
                productComponents = new List<EngineDetailViewModel>();
            }
        }

        private void LoadData()
        {
            try
            {
                if (productComponents != null)
                {
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = productComponents;
                    dataGridView1.Columns[0].Visible = false;
                    dataGridView1.Columns[1].Visible = false;
                    dataGridView1.Columns[2].Visible = false;
                    dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            catch (Exception ex)
            {
                 MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var form = new FormEngineDetail();
            if (form.ShowDialog() == DialogResult.OK)
            {
                if (form.Model != null)
                {
                    if (id.HasValue)
                    {
                        form.Model.EngineId = id.Value;
                    }
                    productComponents.Add(form.Model);
                }
                LoadData();
            }

        }

        private void buttonUpd_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                var form = new FormEngineDetail();
                form.Model = productComponents[dataGridView1.SelectedRows[0].Cells[0].RowIndex];
                if (form.ShowDialog() == DialogResult.OK)
                {
                    productComponents[dataGridView1.SelectedRows[0].Cells[0].RowIndex] = form.Model;
                    LoadData();
                }
            }
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                if (MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        productComponents.RemoveAt(dataGridView1.SelectedRows[0].Cells[0].RowIndex);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    LoadData();
                }
            }
        }

        private void buttonRef_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Заполните цену", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (productComponents == null || productComponents.Count == 0)
            {
                MessageBox.Show("Заполните компоненты", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                List<EngineDetailBindingModel> productComponentBM = new List<EngineDetailBindingModel>();
                for (int i = 0; i < productComponents.Count; ++i)
                {
                    productComponentBM.Add(new EngineDetailBindingModel
                    {
                        Id = productComponents[i].Id,
                        EngineId = productComponents[i].EngineId,
                        DetailId = productComponents[i].DetailId,
                        Number = productComponents[i].Number
                    });
                }
                if (id.HasValue)
                {
                    APIClient.PostRequest<EngineBindingModel, bool>("api/Engine/UpdElement", new EngineBindingModel
                    {
                        Id = id.Value,
                        EngineName = textBox1.Text,
                        Cost = Convert.ToInt32(textBox2.Text),
                        EngineDetails = productComponentBM
                    });
                }
                else
                {
                    APIClient.PostRequest<EngineBindingModel, bool>("api/Engine/AddElement", new EngineBindingModel
                    {
                        EngineName = textBox1.Text,
                        Cost = Convert.ToInt32(textBox2.Text),
                        EngineDetails = productComponentBM
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
