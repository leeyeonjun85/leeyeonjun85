namespace WinFormServer
{
  partial class ChatServerForm
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
      btnStop = new Button();
      btnStart = new Button();
      lbxMsg = new ListBox();
      lbxClients = new ListBox();
      label3 = new Label();
      label2 = new Label();
      label1 = new Label();
      SuspendLayout();
      // 
      // btnStop
      // 
      btnStop.Location = new Point(122, 52);
      btnStop.Name = "btnStop";
      btnStop.Size = new Size(105, 39);
      btnStop.TabIndex = 20;
      btnStop.Text = "중지";
      btnStop.UseVisualStyleBackColor = true;
      // 
      // btnStart
      // 
      btnStart.Location = new Point(11, 52);
      btnStart.Name = "btnStart";
      btnStart.Size = new Size(105, 39);
      btnStart.TabIndex = 19;
      btnStart.Text = "시작";
      btnStart.UseVisualStyleBackColor = true;
      // 
      // lbxMsg
      // 
      lbxMsg.FormattingEnabled = true;
      lbxMsg.ItemHeight = 15;
      lbxMsg.Location = new Point(241, 128);
      lbxMsg.Name = "lbxMsg";
      lbxMsg.Size = new Size(547, 319);
      lbxMsg.TabIndex = 18;
      // 
      // lbxClients
      // 
      lbxClients.FormattingEnabled = true;
      lbxClients.ItemHeight = 15;
      lbxClients.Location = new Point(12, 128);
      lbxClients.Name = "lbxClients";
      lbxClients.Size = new Size(215, 319);
      lbxClients.TabIndex = 17;
      // 
      // label3
      // 
      label3.AutoSize = true;
      label3.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point);
      label3.Location = new Point(241, 104);
      label3.Name = "label3";
      label3.Size = new Size(82, 21);
      label3.TabIndex = 16;
      label3.Text = "Messages";
      // 
      // label2
      // 
      label2.AutoSize = true;
      label2.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point);
      label2.Location = new Point(12, 104);
      label2.Name = "label2";
      label2.Size = new Size(145, 21);
      label2.TabIndex = 15;
      label2.Text = "Connected Clients";
      // 
      // label1
      // 
      label1.BackColor = SystemColors.Highlight;
      label1.Dock = DockStyle.Top;
      label1.Font = new Font("맑은 고딕", 15F, FontStyle.Bold, GraphicsUnit.Point);
      label1.ForeColor = Color.FromArgb(241, 241, 241);
      label1.Location = new Point(0, 0);
      label1.Name = "label1";
      label1.Size = new Size(800, 40);
      label1.TabIndex = 14;
      label1.Text = " Server";
      label1.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // Form1
      // 
      AutoScaleDimensions = new SizeF(7F, 15F);
      AutoScaleMode = AutoScaleMode.Font;
      ClientSize = new Size(800, 450);
      Controls.Add(btnStop);
      Controls.Add(btnStart);
      Controls.Add(lbxMsg);
      Controls.Add(lbxClients);
      Controls.Add(label3);
      Controls.Add(label2);
      Controls.Add(label1);
      Name = "Form1";
      Text = "Form1";
      ResumeLayout(false);
      PerformLayout();
    }

    #endregion

    private Button btnStop;
    private Button btnStart;
    private ListBox lbxMsg;
    private ListBox lbxClients;
    private Label label3;
    private Label label2;
    private Label label1;
  }
}