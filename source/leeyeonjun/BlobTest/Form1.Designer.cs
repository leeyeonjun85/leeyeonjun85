namespace BlobTest
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
            pbImage = new PictureBox();
            pbBlobImg = new PictureBox();
            btnOpen = new Button();
            btnSave = new Button();
            btnLoad = new Button();
            txtPath = new TextBox();
            ((System.ComponentModel.ISupportInitialize)pbImage).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbBlobImg).BeginInit();
            SuspendLayout();
            // 
            // pbImage
            // 
            pbImage.Location = new Point(12, 58);
            pbImage.Name = "pbImage";
            pbImage.Size = new Size(380, 380);
            pbImage.TabIndex = 0;
            pbImage.TabStop = false;
            // 
            // pbBlobImg
            // 
            pbBlobImg.Location = new Point(408, 58);
            pbBlobImg.Name = "pbBlobImg";
            pbBlobImg.Size = new Size(380, 380);
            pbBlobImg.TabIndex = 1;
            pbBlobImg.TabStop = false;
            // 
            // btnOpen
            // 
            btnOpen.Location = new Point(326, 12);
            btnOpen.Name = "btnOpen";
            btnOpen.Size = new Size(150, 40);
            btnOpen.TabIndex = 2;
            btnOpen.Text = "Open";
            btnOpen.UseVisualStyleBackColor = true;
            btnOpen.Click += btnOpen_Click;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(482, 12);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(150, 40);
            btnSave.TabIndex = 3;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnLoad
            // 
            btnLoad.Location = new Point(638, 12);
            btnLoad.Name = "btnLoad";
            btnLoad.Size = new Size(150, 40);
            btnLoad.TabIndex = 4;
            btnLoad.Text = "Load";
            btnLoad.UseVisualStyleBackColor = true;
            btnLoad.Click += btnLoad_Click;
            // 
            // txtPath
            // 
            txtPath.Location = new Point(12, 22);
            txtPath.Name = "txtPath";
            txtPath.Size = new Size(308, 23);
            txtPath.TabIndex = 5;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(txtPath);
            Controls.Add(btnLoad);
            Controls.Add(btnSave);
            Controls.Add(btnOpen);
            Controls.Add(pbBlobImg);
            Controls.Add(pbImage);
            Name = "Form1";
            Text = "Form1";
            FormClosing += Form1_FormClosing;
            ((System.ComponentModel.ISupportInitialize)pbImage).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbBlobImg).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pbImage;
        private PictureBox pbBlobImg;
        private Button btnOpen;
        private Button btnSave;
        private Button btnLoad;
        private TextBox txtPath;
    }
}