using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PinkBus.CheckInClient.PopControls
{
    public partial class DownloadConfirm : Form
    {
        public DownloadConfirm()
        {
            InitializeComponent();
        }

        public string ShowMessage { set { messageshow_msg.Text = value; } }
        public string ShowMessageTitle { set { messageshow_title.Text = value; } }


        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void messageshow_title_MouseDown(object sender, MouseEventArgs e)
        {

        }
    }
}
