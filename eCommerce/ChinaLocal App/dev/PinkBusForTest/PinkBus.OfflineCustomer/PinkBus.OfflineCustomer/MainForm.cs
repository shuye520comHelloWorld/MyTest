using PinkBus.OfflineCustomer.Common;
using PinkBus.OfflineCustomer.DAL;
using PinkBus.OfflineCustomer.Entity;
using PinkBus.OfflineCustomer.PopControls;
using PinkBus.OfflineCustomer.UserControls;
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

namespace PinkBus.OfflineCustomer
{

    public partial class MainForm : BaseForm
    {
        public Guid SelectEventKey
        {
            get;
            set;
        }

        public int CustomerQuantity
        {
            get
            {
                return OfflineCustomerHelper.QueryCustomersQuantity(SelectEventKey.ToString());
            }
        }

        public int UnAsignnedCustomerQuantity
        {
            get
            {
                return OfflineCustomerHelper.QueryUnAssignedCustomersQuantity(SelectEventKey.ToString());
            }
        }

        public void SetUnAssinged(object sender, EventArgs e)
        {
            //this.totalCustomer.Text = "0";
            this.totalCustomer.Text = string.Format("未分配新顾客：0");
        }


        public MainForm()
        {
            InitializeComponent();
            ShowEventList();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // ShowEventList();
            //ResetCustomerDetailInfo();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Left = Convert.ToInt32((Screen.PrimaryScreen.WorkingArea.Width - this.Width) * 0.5);
            this.Top = Convert.ToInt32((Screen.PrimaryScreen.WorkingArea.Height - this.Height) * 0.5);
        }

        public void ShowEventList()
        {
            this.eventPannel.Show();
            this.mainPanel.Hide();
            this.flowLayout_Panel.Controls.Clear();
            List<Event> events = OfflineCustomerHelper.QueryEvents();

            if (Events == null || events.Count == 0)
            {
                this.panel1.Visible = true;
                this.flowLayout_Panel.Visible = false;
                return;
            }
            else
            {
                this.panel1.Visible = false;
                this.flowLayout_Panel.Visible = true;
            }

            for (int i = 0; i < events.Count; i++)
            {
                SyncStatusLog SyncLogDown = OfflineCustomerHelper.GetSyncStatusLog(events[i].EventKey, SyncType.D);
                SyncStatusLog SyncLogUp = OfflineCustomerHelper.GetSyncStatusLog(events[i].EventKey, SyncType.U);

                EventInfo eventInfo = new EventInfo();
                Label eventStatus = new Label();
                int uploadRowCount = OfflineCustomerHelper.UploadRowCount(events[0].EventKey);

                #region label status

                if (SyncLogDown.Complete && !SyncLogUp.Complete)
                {
                    if (!SyncLogDown.Event || !SyncLogDown.Session)
                    {
                        eventInfo.SyncStatus = SyncStatus.Downloading;
                        eventStatus.Text = "下载中";
                        eventStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
                    }
                    else
                    {
                        eventInfo.SyncStatus = SyncStatus.Uploaded;
                        eventStatus.Text = "可上传";
                        eventStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
                    }
                }
                else if (uploadRowCount == 0)
                {
                    eventInfo.SyncStatus = SyncStatus.Uploaded;
                    eventStatus.Text = "已上传";
                    eventStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
                }
                else if (uploadRowCount > 0)
                {
                    eventInfo.SyncStatus = SyncStatus.Uploading;
                    eventStatus.Text = "可上传";
                    //label.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
                    eventStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
                }


                #endregion

                eventInfo.Click += EventInfo_Click;
                eventInfo.label = eventStatus;
                eventInfo.EventKey = events[i].EventKey;
                eventInfo.Title = events[i].EventTitle;
                eventInfo.SessionDate = events[i].CheckinStartDate.ToString("yyyy年MM月dd日 HH:mm") + " -- " + events[i].CheckinEndDate.ToString("yyyy年MM月dd日 HH:mm");

                //如果超过一天则置灰EXCEL导出
                if (events[i].EventEndDate.AddDays(1) < DateTime.Now)
                {
                    eventInfo.ExcelBtnDisabele();
                }

                eventInfo.Margin = new Padding(35, 20, 0, 0);
                //eventInfo.Location = new System.Drawing.Point(35, 0);
                this.flowLayout_Panel.Controls.Add(eventInfo);
            }
        }

