using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PinkBus.OfflineCustomer.Entity;
using PinkBus.OfflineCustomer.DAL;
using PinkBus.OfflineCustomer.Helper;

namespace PinkBus.OfflineCustomer.UserControls
{
    public partial class EventInfo : UserControl
    {
        public EventInfo()
        {
            InitializeComponent();
        }

        /// <summary>
        /// identifier of event
        /// </summary>
        public Guid EventKey { get; set; }

        /// <summary>
        /// the title of event
        /// </summary>
        public string Title
        {
            set
            {
                eventinfo_EvenTtitle.Text = value;
            }
        }
        /// <summary>
        /// the datetime of session
        /// </summary>
        public string SessionDate
        {
            set
            {
                eventinfo_SessionDate.Text = value;
            }
        }

        public Label label { set; get; }

        public SyncStatus SyncStatus { get; set; }

        /// <summary>
        /// change boder style and color while mouse leave
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EventInfo_MouseLeave(object sender, EventArgs e)
        {
            this.BackColor = System.Drawing.Color.White;
            this.BorderStyle = System.Windows.Forms.BorderStyle.None;
        }

        /// <summary>
        ///  change boder style and color while mouse enter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EventInfo_MouseEnter(object sender, EventArgs e)
        {
            this.BackColor = System.Drawing.Color.FromArgb(100, ((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        }

        /// <summary>
        /// export event to excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void eventinfo_Excel_btn_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(this.SyncStatus.ToString());
            //MessageBox.Show("excel+" + this.EventKey.ToString());
            List<Event> events = OfflineCustomerHelper.QueryEvents(" where eventkey='" + this.EventKey.ToString() + "'");

            var folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.Description = "请选择excel导出的目标文件夹";
            var result = folderBrowserDialog.ShowDialog();
            string fileName = "";
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                var folderName = folderBrowserDialog.SelectedPath;
                fileName = folderName + "\\" + events[0].EventTitle + "(新顾客)_" + events[0].CheckinStartDate.ToString("yyyyMMdd") + "_" + events[0].CheckinEndDate.ToString("yyyyMMdd") + ".xlsx";
                NPOIHelper.ExportAllCheckin(fileName, OfflineCustomerHelper.QueryCustomers(this.EventKey.ToString()), events[0]);
                MessageBox.Show("导出成功，文件路径为：" + fileName);
            }

        }

        /// <summary>
        ///init color form load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EventInfo_Load(object sender, EventArgs e)
        {
            eventinfo_SyncStatus.Text = label.Text;
            eventinfo_SyncStatus.BackColor = label.BackColor;
        }

        //置灰Excel导出
        public void ExcelBtnDisabele()
        {
            //eventinfo_Excel_Btn.Enabled = false;
            this.eventinfo_Excel_Btn.Click -= eventinfo_Excel_btn_Click;
            //this.eventinfo_Excel_Btn.Click -= EventInfo_Click;
            this.eventinfo_Excel_Btn.BackgroundImage = PinkBus.OfflineCustomer.Properties.Resources.DisableOutputExcel;
        }
        /// <summary>
        /// download data while click button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EventInfo_Click(object sender, EventArgs e)
        {
            if (this.SyncStatus == SyncStatus.Downloading)
            {
                PassCode pc = new PassCode();
                pc.StartPosition = FormStartPosition.CenterParent;
                pc.SyncType = SyncType.D;
                pc.Owner = this.ParentForm as MainForm;
                pc.EventKey = this.EventKey;
                pc.SyncStatus = SyncStatus.Downloading;
                pc.ShowDialog();
                //MessageBox.Show("this.EventKey.ToString()");
                return;
            }

            (this.ParentForm as MainForm).ShowHiden(false);
            //MessageBox.Show(this.EventKey.ToString());
        }


    }
}
