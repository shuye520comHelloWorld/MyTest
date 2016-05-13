using PinkBus.CheckInClient.DAL;
using PinkBus.CheckInClient.Entitys;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PinkBus.CheckInClient.PopControls
{
    public partial class NewVolunteer : BaseForm
    {
        public NewVolunteer()
            : base()
        {
            //SetClassLong(this.Handle, GCL_STYLE, GetClassLong(this.Handle, GCL_STYLE) | CS_DropSHADOW);
            InitializeComponent();
            
        }

        private void BindProvinces()
        {
            this.province_Combox.DataSource = EventDAL.QueryProvinces();
            this.province_Combox.ValueMember = "ProvinceId";
            this.province_Combox.DisplayMember = "Province";
            this.province_Combox.SelectedIndex = 0;
            this.name_Text.Focus();
            this.county_text.Hide();
        }

        private void Province_Combox_SelectedValueChanged(object sender, EventArgs e)
        {
            this.city_Combox.DataSource = EventDAL.QueryCitys(province_Combox.SelectedValue.ToString());
            this.city_Combox.ValueMember = "CityId";
            this.city_Combox.DisplayMember = "City";
            this.city_Combox.SelectedIndex = 0;
        }

      
        private void city_Combox_SelectedValueChanged(object sender, EventArgs e)
        {
            string custom = "";
            if (city_Combox.SelectedValue.ToString()!="0")
            {
                custom = "or father ='9999'";
            }

            this.county_Combox.DataSource = EventDAL.QueryCountys(city_Combox.SelectedValue.ToString(), custom);
            this.county_Combox.ValueMember = "CountyId";
            this.county_Combox.DisplayMember = "County";
        }
        private void county_Combox_SelectedValueChanged(object sender, EventArgs e)
        {
            if (county_Combox.SelectedValue.ToString() == "-1")
            {
                this.county_Combox.Hide();
                this.county_text.Show();
            }
            else
            {
                this.county_Combox.Show();
                this.county_text.Hide();
                this.county_text.Text = "";
            }
        }

  

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Drag_MouseDowns(object sender, MouseEventArgs e)
        {
            Drag_MouseDown(sender, e);
        }

        private void volunteerSave_btn_Click(object sender, EventArgs e)
        {
            Consultant con = new Consultant();
            con.MappingKey = Guid.NewGuid();
            con.EventKey = (this.Owner as MainForm).SelectEventKey;

            string name = this.name_Text.Text;
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(SellerId_Text.Text) || string.IsNullOrEmpty(this.phone_text.Text))
            {
                this.errMsg.Text = "标示 * 的为必填项！";
                return;
            }
            if (!Regex.IsMatch(name, @"^[a-zA-Z\u4e00-\u9fa5]{2,20}$", RegexOptions.IgnoreCase))
            {
                this.errMsg.Text = "顾客姓名只能为汉字或字母！";
                return;
            }


            if (!Regex.IsMatch(phone_text.Text, @"^[1]+[3,4,5,7,8]+\d{9}", RegexOptions.IgnoreCase))
            {
                this.errMsg.Text = "顾客手机号格式不正确！";
                return;
            }

            //if (string.IsNullOrEmpty(this.vipTicketCount_text.Text) || string.IsNullOrEmpty(this.normalTicketCount_Text.Text))
            //{
            //    this.errMsg.Text = "贵宾票和来宾票不能为空！";
            //    return;
            //}

            if (SellerId_Text.Text.Length != 12)
            {
                this.errMsg.Text = "志愿者编号只能为12位！";
                return;
            }

            var volunteers = EventDAL.QueryVolunteers((this.Owner as MainForm).SelectEventKey.ToString());

            if (volunteers.FindAll(c => c.DirectSellerId == SellerId_Text.Text).Count > 0)
            {
                this.errMsg.Text = "志愿者者编号已存在，请勿重复添加！";
                return;
            }
            ///^[\u4e00\u9fa5 \s]{2,20}$/ 


            if (volunteers.FindAll(c => c.PhoneNumber == phone_text.Text).Count > 0)
            {
                this.errMsg.Text = "志愿者手机号已存在，请勿重复添加！";
                return;
            }

           
           

            con.LastName = name.Length <= 3 ? name.Substring(0, 1) : name.Substring(0, 2);
            con.FirstName = name.Length <= 3 ? name.Substring(1, name.Length - 1) : name.Substring(2, name.Length - 2);
            con.DirectSellerId = SellerId_Text.Text;   
            con.PhoneNumber = phone_text.Text;
            con.Level = level_text.Text;
            con.ResidenceID = residenceId_Text.Text;
            con.Province = province_Combox.SelectedValue.ToString() == "0" ? "" : province_Combox.Text;
            con.City = city_Combox.SelectedValue.ToString()=="0"?"": city_Combox.Text;
            if (county_text.Visible)
            {
                con.CountyName = county_text.Text;
            }
            else
            {
                con.CountyName = county_Combox.SelectedValue.ToString() == "0" ? "" : county_Combox.Text;
            }
            

            if (!string.IsNullOrEmpty(con.ResidenceID))
            {
                if (!Regex.IsMatch(con.ResidenceID, @"^(^\d{15}$|^\d{18}$|^\d{17}(\d|X|x))$", RegexOptions.IgnoreCase))
                {
                    this.errMsg.Text = "请输入正确的身份证号码！";
                    return;
                }
            }
           

            int vipTicketQuantity = 0;
            int.TryParse(this.vipTicketCount_text.Text, out vipTicketQuantity);
            con.VIPTicketQuantity = con.VIPTicketSettingQuantity = vipTicketQuantity;

            if (vipTicketQuantity > int.Parse(vipTicketCount_text.Tag.ToString()))
            {
                this.errMsg.Text = "贵宾票已超出剩余量！";
                return;
            }

            int normalTicketQuantity = 0;
            int.TryParse(this.normalTicketCount_Text.Text, out normalTicketQuantity);
            con.NormalTicketQuantity = con.NormalTicketSettingQuantity = normalTicketQuantity;
            if (normalTicketQuantity > int.Parse(normalTicketCount_Text.Tag.ToString()))
            {
                this.errMsg.Text = "来宾票已超出剩余量";
                return;
            }

           // if()

            
            if (EventDAL.AddVolunteer(con))
            {
                (this.Owner as MainForm).SelectCustomerKey = con.MappingKey;
                (this.Owner as MainForm).DataGridViewRefresh();
                (this.Owner as MainForm).ConsultantInfoShow(con.MappingKey.ToString());

                if ((this.Owner as MainForm).extendForm != null)
                {
                    (this.Owner as MainForm).extendForm.CustomerTitle = "志愿者 " + name;
                }

                this.Close();
            }

        }

        private void KeyPressNumber(object sender, KeyPressEventArgs e)
        {
            KeyPressOnlyNum(sender, e);
        }

        private void residenceId_Text_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!((e.KeyChar >= '0' && e.KeyChar <= '9') || e.KeyChar == ' ') && e.KeyChar != '\b' && e.KeyChar != 'x')//不输入输入除了数字之外的所有非法字符的判断
            {
                e.Handled = true;
            }
        }
        private void Combox_DropDownClosed(object sender, EventArgs e)
        {
            this.panel1.Focus();
        }
        private void TicketLeftCount()
        {
            List<Ticket> Ts = EventDAL.QueryTicketsReserve((this.Owner as MainForm).SelectEventKey);
            this.leftNormalCount_Text.Text = "剩余：" + Ts.FindAll(e => e.TicketType == 1 && (e.TicketStatus == 0 || e.TicketStatus == 1)).Count;
            this.leftVIPCount_Text.Text = "剩余：" + Ts.FindAll(e => e.TicketType == 0 && (e.TicketStatus == 0 || e.TicketStatus == 1)).Count;

            this.vipTicketCount_text.Tag = Ts.FindAll(e => e.TicketType == 0 && (e.TicketStatus == 0 || e.TicketStatus == 1)).Count;
            this.normalTicketCount_Text.Tag = Ts.FindAll(e => e.TicketType == 1 && (e.TicketStatus == 0 || e.TicketStatus == 1)).Count;

        }

        private void NewVolunteer_Load(object sender, EventArgs e)
        {
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);//解决闪烁
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);//解决闪烁 

            TicketLeftCount();
            BindProvinces();
        }

       

      
       
       


    }
}
