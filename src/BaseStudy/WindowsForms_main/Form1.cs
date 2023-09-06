using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Newtonsoft.Json;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace WindowsForms_main
{
    public partial class Form1 : Form
    {
        //Class1 library1 = new Class1();
        CancellationTokenSource tokenSource;

        public Form1()
        {
            InitializeComponent();
            timer1.Interval = 1000;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            clock.Text = System.DateTime.Now.ToString("yyyy-MM-dd  hh:mm:ss");
            timer1.Start();

            // 차트타이틀
            Title title = new Title();
            title.Text = "처음만드는 윈폼차트";
            title.ForeColor = Color.Blue;
            title.Font = new Font("맑은고딕", 14, FontStyle.Bold);
            chart1.Titles.Add(title);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //textBox1.Text = library1.getFileDir();
        }

        private void makeJson_Click(object sender, EventArgs e)
        {
            //var json_path = library1.makeJson();
            //textBox1.Text += $"\r\nJSON 생성 위치 : {json_path}";
        }

        private void loadJson_Click(object sender, EventArgs e)
        {
            //DataTable dataTable = library1.GetDataTable();
            //dataGridView1.DataSource = dataTable;
        }

        private void chart_Click(object sender, EventArgs e)
        {
            //DataTable dataTable = library1.GetDataTable();

            // Bind the DataTable to the chart
            //chart1.DataSource = dataTable;

            //차트 초기화
            chart1.Series.Clear();

            //시리즈 생성
            Series SPC_Value = new Series("SPC_Value");
            Series SPC_UCL = new Series("SPC_UCL");
            Series SPC_Avr = new Series("SPC_Avr");
            Series SPC_LCL = new Series("SPC_LCL");

            //라인 프래프
            SPC_Value.ChartType = SeriesChartType.Line;
            SPC_UCL.ChartType = SeriesChartType.Line;
            SPC_Avr.ChartType = SeriesChartType.Line;
            SPC_LCL.ChartType = SeriesChartType.Line;

            //시리즈에 데이터 넣기
            SPC_Value.YValueMembers = "SPC_Value";
            SPC_UCL.YValueMembers = "SPC_UCL";
            SPC_Avr.YValueMembers = "SPC_Avr";
            SPC_LCL.YValueMembers = "SPC_LCL";

            //차트에 시리즈 추가
            chart1.Series.Add(SPC_Value);
            chart1.Series.Add(SPC_UCL);
            chart1.Series.Add(SPC_Avr);
            chart1.Series.Add(SPC_LCL);

            chart1.Series[0].Color = Color.Blue;
            chart1.Series[1].Color = Color.Red;
            chart1.Series[2].Color = Color.Green;
            chart1.Series[3].Color = Color.Red;

            chart1.Series[0].BorderWidth = 3;
            chart1.Series[1].BorderWidth = 2;
            chart1.Series[2].BorderWidth = 2;
            chart1.Series[3].BorderWidth = 2;
        }

        private void clock_tick(object sender, EventArgs e) {
            clock.Text = System.DateTime.Now.ToString("yyyy-MM-dd  hh:mm:ss");
        }

        private async void auto_Click(object sender, EventArgs e) {
            tokenSource = new CancellationTokenSource();
            var token = tokenSource.Token;
            await DoWorkAsync(sender, e, token);
        }

        private void stop_Click(object sender, EventArgs e) {
            tokenSource.Cancel();
        }

        private async Task DoWorkAsync(object sender, EventArgs e, CancellationToken token) {
            while (!token.IsCancellationRequested) {
                this.makeJson_Click(sender, e);
                this.loadJson_Click(sender, e);
                this.chart_Click(sender, e);
                await Task.Delay(1000);
            }
        }
    }
}
