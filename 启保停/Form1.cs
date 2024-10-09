using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using xktComm.Common;
using Timer = System.Windows.Forms.Timer;

namespace 启保停
{
    public partial class Form1 : Form
    {
        private Timer update_timer = new Timer();// 定时器对象 每100ms触发监测
        private xktComm.SiemensS7 siemens = new xktComm.SiemensS7();// S7通信对象
        public Form1()
        {

            InitializeComponent();
            update_timer.Interval = 500;
            update_timer.Tick += UpdateTimer_Tick;

            bool isConnected = siemens.Connect("192.168.5.39", xktComm.Common.CPU_Type.S71200, 0, 1);
            if (isConnected)
            {
                MessageBox.Show("PLC连接成功");
                update_timer.Start();
            }
            else
            {
                MessageBox.Show("PLC连接失败");
            }
        }

        /// <summary>
        /// 监测M0.1 的状态 进行图片切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateTimer_Tick(Object sender, EventArgs e)
        {
            string flag = siemens.Read("M0.1", VarType.Bit).ToString();
            Console.WriteLine(flag);
            bool val = Convert.ToBoolean(flag);
            atest1.State = val;
            if (val)
            {
                pictureBox1.Image = Properties.Resources.开关_开;
            }
            else
            {
                pictureBox1.Image = Properties.Resources.开关_关;
            }
        }
        /// <summary>
        /// 控制启动按钮 M0.0
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void start_Click(object sender, EventArgs e)
        {
            siemens.Write("M0.0", true);
            Thread.Sleep(50);
            siemens.Write("M0.0", false);

        }

        /// <summary>
        /// 控制停止按钮 M0.2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void stop_Click(object sender, EventArgs e)
        {
            siemens.Write("M0.2", true);
            //Thread.Sleep(50); 可以不写
            siemens.Write("M0.2", false);
        }


        // 用户控件
        private void atest1_StartClick(object sender, EventArgs e)
        {
            siemens.Write("M0.0", true);
            siemens.Write("M0.0", false);
        }

        private void atest1_StopClick(object sender, EventArgs e)
        {
            siemens.Write("M0.2", true);
            siemens.Write("M0.2", false);
        }
    }
}
