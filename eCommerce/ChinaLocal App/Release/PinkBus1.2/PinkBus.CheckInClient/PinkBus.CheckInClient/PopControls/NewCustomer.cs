using PinkBus.CheckInClient.DAL;
using PinkBus.CheckInClient.Entitys;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PinkBus.CheckInClient.PopControls
{
    public partial class NewCustomer : BaseForm
    {
        public NewCustomer()
            : base()
        {
            InitializeComponent();
            // SetClassLong(this.Handle, GCL_STYLE, GetClassLong(this.Handle, GCL_STYLE) | CS_DropSHADOW);
            BindComboxs();
        }


        private void Drag_MouseDowns(object sender, MouseEventArgs e)
        {
            Drag_MouseDown(sender, e);
        }


        private void BindComboxs()
        {
            #region  CustomerType
            IList<ComboBoxValue> cType = new List<ComboBoxValue>();
            cType.Add(new ComboBoxValue() { Id = "-1", Name = "请选择" });
            cType.Add(new ComboBoxValue() { Id = "0", Name = "老顾客" });
            cType.Add(new ComboBoxValue() { Id = "1", Name = "新顾客" });
            cType.Add(new ComboBoxValue() { Id = "2", Name = "VIP" });

            this.CustomerType_Combox.DataSource = cType;
            this.CustomerType_Combox.DisplayMember = "Name";
            this.CustomerType_Combox.ValueMember = "Id";
            this.CustomerType_Combox.SelectedIndex = 0;

            #endregion AgeRange

            #region AgeRange
            IList<ComboBoxValue> aRange = new List<ComboBoxValue>();
            aRange.Add(new ComboBoxValue() { Id = "-1", Name = "请选择" });
            aRange.Add(new ComboBoxValue() { Id = "0", Name = "25岁以下" });
            aRange.Add(new ComboBoxValue() { Id = "1", Name = "25-35岁" });
            aRange.Add(new ComboBoxValue() { Id = "2", Name = "35-45岁" });
            aRange.Add(new ComboBoxValue() { Id = "3", Name = "45岁以上" });

            this.AgeRange_Combox.DataSource = aRange;
            this.AgeRange_Combox.DisplayMember = "Name";
            this.AgeRange_Combox.ValueMember = "Id";
            this.AgeRange_Combox.SelectedIndex = 0;
            #endregion

            #region TicketType
            IList<ComboBoxValue> tType = new List<ComboBoxValue>();
            tType.Add(new ComboBoxValue() { Id = "-1", Name = "请选择" });
            tType.Add(new ComboBoxValue() { Id = "0", Name = "贵宾票" });
            tType.Add(new ComboBoxValue() { Id = "1", Name = "来宾票" });

            this.TicketType_ComBox.DataSource = tType;
            this.TicketType_ComBox.DisplayMember = "Name";
            this.TicketType_ComBox.ValueMember = "Id";
            this.TicketType_ComBox.SelectedIndex = 0;
            #endregion

            IList<ComboBoxValue> ttType = new List<ComboBoxValue>();
            ttType.Add(new ComboBoxValue() { Id = "-1", Name = "请先选择邀请券类型" });
            this.Consultant_ComBox.DataSource = ttType;
            this.Consultant_ComBox.DisplayMember = "Name";
            this.Consultant_ComBox.ValueMember = "Id";


            #region Career
            IList<ComboBoxValue> career = new List<ComboBoxValue>();
            career.Add(new ComboBoxValue() { Id = "-1", Name = "请选择" });
            career.Add(new ComboBoxValue() { Id = "0", Name = "公司职员" });
            career.Add(new ComboBoxValue() { Id = "1", Name = "私营业主" });
            career.Add(new ComboBoxValue() { Id = "2", Name = "家庭主妇" });
            career.Add(new ComboBoxValue() { Id = "3", Name = "自由职业" });

            this.Career_Combox.DataSource = career;
            this.Career_Combox.DisplayMember = "Name";
            this.Career_Combox.ValueMember = "Id";
            this.Career_Combox.SelectedIndex = 0;
            #endregion

            #region BasBeautyClass
            IList<ComboBoxValue> bClass = new List<ComboBoxValue>();
            bClass.Add(new ComboBoxValue() { Id = "-1", Name = "请选择" });
            bClass.Add(new ComboBoxValue() { Id = "1", Name = "是" });
            bClass.Add(new ComboBoxValue() { Id = "0", Name = "否" });

            this.BeautyClass_Combox.DataSource = bClass;
            this.BeautyClass_Combox.DisplayMember = "Name";
            this.BeautyClass_Combox.ValueMember = "Id";
            this.BeautyClass_Combox.SelectedIndex = 0;
            #endregion

            #region HasUsedSet
            IList<ComboBoxValue> usedSet = new List<ComboBoxValue>();
            usedSet.Add(new ComboBoxValue() { Id = "-1", Name = "请选择" });
            usedSet.Add(new ComboBoxValue() { Id = "1", Name = "是" });
            usedSet.Add(new ComboBoxValue() { Id = "0", Name = "否" });

            this.HasUsedSet_Combox.DataSource = usedSet;
            this.HasUsedSet_Combox.DisplayMember = "Name";
            this.HasUsedSet_Combox.ValueMember = "Id";
            this.HasUsedSet_Combox.SelectedIndex = 0;
            #endregion
        }



        private void NewCustomer_Load(object sender, EventArgs e)
        {
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);//解决闪烁
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);//解决闪烁 

            #region consultant
            MainForm mf = this.Owner as MainForm;

            //this.Consultant_ComBox.DataSource = EventDAL.QueryConsultantsWhichHasUnUseTickets(mf.SelectEventKey);
            //this.Consultant_ComBox.DisplayMember = "Name";
            //this.Consultant_ComBox.ValueMember = "MappingKey";
            //this.Consultant_ComBox.SelectedIndex = -1;
            #endregion

        }



        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            this.panel2.Focus();
            this.Errmsg.Text = "";
        }

        private void Combox_DropDownClosed(object sender, EventArgs e)
        {
            this.panel2.Focus();
        }





        private void LabelsCheck_Click(object sender, EventArgs e)
        {
            Label lb = (sender as Label);
            if (lb.Tag.ToString() == "0")
            {
                lb.Image = global::PinkBus.CheckInClient.Properties.Resources.pick;
                lb.Tag = "1";
            }
            else
            {
                lb.Image = global::PinkBus.CheckInClient.Properties.Resources.unpick;
                lb.Tag = "0";
            }
        }

        private void AddNewCustomer_Btn_Save_Click(object sender, EventArgs e)
        {
            Customer cus = new Customer();
            cus.CustomerKey = Guid.NewGuid();
            cus.CustomerName = Name_Text.Text.Trim();
            cus.CustomerPhone = Phone_Text.Text.Trim();



            object type = CustomerType_Combox.SelectedValue;
            object ageRange = AgeRange_Combox.SelectedValue;
            object ticketType = TicketType_ComBox.SelectedValue;
            object consultant = Consultant_ComBox.SelectedValue;
            object beautyClass = BeautyClass_Combox.SelectedValue;
            object hasUsedSet = HasUsedSet_Combox.SelectedValue;
            object career = Career_Combox.SelectedValue;



            if (string.IsNullOrEmpty(cus.CustomerName)
               || string.IsNullOrEmpty(cus.CustomerPhone)
               || type.ToString() == "-1"
               || ageRange.ToString() == "-1"
               || ticketType.ToString() == "-1"
               || consultant.ToString() == Guid.Empty.ToString()
               )
            {
                this.Errmsg.Text = "标示 * 的为必填项！";
                return;
            }



            if (!Regex.IsMatch(cus.CustomerName, @"^[a-zA-Z\u4e00-\u9fa5]{2,20}$", RegexOptions.IgnoreCase))
            {
                this.Errmsg.Text = "顾客姓名只能为汉字或字母！";
                return;
            }

            if (!Regex.IsMatch(cus.CustomerPhone, @"^[1]+[3,4,5,7,8]+\d{9}", RegexOptions.IgnoreCase))
            {
                this.Errmsg.Text = "顾客手机号格式不正确！";
                return;
            }


            var customers = EventDAL.QueryCustomers((this.Owner as MainForm).SelectEventKey.ToString());
            if (customers.FindAll(c => c.CustomerPhone == Phone_Text.Text).Count > 0)
            {
                //update by wang
                var cuso = customers.FindAll(c => c.CustomerPhone == Phone_Text.Text).FirstOrDefault();
                List<Ticket> tickets = EventDAL.QueryTickets(cuso.CustomerKey.ToString());
                Ticket ticket = tickets.First();
                if (ticket.TicketStatus == 5)
                {
                    MsgShow msgshow = new MsgShow("提示", "该顾客今日已签到，请勿重复签到！");
                    msgshow.Owner = this;
                    msgshow.StartPosition = FormStartPosition.CenterParent;
                    msgshow.ShowDialog();
                    if (msgshow.DialogResult.ToString().Equals("Cancel"))
                    {
                        this.Close();
                    }
                }
                else if (ticket.TicketStatus == 4)
                {
                    MsgShow msgshow = new MsgShow("提示", "该顾客已作废,不能签到！");
                    msgshow.Owner = this;
                    msgshow.StartPosition = FormStartPosition.CenterParent;
                    msgshow.ShowDialog();
                    if (msgshow.DialogResult.ToString().Equals("Cancel"))
                    {
                        this.Close();
                    }
                }
                else {
                    CheckInConfirm con = new CheckInConfirm(cuso);
                    con.Owner = this;
                    con.StartPosition = FormStartPosition.CenterParent;
                    con.ShowDialog();
                    if (con.DialogResult.ToString().Equals("Cancel"))
                    {
                        this.Close();
                    }
                }
                return;
                //this.Errmsg.Text = "顾客手机号已存在！";
                //return;
            }


            string interestingTopic = "";
            if (InterestingTopic_DressUp.Tag.ToString() == "1") interestingTopic += "DressUp,";
            if (this.InterestingTopic_FamilyTies.Tag.ToString() == "1") interestingTopic += "FamilyTies,";
            if (this.InterestingTopic_MakeUp.Tag.ToString() == "1") interestingTopic += "MakeUp,";
            if (this.InterestingTopic_SkinCare.Tag.ToString() == "1") interestingTopic += "SkinCare";

            string usedSet = "";
            if (this.UsedSet_TimeWise.Tag.ToString() == "1") usedSet += "TimeWise,";
            if (this.UsedSet_WhiteningSystemFoaming.Tag.ToString() == "1") usedSet += "WhiteningSystemFoaming,";
            if (this.UsedSet_Cleanser.Tag.ToString() == "1") usedSet += "Cleanser,";
            if (this.UsedSet_CalmingInfluence.Tag.ToString() == "1") usedSet += "CalmingInfluence,";
            if (this.UsedSet_Other.Tag.ToString() == "1") usedSet += "Other";

            string interestInCompany = "";
            if (this.InterestInCompany_BeautyConfidence.Tag.ToString() == "1") interestInCompany += "BeautyConfidence,";
            if (this.InterestInCompany_CompanyCulture.Tag.ToString() == "1") interestInCompany += "CompanyCulture,";
            if (this.InterestInCompany_BusinessOpportunity.Tag.ToString() == "1") interestInCompany += "BusinessOpportunity,";
            if (this.InterestInCompany_Other.Tag.ToString() == "1") interestInCompany += "Other";


            cus.CustomerType = int.Parse(CustomerType_Combox.SelectedValue.ToString());
            cus.AgeRange = int.Parse(AgeRange_Combox.SelectedValue.ToString());


            if (BeautyClass_Combox.SelectedValue.ToString() != "-1")
            {
                cus.BeautyClass = BeautyClass_Combox.SelectedValue.ToString() == "1";
            }
            if (HasUsedSet_Combox.SelectedValue.ToString() != "-1")
            {
                cus.UsedProduct = HasUsedSet_Combox.SelectedValue.ToString() == "1";
            }

            if (Career_Combox.SelectedValue.ToString() != "-1")
                cus.Career = int.Parse(Career_Combox.SelectedValue.ToString());

            //C.Career = int.Parse(.ToString());
            cus.InterestingTopic = interestingTopic.TrimEnd(',');
            cus.UsedSet = usedSet.TrimEnd(',');
            cus.InterestInCompany = interestInCompany.TrimEnd(',');


            if (EventDAL.AddNewCustomer(cus, ticketType.ToString(), consultant.ToString()))
            {
                (this.Owner as MainForm).SelectCustomerKey = cus.CustomerKey;

                (this.Owner as MainForm).DataGridViewRefresh();
                (this.Owner as MainForm).CustomerInfoShow(cus.CustomerKey.ToString());
                if ((this.Owner as MainForm).extendForm != null)
                {
                    (this.Owner as MainForm).extendForm.CustomerTitle = ((ticketType.ToString() == "0" ? "贵宾 " : "来宾 ") + Name_Text.Text);
                }
                this.Close();
            }


        }


        private void TicketType_ComBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string val = (sender as ComboBox).SelectedValue != null ? (sender as ComboBox).SelectedValue.ToString() : "";
            List<ConsultantsWhichHasUnUseTicket> unUseTickets = new List<ConsultantsWhichHasUnUseTicket>();

            if (val == "0")
            {
                unUseTickets = EventDAL.QueryConsultantsWhichHasUnUseTickets((this.Owner as MainForm).SelectEventKey).FindAll(c => c.TicketType == 0);
            }
            else if (val == "1")
            {
                unUseTickets = EventDAL.QueryConsultantsWhichHasUnUseTickets((this.Owner as MainForm).SelectEventKey).FindAll(c => c.TicketType == 1);
            }
            unUseTickets.Add(new ConsultantsWhichHasUnUseTicket() { LastName = "请", FirstName = "选择", MappingKey = Guid.Empty });
            unUseTickets.Reverse();
            this.Consultant_ComBox.DataSource = unUseTickets;
            this.Consultant_ComBox.DisplayMember = "Name";
            this.Consultant_ComBox.ValueMember = "MappingKey";
            this.Consultant_ComBox.SelectedIndex = 0;
        }

        private void close_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void KeyPressNumber(object sender, KeyPressEventArgs e)
        {
            KeyPressOnlyNum(sender, e);
        }

    }

    public class ComboBoxValue
    {
        public string Id { get; set; }
        public string Name { get; set; }

    }
}
