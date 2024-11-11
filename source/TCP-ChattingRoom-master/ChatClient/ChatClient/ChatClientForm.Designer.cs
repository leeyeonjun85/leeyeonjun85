namespace WinFormClient
{
  partial class ChatClientForm
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
      txtMessage = new TextBox();
      txtName = new TextBox();
      label2 = new Label();
      label4 = new Label();
      btnStop = new Button();
      btnConnect = new Button();
      lbxMsg = new ListBox();
      label3 = new Label();
      label1 = new Label();
      nudRoomId = new NumericUpDown();
      ((System.ComponentModel.ISupportInitialize)nudRoomId).BeginInit();
      SuspendLayout();
      // 
      // txtMessage
      // 
      txtMessage.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      txtMessage.Location = new Point(93, 135);
      txtMessage.Name = "txtMessage";
      txtMessage.Size = new Size(356, 23);
      txtMessage.TabIndex = 35;
      // 
      // txtName
      // 
      txtName.Location = new Point(93, 103);
      txtName.Name = "txtName";
      txtName.Size = new Size(135, 23);
      txtName.TabIndex = 34;
      // 
      // label2
      // 
      label2.AutoSize = true;
      label2.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point);
      label2.Location = new Point(12, 105);
      label2.Name = "label2";
      label2.Size = new Size(53, 21);
      label2.TabIndex = 33;
      label2.Text = "Name";
      // 
      // label4
      // 
      label4.AutoSize = true;
      label4.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point);
      label4.Location = new Point(12, 72);
      label4.Name = "label4";
      label4.Size = new Size(75, 21);
      label4.TabIndex = 31;
      label4.Text = "Room ID";
      // 
      // btnStop
      // 
      btnStop.Location = new Point(345, 72);
      btnStop.Name = "btnStop";
      btnStop.Size = new Size(105, 54);
      btnStop.TabIndex = 30;
      btnStop.Text = "종료";
      btnStop.UseVisualStyleBackColor = true;
      // 
      // btnConnect
      // 
      btnConnect.Location = new Point(234, 72);
      btnConnect.Name = "btnConnect";
      btnConnect.Size = new Size(105, 54);
      btnConnect.TabIndex = 29;
      btnConnect.Text = "연결";
      btnConnect.UseVisualStyleBackColor = true;
      // 
      // lbxMsg
      // 
      lbxMsg.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      lbxMsg.FormattingEnabled = true;
      lbxMsg.ItemHeight = 15;
      lbxMsg.Location = new Point(12, 164);
      lbxMsg.Name = "lbxMsg";
      lbxMsg.Size = new Size(439, 289);
      lbxMsg.TabIndex = 28;
      // 
      // label3
      // 
      label3.AutoSize = true;
      label3.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point);
      label3.Location = new Point(12, 140);
      label3.Name = "label3";
      label3.Size = new Size(82, 21);
      label3.TabIndex = 27;
      label3.Text = "Messages";
      // 
      // label1
      // 
      label1.BackColor = Color.Maroon;
      label1.Dock = DockStyle.Top;
      label1.Font = new Font("맑은 고딕", 15F, FontStyle.Bold, GraphicsUnit.Point);
      label1.ForeColor = Color.FromArgb(241, 241, 241);
      label1.Location = new Point(0, 0);
      label1.Name = "label1";
      label1.Size = new Size(461, 40);
      label1.TabIndex = 26;
      label1.Text = " Client";
      label1.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // nudRoomId
      // 
      nudRoomId.Location = new Point(93, 72);
      nudRoomId.Name = "nudRoomId";
      nudRoomId.Size = new Size(41, 23);
      nudRoomId.TabIndex = 36;
      // 
      // Form1
      // 
      AutoScaleDimensions = new SizeF(7F, 15F);
      AutoScaleMode = AutoScaleMode.Font;
      ClientSize = new Size(461, 464);
      Controls.Add(nudRoomId);
      Controls.Add(txtMessage);
      Controls.Add(txtName);
      Controls.Add(label2);
      Controls.Add(label4);
      Controls.Add(btnStop);
      Controls.Add(btnConnect);
      Controls.Add(lbxMsg);
      Controls.Add(label3);
      Controls.Add(label1);
      Name = "Form1";
      Text = "Form1";
      ((System.ComponentModel.ISupportInitialize)nudRoomId).EndInit();
      ResumeLayout(false);
      PerformLayout();
    }

    #endregion

    private TextBox txtMessage;
    private TextBox txtName;
    private Label label2;
    private Label label4;
    private Button btnStop;
    private Button btnConnect;
    private ListBox lbxMsg;
    private Label label3;
    private Label label1;
    private NumericUpDown nudRoomId;
  }
}