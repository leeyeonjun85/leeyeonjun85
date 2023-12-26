namespace JsonStudy
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tableLayoutPanel1 = new TableLayoutPanel();
            btnClearData = new Button();
            btnLoadData = new Button();
            btnSaveJson = new Button();
            dataGridView1 = new DataGridView();
            btnInitData = new Button();
            label1 = new Label();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
            tableLayoutPanel1.Controls.Add(btnClearData, 0, 3);
            tableLayoutPanel1.Controls.Add(btnLoadData, 0, 2);
            tableLayoutPanel1.Controls.Add(btnSaveJson, 0, 1);
            tableLayoutPanel1.Controls.Add(dataGridView1, 1, 0);
            tableLayoutPanel1.Controls.Add(btnInitData, 0, 0);
            tableLayoutPanel1.Controls.Add(label1, 0, 4);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 5;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Size = new Size(800, 450);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // btnClearData
            // 
            btnClearData.Dock = DockStyle.Fill;
            btnClearData.Font = new Font("맑은 고딕", 14F);
            btnClearData.Location = new Point(3, 273);
            btnClearData.Name = "btnClearData";
            btnClearData.Size = new Size(314, 84);
            btnClearData.TabIndex = 5;
            btnClearData.Text = "Clear Data";
            btnClearData.UseVisualStyleBackColor = true;
            btnClearData.Click += BtnClearData_Click;
            // 
            // btnLoadData
            // 
            btnLoadData.Dock = DockStyle.Fill;
            btnLoadData.Font = new Font("맑은 고딕", 14F);
            btnLoadData.Location = new Point(3, 183);
            btnLoadData.Name = "btnLoadData";
            btnLoadData.Size = new Size(314, 84);
            btnLoadData.TabIndex = 3;
            btnLoadData.Text = "Load Data";
            btnLoadData.UseVisualStyleBackColor = true;
            btnLoadData.Click += BtnLoadData_Click;
            // 
            // btnSaveJson
            // 
            btnSaveJson.Dock = DockStyle.Fill;
            btnSaveJson.Font = new Font("맑은 고딕", 14F);
            btnSaveJson.Location = new Point(3, 93);
            btnSaveJson.Name = "btnSaveJson";
            btnSaveJson.Size = new Size(314, 84);
            btnSaveJson.TabIndex = 2;
            btnSaveJson.Text = "Save Json";
            btnSaveJson.UseVisualStyleBackColor = true;
            btnSaveJson.Click += BtnSaveJson_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.Location = new Point(323, 3);
            dataGridView1.Name = "dataGridView1";
            tableLayoutPanel1.SetRowSpan(dataGridView1, 5);
            dataGridView1.Size = new Size(474, 444);
            dataGridView1.TabIndex = 0;
            // 
            // btnInitData
            // 
            btnInitData.Dock = DockStyle.Fill;
            btnInitData.Font = new Font("맑은 고딕", 14F);
            btnInitData.Location = new Point(3, 3);
            btnInitData.Name = "btnInitData";
            btnInitData.Size = new Size(314, 84);
            btnInitData.TabIndex = 1;
            btnInitData.Text = "Init Data";
            btnInitData.UseVisualStyleBackColor = true;
            btnInitData.Click += BtnInitData_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Dock = DockStyle.Fill;
            label1.Font = new Font("맑은 고딕", 12F);
            label1.Location = new Point(3, 360);
            label1.Name = "label1";
            label1.Size = new Size(314, 90);
            label1.TabIndex = 6;
            label1.Text = "label1";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(tableLayoutPanel1);
            Name = "Form1";
            Text = "Form1";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private DataGridView dataGridView1;
        private Button btnInitData;
        private Button btnClearData;
        private Button btnLoadData;
        private Button btnSaveJson;
        private Label label1;
    }
}
