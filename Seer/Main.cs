using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Seer
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void 刷新ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            webBrowser1.Refresh();
        }

        private void 开启ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Mute.waveOutSetVolume(IntPtr.Zero, 0);
        }

        private void 关闭ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Mute.waveOutSetVolume(IntPtr.Zero, 0xffffffff);
        }

        private void cEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(Environment.CurrentDirectory + "\\Tools\\cheat-engine\\Cheat Engine.exe");
        }

        private void fDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(Environment.CurrentDirectory + "\\Tools\\fiddler\\Fiddler.exe");
        }

        private void yoSoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(Environment.CurrentDirectory + "\\Tools\\dailiqi.ysd");
        }

        private void 连点器ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new MouseClick().Show();
        }

        private void 计算器ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(Environment.CurrentDirectory + "\\Tools\\赛尔数据计算器\\赛尔数据计算器.exe");
        }

        private void 图鉴ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("http://news.4399.com/seer/jinglingdaquan/");
        }

        private void 关于ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            new About().ShowDialog();
        }
    }
}
