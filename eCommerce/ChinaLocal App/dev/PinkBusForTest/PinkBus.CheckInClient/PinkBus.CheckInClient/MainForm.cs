using Newtonsoft.Json;
using NLog;
using PinkBus.CheckInClient.DAL;
using PinkBus.CheckInClient.Entitys;
using PinkBus.CheckInClient.Helper;
using PinkBus.CheckInClient.PartialControls;
using PinkBus.CheckInClient.PopControls;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PinkBus.CheckInClient
{
    public partial class MainForm : BaseForm
    {

        public MainForm()
            : base()
        {
            InitializeComponent();


        }

        public Guid SelectEventKey { get; set; }
        public Guid SelectCustomerKey { get; set; }
        public Guid SelectTicketKey { get; set; }

        public ExtendForm extendForm = null;
        private void MainForm_Load(object sender, EventArgs e)
        {

            SetMainFormLocation();

            CheckSecondScreen();

            //通过消息得到解码信息
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;

            //通过消息得到解码信息
            Dll_Camera.GetAppHandle(this.Handle);

            ShowEventList();

            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);//解决闪烁
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);//解决闪烁 
        }

        private void CheckSecondScreen()
        {
            Screen[] scs = Screen.AllScreens;
            if (scs.Length == 1)
            {

                ShowMsgBox("没有发现拓展屏幕！");

            }
            else
            {
                extendForm = new ExtendForm();
                extendForm.EventTitle = "欢迎光临粉巴活动";
                extendForm.FormBorderStyle = FormBorderStyle.None;
                extendForm.Left = scs[0].Bounds.Width;
                extendForm.Top = 0;
                //extendForm.Location = scs[1].Bounds.Location;
                Point p = new Point(scs[1].Bounds.Location.X, scs[1].Bounds.Location.Y);
                extendForm.Location = p;
                extendForm.StartPosition = FormStartPosition.Manual;
                //extendForm.WindowState = FormWindowState.Maximized;
                extendForm.Size = new System.Drawing.Size(Screen.AllScreens[1].Bounds.Width, Screen.AllScreens[1].Bounds.Height);
                extendForm.Show();

            }
        }

        private void SetMainFormLocation()
        {
            int width = Screen.PrimaryScreen.Bounds.Width;
            int height = Screen.PrimaryScreen.Bounds.Height;
            // MessageBox.Show(width.ToString()+","+height.ToString());
            this.Location = new Point((width - 1100) / 2, (height - 640) / 2);
        }
        private void EnabledFun(object val, EventArgs e)
        {
            this.ClientTitleClose_Btn.Enabled = (bool)val;
            this.DownLoad_Panel.Enabled = (bool)val;
            this.EventList_Panel.Enabled = (bool)val;
        }


        private void Top_Close_btn_Click(object sender, EventArgs e)
        {
            if (deviceState)
            {
                Dll_Camera.ReleaseDevice();
            }
            else
            {
                Dll_Camera.ReleaseLostDevice();
            }
            this.Close();
        }

        private void Top2panel_downBtn_Click(object sender, EventArgs e)
        {
            if (!Common.IsConnected())
            {
                MessageBox.Show("当前客户端没有网络连接，请连接后重试！");
                return;
            }
            PassCode passCode = new PassCode();
            passCode.StartPosition = FormStartPosition.CenterParent;
            passCode.Owner = this;
            passCode.LabelTitle = "请导入6位活动导入码";
            passCode.SyncType = SyncType.D;
            passCode.SyncStatus = SyncStatus.None;
            passCode.ShowDialog();

        }

        private void CheckinPanel_Addcustomer_Btn_Click(object sender, EventArgs e)
        {
            if (EventDAL.QueryConsultantsWhichHasUnUseTickets(SelectEventKey).Count > 0)
            {

                NewCustomer newCustomer = new NewCustomer();
                newCustomer.Owner = this;
                newCustomer.StartPosition = FormStartPosition.CenterParent;
                newCustomer.ShowDialog();
            }
            else
            {
                ShowMsgBox("没有剩余的券可供添加顾客！");
            }
        }


        private void TopPanel_MouseDown(object sender, MouseEventArgs e)
        {
            Drag_MouseDown(sender, e);
        }




        public void ShowEventList()
        {
            //
            //Thread.Sleep(500);
            //this.flowLayoutPanel1 = 2;// Controls.Add(e);
            List<Event> events = EventDAL.QueryEvents();
            if (events.Count > 0)
            {
                this.flowLayoutPanel1.Controls.Clear();
            }

            for (int i = 0; i < events.Count; i++)
            {
                SyncStatusLog SyncLogDown = EventDAL.GetSyncStatusLog(events[i].EventKey, SyncType.D);
                SyncStatusLog SyncLogUp = EventDAL.GetSyncStatusLog(events[i].EventKey, SyncType.U);

                int conCount, cusCount, ticCount, volCount = 0;
                int uploadRowCount = EventDAL.UploadRowCount(events[i].EventKey, out conCount, out cusCount, out ticCount, out volCount);


                EventInfo eventInfo = new EventInfo();
                Label label = new Label();
                #region label status
                if (SyncLogDown.Complete && !SyncLogUp.Complete)
                {
                    eventInfo.SyncStatus = SyncStatus.Downloaded;
                    label.Text = "可上传";
                    label.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
                }
                else if (!SyncLogDown.Event || !SyncLogDown.Session || !SyncLogDown.Consultant || !SyncLogDown.Ticket || !SyncLogDown.Customer)
                {
                    eventInfo.SyncStatus = SyncStatus.Downloading;
                    label.Text = "下载中";
                    label.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
                }
                else if (uploadRowCount == 0)
                {
                    eventInfo.SyncStatus = SyncStatus.Uploaded;
                    label.Text = "已上传";
                    label.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
                }
                else if (uploadRowCount > 0)
                {
                    eventInfo.SyncStatus = SyncStatus.Uploading;
                    label.Text = "可上传";
                    //label.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
                    label.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
                }

                #endregion


                eventInfo.Click += event_Click;
                eventInfo.label = label;
                eventInfo.EventKey = events[i].EventKey;
                eventInfo.Title = events[i].EventTitle;
                eventInfo.SessionDate = events[i].CheckinStartDate.ToString("yyyy年MM月dd日 HH:mm") + " -- " + events[i].CheckinEndDate.ToString("yyyy年MM月dd日 HH:mm");

                if (events[i].EventEndDate.AddDays(1) < DateTime.Now)
                {
                    eventInfo.ExcelBtbDisable();
                }

                this.flowLayoutPanel1.Controls.Add(eventInfo);
            }
        }







        void event_Click(object sender, EventArgs e)
        {
            selectEventKey_label.Tag = (sender as EventInfo).EventKey.ToString();
            SelectEventKey = (sender as EventInfo).EventKey;
            // MessageBox.Show(base.EventKey.ToString());
            ConsultantsListDataBind();
            //MessageBox.Show(this.SelectEventKey.ToString());
        }



        private void CheckinPanel_Outputvolunteer_Btn_Click(object sender, EventArgs e)
        {
            DataTable dt = EventDAL.QueryVolunteerExcels(SelectEventKey);
            if (dt.Rows.Count < 1)
            {
                ShowMsgBox("当前活动尚未有志愿者签到！");
                return;
            }

            OutputVolunteer outputVolunteer = new OutputVolunteer();
            outputVolunteer.Owner = this;
            outputVolunteer.StartPosition = FormStartPosition.CenterParent;
            outputVolunteer.ShowDialog();
        }

        private void CheckinPanel_AddVolunteer_Btn_Click(object sender, EventArgs e)
        {
            NewVolunteer newVolunteer = new NewVolunteer();
            newVolunteer.Owner = this;
            newVolunteer.StartPosition = FormStartPosition.CenterParent;
            newVolunteer.ShowDialog();
        }

        private void CheckinPanel_ShowPasscode_Btn_Click(object sender, EventArgs e)
        {
            CheckinCode checkinCode = new CheckinCode();
            checkinCode.StartPosition = FormStartPosition.CenterParent;
            checkinCode.Owner = this;
            checkinCode.ShowDialog();


        }

        private void checkinpanel_upload_btn_Click(object sender, EventArgs e)
        {
            int conCount, cusCount, ticCount, volCount = 0;
            int uploadRowCount = EventDAL.UploadRowCount(SelectEventKey, out conCount, out cusCount, out ticCount, out volCount);
            if (uploadRowCount < 1)
            {
                ShowMsgBox("没有变更数据需要上传！");
                return;
            }

            UploadPassCode up = new UploadPassCode();
            up.StartPosition = FormStartPosition.CenterParent;
            up.Owner = this;
            up.ShowDialog();
        }
        public void ShowHiden(bool eventListShow, string eventTitle = "")
        {
            if (eventListShow)
            {
                this.EventList_Panel.Show();
                this.CheckinList_Panel.Hide();
                Clienttitle_change_btn.Hide();
                ClientTitleClose_Btn.Show();
            }
            else
            {
                this.EventList_Panel.Hide();
                this.CheckinList_Panel.Show();
                Clienttitle_change_btn.Show();
                ClientTitleClose_Btn.Hide();

            }

            if (extendForm != null)
            {
                extendForm.EventTitle = string.IsNullOrEmpty(eventTitle) ? "欢迎光临粉巴活动" : eventTitle;
            }
        }




        private void ConsultantsListDataBind(bool search = false)
        {
            DataGridViewCheckin.AutoGenerateColumns = false;

            var c1 = new DataGridViewTextBoxColumn { DataPropertyName = "TicketKey", HeaderText = "", Visible = false };
            var c2 = new DataGridViewTextBoxColumn { DataPropertyName = "CustomerKey", HeaderText = "", Visible = false };
            var c3 = new DataGridViewTextBoxColumn { DataPropertyName = "SMSToken", HeaderText = "", Visible = false };
            var c4 = new DataGridViewTextBoxColumn { DataPropertyName = "TicketType", HeaderText = "", Visible = false };
            var c5 = new DataGridViewTextBoxColumn { DataPropertyName = "DirectSellerId", HeaderText = "", Visible = false };
            var c6 = new DataGridViewTextBoxColumn { DataPropertyName = "UserType", HeaderText = "   身份", Visible = true, Width = 150 };
            var c7 = new DataGridViewTextBoxColumn { DataPropertyName = "UserName", HeaderText = "   姓名", Visible = true, Width = 210 };
            var c8 = new DataGridViewTextBoxColumn { DataPropertyName = "UserPhoneNumber", HeaderText = "    手机号码", Visible = true, Width = 210 };
            var c9 = new DataGridViewTextBoxColumn { DataPropertyName = "CheckinStatus", HeaderText = "    签到状态", Visible = true, Width = 210 };

            DataGridViewCheckin.Columns.Clear();
            DataGridViewCheckin.Columns.AddRange(c1, c2, c3, c4, c5, c6, c7, c8, c9);

            List<CustomerTicket> customers = DataSourceHelper.DataGridViewDataSource(this.SelectEventKey);

            string text = this.search_text.Text.Trim();
            if (search && !string.IsNullOrEmpty(text))
            {

                if (text == ("贵宾"))
                {
                    customers = customers.FindAll(e => e.TicketType == TicketType.VIP).ToList();
                }
                else if (text == ("来宾"))
                {
                    customers = customers.FindAll(e => e.TicketType == TicketType.Normal).ToList();
                }
                else if (text == ("志愿者"))
                {
                    customers = customers.FindAll(e => e.TicketType == TicketType.Volunteer).ToList();
                }
                else if (text == ("未签到"))
                {
                    customers = customers.FindAll(e => e.CheckinStatus.Contains("未签到")).ToList();
                }
                else if (text == ("已签到"))
                {
                    customers = customers.FindAll(e => e.CheckinStatus.Contains("已签到")).ToList();
                }
                else if (text == "签到")
                {
                    customers = customers.FindAll(e => e.CheckinStatus.Contains("签到")).ToList();
                }
                else if (text.Contains("作废"))
                {
                    customers = customers.FindAll(e => e.TicketStatus == TicketStatus.Canceled).ToList();
                }
                else
                {
                    customers = customers.FindAll(e => e.CustomerName.Contains(text) || e.CustomerPhone.Contains(text)).ToList();
                }

            }


            DataGridViewCheckin.DataSource = customers;


            foreach (DataGridViewRow row in DataGridViewCheckin.Rows)
            {
                row.Height = 45;
                //row.Cells[0].a
                DataGridViewCellStyle aa = new DataGridViewCellStyle();
                row.Cells[3].Style.ForeColor = Color.Green;// new DataGridViewCellStyle { ForeColor = ColorTranslator.FromHtml("#00FF00") };
                //row.Selected = false;
                //row.
            }

            //DataGridViewCheckin.Focus();
            //DataGridViewCheckin.Rows[0].Selected = true;
            SummaryCountRefresh();
        }

        private void ClientTitle_Change_Btn_Click(object sender, EventArgs e)
        {
            this.SelectEventKey = Guid.Empty;
            this.SelectCustomerKey = Guid.Empty;
            ShowEventList();
            ShowHiden(true);
        }



        private void DataGridViewCheckin_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                DataGridViewCheckin.Rows[e.RowIndex].Selected = true;
                //DataGridViewCheckin.Rows[e.RowIndex].Visible = false; //DataGridViewCheckin.Rows[e.RowIndex].Selected = true;
                string val = DataGridViewCheckin.Rows[e.RowIndex].Cells[3].Value.ToString();
                TicketType ticketType = (TicketType)Enum.Parse(typeof(TicketType), val);
                string CustomerKey = DataGridViewCheckin.Rows[e.RowIndex].Cells[1].Value.ToString();
                this.SelectCustomerKey = Guid.Parse(CustomerKey);


                if (ticketType != TicketType.Volunteer)
                {
                    this.SelectTicketKey = Guid.Parse(DataGridViewCheckin.Rows[e.RowIndex].Cells[0].Value.ToString());
                    CustomerInfoShow(CustomerKey);
                }
                else
                {
                    this.SelectTicketKey = Guid.Parse(DataGridViewCheckin.Rows[e.RowIndex].Cells[1].Value.ToString());
                    ConsultantInfoShow(CustomerKey);
                }

            }
        }

        public void CustomerInfoShow(string CustomerKey)
        {
            CustomerInfo cusInfo = EventDAL.GetCustomer(CustomerKey, this.SelectEventKey);
            this.infopanel_consultant_img.Image = global::PinkBus.CheckInClient.Properties.Resources.portrait;
            this.infopanel_username.Text = cusInfo.CustomerName;
            this.infopanel_usertype.Text = cusInfo.TicketType == TicketType.Normal ? "来宾顾客" : "贵宾顾客";
            if (cusInfo.TicketStatus == TicketStatus.Canceled)
            {
                this.infopanel_disableuser_btn.BackgroundImage = global::PinkBus.CheckInClient.Properties.Resources.but11;
                this.infopanel_disableuser_btn.Enabled = false;
            }
            else if (cusInfo.TicketStatus == TicketStatus.Checkin)
            {
                this.infopanel_disableuser_btn.BackgroundImage = global::PinkBus.CheckInClient.Properties.Resources.but17;
                infopanel_disableuser_btn.Enabled = false;
            }
            else
            {
                this.infopanel_disableuser_btn.BackgroundImage = global::PinkBus.CheckInClient.Properties.Resources.but0;
                this.infopanel_disableuser_btn.Enabled = true;
            }


            this.infopanel_volunteerpanel.Hide();
            this.infopanel_customerpanel.Show();
            this.customer_phone.Text = cusInfo.CustomerPhone;
            this.customerType.Text = cusInfo.CustomerType == 0 ? "老顾客" : (cusInfo.CustomerType==1?"新顾客":"VIP");
            this.customer_range.Text = cusInfo.AgeRange == 0 ? "25岁以下" : (cusInfo.AgeRange == 1 ? "25-35岁" : (cusInfo.AgeRange == 2 ? "35-45岁" : "45岁以上"));
            this.customer_inviter.Text = cusInfo.ConsultantName;
            this.customer_inviterphone.Text = cusInfo.ConsultantPhone;
            this.customer_hasbeautyclass.Text = cusInfo.BeautyClass.HasValue && cusInfo.BeautyClass.Value ? "是" : "否";
            this.customer_UsedProduct.Text = cusInfo.UsedProduct.HasValue && cusInfo.UsedProduct.Value ? "是" : "否";
            this.customer_Career.Text = !cusInfo.Career.HasValue ? "" : (cusInfo.Career == 0 ? "公司职员" : (cusInfo.Career == 1 ? "私营业主" : (cusInfo.Career == 2 ? "家庭主妇" : "自由职业")));
            this.customer_iterestingtopic.Text = string.IsNullOrEmpty(cusInfo.InterestingTopic) ? "" : cusInfo.InterestingTopic.Replace("SkinCare", "美容护肤").Replace("MakeUp", "彩妆技巧").Replace("DressUp", "服饰搭配").Replace("FamilyTies", "家庭关系").Replace(",", "，");
            this.customer_usedset.Text = string.IsNullOrEmpty(cusInfo.UsedSet) ? "" : cusInfo.UsedSet.Replace("TimeWise", "幻时/幻时佳").Replace("WhiteningSystemFoaming", "美白").Replace("Cleanser", "经典").Replace("CalmingInfluence", "舒颜").Replace("Other", "其他系列").Replace(",", "，");
            this.customer_interestincompany.Text = string.IsNullOrEmpty(cusInfo.InterestInCompany) ? "" : cusInfo.InterestInCompany.Replace("BeautyConfidence", "美丽自信").Replace("CompanyCulture", "公司文化").Replace("Other", "其他").Replace("BusinessOpportunity", "事业机会").Replace(",", "，");

            //SelectCustomerKey = cus.CustomerKey;


        }

        public void ConsultantInfoShow(string MappingKey)
        {
            Consultant cus = EventDAL.GetConsultant(MappingKey, this.SelectEventKey);

            var headerFull = AppDomain.CurrentDomain.BaseDirectory + "\\Data\\HeaderImgs\\" + cus.ContactId + ".jpg";
            if (System.IO.File.Exists(headerFull))
                infopanel_consultant_img.Image = Image.FromFile(headerFull);
            else
                infopanel_consultant_img.Image = global::PinkBus.CheckInClient.Properties.Resources.portrait;



            this.infopanel_username.Text = cus.LastName + cus.FirstName;
            this.infopanel_usertype.Text = "志愿者";
            this.infopanel_volunteerpanel.Show();
            this.infopanel_customerpanel.Hide();
            List<CustomerTicket> cusTickets = DataGridViewCheckin.DataSource as List<CustomerTicket>;
            if (cus.Status == (int)ConsultantStatus.CheckedIn)
            {
                this.infopanel_disableuser_btn.BackgroundImage = global::PinkBus.CheckInClient.Properties.Resources.but17;
                infopanel_disableuser_btn.Enabled = false;
            }
            else if (cus.Status == (int)ConsultantStatus.Canceled)
            {
                this.infopanel_disableuser_btn.BackgroundImage = global::PinkBus.CheckInClient.Properties.Resources.but11;
                this.infopanel_disableuser_btn.Enabled = false;
            }
            else
            {
                this.infopanel_disableuser_btn.BackgroundImage = global::PinkBus.CheckInClient.Properties.Resources.but0;
                this.infopanel_disableuser_btn.Enabled = true;
            }


            this.volunteer_sellerid.Text = cus.DirectSellerId;
            this.volunteer_phone.Text = cus.PhoneNumber;
            this.volunteer_level.Text = cus.Level;
            this.volunteer_residenceid.Text = cus.ResidenceID;
            this.volunteer_location.Text = cus.Province + " " + (cus.Province.Contains("市")?"": cus.City) + " " + cus.CountyName;

            //SelectCustomerKey = cus.MappingKey;
        }


        private void SummaryCountRefresh()
        {
            List<Ticket> tickets = EventDAL.QueryTickets(this.SelectEventKey);
            List<ConsultantTicket> conTickets = EventDAL.QueryVolunteers(this.SelectEventKey.ToString());
            this.summary_vips.Text = "贵宾: " + tickets.FindAll(e => e.TicketType == 0 && (e.TicketStatus == 5 )).Count + "/" + tickets.FindAll(e => e.TicketType == 0 && (e.TicketStatus == 2 || e.TicketStatus == 5 )).Count;
            this.summary_normals.Text = "来宾: " + tickets.FindAll(e => e.TicketType == 1 && (e.TicketStatus == 5 )).Count + "/" + tickets.FindAll(e => e.TicketType == 1 && (e.TicketStatus == 2 || e.TicketStatus == 5 )).Count;
            this.summary_volunters.Text = "志愿者: " + conTickets.FindAll(e => e.CheckinCount.HasValue).Count + "/" + conTickets.Count;
            this.summary_lefttickets.Text = "剩余贵宾/来宾券: " + tickets.FindAll(e => e.TicketType == 0 && (e.TicketStatus == 0 || e.TicketStatus == 1)).Count + "/" + tickets.FindAll(e => e.TicketType == 1 && (e.TicketStatus == 0 || e.TicketStatus == 1)).Count;
        }



        private void Infopanel_Customerpanel_MouseEnter(object sender, EventArgs e)
        {
            this.infopanel_customerpanel.Focus();
        }

        private void Infopanel_Customerpanel_MouseLeave(object sender, EventArgs e)
        {
            this.infopanel.Focus();
        }

        private void Infopanel_Disableuser_Btn_Click(object sender, EventArgs e)
        {
            Confirm con = new Confirm("提示","确定要作废此券码？");
            con.Owner = this;
            con.StartPosition = FormStartPosition.CenterParent;
            con.ShowDialog();

           
        }
        public void cancelTicket(object sender)
        {
            List<CustomerTicket> cusTickets = DataGridViewCheckin.DataSource as List<CustomerTicket>;
            int res = 0;
            if (infopanel_usertype.Text == "志愿者")
            {
                res = EventDAL.CancelVolunter(this.SelectTicketKey.ToString());
            }
            else
            {
                res = EventDAL.CancelTicket(this.SelectTicketKey.ToString());
                //aa.Find(c => c.TicketKey == this.SelectCustomerKey.ToString()).TicketStatus = TicketStatus.Canceled;
            }

            if (res > 0)
            {
                this.infopanel_disableuser_btn.BackgroundImage = global::PinkBus.CheckInClient.Properties.Resources.but11;
                this.infopanel_disableuser_btn.Enabled = false;
            }
            ConsultantsListDataBind();
            (sender as Confirm).Close();
        }


        public void DataGridViewRefresh()
        {
            ConsultantsListDataBind();
        }

        private void DataGridViewCheckin_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            //MessageBox.Show(SelectCustomerKey.ToString());
            if (SelectCustomerKey != Guid.Empty)
            {
                foreach (DataGridViewRow row in DataGridViewCheckin.Rows)
                {
                    var customerKey = row.Cells[1].Value.ToString().ToLower();
                    if (SelectCustomerKey.ToString().ToLower() == customerKey)
                    {
                        row.Selected = true;
                        DataGridViewCheckin.FirstDisplayedScrollingRowIndex = row.Index;

                        string val = DataGridViewCheckin.Rows[row.Index].Cells[3].Value.ToString();
                        TicketType ticketType = (TicketType)Enum.Parse(typeof(TicketType), val);
                        string CustomerKey = DataGridViewCheckin.Rows[row.Index].Cells[1].Value.ToString();
                        this.SelectCustomerKey = Guid.Parse(CustomerKey);


                        if (ticketType != TicketType.Volunteer)
                        {
                            this.SelectTicketKey = Guid.Parse(DataGridViewCheckin.Rows[row.Index].Cells[0].Value.ToString());
                            CustomerInfoShow(CustomerKey);
                        }
                        else
                        {
                            this.SelectTicketKey = Guid.Parse(DataGridViewCheckin.Rows[row.Index].Cells[1].Value.ToString());
                            ConsultantInfoShow(CustomerKey);
                        }
                    }
                }
            }
            else
            {
                if (DataGridViewCheckin.Rows.Count > 0)
                {
                    string val = DataGridViewCheckin.Rows[0].Cells[3].Value.ToString();
                    TicketType t = (TicketType)Enum.Parse(typeof(TicketType), val);
                    string CustomerKey = DataGridViewCheckin.Rows[0].Cells[1].Value.ToString();
                    this.SelectCustomerKey = Guid.Parse(CustomerKey);

                    if (t != TicketType.Volunteer)
                    {
                        this.SelectTicketKey = Guid.Parse(DataGridViewCheckin.Rows[0].Cells[0].Value.ToString());
                        CustomerInfoShow(CustomerKey);
                    }
                    else
                    {
                        this.SelectTicketKey = Guid.Parse(DataGridViewCheckin.Rows[0].Cells[1].Value.ToString());
                        ConsultantInfoShow(CustomerKey);
                    }

                }
            }

            DataGridViewCheckin.Focus();

        }

        private void DataGridViewCheckin_MouseEnter(object sender, EventArgs e)
        {
            DataGridViewCheckin.Focus();
        }

        private void DataGridViewCheckin_MouseLeave(object sender, EventArgs e)
        {
            ClientTitle_Panel.Focus();
        }

        //设备状态
        public Boolean deviceState = false;
        public Boolean qrEnable = true;
        public Boolean showNone = false;

        private void timer1_Tick(object sender, EventArgs e)
        {
            //this.timer1.Interval = 3000;
            int result = Dll_Camera.GetDevice();
            if (result == 1 && !deviceState)
            {
                //timer1.Enabled = false;
                int flag = Dll_Camera.StartDevice();
                //MessageBox.Show(flag.ToString());
                if (flag == 1)
                {
                    //设置qr
                    Dll_Camera.setQRable(qrEnable);
                    //设置扫码成功蜂鸣
                    Dll_Camera.SetBeepTime(100);
                    deviceState = true;
                    this.timer1.Interval = 3000;
                    ShowMsgBox("扫码设备已连接！");

                }
                else if (flag == 0)
                {
                    
                }
                //else if (flag == -1)
                //{
                //    MessageBox.Show("设备已启动");
                //}
                //else if (flag == -2)
                //{
                //    MessageBox.Show("设备已断开");
                //}
                //else if (flag == -3)
                //{
                //    MessageBox.Show("设备已初始化失败");
                //}
            }
            else
            {
                // MessageBox.Show(result.ToString());
                if (result != 1 && deviceState)
                {
                    deviceState = false;
                    Dll_Camera.ReleaseLostDevice();
                    ShowMsgBox("设备已断开!");
                }
                else if (!showNone && result != 1)
                {
                    showNone = true;
                    //MessageBox.Show("没有发现扫码设备!");
                    ShowMsgBox("没有发现扫码设备！");
                }
            }


        }
        private static Logger logger = LogManager.GetCurrentClassLogger(); 

        protected override void DefWndProc(ref Message msg)//两种方式获取解码信息，一种是通过此方式，另一种通过调用dll提供的解码函数获取
        {
            try
            {
                logger.Debug(msg.Msg);
                //Thread.Sleep(1000);
                if (msg.Msg == 1124 || msg.Msg == 123)//receive code info from the 532 dll
                {

                    Dll_Camera.setQRable(false);

                    timer1.Enabled = false;
                    IntPtr wp = new IntPtr((int)msg.WParam);
                    byte[] wpbuf = new byte[((int)msg.LParam)];
                    Marshal.Copy(wp, wpbuf, 0, ((int)msg.LParam));
                    string strBC = System.Text.Encoding.GetEncoding("GB2312").GetString(wpbuf, 0, ((int)msg.LParam));
                    //this.codeInfo.AppendText(strBC.Trim());

                    QRCodeCheckin(strBC);

                    Thread.Sleep(1000);
                    if (this.qrEnable == true)
                    {
                        Dll_Camera.setQRable(true);
                    }


                }
                else
                {
                    base.DefWndProc(ref msg);
                }
            }catch(Exception ex){
                logger.Debug(ex.ToString());
            }
        }
       
        private void QRCodeCheckin(string QRCode)
        {
            Guid codeEmpty = Guid.Empty;

            if (QRCode.ToLower().Contains("mkiapp.com/dashboard/ecard") || QRCode.ToLower().Contains("mkiapp.com/pinkbus/checkin"))
            {
                QRCode = QRCode.Split('/')[QRCode.Split('/').Length - 1];
                Regex reg = new Regex("^[A-F0-9]{8}(-[A-F0-9]{4}){3}-[A-F0-9]{12}$", RegexOptions.Compiled);
                if (Guid.TryParse(QRCode, out codeEmpty))
                {
                    QRCode = codeEmpty.ToString();
                }
                else
                {
                    if (QRCode.Length == 15)
                    {
                        QRCode = Common.DeserializeQRCode(QRCode);
                        if (QRCode.Length != 12)
                        {
                            ShowMsgBox("签到码有误！");
                            return;
                        }
                    }
                }

                Guid CustomerKey = Guid.Empty;
                string msg = "";
                if (EventDAL.QRCodeCheckin(SelectEventKey, QRCode, ref CustomerKey, ref msg))
                {
                    if (extendForm != null)
                    {
                       // extendForm.customerNameChange(msg);
                        extendForm.CustomerTitle = msg;
                    }

                    this.SelectCustomerKey = CustomerKey;
                    this.DataGridViewRefresh();
                    ShowMsgBox("签到成功！");

                }
                else
                {
                    ShowMsgBox(msg);
                }
            }
            else
            {

                ShowMsgBox("签到二维码无效！");

                //dC.StartPosition = FormStartPosition.CenterParent;
                //dC.ShowMessageTitle = "错误信息";
                //dC.ShowMessage = "签到二维码无效！";
                //dC.ShowDialog();
            }
        }

        private MsgShow msgShow = null;
        private delegate void showMessagesgBox(string msg, string title);


        public void ShowMsgBox(string msg, string title = "提示")
        {
            //ShowMsgBoxmsg(msg, title);
            showMessagesgBox smb = new showMessagesgBox(ShowMsgBoxmsg);
            this.Invoke(smb,msg, title);
           
        }
        private Thread ToggleTitleThread = null;
        private void ShowMsgBoxmsg(string msg, string title = "提示")
        {
           
            if (msgShow != null) { msgShow.Dispose(); ToggleTitleThread.Abort(); }
            this.ToggleTitleThread = new Thread(new ThreadStart(() =>
            {
                MsgShow ms = new MsgShow(title, msg);
                msgShow = ms;
                ms.StartPosition = FormStartPosition.CenterParent;
                //ms.Owner = this;
                ms.ShowDialog();
            }));
            this.ToggleTitleThread.Start();

        }



        private void search_Btn_Click(object sender, EventArgs e)
        {
            ConsultantsListDataBind(true);
        }

        private void volunteer_sellerid_Click(object sender, EventArgs e)
        {
            if (AppSetting.SyncDataAPI.Contains("dev") || AppSetting.SyncDataAPI.Contains("qa") || AppSetting.SyncDataAPI.Contains("uat"))
            {
                string ascCode = Common.SerializeQRCode(this.volunteer_sellerid.Text);
                this.search_text.Text = ascCode;
            }


        }

        private void customer_phone_Click(object sender, EventArgs e)
        {
            if (AppSetting.SyncDataAPI.Contains("dev") || AppSetting.SyncDataAPI.Contains("qa") || AppSetting.SyncDataAPI.Contains("uat"))
            {
                this.search_text.Text = "http://qrCode.dev.mkiapp.com/PinkBus/CheckIn/" + SelectTicketKey.ToString();
            }
        }

        private void minsizeBtn_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void flowLayoutPanel1_MouseEnter(object sender, EventArgs e)
        {
            this.flowLayoutPanel1.Focus();
        }




    }


    public class BaseForm : Form
    {
        public BaseForm()
        {
            SetClassLong(this.Handle, GCL_STYLE, GetClassLong(this.Handle, GCL_STYLE) | CS_DropSHADOW);
            if (!Common.IsConnected())
            {
                MessageBox.Show("当前客户端没有网络连接，请连接后重试！");
            }
        }


        #region  支持按住panel在桌面拖动
        [DllImport("User32.DLL")]
        private static extern int SendMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);
        [DllImport("User32.DLL")]
        private static extern bool ReleaseCapture();
        private const uint WM_SYSCOMMAND = 0x0112;
        private const int SC_MOVE = 61456;
        private const int HTCAPION = 2;
        protected void Drag_MouseDown(object sender, MouseEventArgs e)
        {
            // return;
            ReleaseCapture();
            SendMessage(Handle, WM_SYSCOMMAND, SC_MOVE | HTCAPION, 0);
        }

        protected void KeyPressOnlyNum(object sender, KeyPressEventArgs e)
        {
            if (!((e.KeyChar >= '0' && e.KeyChar <= '9') || e.KeyChar == ' ') && e.KeyChar != '\b')//不输入输入除了数字之外的所有非法字符的判断
            {
                e.Handled = true;
            }
        }

        #endregion

        #region 窗体边框阴影效果变量申明

        protected const int CS_DropSHADOW = 0x20000;
        protected const int GCL_STYLE = (-26);
        //声明Win32 API
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SetClassLong(IntPtr hwnd, int nIndex, int dwNewLong);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetClassLong(IntPtr hwnd, int nIndex);

        #endregion


    }




}
