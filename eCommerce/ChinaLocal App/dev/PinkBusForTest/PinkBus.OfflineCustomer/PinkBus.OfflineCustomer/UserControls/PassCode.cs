using Newtonsoft.Json;
using PinkBus.OfflineCustomer.DAL;
using PinkBus.OfflineCustomer.Entity;
using PinkBus.OfflineCustomer.Helper;
using PinkBus.OfflineCustomer;
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

namespace PinkBus.OfflineCustomer.UserControls
{
    /// <summary>
    ///check token while download or upload data
    /// </summary>
    public partial class PassCode : BaseForm
    {
        public PassCode()
            : base()
        {
            SetClassLong(this.Handle, GCL_STYLE, GetClassLong(this.Handle, GCL_STYLE) | CS_DropSHADOW);
            InitializeComponent();
        }

        public string LabelTitle
        {
            set
            {
                passcode_Title.Text = value;
            }
        }

        public SyncType SyncType
        {
            set;
            get;
        }

        public Guid EventKey
        {
            get;
            set;
        }

        public SyncStatus SyncStatus
        {
            get;
            set;
        }

        private delegate void ChangeTxetBox(string text);

        #region BackgroundWorker1

        void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            if (SyncStatus == SyncStatus.Downloading)
            {
                string where = " where LOWER(eventkey)='" + EventKey.ToString().ToLower() + "'";
                List<Event> events = OfflineCustomerHelper.QueryEvents(where);
                //  bool aa = passcode_textBox.InvokeRequired;
                ChangeTxetBox ct = new ChangeTxetBox(ChangeTextBoxValue);
                this.Invoke(ct, events[0].DownloadToken);

            }
            // bool bb = passcode_textBox.InvokeRequired;
            EventKey = SyncHelper.DownloadEvents(worker, this.passcode_TextBox.Text);
            if (EventKey != Guid.Empty)
            {
                worker.ReportProgress(10);             
            }
            else
            {
                worker.CancelAsync();
                e.Cancel = true;
                //this.passcede_warn.Text = "该活动下载码无效！";
            }
        }

        void BackgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (progressBar.Value < e.ProgressPercentage)
            {
                this.progressBar.Value = e.ProgressPercentage;
                this.progressPercent_Label.Text = e.ProgressPercentage.ToString();
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
                ChangeTxetBox ct = new ChangeTxetBox(ChangeTextBoxValue);
                this.Invoke(ct, "该活动下载码无效！");
                this.passcodeWarn_Lable.Visible = true;
                this.passCode_Panel.Show();
                this.passcode_Pogress_Panel.Hide();
            }
            else
            {
                this.Close();
                MainForm mf = this.Owner as MainForm;
                mf.ShowEventList();
            }

        }

        #endregion

        private void passcode_cancel_button_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }


        private void passcode_submit_button_Click(object sender, EventArgs e)
        {
            if (this.SyncStatus == SyncStatus.None)
            {
                string where = " where LOWER(DownloadToken)='" + this.passcode_TextBox.Text.ToLower() + "'";
                List<Event> events = OfflineCustomerHelper.QueryEvents(where);
                if (events.Count > 0)
                {
                    this.passcodeWarn_Lable.Text = "该活动已下载,无法再次导入";
                    this.passcodeWarn_Lable.Visible = true;
                    return;
                }
            }
            if (this.SyncStatus == SyncStatus.Downloading)
            {

            }

            this.passCode_Panel.Hide();
            this.passcode_Pogress_Panel.Show();
            backgroundWorker1.RunWorkerAsync(10);
        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            base.Drag_MouseDown(sender, e);
        }

        private void passcode_title_MouseDown(object sender, MouseEventArgs e)
        {
            base.Drag_MouseDown(sender, e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ChangeTextBoxValue(string text)
        {
            this.passcodeWarn_Lable.Text = text;
        }
        private void passcode_textBox_Click(object sender, EventArgs e)
        {
            this.passcodeWarn_Lable.Visible = false;
        }

        private void PassCode_Load(object sender, EventArgs e)
        {
            if (this.SyncType == SyncType.D && this.SyncStatus == SyncStatus.Downloading)
            {
                this.passcode_Pogress_Panel.Hide();
                this.passcode_TextBox.Hide();
                this.passcode_Confirm_Txt1.Show();
                this.passcode_Confirm_Txt2.Show();
            }
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);//解决闪烁
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);//解决闪烁 
        }

      
    }
}
