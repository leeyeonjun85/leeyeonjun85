namespace WindowsForms_main
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.button1 = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.makeJson = new System.Windows.Forms.Button();
            this.loadJson = new System.Windows.Forms.Button();
            this.chart = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.clock = new System.Windows.Forms.Label();
            this.auto = new System.Windows.Forms.Button();
            this.stop = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(354, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "info";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(354, 187);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "닫기";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.Info;
            this.textBox1.Location = new System.Drawing.Point(12, 41);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(336, 169);
            this.textBox1.TabIndex = 2;
            this.textBox1.WordWrap = false;
            // 
            // chart1
            // 
            chartArea2.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.chart1.Legends.Add(legend2);
            this.chart1.Location = new System.Drawing.Point(12, 216);
            this.chart1.Name = "chart1";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.chart1.Series.Add(series2);
            this.chart1.Size = new System.Drawing.Size(776, 222);
            this.chart1.TabIndex = 3;
            this.chart1.Text = "chart1";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(435, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(353, 198);
            this.dataGridView1.TabIndex = 4;
            // 
            // makeJson
            // 
            this.makeJson.Location = new System.Drawing.Point(354, 42);
            this.makeJson.Name = "makeJson";
            this.makeJson.Size = new System.Drawing.Size(75, 23);
            this.makeJson.TabIndex = 5;
            this.makeJson.Text = "makeJson";
            this.makeJson.UseVisualStyleBackColor = true;
            this.makeJson.Click += new System.EventHandler(this.makeJson_Click);
            // 
            // loadJson
            // 
            this.loadJson.Location = new System.Drawing.Point(354, 71);
            this.loadJson.Name = "loadJson";
            this.loadJson.Size = new System.Drawing.Size(75, 23);
            this.loadJson.TabIndex = 6;
            this.loadJson.Text = "loadJson";
            this.loadJson.UseVisualStyleBackColor = true;
            this.loadJson.Click += new System.EventHandler(this.loadJson_Click);
            // 
            // chart
            // 
            this.chart.Location = new System.Drawing.Point(354, 100);
            this.chart.Name = "chart";
            this.chart.Size = new System.Drawing.Size(75, 23);
            this.chart.TabIndex = 7;
            this.chart.Text = "chart";
            this.chart.UseVisualStyleBackColor = true;
            this.chart.Click += new System.EventHandler(this.chart_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.clock_tick);
            // 
            // clock
            // 
            this.clock.AutoSize = true;
            this.clock.Font = new System.Drawing.Font("굴림", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.clock.Location = new System.Drawing.Point(12, 16);
            this.clock.Name = "clock";
            this.clock.Size = new System.Drawing.Size(234, 21);
            this.clock.TabIndex = 8;
            this.clock.Text = "2023-06-14  13:13:13";
            // 
            // auto
            // 
            this.auto.Location = new System.Drawing.Point(354, 129);
            this.auto.Name = "auto";
            this.auto.Size = new System.Drawing.Size(75, 23);
            this.auto.TabIndex = 9;
            this.auto.Text = "auto";
            this.auto.UseVisualStyleBackColor = true;
            this.auto.Click += new System.EventHandler(this.auto_Click);
            // 
            // stop
            // 
            this.stop.Location = new System.Drawing.Point(354, 158);
            this.stop.Name = "stop";
            this.stop.Size = new System.Drawing.Size(75, 23);
            this.stop.TabIndex = 10;
            this.stop.Text = "stop";
            this.stop.UseVisualStyleBackColor = true;
            this.stop.Click += new System.EventHandler(this.stop_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.stop);
            this.Controls.Add(this.auto);
            this.Controls.Add(this.clock);
            this.Controls.Add(this.chart);
            this.Controls.Add(this.loadJson);
            this.Controls.Add(this.makeJson);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button makeJson;
        private System.Windows.Forms.Button loadJson;
        private System.Windows.Forms.Button chart;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label clock;
        private System.Windows.Forms.Button auto;
        private System.Windows.Forms.Button stop;
    }
}

