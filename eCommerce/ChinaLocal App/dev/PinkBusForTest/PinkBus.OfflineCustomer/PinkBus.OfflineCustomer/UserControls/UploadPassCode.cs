using PinkBus.OfflineCustomer.DAL;
using PinkBus.OfflineCustomer.Entity;
using PinkBus.OfflineCustomer.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PinkBus.OfflineCustomer.PopControls
{
    public partial class UploadPassCode : BaseForm
    {
        public Guid EventKey { get; set; }
        public UploadPassCode(Guid eventKey)
            : base()
        {
            InitializeComponent();
            SetClassLong(this.Handle, GCL_STYLE, GetClassLong(this.Handle, GCL_STYLE) | CS_DropSHADOW);
            //BindComboxs();
            EventKey = eventKey;
        }
        public SyncType SyncType { set; get; }

        public SyncStatus SyncStatus { get; set; }

        private delegate void ChangeTextBox(string text);
        private StringBuilder strBuilder = new StringBuilder();

        private void passcode_cancel_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        void BackgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (progressBar1.Value < e.ProgressPercentage)
            {
                this.progressBar1.Value = e.ProgressPercentage;
                this.progress_label.Text = string.Format("{0}%", e.ProgressPercentage.ToString());
            }
        }
        void BackgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // First, handle the case where an exception was thrown.
            if (e.Error != null)
            {
                //ShowMsgBox(e.Error.Message);
                MessageBox.Show(e.Error.Message);
            }
            else if (e.Cancelled)
            {
                this.PassCode_panel.Show();
                this.passcode_progress_panel.Hide();
            }
            else
            {

                MessageBox.Show(strBuilder.ToString());
                this.Close();
                // MainForm mf = this.Owner as MainForm;
                // mf.ShowEventList();
            }
        }


        void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            
            Guid eventKey = SyncHelper.UploadCheckEvent(passcode_textBox.Text);

            if (eventKey == Guid.Empty || eventKey != (this.Owner as MainForm).SelectEventKey)
            {
                worker.CancelAsync();
                e.Cancel = true;
                ChangeTextBox wt = new ChangeTextBox(PasscedeWarnText);
                this.Invoke(wt, "上传码错误，请重新输入！");
                return;
            }
            //passcede_warn.Hide();


            strBuilder = new StringBuilder();
            strBuilder.AppendLine("上传已全部完成！");

            ChangeTextBox ct = new ChangeTextBox(ChangeRowCountValue);

            //all customer which state is in (0,1) :0:new ;1:assigned
            List<Customer> unUploadCustomers = OfflineCustomerHelper.QueryUploadCustomers(eventKey.ToString());
            List<Customer> uploadCustomers = unUploadCustomers.Where(p => p.State == "1").ToList();
            if (uploadCustomers != null && uploadCustomers.Count >= 0)
            {
                int total = uploadCustomers.Count;
                this.Invoke(ct, "共有 " + total + " 条变更记录需要上传");

                //string sqlI = "insert into SyncStatusLog values ('" + eventKey.ToString().ToLower() + "','U',0,0,0,0,0,0,0 ,'" + DateTime.Now + "')";
                int count = 1;
                foreach (var customer in uploadCustomers)
                {
                    bool res = SyncHelper.UploadCustomer(eventKey, customer);
                    int percent = int.Parse(Math.Round((count * 1.00 / total * 100.00)).ToString());
                    ChangeTextBox cusCT = new ChangeTextBox(ProgressLabelValue);
                    this.Invoke(cusCT, "正在上传第 " + count + " 条记录");
                    count++;
                    worker.ReportProgress(percent);
                }
            }
            string sqlU = String.Empty;
            if (unUploadCustomers.Count == uploadCustomers.Count)
            {
                sqlU = "update SyncStatusLog set Customer=1,Complete=1,SyncType='U' where eventkey='" + eventKey.ToString().ToLower() + "' ";
            }
            else
                sqlU = "update SyncStatusLog set Customer=1,Complete=1 where eventkey='" + eventKey.ToString().ToLower() + "' ";

            OfflineCustomerHelper.ExecuteNonQuerySql(sqlU);
        }

        private void passcode_submit_button_Click(object sender, EventArgs e)
        {
            if (passcode_textBox.Text.Length != 6)
            {
                PasscedeWarnText("请输入正确的上传活动码！");
                return;
            }

            this.PassCode_panel.Hide();
            this.passcode_progress_panel.Show();
            backgroundWorker1.RunWorkerAsync(10);
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            Drag_MouseDown(sender, e);
        }

        private void ChangeRowCountValue(string text)
        {
            this.label1.Text = text;
        }
        private void ProgressLabelValue(string text)
        {
            this.progress_label.Text = text;
        }
        private void PasscedeWarnText(string text)
        {
            passcede_warn.Visible = true;
            passcede_warn.Text = text;
        }

        private void passcode_textBox_Click(object sender, EventArgs e)
        {
            //passcede_warn.Hide();
            //if (!string.IsNullOrEmpty(this.passcode_textBox.Text))
            //    this.passcode_textBox.Enabled = true;
        }

        private void UploadPassCode_Load(object sender, EventArgs e)
        {
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);//解决闪烁
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);//解决闪烁
        }
    }
}
