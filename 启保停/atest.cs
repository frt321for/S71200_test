using System.Threading;
using System;
using System.Windows.Forms;
using xktComm;
using xktComm.Common;
using Timer = System.Windows.Forms.Timer;

namespace 启保停
{
    public partial class atest : UserControl
    {
        public event EventHandler StartClick;
        public event EventHandler StopClick;
        public atest()
        {
            InitializeComponent();
        }

        
        private bool state;

        public bool State
        {
            get 
            { 
                return state; 
            }
            set
            { 
                state = value;
                if (value)
                {
                    pictureBox1.Image = Properties.Resources.开关_开;
                }
                else
                {
                    pictureBox1.Image = Properties.Resources.开关_关;
                }
            }
        }


        private void start_Click(object sender, EventArgs e)
        {
            StartClick?.Invoke(sender, e);

        }

        private void stop_Click(object sender, EventArgs e)
        {
            StopClick?.Invoke(sender, e);
        }

    }
}
