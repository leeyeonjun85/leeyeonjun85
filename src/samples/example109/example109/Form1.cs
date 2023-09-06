using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace example109
{
    public partial class Form1 : Form
    {
        MqttClient client;
        string clientId;

        int temp_x = 0;
        int humi_x = 0;
        int dust_x = 0;
        int co2_x = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string BrokerAddress = "broker.mqtt-dashboard.com";
            client = new MqttClient(BrokerAddress);

            // register a callback-function (we have to implement, see below) which is called by the library when a message was received
            client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;

            // use a unique id as client id, each time we start the application
            clientId = Guid.NewGuid().ToString();
            client.Connect(clientId);

            //구독정보를 지정
            //nockanda/temp
            //nockanda/humi
            string[] topic = { "nockanda/temp", "nockanda/humi","nockanda/dust", "nockanda/co2" };
            byte[] qos = { 0, 0, 0, 0 };
            client.Subscribe(topic, qos);
        }

        void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            string ReceivedMessage = Encoding.UTF8.GetString(e.Message);

            //DO SOMETHING..!
            if(e.Topic == "nockanda/temp")
            {
                label1.Text = "온도 : " + ReceivedMessage + " 'c";

                ListViewItem lvi = new ListViewItem();
                lvi.Text = "온도";
                lvi.SubItems.Add(ReceivedMessage);

                listView1.Items.Add(lvi);

                //온도 그래프를 그려보자!
                //chart1

                double value = double.Parse(ReceivedMessage);

                Chart mychart = chart1;

                mychart.Series[0].Points.AddXY(temp_x, value);

                //데이터셋의 갯수가 윈도우 사이즈를 초과했는가?
                if(mychart.Series[0].Points.Count > 10)
                {
                    //제일 먼저탄 손님을 내리게 한다!
                    mychart.Series[0].Points.RemoveAt(0);
                }

                //버스를 바라보는 카메라의 시야를 조정한다!
                mychart.ChartAreas[0].AxisX.Maximum = temp_x;
                mychart.ChartAreas[0].AxisX.Minimum = mychart.Series[0].Points[0].XValue;


                //현재 버스에 타고있는 손님중에 가장 키큰사람의 키가 몇이냐?
                double max = 0;
                for(int i = 0; i < mychart.Series[0].Points.Count; i++)
                {
                    //내가 생각하고 있던 값보다 더큰값이 있다면..
                    //그값이 max값이다!
                    if(max < mychart.Series[0].Points[i].YValues[0])
                    {
                        max = mychart.Series[0].Points[i].YValues[0];
                    }
                }

                mychart.ChartAreas[0].AxisY.Maximum = max+5;

                temp_x++;
            }
            else if(e.Topic == "nockanda/humi")
            {
                label2.Text = "습도 : " + ReceivedMessage + " %";

                ListViewItem lvi = new ListViewItem();
                lvi.Text = "습도";
                lvi.SubItems.Add(ReceivedMessage);

                listView1.Items.Add(lvi);


                //습도그래프를 그려보자!
                //chart2
                double value = double.Parse(ReceivedMessage);

                Chart mychart = chart2;

                mychart.Series[0].Points.AddXY(humi_x, value);

                //데이터셋의 갯수가 윈도우 사이즈를 초과했는가?
                if (mychart.Series[0].Points.Count > 10)
                {
                    //제일 먼저탄 손님을 내리게 한다!
                    mychart.Series[0].Points.RemoveAt(0);
                }

                //버스를 바라보는 카메라의 시야를 조정한다!
                mychart.ChartAreas[0].AxisX.Maximum = humi_x;
                mychart.ChartAreas[0].AxisX.Minimum = mychart.Series[0].Points[0].XValue;


                //현재 버스에 타고있는 손님중에 가장 키큰사람의 키가 몇이냐?
                double max = 0;
                for (int i = 0; i < mychart.Series[0].Points.Count; i++)
                {
                    //내가 생각하고 있던 값보다 더큰값이 있다면..
                    //그값이 max값이다!
                    if (max < mychart.Series[0].Points[i].YValues[0])
                    {
                        max = mychart.Series[0].Points[i].YValues[0];
                    }
                }

                mychart.ChartAreas[0].AxisY.Maximum = max + 5;

                humi_x++;
            }else if(e.Topic == "nockanda/dust")
            {
                label3.Text = ReceivedMessage + " mg/m3";


                double value = double.Parse(ReceivedMessage);

                Chart mychart = chart3;

                mychart.Series[0].Points.AddXY(dust_x, value);

                //데이터셋의 갯수가 윈도우 사이즈를 초과했는가?
                if (mychart.Series[0].Points.Count > 10)
                {
                    //제일 먼저탄 손님을 내리게 한다!
                    mychart.Series[0].Points.RemoveAt(0);
                }

                //버스를 바라보는 카메라의 시야를 조정한다!
                mychart.ChartAreas[0].AxisX.Maximum = dust_x;
                mychart.ChartAreas[0].AxisX.Minimum = mychart.Series[0].Points[0].XValue;


                //현재 버스에 타고있는 손님중에 가장 키큰사람의 키가 몇이냐?
                double max = 0;
                for (int i = 0; i < mychart.Series[0].Points.Count; i++)
                {
                    //내가 생각하고 있던 값보다 더큰값이 있다면..
                    //그값이 max값이다!
                    if (max < mychart.Series[0].Points[i].YValues[0])
                    {
                        max = mychart.Series[0].Points[i].YValues[0];
                    }
                }

                mychart.ChartAreas[0].AxisY.Maximum = max + 5;

                dust_x++;
            }else if(e.Topic == "nockanda/co2")
            {
                label4.Text = ReceivedMessage + " PPM";

                double value = double.Parse(ReceivedMessage);

                Chart mychart = chart4;

                mychart.Series[0].Points.AddXY(co2_x, value);

                //데이터셋의 갯수가 윈도우 사이즈를 초과했는가?
                if (mychart.Series[0].Points.Count > 10)
                {
                    //제일 먼저탄 손님을 내리게 한다!
                    mychart.Series[0].Points.RemoveAt(0);
                }

                //버스를 바라보는 카메라의 시야를 조정한다!
                mychart.ChartAreas[0].AxisX.Maximum = co2_x;
                mychart.ChartAreas[0].AxisX.Minimum = mychart.Series[0].Points[0].XValue;


                //현재 버스에 타고있는 손님중에 가장 키큰사람의 키가 몇이냐?
                double max = 0;
                for (int i = 0; i < mychart.Series[0].Points.Count; i++)
                {
                    //내가 생각하고 있던 값보다 더큰값이 있다면..
                    //그값이 max값이다!
                    if (max < mychart.Series[0].Points[i].YValues[0])
                    {
                        max = mychart.Series[0].Points[i].YValues[0];
                    }
                }

                mychart.ChartAreas[0].AxisY.Maximum = max + 5;

                co2_x++;
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            client.Disconnect();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
