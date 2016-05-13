using System;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PinkBus.OfflineCustomer.UserControls
{
    public partial class MsgShow : BaseForm
    {
        public MsgShow(string title,string msg)
        {
            InitializeComponent();
            ShowMessageTitle = title;
            ShowMessage = msg;
        }

        public string ShowMessage { set { messageshow_msg.Text = value; } }
        public string ShowMessageTitle { set { messageshow_title.Text = value; } }


        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DownloadConfirm_Load(object sender, EventArgs e)
        {
            //this.Location = new Point((width - 1100) / 2, (height - 640) / 2);
            //this.messageshow_msg.Location = new Point((this.Width - ) / 2, (this.Height - 640) / 2);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);//解决闪烁
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);//解决闪烁
        }


        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            Drag_MouseDown(sender, e);
        }
    }
}