        /// <summary>
        /// select one event 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void EventInfo_Click(object sender, EventArgs e)
        {
            //  SelectEventKey_label.Text = (sender as EventInfo).EventKey.ToString();
            SelectEventKey = (sender as EventInfo).EventKey;
            // MessageBox.Show(base.EventKey.ToString());
            CustomerListDataBind();
            //ResetCustomerDetailInfo();

            this.totalCustomer.Text = string.Format("未分配新顾客：{0}", UnAsignnedCustomerQuantity);
            this.totalAssignCustomer.Text = string.Format("新顾客总数：{0}", CustomerQuantity);
            // MessageBox.Show(this.SelectEventKey.ToString());
        }

        /// <summary>
        /// click 切换活动 button, switch download form and show customer form
        /// </summary>
        /// <param name="EventListShow"></param>
        public void ShowHiden(bool showEventList)
        {
            if (showEventList)
            {
                this.eventPannel.Show();
                //this.Hide();
                clientTitle_Change_Btn.Hide();
                this.mainPanel.Hide();
                this.clientTitle_Close_Btn.Show();
            }
            else
            {
                this.eventPannel.Hide();
                this.mainPanel.Show();
                clientTitle_Change_Btn.Show();
                clientTitle_Close_Btn.Hide();
                //clientTitle_Close_btn.Hide();
            }
        }

        /// <summary>
        /// 切换 活动按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clientTitle_Change_btn_Click(object sender, EventArgs e)
        {
            ShowEventList();
            ShowHiden(true);
        }

        /// <summary>
        /// drag form as form header was hidden
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClientTitle_Panel_MouseDown(object sender, MouseEventArgs e)
        {
            Drag_MouseDown(sender, e);
        }

        private void clientTitle_Close_Btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// click 下载活动 button 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void downLoad_Btn_Click(object sender, EventArgs e)
        {
            if (!ConnectStatus.IsConnected())
            {
                MessageBox.Show("当前客户端没有网络连接，请连接后重试！");
                return;
            }
            PassCode pc = new PassCode();
            pc.StartPosition = FormStartPosition.CenterParent;
            pc.Owner = this;
            pc.LabelTitle = "请导入6位活动导入码";
            pc.SyncType = SyncType.D;
            pc.SyncStatus = SyncStatus.None;
            pc.ShowDialog();

        }
        /// <summary>
        /// 添加顾客
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkinPanel_Addcustomer_btn_Click(object sender, EventArgs e)
        {
            NewCustomer customer = new NewCustomer(this.SelectEventKey);
            customer.StartPosition = FormStartPosition.CenterParent;
            customer.AfterCustomerClosed += new EventHandler(CustomerAfterCustomerClosed);
            customer.Owner = this;
            customer.ShowDialog();


        }

        private void CustomerAfterCustomerClosed(object sender, EventArgs e)
        {
            //todo: show customer in gridview
            CustomerListDataBind();



            this.totalCustomer.Text = string.Format("未分配新顾客：{0}", UnAsignnedCustomerQuantity);
            this.totalAssignCustomer.Text = string.Format("新顾客总数：{0}", CustomerQuantity);
        }

