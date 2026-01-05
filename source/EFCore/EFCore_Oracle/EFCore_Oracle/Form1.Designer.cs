namespace EFCore_Oracle
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
            dataGridView1 = new DataGridView();
            textBox1 = new TextBox();
            btnCreate = new Button();
            btnConnection = new Button();
            btnAddOneStudent = new Button();
            addName = new TextBox();
            btnDelete = new Button();
            btnUpdate = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(271, 93);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.Size = new Size(517, 345);
            dataGridView1.TabIndex = 0;
            // 
            // textBox1
            // 
            textBox1.BackColor = SystemColors.Info;
            textBox1.Location = new Point(12, 93);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.ScrollBars = ScrollBars.Both;
            textBox1.Size = new Size(253, 345);
            textBox1.TabIndex = 1;
            textBox1.Text = "== Text Box ==";
            // 
            // btnCreate
            // 
            btnCreate.Location = new Point(12, 41);
            btnCreate.Name = "btnCreate";
            btnCreate.Size = new Size(113, 23);
            btnCreate.TabIndex = 2;
            btnCreate.Text = "Create Table";
            btnCreate.UseVisualStyleBackColor = true;
            btnCreate.Click += btnCreate_Click;
            // 
            // btnConnection
            // 
            btnConnection.Location = new Point(12, 12);
            btnConnection.Name = "btnConnection";
            btnConnection.Size = new Size(113, 23);
            btnConnection.TabIndex = 3;
            btnConnection.Text = "Connect Oracle";
            btnConnection.UseVisualStyleBackColor = true;
            btnConnection.Click += btnConnection_Click;
            // 
            // btnAddOneStudent
            // 
            btnAddOneStudent.Location = new Point(131, 41);
            btnAddOneStudent.Name = "btnAddOneStudent";
            btnAddOneStudent.Size = new Size(113, 23);
            btnAddOneStudent.TabIndex = 4;
            btnAddOneStudent.Text = "+1 Student";
            btnAddOneStudent.UseVisualStyleBackColor = true;
            btnAddOneStudent.Click += btnAddOneStudent_Click;
            // 
            // addName
            // 
            addName.Location = new Point(131, 12);
            addName.Name = "addName";
            addName.Size = new Size(113, 23);
            addName.TabIndex = 5;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(250, 41);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(113, 23);
            btnDelete.TabIndex = 6;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(250, 12);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(113, 23);
            btnUpdate.TabIndex = 7;
            btnUpdate.Text = "Update";
            btnUpdate.UseVisualStyleBackColor = true;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnUpdate);
            Controls.Add(btnDelete);
            Controls.Add(addName);
            Controls.Add(btnAddOneStudent);
            Controls.Add(btnConnection);
            Controls.Add(btnCreate);
            Controls.Add(textBox1);
            Controls.Add(dataGridView1);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView1;
        private TextBox textBox1;
        private Button btnCreate;
        private Button btnConnection;
        private Button btnAddOneStudent;
        private TextBox addName;
        private Button btnDelete;
        private Button btnUpdate;
    }
}