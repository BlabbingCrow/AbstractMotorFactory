namespace AbstractMotorFactoryView
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.справочникиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.клиентыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.компонентыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.изделияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.buttonCreateProduction = new System.Windows.Forms.Button();
            this.buttonTakeProductionInWork = new System.Windows.Forms.Button();
            this.buttonProductionReady = new System.Windows.Forms.Button();
            this.buttonPayProduction = new System.Windows.Forms.Button();
            this.buttonRef = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.справочникиToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(942, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // справочникиToolStripMenuItem
            // 
            this.справочникиToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.клиентыToolStripMenuItem,
            this.компонентыToolStripMenuItem,
            this.изделияToolStripMenuItem});
            this.справочникиToolStripMenuItem.Name = "справочникиToolStripMenuItem";
            this.справочникиToolStripMenuItem.Size = new System.Drawing.Size(94, 20);
            this.справочникиToolStripMenuItem.Text = "Справочники";
            // 
            // клиентыToolStripMenuItem
            // 
            this.клиентыToolStripMenuItem.Name = "клиентыToolStripMenuItem";
            this.клиентыToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.клиентыToolStripMenuItem.Text = "Покупатели";
            this.клиентыToolStripMenuItem.Click += new System.EventHandler(this.клиентыToolStripMenuItem_Click);
            // 
            // компонентыToolStripMenuItem
            // 
            this.компонентыToolStripMenuItem.Name = "компонентыToolStripMenuItem";
            this.компонентыToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.компонентыToolStripMenuItem.Text = "Детали";
            this.компонентыToolStripMenuItem.Click += new System.EventHandler(this.компонентыToolStripMenuItem_Click);
            // 
            // изделияToolStripMenuItem
            // 
            this.изделияToolStripMenuItem.Name = "изделияToolStripMenuItem";
            this.изделияToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.изделияToolStripMenuItem.Text = "Двигатели";
            this.изделияToolStripMenuItem.Click += new System.EventHandler(this.изделияToolStripMenuItem_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(0, 27);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(737, 422);
            this.dataGridView1.TabIndex = 1;
            // 
            // buttonCreateProduction
            // 
            this.buttonCreateProduction.Location = new System.Drawing.Point(763, 54);
            this.buttonCreateProduction.Name = "buttonCreateProduction";
            this.buttonCreateProduction.Size = new System.Drawing.Size(167, 23);
            this.buttonCreateProduction.TabIndex = 2;
            this.buttonCreateProduction.Text = "Создать заказ";
            this.buttonCreateProduction.UseVisualStyleBackColor = true;
            this.buttonCreateProduction.Click += new System.EventHandler(this.buttonCreateProduction_Click);
            // 
            // buttonTakeProductionInWork
            // 
            this.buttonTakeProductionInWork.Location = new System.Drawing.Point(763, 109);
            this.buttonTakeProductionInWork.Name = "buttonTakeProductionInWork";
            this.buttonTakeProductionInWork.Size = new System.Drawing.Size(167, 23);
            this.buttonTakeProductionInWork.TabIndex = 3;
            this.buttonTakeProductionInWork.Text = "Отдать на выполнение";
            this.buttonTakeProductionInWork.UseVisualStyleBackColor = true;
            this.buttonTakeProductionInWork.Click += new System.EventHandler(this.buttonTakeProductionInWork_Click);
            // 
            // buttonProductionReady
            // 
            this.buttonProductionReady.Location = new System.Drawing.Point(763, 167);
            this.buttonProductionReady.Name = "buttonProductionReady";
            this.buttonProductionReady.Size = new System.Drawing.Size(167, 23);
            this.buttonProductionReady.TabIndex = 4;
            this.buttonProductionReady.Text = "Заказ готов";
            this.buttonProductionReady.UseVisualStyleBackColor = true;
            this.buttonProductionReady.Click += new System.EventHandler(this.buttonProductionReady_Click);
            // 
            // buttonPayProduction
            // 
            this.buttonPayProduction.Location = new System.Drawing.Point(763, 229);
            this.buttonPayProduction.Name = "buttonPayProduction";
            this.buttonPayProduction.Size = new System.Drawing.Size(167, 23);
            this.buttonPayProduction.TabIndex = 5;
            this.buttonPayProduction.Text = "Заказ оплачен";
            this.buttonPayProduction.UseVisualStyleBackColor = true;
            this.buttonPayProduction.Click += new System.EventHandler(this.buttonPayProduction_Click);
            // 
            // buttonRef
            // 
            this.buttonRef.Location = new System.Drawing.Point(763, 288);
            this.buttonRef.Name = "buttonRef";
            this.buttonRef.Size = new System.Drawing.Size(167, 23);
            this.buttonRef.TabIndex = 6;
            this.buttonRef.Text = "Обновить список";
            this.buttonRef.UseVisualStyleBackColor = true;
            this.buttonRef.Click += new System.EventHandler(this.buttonRef_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(942, 450);
            this.Controls.Add(this.buttonRef);
            this.Controls.Add(this.buttonPayProduction);
            this.Controls.Add(this.buttonProductionReady);
            this.Controls.Add(this.buttonTakeProductionInWork);
            this.Controls.Add(this.buttonCreateProduction);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormMain";
            this.Text = "Моторный завод";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem справочникиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem клиентыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem компонентыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem изделияToolStripMenuItem;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button buttonCreateProduction;
        private System.Windows.Forms.Button buttonTakeProductionInWork;
        private System.Windows.Forms.Button buttonProductionReady;
        private System.Windows.Forms.Button buttonPayProduction;
        private System.Windows.Forms.Button buttonRef;
    }
}