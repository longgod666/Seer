using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace Seer
{
    public partial class MouseClick : Form
    {
        private int LaterTime { get; set; }
        private int SleepTimePerClick { get; set; }
        private int TotalTime { get; set; }
        private readonly System.Timers.Timer timer = new System.Timers.Timer();
        public MouseClick()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            //忽略线程访问安全机制，允许另一个线程访问其它线程创建的控件而不抛出异常。（影响不大）

            LaterTime = 5;
            SleepTimePerClick = 50;
            TotalTime = 5;
            numericUpDown1.Value = 5;
            numericUpDown2.Value = 50;
            numericUpDown3.Value = 5;
            comboBox1.SelectedIndex = 0;
        }
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            LaterTime = Convert.ToInt32(numericUpDown1.Value);
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            SleepTimePerClick = Convert.ToInt32(numericUpDown2.Value);
            timer.Interval = SleepTimePerClick;
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            TotalTime = Convert.ToInt32(numericUpDown3.Value);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Action action = new Action(MouseClickStart);
            //numericUpDown1.Enabled = false;
            //numericUpDown2.Enabled = false;
            //numericUpDown3.Enabled = false;
            //comboBox1.Enabled = false;
            //buttonStart.Enabled = false;
            this.Enabled = false;
            action.BeginInvoke(null, null);
        }

        private void MouseClickStart()
        {
            int ClickType = comboBox1.SelectedIndex;
            int Count = TotalTime * 1000 / SleepTimePerClick;
            Action<int, int, System.Timers.Timer> action;
            switch (ClickType)
            {
                case 0:
                    action = new Action<int, int, System.Timers.Timer>(Mouse.MouseLeftClick); break;
                case 1:
                    action = new Action<int, int, System.Timers.Timer>(Mouse.MouseRightClick); break;
                case 2:
                    action = new Action<int, int, System.Timers.Timer>(Mouse.MouseMiddleClick); break;
                default: throw new Exception("ClickType Argument Error");
            }
            Thread.Sleep(LaterTime * 1000);
            action.Invoke(Count, SleepTimePerClick, timer);
            Thread.Sleep(TotalTime * 1000);
            timer.Stop();
            //numericUpDown1.Enabled = true;
            //numericUpDown2.Enabled = true;
            //numericUpDown3.Enabled = true;
            //comboBox1.Enabled = true;
            //buttonStart.Enabled = true;
            this.Enabled = true;
        }

    }

    class Mouse
    {
        private static int Count { get; set; }
        private static int ClickType { get; set; }
        public static System.Timers.Timer Timer { get; set; }

        [System.Runtime.InteropServices.DllImport("user32")]
        private static extern int mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);
        //移动鼠标 
        const int MOUSEEVENTF_MOVE = 0x0001;
        //模拟鼠标左键按下 
        const int MOUSEEVENTF_LEFTDOWN = 0x0002;
        //模拟鼠标左键抬起 
        const int MOUSEEVENTF_LEFTUP = 0x0004;
        //模拟鼠标右键按下 
        const int MOUSEEVENTF_RIGHTDOWN = 0x0008;
        //模拟鼠标右键抬起 
        const int MOUSEEVENTF_RIGHTUP = 0x0010;
        //模拟鼠标中键按下 
        const int MOUSEEVENTF_MIDDLEDOWN = 0x0020;
        //模拟鼠标中键抬起 
        const int MOUSEEVENTF_MIDDLEUP = 0x0040;
        //标示是否采用绝对坐标 
        const int MOUSEEVENTF_ABSOLUTE = 0x8000;
        //模拟鼠标滚轮滚动操作，必须配合dwData参数
        const int MOUSEEVENTF_WHEEL = 0x0800;

        public static void MouseLeftClick(int Count, int SleepTimePerClick, System.Timers.Timer timer)
        {
            Mouse.Count = Count;
            Mouse.ClickType = 0;
            Mouse.Timer = timer;
            timer.Interval = SleepTimePerClick;
            timer.Elapsed += Timer_Tick_Left;
            timer.Start();
        }

        private static void Timer_Tick_Left(object sender, ElapsedEventArgs e)
        {
            if (Mouse.Count >= 0)
            {
                mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                Mouse.Count -= 1;
            }
            else
            {
                Mouse.Timer.Stop();
                return;
            }
        }

        public static void MouseRightClick(int Count, int SleepTimePerClick, System.Timers.Timer timer)
        {
            Mouse.Count = Count;
            Mouse.ClickType = 1;
            Mouse.Timer = timer;
            timer.Interval = SleepTimePerClick;
            timer.Elapsed += Timer_Tick_Right;
            timer.Start();
        }

        private static void Timer_Tick_Right(object sender, ElapsedEventArgs e)
        {
            if (Mouse.Count >= 0)
            {
                mouse_event(MOUSEEVENTF_RIGHTDOWN | MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
                Mouse.Count -= 1;
            }
            else
            {
                Mouse.Timer.Stop();
                return;
            }
        }

        public static void MouseMiddleClick(int Count, int SleepTimePerClick, System.Timers.Timer timer)
        {
            Mouse.Count = Count;
            Mouse.ClickType = 2;
            Mouse.Timer = timer;
            timer.Interval = SleepTimePerClick;
            timer.Elapsed += Timer_Tick_Middle;
            timer.Start();
        }

        private static void Timer_Tick_Middle(object sender, ElapsedEventArgs e)
        {
            if (Mouse.Count >= 0)
            {
                mouse_event(MOUSEEVENTF_MIDDLEDOWN | MOUSEEVENTF_MIDDLEUP, 0, 0, 0, 0);
                Mouse.Count -= 1;
            }
            else
            {
                Mouse.Timer.Stop();
                return;
            }
        }
    }
}
