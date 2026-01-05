namespace EFCore_MySQL
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
            dataGridView1 = new DataGridView();
            studentBindingSource = new BindingSource(components);
            tbName = new TextBox();
            tbxIP = new TextBox();
            tbxPort = new TextBox();
            tbxID = new TextBox();
            tbxPW = new TextBox();
            tbxDbName = new TextBox();
            tableLayoutPanel1 = new TableLayoutPanel();
            flowLayoutPanel1 = new FlowLayoutPanel();
            pictureBox1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)studentBindingSource).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(3, 175);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(78, 15);
            lblStatus.TabIndex = 1;
            lblStatus.Text = "상태 : 대기중";
            // 
            // btnSave
            // 
            btnSave.Location = new Point(3, 222);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(142, 24);
            btnSave.TabIndex = 3;
            btnSave.Text = "저장";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(3, 252);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(142, 24);
            btnUpdate.TabIndex = 5;
            btnUpdate.Text = "수정";
            btnUpdate.UseVisualStyleBackColor = true;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(3, 282);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(142, 24);
            btnDelete.TabIndex = 6;
            btnDelete.Text = "삭제";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnConnect
            // 
            btnConnect.Location = new Point(3, 148);
            btnConnect.Name = "btnConnect";
            btnConnect.Size = new Size(142, 24);
            btnConnect.TabIndex = 0;
            btnConnect.Text = "DB 연결";
            btnConnect.UseVisualStyleBackColor = true;
            btnConnect.Click += btnConnect_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.Location = new Point(173, 3);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.Size = new Size(568, 163);
            dataGridView1.TabIndex = 0;
            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;
            // 
            // studentBindingSource
            // 
            studentBindingSource.DataSource = typeof(Models.Student);
            // 
            // tbName
            // 
            tbName.Location = new Point(3, 193);
            tbName.Name = "tbName";
            tbName.Size = new Size(142, 23);
            tbName.TabIndex = 8;
            // 
            // tbxIP
            // 
            tbxIP.Location = new Point(3, 3);
            tbxIP.Name = "tbxIP";
            tbxIP.Size = new Size(142, 23);
            tbxIP.TabIndex = 9;
            // 
            // tbxPort
            // 
            tbxPort.Location = new Point(3, 32);
            tbxPort.Name = "tbxPort";
            tbxPort.Size = new Size(142, 23);
            tbxPort.TabIndex = 10;
            // 
            // tbxID
            // 
            tbxID.Location = new Point(3, 90);
            tbxID.Name = "tbxID";
            tbxID.Size = new Size(142, 23);
            tbxID.TabIndex = 11;
            // 
            // tbxPW
            // 
            tbxPW.Location = new Point(3, 119);
            tbxPW.Name = "tbxPW";
            tbxPW.Size = new Size(142, 23);
            tbxPW.TabIndex = 12;
            // 
            // tbxDbName
            // 
            tbxDbName.Location = new Point(3, 61);
            tbxDbName.Name = "tbxDbName";
            tbxDbName.Size = new Size(142, 23);
            tbxDbName.TabIndex = 13;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 170F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(dataGridView1, 1, 0);
            tableLayoutPanel1.Controls.Add(flowLayoutPanel1, 0, 0);
            tableLayoutPanel1.Controls.Add(pictureBox1, 1, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 40F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 60F));
            tableLayoutPanel1.Size = new Size(744, 423);
            tableLayoutPanel1.TabIndex = 14;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Controls.Add(tbxIP);
            flowLayoutPanel1.Controls.Add(tbxPort);
            flowLayoutPanel1.Controls.Add(tbxDbName);
            flowLayoutPanel1.Controls.Add(tbxID);
            flowLayoutPanel1.Controls.Add(tbxPW);
            flowLayoutPanel1.Controls.Add(btnConnect);
            flowLayoutPanel1.Controls.Add(lblStatus);
            flowLayoutPanel1.Controls.Add(tbName);
            flowLayoutPanel1.Controls.Add(btnSave);
            flowLayoutPanel1.Controls.Add(btnUpdate);
            flowLayoutPanel1.Controls.Add(btnDelete);
            flowLayoutPanel1.Dock = DockStyle.Fill;
            flowLayoutPanel1.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel1.Location = new Point(3, 3);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            tableLayoutPanel1.SetRowSpan(flowLayoutPanel1, 2);
            flowLayoutPanel1.Size = new Size(164, 417);
            flowLayoutPanel1.TabIndex = 0;
            // 
            // pictureBox1
            // 
            pictureBox1.Dock = DockStyle.Fill;
            pictureBox1.Location = new Point(173, 172);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(568, 248);
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(744, 423);
            Controls.Add(tableLayoutPanel1);
            Name = "MainForm";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)studentBindingSource).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Label lblStatus;
        private Button btnSave;
        private Button btnUpdate;
        private Button btnDelete;
        private Button btnConnect;
        private DataGridView dataGridView1;
        private TextBox tbName;
        private BindingSource studentBindingSource;
        private TextBox tbxIP;
        private TextBox tbxPort;
        private TextBox tbxID;
        private TextBox tbxPW;
        private TextBox tbxDbName;
        private TableLayoutPanel tableLayoutPanel1;
        private FlowLayoutPanel flowLayoutPanel1;
        private PictureBox pictureBox1;
    }
}