using Newtonsoft.Json;
using PinkBus.CheckInClient.DAL;
using PinkBus.CheckInClient.Entitys;
using PinkBus.CheckInClient.Helper;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PinkBus.CheckInClient.PopControls
{
    public partial class PassCode : BaseForm
    {
        public PassCode()
            : base()
        {
           // SetClassLong(this.Handle, GCL_STYLE, GetClassLong(this.Handle, GCL_STYLE) | CS_DropSHADOW);
            InitializeComponent();
        }

        public string LabelTitle { set { passcode_title.Text = value; } }
        public SyncType SyncType { set; get; }

        public Guid EventKey { get; set; }

        public SyncStatus SyncStatus { get; set; }

        private delegate void ChangeTxetBox(string text);

        void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            worker.ReportProgress(0);

            if (SyncStatus == SyncStatus.Downloading)
            {
                string where = " where LOWER(eventkey)='" + EventKey.ToString().ToLower() + "'";
                List<Event> EV = EventDAL.QueryEvents(where);

                ChangeTxetBox ct = new ChangeTxetBox(ChangeTextBoxValue);
                this.Invoke(ct, EV[0].DownloadToken);
                //ct(EV[0].DownloadToken);
            }

            string testpasscode = this.passcode_textBox.Text.Trim();
            EventKey = SyncHelper.DownloadEvents(worker, this.passcode_textBox.Text.Trim());
            if (EventKey != Guid.Empty)
            {
                worker.ReportProgress(10);
                if (!EventDAL.GetSyncStatusLog(EventKey, SyncType.D).Consultant)
                {
                    SyncHelper.DownloadConsultantInfo(worker, EventKey, 1);
                }
                if (!EventDAL.GetSyncStatusLog(EventKey, SyncType.D).Ticket)
                {
                    SyncHelper.DownloadConsultantInfo(worker, EventKey, 2);
                }
                if (!EventDAL.GetSyncStatusLog(EventKey, SyncType.D).Customer)
                {
                    SyncHelper.DownloadConsultantInfo(worker, EventKey, 3);
                }
                if (!EventDAL.GetSyncStatusLog(EventKey, SyncType.D).HeaderImgs)
                {
                    SyncHelper.DownloadSellerHeaders(worker, EventKey);
                }
            }
            else
            {
                worker.CancelAsync();
                e.Cancel = true;
              
               
            }



        }

        void BackgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (progressBar1.Value < e.ProgressPercentage)
            {
                this.progressBar1.Value = e.ProgressPercentage;
                this.progress_label.Text = e.ProgressPercentage.ToString();
            }
        }

        void BackgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // First, handle the case where an exception was thrown.
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
            }
            else if (e.Cancelled)
            {
                ChangeTxetBox ct = new ChangeTxetBox(ChangeWarnValue);
                this.Invoke(ct, "该活动下载码无效！");
                this.passcede_warn.Visible = true;
                this.PassCode_panel.Show();
                this.passcode_progress_panel.Hide();
            }
            else
            {
                this.Close();
                MainForm mf = this.Owner as MainForm;
                mf.ShowEventList();
            }

        }

        private void passcode_cancel_button_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }


        private void passcode_submit_button_Click(object sender, EventArgs e)
        {
            if (!Common.IsConnected())
            {
                MessageBox.Show("当前客户端没有网络连接，请连接后重试！");
                return;
            }

            if (this.SyncStatus == SyncStatus.None)
            {
                string where = " where LOWER(DownloadToken)='" + this.passcode_textBox.Text.ToLower().Trim() + "'";
                List<Event> events = EventDAL.QueryEvents(where);
                if (events.Count > 0)
                {
                    this.passcede_warn.Text = "该活动已下载,无法再次导入";
                    this.passcede_warn.Visible = true;
                    return;
                }
            }
            if (this.SyncStatus == SyncStatus.Downloading)
            {

            }

            this.PassCode_panel.Hide();
            this.passcode_progress_panel.Show();
            backgroundWorker1.RunWorkerAsync(10);
        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            Drag_MouseDown(sender, e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void passcode_textBox_Click(object sender, EventArgs e)
        {
            this.passcede_warn.Visible = false;
        }

        private void PassCode_Load(object sender, EventArgs e)
        {
            if (this.SyncType == SyncType.D && this.SyncStatus == SyncStatus.Downloading)
            {
                this.passcode_progress_panel.Hide();
                this.passcode_textBox.Hide();
                this.passcode_confirmtext1.Show();
                this.passcode_confirmtext2.Show();
            }
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);//解决闪烁
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);//解决闪烁 

        }

        private void ChangeTextBoxValue(string text)
        {
            this.passcode_textBox.Text = text;
        }

        private void ChangeWarnValue(string text)
        {
            this.passcede_warn.Text = text;
        }

        private void passcode_title_MouseDown(object sender, MouseEventArgs e)
        {
            Drag_MouseDown(sender, e);
        }

       




    }
}
