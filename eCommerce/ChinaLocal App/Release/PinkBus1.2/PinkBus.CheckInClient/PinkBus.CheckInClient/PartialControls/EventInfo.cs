using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PinkBus.CheckInClient.Entitys;
using PinkBus.CheckInClient.PopControls;
using PinkBus.CheckInClient.Helper;
using PinkBus.CheckInClient.DAL;

namespace PinkBus.CheckInClient.PartialControls
{
    public partial class EventInfo : UserControl
    {
        public EventInfo()
        {
            InitializeComponent();
           
        }

        public Guid EventKey { get; set; }
        public string Title { set { eventinfo_eventtitle.Text = value; } }
        public string SessionDate { set { eventinfo_sessiondate.Text = value; } }
        public Label label { set; get; }

        public SyncStatus SyncStatus { get; set; }

        public void ExcelBtbDisable()
        {
            //eventinfo_Excel_btn.Enabled = false;
            this.eventinfo_Excel_btn.Click -= eventinfo_Excel_btn_Click;
            this.eventinfo_Excel_btn.BackgroundImage = global::PinkBus.CheckInClient.Properties.Resources.but222;
          

        }

        private void EventInfo_MouseLeave(object sender, EventArgs e)
        {
            this.BackColor = System.Drawing.Color.White;
            this.BorderStyle = System.Windows.Forms.BorderStyle.None;
           
        }


        private void EventInfo_MouseEnter(object sender, EventArgs e)
        {
            this.BackColor = System.Drawing.Color.FromArgb(100,((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        }

      
        private void eventinfo_Excel_btn_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(this.SyncStatus.ToString());
            //MessageBox.Show("excel+"+ this.EventKey.ToString());
            List<Event> events = EventDAL.QueryEvents(" where eventkey='"+this.EventKey.ToString()+"'");
            
            var folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.Description = "请选择excel导出的目标文件夹";
            var result = folderBrowserDialog.ShowDialog();
            string fileName="";
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                var folderName = folderBrowserDialog.SelectedPath;
                fileName = folderName + "\\" + events[0].EventTitle+"(签到)_"+events[0].CheckinStartDate.ToString("yyyyMMdd")+"_"+events[0].CheckinEndDate.ToString("yyyyMMdd") + ".xlsx";
                NPOIHelper.ExportAllCheckin(fileName, DataSourceHelper.DataGridViewDataSource(EventKey), events[0]);
                MessageBox.Show("导出成功，文件路径为：" + fileName);
            }
           
        }

        private void EventInfo_Load(object sender, EventArgs e)
        {
            
            eventinfo_syncstatus.Text = label.Text;
            eventinfo_syncstatus.BackColor = label.BackColor;

            

        }

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

            (this.ParentForm as MainForm).ShowHiden(false, this.eventinfo_eventtitle.Text);
            //(this.ParentForm as MainForm).extendForm.EventTitle = "";
            //MessageBox.Show(this.EventKey.ToString());
        }
        
    }
}