        public void CustomerListDataBind(bool search = false)
        {

            customer_GridView.AutoGenerateColumns = false;

            var name = new DataGridViewTextBoxColumn { DataPropertyName = "CustomerName", HeaderText = "     姓名", Visible = true, Width = 120 };
            var contactInfo = new DataGridViewTextBoxColumn { DataPropertyName = "ContactInfo", HeaderText = "   联系方式 ", Visible = true, Width = 170 };
            var customerType = new DataGridViewTextBoxColumn { DataPropertyName = "CustomerType", HeaderText = "   顾客身份", Visible = true, Width = 120 };
            var age = new DataGridViewTextBoxColumn { DataPropertyName = "AgeRange", HeaderText = "     年龄", Visible = true, Width = 115 };
            var directSellerId = new DataGridViewTextBoxColumn { DataPropertyName = "DirectSellerId", HeaderText = "   分配顾问编号", Visible = true, Width = 150 };
            
            DataGridViewImageColumn btnImageDelete = new DataGridViewImageColumn(false);
            Image imgEdit = global::PinkBus.OfflineCustomer.Properties.Resources.deleteTrue_p;
            btnImageDelete.Image = imgEdit;
            btnImageDelete.HeaderText = "操作";
            btnImageDelete.Name = "btnImageDelete";
            btnImageDelete.DefaultCellStyle.Padding = new Padding(2, 2,2, 2);
            btnImageDelete.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.customer_GridView.CellFormatting -= customer_GridView_CellFormatting;
            this.customer_GridView.CellFormatting += customer_GridView_CellFormatting;

            customer_GridView.Columns.Clear();

            customer_GridView.Columns.AddRange(name, contactInfo, customerType, age, directSellerId, btnImageDelete);
            

            List<Customer> customers = OfflineCustomerHelper.QueryCustomers(this.SelectEventKey.ToString());
            if (customers.Count == 0)
            {
                emptyData_GridView.Visible = true;
            }
            else
            {
                emptyData_GridView.Visible = false;
            }
                

            #region convert customer data
              customers.ForEach((p) =>
            {
                switch (p.CustomerType)
                {
                    case "0": p.CustomerType = "老顾客"; break;
                    case "1": p.CustomerType = "新顾客"; break;
                    case "2": p.CustomerType = "在校学生"; break;
                }

                switch (p.AgeRange)
                {
                    case "Bellow25": p.AgeRange = "25岁以下"; break;
                    case "Between2535": p.AgeRange = "25-35岁"; break;
                    case "Between3545": p.AgeRange = "35-45岁"; break;
                    case "Above45": p.AgeRange = "45岁以上"; break;
                }

                switch (p.ContactType)
                {
                    case "PhoneNumber": p.ContactInfo = string.Format("{0}(手机)", p.ContactInfo); break;
                    case "QQ": p.ContactInfo = string.Format("{0}(QQ)", p.ContactInfo); break;
                    case "Wechat": p.ContactInfo = string.Format("{0}(微信)", p.ContactInfo); break;
                    case "Other": p.ContactInfo = string.Format("{0}(其它)", p.ContactInfo); break;
                }
            });

            #endregion
          
           // this.customer_GridView.DataSource = customers;


            string text = string.Empty;
            text = this.search_Textbox.Text.ToString();
            if (search && !string.IsNullOrEmpty(text))
            {
                if (text == ("在校学生"))
                {
                    customers = customers.FindAll(e => e.CustomerType == "在校学生").ToList();
                }
                else if (text == ("老顾客"))
                {
                    customers = customers.FindAll(e => e.CustomerType == "老顾客").ToList();
                }
                else if (text == ("新顾客"))
                {
                    customers = customers.FindAll(e => e.CustomerType == "新顾客").ToList();
                }
                else
                {
                    customers = customers.FindAll(e => e.CustomerName.Contains(text) || e.ContactInfo.Contains(text)).ToList();
                }
                //customers = customers.FindAll(e => e.State.Contains(text)).ToList();
                
            }
			this.customer_GridView.DataSource = customers;


        }

        /// <summary>
        /// add different image 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void customer_GridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (customer_GridView.Columns[e.ColumnIndex].Name == "btnImageDelete")
            {
                if (this.customer_GridView.Rows[e.RowIndex].Cells[4].Value == null)
                {
                    e.Value = global::PinkBus.OfflineCustomer.Properties.Resources.deleteTrue_p;
                }
                else 
                {
                    e.Value = global::PinkBus.OfflineCustomer.Properties.Resources.deleteFalse_p;
                }
            }
           
        }

