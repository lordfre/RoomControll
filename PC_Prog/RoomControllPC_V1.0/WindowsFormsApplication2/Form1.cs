using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        bool isConnected = false;
        SerialPort port;
        public Form1()
        {
            InitializeComponent();
            comboBox2.Items.Add("None");
            comboBox2.Items.Add("Light Soft Box");
            comboBox2.Items.Add("RGB");
            comboBox2.Items.Add("References");
            this.Size = new System.Drawing.Size(300,80);
            
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void connectToArduino()
        {
            isConnected = true;
            string selectedPort = comboBox1.GetItemText(comboBox1.SelectedItem);
            port = new SerialPort(selectedPort, 9600, Parity.None, 8, StopBits.One);
            port.Open();
            button2.Text = "Disconnect";
            button3.Visible = true;
            button4.Visible = true;
            button5.Visible = true;
            button6.Visible = true;
            button7.Visible = true;
            comboBox2.Visible = true;
            this.Size = new System.Drawing.Size(429, 110);
        }

        private void disconnectFromArduino()
        {
            isConnected = false;
            port.Close();
            button2.Text = "Connect";
            button3.Visible = false;
            button4.Visible = false;
            button5.Visible = false;
            button6.Visible = false;
            button7.Visible = false;
            comboBox2.Visible = false;
            trackBar2.Visible = false;
            trackBar1.Visible = false;
            trackBar3.Visible = false;
            trackBar4.Visible = false;
            trackBar5.Visible = false;
            label1.Visible = false;
            label2.Visible = false;
            comboBox2.Visible = false;
            linkLabel1.Visible = false;
            this.Size = new System.Drawing.Size(300, 80);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            // Получаем список COM портов доступных в системе
            string[] portnames = SerialPort.GetPortNames();
            // Проверяем есть ли доступные
            if (portnames.Length == 0)
            {
                MessageBox.Show("COM PORT not found");
            }
            foreach (string s in portnames)
            {
                //добавляем доступные COM порты в список              
                comboBox1.Items.Add(s);
                
            }
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!isConnected)
            {
                connectToArduino();
            }
            else
            {
                disconnectFromArduino();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            port.Write("#R|");
            disconnectFromArduino();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string vis = comboBox2.GetItemText(comboBox2.SelectedItem);
            if(vis == "None")
            {
                this.Size = new System.Drawing.Size(429, 110);
                trackBar2.Visible = false;
                trackBar1.Visible = false;
                label1.Visible = false;
                label2.Visible = false;
                trackBar3.Visible = false;
                trackBar4.Visible = false;
                trackBar5.Visible = false;
                linkLabel1.Visible = false;
            }
            if (vis=="RGB")
            {
                trackBar2.Visible=false;
                trackBar1.Visible = false;
                label1.Visible = false;
                label2.Visible = false;
                trackBar3.Visible = true;
                trackBar4.Visible = true;
                trackBar5.Visible = true;
                linkLabel1.Visible = false;
                this.Size = new System.Drawing.Size(429, 255);
            }
            else if (vis == "Light Soft Box")
            {
                trackBar2.Visible = true;
                trackBar1.Visible = true;
                label1.Visible = true;
                label2.Visible = true;
                trackBar3.Visible = false;
                trackBar4.Visible = false;
                trackBar5.Visible = false;
                linkLabel1.Visible = false;
                this.Size = new System.Drawing.Size(429, 255);
            }
            else if (vis == "References")
            {
                trackBar2.Visible = false;
                trackBar1.Visible = false;
                label1.Visible = false;
                label2.Visible = false;
                trackBar3.Visible = false;
                trackBar4.Visible = false;
                trackBar5.Visible = false;
                linkLabel1.Visible = true;
                this.Size = new System.Drawing.Size(429, 126);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            port.Write("#OA|");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            port.Write("#V24|");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            port.Write("#V12|");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            port.Write("#V5|");
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            String LO = "#LO";
            LO = LO + Convert.ToString(trackBar1.Value, 16);
            LO = LO + "|";
            port.Write(LO);
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            String LP = "#LP";
            LP = LP + Convert.ToString(trackBar2.Value, 16);
            LP = LP + "|";
            port.Write(LP);
        }

        private void label3_Click(object sender, EventArgs e)
        {
         
        }


        private void trackBar5_Scroll(object sender, EventArgs e)
        {
            String LG = "#LG";
            LG = LG + Convert.ToString(trackBar5.Value, 16);
            LG = LG + "|";
            port.Write(LG);
        }

        private void trackBar4_Scroll(object sender, EventArgs e)
        {
            String LR = "#LR";
            LR = LR + Convert.ToString(trackBar4.Value, 16);
            LR = LR + "|";
            port.Write(LR);
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            String LB = "#LB";
            LB = LB + Convert.ToString(trackBar3.Value, 16);
            LB = LB + "|";
            port.Write(LB);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/lordfre/RoomControll");
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }


    }
}
