namespace SecureEncodeDecode
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
            tabControl1 = new TabControl();
            tabHash = new TabPage();
            tableLayoutPanel2 = new TableLayoutPanel();
            tbxMessage = new TextBox();
            label2 = new Label();
            tbxKey = new TextBox();
            btnHMAC = new Button();
            label1 = new Label();
            btnSHA256 = new Button();
            tabPage2 = new TabPage();
            tableLayoutPanel1 = new TableLayoutPanel();
            statusStrip1 = new StatusStrip();
            tbxLog = new TextBox();
            btnSHA384 = new Button();
            btnSHA512 = new Button();
            btnMD5 = new Button();
            tabControl1.SuspendLayout();
            tabHash.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabHash);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(3, 203);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(794, 214);
            tabControl1.TabIndex = 0;
            // 
            // tabHash
            // 
            tabHash.Controls.Add(tableLayoutPanel2);
            tabHash.Location = new Point(4, 24);
            tabHash.Name = "tabHash";
            tabHash.Padding = new Padding(3);
            tabHash.Size = new Size(786, 186);
            tabHash.TabIndex = 0;
            tabHash.Text = "Hash";
            tabHash.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 3;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            tableLayoutPanel2.Controls.Add(btnMD5, 2, 4);
            tableLayoutPanel2.Controls.Add(btnSHA512, 2, 3);
            tableLayoutPanel2.Controls.Add(btnSHA384, 2, 2);
            tableLayoutPanel2.Controls.Add(tbxMessage, 1, 1);
            tableLayoutPanel2.Controls.Add(label2, 0, 1);
            tableLayoutPanel2.Controls.Add(tbxKey, 1, 0);
            tableLayoutPanel2.Controls.Add(btnHMAC, 2, 0);
            tableLayoutPanel2.Controls.Add(label1, 0, 0);
            tableLayoutPanel2.Controls.Add(btnSHA256, 2, 1);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(3, 3);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 6;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Size = new Size(780, 180);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // tbxMessage
            // 
            tbxMessage.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            tbxMessage.Location = new Point(103, 33);
            tbxMessage.Name = "tbxMessage";
            tbxMessage.Size = new Size(574, 23);
            tbxMessage.TabIndex = 8;
            tbxMessage.Text = "Test Message";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Dock = DockStyle.Fill;
            label2.Location = new Point(3, 30);
            label2.Name = "label2";
            label2.Size = new Size(94, 30);
            label2.TabIndex = 7;
            label2.Text = "Message";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tbxKey
            // 
            tbxKey.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            tbxKey.Location = new Point(103, 3);
            tbxKey.Name = "tbxKey";
            tbxKey.Size = new Size(574, 23);
            tbxKey.TabIndex = 0;
            tbxKey.Text = "a123456789";
            // 
            // btnHMAC
            // 
            btnHMAC.Dock = DockStyle.Fill;
            btnHMAC.Location = new Point(683, 3);
            btnHMAC.Name = "btnHMAC";
            btnHMAC.Size = new Size(94, 24);
            btnHMAC.TabIndex = 1;
            btnHMAC.Text = "HMAC";
            btnHMAC.UseVisualStyleBackColor = true;
            btnHMAC.Click += btnHMAC_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Dock = DockStyle.Fill;
            label1.Location = new Point(3, 0);
            label1.Name = "label1";
            label1.Size = new Size(94, 30);
            label1.TabIndex = 6;
            label1.Text = "Key";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnSHA256
            // 
            btnSHA256.Dock = DockStyle.Fill;
            btnSHA256.Location = new Point(683, 33);
            btnSHA256.Name = "btnSHA256";
            btnSHA256.Size = new Size(94, 24);
            btnSHA256.TabIndex = 9;
            btnSHA256.Text = "SHA256";
            btnSHA256.UseVisualStyleBackColor = true;
            btnSHA256.Click += btnSHA256_Click;
            // 
            // tabPage2
            // 
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(786, 186);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "tabPage2";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(tabControl1, 0, 1);
            tableLayoutPanel1.Controls.Add(statusStrip1, 0, 2);
            tableLayoutPanel1.Controls.Add(tbxLog, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 200F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel1.Size = new Size(800, 450);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // statusStrip1
            // 
            statusStrip1.Dock = DockStyle.Fill;
            statusStrip1.Location = new Point(0, 420);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(800, 30);
            statusStrip1.TabIndex = 1;
            statusStrip1.Text = "statusStrip1";
            // 
            // tbxLog
            // 
            tbxLog.BackColor = SystemColors.Info;
            tbxLog.Dock = DockStyle.Fill;
            tbxLog.Location = new Point(3, 3);
            tbxLog.Multiline = true;
            tbxLog.Name = "tbxLog";
            tbxLog.ScrollBars = ScrollBars.Both;
            tbxLog.Size = new Size(794, 194);
            tbxLog.TabIndex = 2;
            // 
            // btnSHA384
            // 
            btnSHA384.Dock = DockStyle.Fill;
            btnSHA384.Location = new Point(683, 63);
            btnSHA384.Name = "btnSHA384";
            btnSHA384.Size = new Size(94, 24);
            btnSHA384.TabIndex = 10;
            btnSHA384.Text = "SHA384";
            btnSHA384.UseVisualStyleBackColor = true;
            btnSHA384.Click += btnSHA384_Click;
            // 
            // btnSHA512
            // 
            btnSHA512.Dock = DockStyle.Fill;
            btnSHA512.Location = new Point(683, 93);
            btnSHA512.Name = "btnSHA512";
            btnSHA512.Size = new Size(94, 24);
            btnSHA512.TabIndex = 11;
            btnSHA512.Text = "SHA512";
            btnSHA512.UseVisualStyleBackColor = true;
            btnSHA512.Click += btnSHA512_Click;
            // 
            // btnMD5
            // 
            btnMD5.Dock = DockStyle.Fill;
            btnMD5.Location = new Point(683, 123);
            btnMD5.Name = "btnMD5";
            btnMD5.Size = new Size(94, 24);
            btnMD5.TabIndex = 12;
            btnMD5.Text = "MD5";
            btnMD5.UseVisualStyleBackColor = true;
            btnMD5.Click += btnMD5_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(tableLayoutPanel1);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            tabControl1.ResumeLayout(false);
            tabHash.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControl1;
    private TabPage tabHash;
    private TabPage tabPage2;
    private TableLayoutPanel tableLayoutPanel1;
    private StatusStrip statusStrip1;
    private TableLayoutPanel tableLayoutPanel2;
    private TextBox tbxKey;
    private Button btnHMAC;
        private TextBox tbxLog;
        private Label label1;
        private TextBox tbxMessage;
        private Label label2;
        private Button btnSHA256;
        private Button btnSHA512;
        private Button btnSHA384;
        private Button btnMD5;
    }
}
