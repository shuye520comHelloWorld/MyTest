using PinkBus.CheckInClient.DAL;
using PinkBus.CheckInClient.Entitys;
using PinkBus.CheckInClient.Helper;
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
    public partial class UploadPassCode : BaseForm
    {
        public UploadPassCode()
        {
            InitializeComponent();
        }
        public SyncType SyncType { set; get; }

        public SyncStatus SyncStatus { get; set; }

        private delegate void ChangeTxetBox(string text);
        private StringBuilder strBuilder = new StringBuilder();

        private void passcode_cancel_button_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        void BackgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (progressBar1.Value < e.ProgressPercentage && e.ProgressPercentage<=100)
            {
                this.progressBar1.Value = e.ProgressPercentage;
                //this.progress_label.Text = e.ProgressPercentage.ToString();
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

            Guid eventKey = SyncHelper.UploadCheckEvent(passcode_textBox.Text.Trim());

            if (eventKey == Guid.Empty||eventKey != (this.Owner as MainForm).SelectEventKey)
            {
               
                worker.CancelAsync();
                e.Cancel = true;
               
                ChangeTxetBox wt = new ChangeTxetBox(PasscedeWarnText);
                this.Invoke(wt, "上传码错误，请重新输入！");

                return;
            }

            strBuilder = new StringBuilder();
            strBuilder.AppendLine("上传已全部完成！");

            ChangeTxetBox ct = new ChangeTxetBox(ChangeRowCountValue);
            int conCount, cusCount, ticCount, volCount = 0;
            int uploadRowCount = EventDAL.UploadRowCount(eventKey, out conCount, out cusCount, out ticCount, out volCount);
            this.Invoke(ct, "共有 " + uploadRowCount + " 条变更记录需要上传");
            int uploadedCount = 0;
            string sqlI = "insert into SyncStatusLog values ('" + eventKey.ToString().ToLower() + "','U',0,0,0,0,0,0,0 ,'" + DateTime.Now + "')";
            EventDAL.ExecuteNonQuerySql(sqlI);

            for (int i = 1; i <= conCount*3; i++)
            {
                Consultant con = EventDAL.GetUploadConsultantTop1(eventKey);
                if (con.MappingKey == Guid.Empty) break;

                string contactId = "";
                bool res = SyncHelper.UploadConsultant(eventKey, con,out contactId);
                if (!res)
                {
                    if (string.IsNullOrEmpty(contactId)||contactId=="0")
                    {
                        if (!strBuilder.ToString().Contains(con.DirectSellerId))
                        {
                            strBuilder.AppendLine("顾问编号'" + con.DirectSellerId + "'编号不正确，上传失败！");
                            string sql = "delete from pb_volunteercheckin where mappingkey='"+con.MappingKey+"'";
                            EventDAL.ExecuteNonQuerySql(sql);

                        }
                    }
                }
                uploadedCount++;
                int per = int.Parse(Math.Round((uploadedCount * 1.00 / uploadRowCount * 100.00)).ToString());
                ChangeTxetBox conCT = new ChangeTxetBox(ProgressLabelValue);
                this.Invoke(conCT, "正在上传第 " + uploadedCount + " 条记录");
                worker.ReportProgress(per);
            }



            for (int i = 1; i <= cusCount; i++)
            {
                Customer cus = EventDAL.GetUploadCustomerTop1(eventKey);
                if (cus == null) break;
                bool res = SyncHelper.UploadCustomer(eventKey, cus);
                
                int per = int.Parse(Math.Round(((i + conCount) * 1.00 / uploadRowCount * 100.00)).ToString());
                ChangeTxetBox cusCT = new ChangeTxetBox(ProgressLabelValue);
                this.Invoke(cusCT, "正在上传第 " + (i+conCount) + " 条记录");
                worker.ReportProgress(per);
            }

            for (int i = 1; i <= ticCount; i++)
            {
                Ticket tic = EventDAL.GetUploadTicketTop1(eventKey);
                if (tic == null) break;
                bool res = SyncHelper.UploadTicket(eventKey, tic);
                int per = int.Parse(Math.Round(((i + conCount + cusCount) * 1.00 / uploadRowCount * 100.00)).ToString());
                ChangeTxetBox ticCT = new ChangeTxetBox(ProgressLabelValue);
                this.Invoke(ticCT, "正在上传第 " + (i + conCount + cusCount) + " 条记录");
                worker.ReportProgress(per);
            }

            for (int i = 1; i <= volCount; i++)
            {
                VolunteerCheckin vol = EventDAL.GetUploadVolunteerTop1(eventKey);
                if (vol == null) break;
                bool res = SyncHelper.UploadCheckinVolunteer(eventKey, vol);
                int per = int.Parse(Math.Round(((i + conCount + cusCount + ticCount) * 1.00 / uploadRowCount * 100.00)).ToString());
                ChangeTxetBox volCT = new ChangeTxetBox(ProgressLabelValue);
                this.Invoke(volCT, "正在上传第 " + (i + conCount + cusCount + ticCount) + " 条记录");
                worker.ReportProgress(per);
            }

            string sqlU = "update SyncStatusLog set Consultant=1,Ticket=1,Customer=1,Complete=1 where eventkey='"+ eventKey.ToString().ToLower() + "' ";
            EventDAL.ExecuteNonQuerySql(sqlU);


        }

        private void passcode_submit_button_Click(object sender, EventArgs e)
        {
            if (passcode_textBox.Text.Trim().Length != 6)
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
            passcede_warn.Hide();
        }

        private void UploadPassCode_Load(object sender, EventArgs e)
        {
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);//解决闪烁
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);//解决闪烁 

        }

     
       
    }
}
