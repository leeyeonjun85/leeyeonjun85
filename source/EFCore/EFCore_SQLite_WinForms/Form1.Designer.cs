namespace EFCore_SQLite_WinForms;

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
        btnOK = new Button();
        dtnDelete = new Button();
        btnUpdate = new Button();
        cmbSchool = new ComboBox();
        tbName = new TextBox();
        btnAdd = new Button();
        dataGridView1 = new DataGridView();
        tableLayoutPanel1.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
        SuspendLayout();
        // 
        // tableLayoutPanel1
        // 
        tableLayoutPanel1.ColumnCount = 1;
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tableLayoutPanel1.Controls.Add(btnOK, 0, 4);
        tableLayoutPanel1.Controls.Add(dtnDelete, 0, 5);
        tableLayoutPanel1.Controls.Add(btnUpdate, 0, 3);
        tableLayoutPanel1.Controls.Add(cmbSchool, 0, 0);
        tableLayoutPanel1.Controls.Add(tbName, 0, 1);
        tableLayoutPanel1.Controls.Add(btnAdd, 0, 2);
        tableLayoutPanel1.Dock = DockStyle.Left;
        tableLayoutPanel1.Location = new Point(0, 0);
        tableLayoutPanel1.Name = "tableLayoutPanel1";
        tableLayoutPanel1.RowCount = 7;
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
        tableLayoutPanel1.Size = new Size(200, 450);
        tableLayoutPanel1.TabIndex = 0;
        // 
        // btnOK
        // 
        btnOK.Dock = DockStyle.Fill;
        btnOK.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point);
        btnOK.Location = new Point(3, 163);
        btnOK.Name = "btnOK";
        btnOK.Size = new Size(194, 34);
        btnOK.TabIndex = 7;
        btnOK.Text = "저장";
        btnOK.UseVisualStyleBackColor = true;
        btnOK.Click += btnOK_Click;
        // 
        // dtnDelete
        // 
        dtnDelete.Dock = DockStyle.Fill;
        dtnDelete.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point);
        dtnDelete.Location = new Point(3, 203);
        dtnDelete.Name = "dtnDelete";
        dtnDelete.Size = new Size(194, 34);
        dtnDelete.TabIndex = 6;
        dtnDelete.Text = "삭제";
        dtnDelete.UseVisualStyleBackColor = true;
        dtnDelete.Click += dtnDelete_Click;
        // 
        // btnUpdate
        // 
        btnUpdate.Dock = DockStyle.Fill;
        btnUpdate.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point);
        btnUpdate.Location = new Point(3, 123);
        btnUpdate.Name = "btnUpdate";
        btnUpdate.Size = new Size(194, 34);
        btnUpdate.TabIndex = 5;
        btnUpdate.Text = "수정";
        btnUpdate.UseVisualStyleBackColor = true;
        btnUpdate.Click += btnUpdate_Click;
        // 
        // cmbSchool
        // 
        cmbSchool.Dock = DockStyle.Fill;
        cmbSchool.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point);
        cmbSchool.FormattingEnabled = true;
        cmbSchool.Location = new Point(3, 3);
        cmbSchool.Name = "cmbSchool";
        cmbSchool.Size = new Size(194, 29);
        cmbSchool.TabIndex = 2;
        cmbSchool.Text = "학교이름";
        // 
        // tbName
        // 
        tbName.Dock = DockStyle.Fill;
        tbName.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point);
        tbName.Location = new Point(3, 43);
        tbName.Name = "tbName";
        tbName.Size = new Size(194, 29);
        tbName.TabIndex = 3;
        tbName.Text = "이름";
        // 
        // btnAdd
        // 
        btnAdd.Dock = DockStyle.Fill;
        btnAdd.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point);
        btnAdd.Location = new Point(3, 83);
        btnAdd.Name = "btnAdd";
        btnAdd.Size = new Size(194, 34);
        btnAdd.TabIndex = 4;
        btnAdd.Text = "학생 추가";
        btnAdd.UseVisualStyleBackColor = true;
        btnAdd.Click += btnAdd_Click;
        // 
        // dataGridView1
        // 
        dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dataGridView1.Dock = DockStyle.Right;
        dataGridView1.Location = new Point(206, 0);
        dataGridView1.Name = "dataGridView1";
        dataGridView1.RowTemplate.Height = 25;
        dataGridView1.Size = new Size(594, 450);
        dataGridView1.TabIndex = 1;
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(800, 450);
        Controls.Add(dataGridView1);
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
    private ComboBox cmbSchool;
    private TextBox tbName;
    private Button btnAdd;
    private DataGridView dataGridView1;
    private Button dtnDelete;
    private Button btnUpdate;
    private Button btnOK;
}