        /// <summary>
        /// display customer detail infomation
        /// </summary>
        /// <param name="customer"></param>
        private void DisplayCustomerDetailInfo(Customer customer)
        {
            ResetCustomerDetailInfo();

            this.customer_UserName.Text = customer.CustomerName;
            this.customer_Phone.Text = customer.ContactInfo;

            if (customer.IsHearMaryKay.HasValue)
            {
                if (customer.IsHearMaryKay.Value)
                    this.customer_IsHearedMarykay.Text = "是";
                else
                    this.customer_IsHearedMarykay.Text = "否";
            }
            else
                this.customer_IsHearedMarykay.Text = string.Empty;

            if (customer.IsJoinEvent.HasValue)
            {
                if (customer.IsJoinEvent.Value)
                    this.customer_IsJoinEvent.Text = "是";
                else
                    this.customer_IsJoinEvent.Text = "否";
            }
            else
                this.customer_IsJoinEvent.Text = string.Empty;

            if (customer.UsedProduct.HasValue)
            {
                if (customer.UsedProduct.Value)
                    this.customer_UsedProduct.Text = "是";
                else
                    this.customer_UsedProduct.Text = "否";
            }
            else
                this.customer_UsedProduct.Text = string.Empty;

            if (customer.CustomerResponse.HasValue)
            {
                string response = string.Empty;
                switch (customer.CustomerResponse)
                {
                    case 0: response = "对产品有兴趣"; break;
                    case 1: response = "对公司有兴趣"; break;
                    case 2: response = "一般"; break;
                    case 3: response = "没兴趣"; break;
                }
                this.customer_CustomerResponse.Text = response;
            }

            string ageRange = string.Empty;
            
            this.customer_AgeRange.Text = customer.AgeRange;

            string adviceDate = string.Empty;

            if (!string.IsNullOrEmpty(customer.AdviceContactDate))
            {
                switch (customer.AdviceContactDate)
                {
                    case "0": adviceDate = "白天"; break;
                    case "1": adviceDate = "晚上"; break;
                };
            }
            this.customer_AdviceDate.Text = adviceDate;

            string bestContractDate = string.Empty;
            if (!string.IsNullOrEmpty(customer.BestContactDate))
            {
                switch (customer.BestContactDate)
                {
                    case "0": bestContractDate = "工作日"; break;
                    case "1": bestContractDate = "双休日"; break;
                };
            }
            this.customer_BestContactDate.Text = bestContractDate;

            
            string interestingTopic = string.Empty;
            if (!string.IsNullOrEmpty(customer.InterestingTopic))
            {
                if (customer.InterestingTopic.IndexOf("DressUp") > -1)
                    interestingTopic += "服饰搭配 ";
                if (customer.InterestingTopic.IndexOf("FamilyTies") > -1)
                    interestingTopic += "家庭关系 ";
                if (customer.InterestingTopic.IndexOf("MakeUp") > -1)
                    interestingTopic += "彩妆技巧 ";
                if (customer.InterestingTopic.IndexOf("SkinCare") > -1)
                    interestingTopic += "美容护肤 ";
            }
            this.customer_InterestTopic.Text = interestingTopic;

            
            this.customer_Career.Text = customer.Career;
            this.customer_Address.Text = customer.Province + customer.City + customer.County;
        }

        private void ResetCustomerDetailInfo()
        {
            this.customer_UserName.Text = string.Empty;
            this.customer_Phone.Text = string.Empty;
            this.customer_IsHearedMarykay.Text = string.Empty;
            this.customer_IsJoinEvent.Text = string.Empty;
            this.customer_UsedProduct.Text = string.Empty;
            this.customer_CustomerResponse.Text = string.Empty;
            this.customer_AgeRange.Text = string.Empty;
            this.customer_AdviceDate.Text = string.Empty;
            this.customer_BestContactDate.Text = string.Empty;
            this.customer_Career.Text = string.Empty;
            this.customer_InterestTopic.Text = string.Empty;
            this.customer_Address.Text = string.Empty;
        }
        /// <summary>
        ///选择顾客列表中的一行，显示详细信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void customer_GridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
                return;

