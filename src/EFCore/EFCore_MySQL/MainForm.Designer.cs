﻿namespace EFCore_MySQL
{
    partial class MainForm
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
            components = new System.ComponentModel.Container();
            lblStatus = new Label();
            btnSave = new Button();
            btnUpdate = new Button();
            btnDelete = new Button();
            btnConnect = new Button();
            panel1 = new Panel();
            dataGridView1 = new DataGridView();
            studentBindingSource = new BindingSource(components);
            tbName = new TextBox();
            tbxIP = new TextBox();
            tbxPort = new TextBox();
            tbxID = new TextBox();
            tbxPW = new TextBox();
            tbxDbName = new TextBox();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)studentBindingSource).BeginInit();
            SuspendLayout();
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(12, 221);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(78, 15);
            lblStatus.TabIndex = 1;
            lblStatus.Text = "상태 : 대기중";
            // 
            // btnSave
            // 
            btnSave.Location = new Point(12, 268);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(142, 24);
            btnSave.TabIndex = 3;
            btnSave.Text = "저장";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(12, 298);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(142, 24);
            btnUpdate.TabIndex = 5;
            btnUpdate.Text = "수정";
            btnUpdate.UseVisualStyleBackColor = true;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(12, 328);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(142, 24);
            btnDelete.TabIndex = 6;
            btnDelete.Text = "삭제";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnConnect
            // 
            btnConnect.Location = new Point(12, 157);
            btnConnect.Name = "btnConnect";
            btnConnect.Size = new Size(142, 24);
            btnConnect.TabIndex = 0;
            btnConnect.Text = "DB 연결";
            btnConnect.UseVisualStyleBackColor = true;
            btnConnect.Click += btnConnect_Click;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel1.Controls.Add(dataGridView1);
            panel1.Location = new Point(160, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(443, 338);
            panel1.TabIndex = 7;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.Location = new Point(0, 0);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.Size = new Size(443, 338);
            dataGridView1.TabIndex = 0;
            // 
            // studentBindingSource
            // 
            studentBindingSource.DataSource = typeof(Models.Student);
            // 
            // tbName
            // 
            tbName.Location = new Point(12, 239);
            tbName.Name = "tbName";
            tbName.Size = new Size(142, 23);
            tbName.TabIndex = 8;
            // 
            // tbxIP
            // 
            tbxIP.Location = new Point(12, 12);
            tbxIP.Name = "tbxIP";
            tbxIP.Size = new Size(142, 23);
            tbxIP.TabIndex = 9;
            // 
            // tbxPort
            // 
            tbxPort.Location = new Point(12, 41);
            tbxPort.Name = "tbxPort";
            tbxPort.Size = new Size(142, 23);
            tbxPort.TabIndex = 10;
            // 
            // tbxID
            // 
            tbxID.Location = new Point(12, 99);
            tbxID.Name = "tbxID";
            tbxID.Size = new Size(142, 23);
            tbxID.TabIndex = 11;
            // 
            // tbxPW
            // 
            tbxPW.Location = new Point(12, 128);
            tbxPW.Name = "tbxPW";
            tbxPW.Size = new Size(142, 23);
            tbxPW.TabIndex = 12;
            // 
            // tbxDbName
            // 
            tbxDbName.Location = new Point(12, 70);
            tbxDbName.Name = "tbxDbName";
            tbxDbName.Size = new Size(142, 23);
            tbxDbName.TabIndex = 13;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(615, 362);
            Controls.Add(tbxDbName);
            Controls.Add(tbxPW);
            Controls.Add(tbxID);
            Controls.Add(tbxPort);
            Controls.Add(tbxIP);
            Controls.Add(tbName);
            Controls.Add(panel1);
            Controls.Add(btnDelete);
            Controls.Add(btnUpdate);
            Controls.Add(btnSave);
            Controls.Add(btnConnect);
            Controls.Add(lblStatus);
            Name = "MainForm";
            Text = "Form1";
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)studentBindingSource).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label lblStatus;
        private Button btnSave;
        private Button btnUpdate;
        private Button btnDelete;
        private Button btnConnect;
        private Panel panel1;
        private DataGridView dataGridView1;
        private TextBox tbName;
        private BindingSource studentBindingSource;
        private TextBox tbxIP;
        private TextBox tbxPort;
        private TextBox tbxID;
        private TextBox tbxPW;
        private TextBox tbxDbName;
    }
}