            customer_GridView.Rows[e.RowIndex].Selected = true;
            DataGridViewRow currentRow = customer_GridView.Rows[e.RowIndex];
            DisplayCustomerDetailInfo((Customer)currentRow.DataBoundItem);
        }

        /// <summary>
        /// 分配新顾客
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkinPanel_Assign_btn_Click(object sender, EventArgs e)
        {
            AssignCustomer customer = new AssignCustomer(this.SelectEventKey);
            customer.StartPosition = FormStartPosition.CenterParent;
            customer.SetUnassignedCustomer += new EventHandler(SetUnAssinged);
            customer.Owner = this;
            customer.ShowDialog();
        }

        /// <summary>
        //上传顾客
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkinPanel_Upload_btn_Click(object sender, EventArgs e)
        {
            if (UnAsignnedCustomerQuantity > 0)
            {
                ShowMessage info = new ShowMessage("请先分配新顾客,未分配的顾客不进行上传");
                info.StartPosition = FormStartPosition.CenterParent;
                info.AfterContinue += ContinueAddCustomer_Btn_Click;
                info.AfterEnd += EndAddCustomer_Btn_Click;
                info.ShowDialog();
            }
            else
            {
                UploadPassCode customer = new UploadPassCode(this.SelectEventKey);
                customer.StartPosition = FormStartPosition.CenterParent;
                customer.Owner = this;
                customer.ShowDialog();
            }

        }

        private void ContinueAddCustomer_Btn_Click(object sender, EventArgs e)
        {
            UploadPassCode customer = new UploadPassCode(this.SelectEventKey);
            customer.StartPosition = FormStartPosition.CenterParent;
            customer.Owner = this;
            customer.ShowDialog();
        }

        private void EndAddCustomer_Btn_Click(object sender, EventArgs e)
        {
            // this.Close();
        }
        private void mainPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void search_Btn_Click(object sender, EventArgs e)
        {
            CustomerListDataBind(true);
        }

        private void minSize_Btn_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        //删除顾客数据
        private void customer_GridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            List<Customer> customers = OfflineCustomerHelper.QueryCustomers(this.SelectEventKey.ToString());
            if (e.RowIndex >= 0)
            {
                string customerKey = customers[e.RowIndex].CustomerKey.ToString();
                string DelID = customers[e.RowIndex].DirectSellerId;
                if (DelID != null)
                {
                    return;
                }
                else
                {

                    if (e.ColumnIndex == 5)
                    {
                        MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                        DialogResult result = MessageBox.Show("确定删除吗？", "提示", buttons);
                        if (result == DialogResult.Yes)
                        {
                            OfflineCustomerHelper.DeleteCustomer(customerKey);
                            CustomerListDataBind();
                        }
                        //else
                        //    return;
                    }
                    //else
                    //    return;
                }
            }
            //else
            //    return;

        }

        //private void customer_GridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        //{
        //    DataGridViewRow currentRow = customer_GridView.Rows[0];
        //    DisplayCustomerDetailInfo((Customer)currentRow.DataBoundItem);
        //}
    }




    public class BaseForm : Form
    {
        public BaseForm()
        {
            SetClassLong(this.Handle, GCL_STYLE, GetClassLong(this.Handle, GCL_STYLE) | CS_DropSHADOW);
            if (!ConnectStatus.IsConnected())
            {
                MessageBox.Show("当前客户端没有网络连接，请连接后重试！");
            }
        }

        protected void KeyPressOnlyNum(object sender, KeyPressEventArgs e)
        {
            if (!((e.KeyChar >= '0' && e.KeyChar <= '9') || e.KeyChar == ' ') && e.KeyChar != '\b')//不输入输入除了数字之外的所有非法字符的判断
            {
                e.Handled = true;
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
