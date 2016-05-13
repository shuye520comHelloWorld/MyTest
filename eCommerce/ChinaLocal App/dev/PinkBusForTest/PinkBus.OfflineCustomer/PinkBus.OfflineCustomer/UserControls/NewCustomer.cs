using PinkBus.OfflineCustomer.DAL;
using PinkBus.OfflineCustomer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using PinkBus.OfflineCustomer.Entity;

namespace PinkBus.OfflineCustomer.UserControls
{
    public partial class NewCustomer : BaseForm
    {
        public Guid EventKey { get; set; }



        public const string DefaultOption = "请选择";
        public EventHandler AfterCustomerClosed;

        public delegate void UpdateCustomer(bool res);

        public NewCustomer(Guid eventKey)
            : base()
        {
            InitializeComponent();
            SetClassLong(this.Handle, GCL_STYLE, GetClassLong(this.Handle, GCL_STYLE) | CS_DropSHADOW);
            BindComboxs();
            EventKey = eventKey;
        }

        /// <summary>
        /// close form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (AfterCustomerClosed != null)
            {
                AfterCustomerClosed(this, e);
            }
            this.Close();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            Drag_MouseDown(sender, e);
        }

        private IList<ComboBoxValue> BoolComboBoxValue()
        {
            IList<ComboBoxValue> boolOptions = new List<ComboBoxValue>();
            boolOptions.Add(new ComboBoxValue() { Id = DefaultOption, Name = DefaultOption });
            boolOptions.Add(new ComboBoxValue() { Id = "true", Name = "是" });
            boolOptions.Add(new ComboBoxValue() { Id = "false", Name = "否" });
            return boolOptions;
        }


        private void BindComboxs()
        {
            #region ContactType
            IList<ComboBoxValue> contactType = new List<ComboBoxValue>();
            //contactType.Add(new ComboBoxValue() { Id = DefaultOption, Name = DefaultOption });
            contactType.Add(new ComboBoxValue() { Id = ContactType.PhoneNumber.ToString(), Name = "手机" });
            contactType.Add(new ComboBoxValue() { Id = ContactType.QQ.ToString(), Name = "QQ" });
            contactType.Add(new ComboBoxValue() { Id = ContactType.Wechat.ToString(), Name = "微信" }); ;
            contactType.Add(new ComboBoxValue() { Id = ContactType.Other.ToString(), Name = "其它" });

            this.contactType_comBox.DataSource = contactType;
            this.contactType_comBox.DisplayMember = "Name";
            this.contactType_comBox.ValueMember = "Id";
            #endregion
            #region  AgeRange
            //IList<ComboBoxValue> ageRange = new List<ComboBoxValue>();
            //ageRange.Add(new ComboBoxValue() { Id = DefaultOption, Name = DefaultOption });
            //ageRange.Add(new ComboBoxValue() { Id = "0", Name = "25岁以下" });
            //ageRange.Add(new ComboBoxValue() { Id = "1", Name = "25-35岁" });
            //ageRange.Add(new ComboBoxValue() { Id = "2", Name = "35-45岁" }); ;
            //ageRange.Add(new ComboBoxValue() { Id = "3", Name = "45岁以上" });

            //改为枚举类型
            IList<ComboBoxValue> ageRange = new List<ComboBoxValue>();
            ageRange.Add(new ComboBoxValue() { Id = DefaultOption, Name = DefaultOption });
            ageRange.Add(new ComboBoxValue() { Id = AgeRanges.Bellow25.ToString(), Name = "25岁以下" });
            ageRange.Add(new ComboBoxValue() { Id = AgeRanges.Between2535.ToString(), Name = "25-35岁" });
            ageRange.Add(new ComboBoxValue() { Id = AgeRanges.Between3545.ToString(), Name = "35-45岁" }); ;
            ageRange.Add(new ComboBoxValue() { Id = AgeRanges.Above45.ToString(), Name = "45岁以上" });

            this.ageRange_Combox.DataSource = ageRange;
            this.ageRange_Combox.DisplayMember = "Name";
            this.ageRange_Combox.ValueMember = "Id";

            #endregion AgeRange


            this.isHeared_Combobox.DataSource = BoolComboBoxValue();
            this.isHeared_Combobox.DisplayMember = "Name";
            this.isHeared_Combobox.ValueMember = "Id";

            this.isJoinEvent_ComboBox.DataSource = BoolComboBoxValue();
            this.isJoinEvent_ComboBox.DisplayMember = "Name";
            this.isJoinEvent_ComboBox.ValueMember = "Id";

            this.useset_ComboBox.DataSource = BoolComboBoxValue();
            this.useset_ComboBox.DisplayMember = "Name";
            this.useset_ComboBox.ValueMember = "Id";
            #region interest

            //IList<ComboBoxValue> interestOption = new List<ComboBoxValue>();
            //interestOption.Add(new ComboBoxValue() { Id = DefaultOption, Name = DefaultOption });
            //interestOption.Add(new ComboBoxValue() { Id = "1", Name = "美容护肤" }); 
            //interestOption.Add(new ComboBoxValue() { Id = "2", Name = "彩妆技巧" });
            //interestOption.Add(new ComboBoxValue() { Id = "3", Name = "服饰搭配" });          
            //interestOption.Add(new ComboBoxValue() { Id = "4", Name = "家庭关系" });

            //this.interest_ComBox.DataSource = interestOption;
            //this.interest_ComBox.DisplayMember = "Name";
            //this.interest_ComBox.ValueMember = "Id";
            #endregion

            #region customerType
            IList<ComboBoxValue> customerType = new List<ComboBoxValue>();
            // ComboBoxValue C0 = new ComboBoxValue() { Id = "0", Name = "" };
            customerType.Add(new ComboBoxValue() { Id = DefaultOption, Name = DefaultOption });
            customerType.Add(new ComboBoxValue() { Id = "0", Name = "老顾客" });
            customerType.Add(new ComboBoxValue() { Id = "1", Name = "新顾客" });
            customerType.Add(new ComboBoxValue() { Id = "2", Name = "在校学生" });

            this.customerType_ComboBox.DataSource = customerType;
            this.customerType_ComboBox.DisplayMember = "Name";
            this.customerType_ComboBox.ValueMember = "Id";

            #endregion

            #region Career
            //IList<ComboBoxValue> career = new List<ComboBoxValue>();
            //career.Add(new ComboBoxValue() { Id = DefaultOption, Name = DefaultOption });
            //career.Add(new ComboBoxValue() { Id = "0", Name = "公司职员" });
            //career.Add(new ComboBoxValue() { Id = "1", Name = "私营业主" });
            //career.Add(new ComboBoxValue() { Id = "2", Name = "家庭主妇" });
            //career.Add(new ComboBoxValue() { Id = "3", Name = "自由职业" });
            //改为枚举类型
            //IList<ComboBoxValue> career = new List<ComboBoxValue>();
            //career.Add(new ComboBoxValue() { Id = DefaultOption, Name = DefaultOption });
            //career.Add(new ComboBoxValue() { Id = Careers.Clerk.ToString(), Name = "公司职员" });
            //career.Add(new ComboBoxValue() { Id = Careers.PrivateOwner.ToString(), Name = "私营业主" });
            //career.Add(new ComboBoxValue() { Id = Careers.Housewife.ToString(), Name = "家庭主妇" });
            //career.Add(new ComboBoxValue() { Id = Careers.FreeLancers.ToString(), Name = "自由职业" });

            //this.career_ComboBox.DataSource = career;
            //this.career_ComboBox.DisplayMember = "Name";
            //this.career_ComboBox.ValueMember = "Id";

            #endregion

            #region resposne
            IList<ComboBoxValue> customerResposne = new List<ComboBoxValue>();
            customerResposne.Add(new ComboBoxValue() { Id = DefaultOption, Name = DefaultOption });
            customerResposne.Add(new ComboBoxValue() { Id = "0", Name = "对产品有兴趣" });
            customerResposne.Add(new ComboBoxValue() { Id = "1", Name = "对公司有兴趣" });
            customerResposne.Add(new ComboBoxValue() { Id = "2", Name = "一般" });
            customerResposne.Add(new ComboBoxValue() { Id = "3", Name = "没兴趣" });
            this.response_ComboBox.DataSource = customerResposne;
            this.response_ComboBox.DisplayMember = "Name";
            this.response_ComboBox.ValueMember = "Id";

            #endregion

            IList<ComboBoxValue> adviceTime = new List<ComboBoxValue>();
            adviceTime.Add(new ComboBoxValue() { Id = DefaultOption, Name = DefaultOption });
            adviceTime.Add(new ComboBoxValue() { Id = "0", Name = "白天" });
            adviceTime.Add(new ComboBoxValue() { Id = "1", Name = "晚上" });
            this.adviceTimePeriod_Combobox.DataSource = adviceTime;
            this.adviceTimePeriod_Combobox.DisplayMember = "Name";
            this.adviceTimePeriod_Combobox.ValueMember = "Id";

            IList<ComboBoxValue> bestContactTime = new List<ComboBoxValue>();
            bestContactTime.Add(new ComboBoxValue() { Id = DefaultOption, Name = DefaultOption });
            bestContactTime.Add(new ComboBoxValue() { Id = "0", Name = "工作日" });
            bestContactTime.Add(new ComboBoxValue() { Id = "1", Name = "双休日" });
            this.bestContactTime_ComboBox.DataSource = bestContactTime;
            this.bestContactTime_ComboBox.DisplayMember = "Name";
            this.bestContactTime_ComboBox.ValueMember = "Id";

            List<Province> province = OfflineCustomerHelper.QueryProvince();
            province.Insert(0, new Province { ProvinceId = "", ProvinceName = "请选择省" });
            this.province_Combobox.DataSource = province;
            this.province_Combobox.ValueMember = "ProvinceId";
            this.province_Combobox.DisplayMember = "ProvinceName";
        }

        private void NewCustomer_Load(object sender, EventArgs e)
        {
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);//解决闪烁
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);//解决闪烁

            this.panel2.Focus();
            this.customerConfirm_Btn.Enabled = false;
            #region consultant
            //MainForm mf = this.Owner as MainForm;

            //this.response_ComboBox.DataSource = OfflineCustomerHelper.QueryConsultantsWhichHasUnUseTickets(mf.SelectEventKey);
            //this.response_ComboBox.DisplayMember = "Name";
            //this.response_ComboBox.ValueMember = "MappingKey";

            #endregion
        }

        private void consultant_comBox_DropDownClosed(object sender, EventArgs e)
        {
            this.panel2.Focus();
            //consultant_comBox.Enabled = false;
            //consultant_comBox.Enabled = true;
            //MessageBox.Show("guanbi");
        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            this.panel2.Focus();
        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            Drag_MouseDown(sender, e);
        }


        private void name_Text_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.name_Text.Text) && !string.IsNullOrEmpty(this.contactInfo_Txt.Text))
                this.customerConfirm_Btn.Enabled = true;
            else
                this.customerConfirm_Btn.Enabled = false;


        }

        /// <summary>
        /// query county list after select city
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void city_Combobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<County> county = OfflineCustomerHelper.QueryCounty(this.city_Combobox.SelectedValue.ToString());
            county.Insert(0, new County { CountyId = "", CountyName = "请选择县" });
            if (county.Count > 0)
            {
                county.Insert(county.Count, new County { CountyId = "define", CountyName = "自定义区县" });
            }
            this.county_Combobox.DataSource = county;
            this.county_Combobox.ValueMember = "CountyId";
            this.county_Combobox.DisplayMember = "CountyName";

            this.custom_County.Visible = false;
            this.county_Combobox.Visible = true;
        }

        /// <summary>
        /// query county list after select
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void county_Combobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.county_Combobox.SelectedValue.ToString() == "define")
            {
                custom_County.Visible = true;
                this.county_Combobox.Visible = false;
            }
        }

        /// <summary>
        /// query county list after select province
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void provinceCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<City> city = OfflineCustomerHelper.QueryCity(this.province_Combobox.SelectedValue.ToString());

            city.Insert(0, new City { CityId = "", CityName = "请选择市" });
            this.city_Combobox.DataSource = city;
            this.city_Combobox.ValueMember = "CityId";
            this.city_Combobox.DisplayMember = "CityName";
            this.custom_County.Visible = false;
            this.county_Combobox.Visible = true;
        }

        /// <summary>
        /// add customer button click,submit customer info
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void customerConfirm_Btn_Click(object sender, EventArgs e)
        {

            if (!Regex.IsMatch(this.name_Text.Text.Trim(), @"^[a-zA-Z\u4e00-\u9fa5]{2,20}$", RegexOptions.IgnoreCase))
            {
                this.message_Lable.Text = "顾客姓名只能为汉字或字母!";
                return;
            }

            
            if (this.contactType_comBox.SelectedIndex == 0)
            {
                if (!Regex.IsMatch(this.contactInfo_Txt.Text.Trim(), @"^[1]+[3,4,5,7,8]+\d{9}", RegexOptions.IgnoreCase))
                {
                    this.message_Lable.Text = "顾客手机号格式不正确!";
                    return;
                }
            }           
            else
            {
                if (string.IsNullOrEmpty(this.contactInfo_Txt.Text.Trim()))
                {
                    this.message_Lable.Text = "请填写联系方式!";
                    return;
                }
            }

            //去重判断
            if (IsExisted(this.contactType_comBox.SelectedValue.ToString(),this.contactInfo_Txt.Text.Trim()))
            {
                MessageBox.Show("已存在相同联系方式的顾客，无法添加！");
                return;
            }

            //if(this.phone_Text.v)
            //TODO: validate customer name and phone number
            Customer customer = new Customer();
            customer.CustomerName = this.name_Text.Text.Trim();
            customer.CustomerKey = Guid.NewGuid();
            customer.ContactInfo = this.contactInfo_Txt.Text.Trim();
            customer.ContactType = this.contactType_comBox.SelectedValue.ToString();

            if (this.ageRange_Combox.SelectedIndex != 0)
                customer.AgeRange = this.ageRange_Combox.SelectedValue.ToString();

            if (this.isHeared_Combobox.SelectedIndex > 0)
                customer.IsHearMaryKay = bool.Parse(this.isHeared_Combobox.SelectedValue.ToString());

            //if (this.interest_ComBox.SelectedIndex > 0)
            //    customer.InterestingTopic = this.interest_ComBox.SelectedValue.ToString();
            string interestingTopic = "";
            if (InterestingTopic_DressUp.Tag.ToString() == "1") interestingTopic += "DressUp,";
            if (this.InterestingTopic_FamilyTies.Tag.ToString() == "1") interestingTopic += "FamilyTies,";
            if (this.InterestingTopic_MakeUp.Tag.ToString() == "1") interestingTopic += "MakeUp,";
            if (this.InterestingTopic_SkinCare.Tag.ToString() == "1") interestingTopic += "SkinCare";
            customer.InterestingTopic = interestingTopic;


            if (this.customerType_ComboBox.SelectedIndex > 0)
                customer.CustomerType = this.customerType_ComboBox.SelectedValue.ToString();

            //if (this.career_ComboBox.SelectedIndex > 0)
            //    customer.Career = this.career_ComboBox.SelectedValue.ToString();
            if (this.career_Text.Text.ToString() != null)
                customer.Career = this.career_Text.Text.ToString();

            if (this.response_ComboBox.SelectedIndex > 0)
                customer.CustomerResponse = int.Parse(this.response_ComboBox.SelectedValue.ToString());

            if (this.isJoinEvent_ComboBox.SelectedIndex > 0)
                customer.IsJoinEvent = bool.Parse(this.isJoinEvent_ComboBox.SelectedValue.ToString());

            if (this.useset_ComboBox.SelectedIndex > 0)
                customer.UsedProduct = bool.Parse(this.useset_ComboBox.SelectedValue.ToString());

            if (this.adviceTimePeriod_Combobox.SelectedIndex > 0)
                customer.AdviceContactDate = this.adviceTimePeriod_Combobox.SelectedValue.ToString();

            if (this.bestContactTime_ComboBox.SelectedIndex > 0)
                customer.BestContactDate = this.bestContactTime_ComboBox.SelectedValue.ToString();

            if (this.province_Combobox.SelectedIndex > 0)
                customer.Province = ((Province)this.province_Combobox.SelectedItem).ProvinceName.ToString();

            if (this.city_Combobox.SelectedIndex > 0)
                customer.City = ((City)this.city_Combobox.SelectedItem).CityName.ToString();

            if (this.custom_County.Visible)
            {
                customer.County = this.custom_County.Text.Trim();
            }
            else
            {
                if (this.county_Combobox.SelectedIndex > 0)
                    customer.County = ((County)this.county_Combobox.SelectedItem).CountyName.ToString();
            }
            customer.EventKey = EventKey.ToString();
            OfflineCustomerHelper.InsertCustomer(customer, EventKey);
            NewCustomerInfo info = new NewCustomerInfo();
            info.StartPosition = FormStartPosition.CenterParent;
            info.AfterContinueAddCustomer -= ContinueAddCustomer_Btn_Click;
            info.AfterContinueAddCustomer += ContinueAddCustomer_Btn_Click;

            info.AfterEndAddCustomer -= EndAddCustomer_Btn_Click;
            info.AfterEndAddCustomer += EndAddCustomer_Btn_Click;

            info.ShowDialog();

            UpdateCustomer uc = new UpdateCustomer((this.Owner as MainForm).CustomerListDataBind);
            uc(false);
        }

        //判断联系方式是否有相同
        private bool IsExisted(string type, string info)
        {
            bool result = false;
            List<Customer> rows = OfflineCustomerHelper.QueryCustomers(EventKey.ToString());
            for(int i = 0; i < rows.Count; i++)
            {
                if (info == rows[i].ContactInfo.ToString() && type == rows[i].ContactType)
                    result = true;
            }
            return result;
        }

        private void EndAddCustomer_Btn_Click(object sender, EventArgs e)
        {
            if (AfterCustomerClosed != null)
            {
                AfterCustomerClosed(this, e);
            }

            this.Close();
        }

        private void ContinueAddCustomer_Btn_Click(object sender, EventArgs e)
        {

            this.name_Text.Text = string.Empty;
            this.contactType_comBox.SelectedIndex = 0;
            this.contactInfo_Txt.Text = string.Empty;
            //this.career_ComboBox.SelectedIndex = 0;
            this.career_Text.Text = string.Empty;
            this.isHeared_Combobox.SelectedIndex = 0;
            this.isJoinEvent_ComboBox.SelectedIndex = 0;
            this.customerType_ComboBox.SelectedIndex = 0;
            bestContactTime_ComboBox.SelectedIndex = 0;
            adviceTimePeriod_Combobox.SelectedIndex = 0;
            useset_ComboBox.SelectedIndex = 0;
            ageRange_Combox.SelectedIndex = 0;
            //interest_ComBox.SelectedIndex = 0;
            //重置兴趣爱好复选框
            InterestingTopic_DressUp.Tag = "0";
            InterestingTopic_DressUp.Image = PinkBus.OfflineCustomer.Properties.Resources.unpick;
            InterestingTopic_FamilyTies.Tag = "0";
            InterestingTopic_FamilyTies.Image = PinkBus.OfflineCustomer.Properties.Resources.unpick;
            InterestingTopic_MakeUp.Tag = "0";
            InterestingTopic_MakeUp.Image = PinkBus.OfflineCustomer.Properties.Resources.unpick;
            InterestingTopic_SkinCare.Tag = "0";
            InterestingTopic_SkinCare.Image = PinkBus.OfflineCustomer.Properties.Resources.unpick;

            response_ComboBox.SelectedIndex = 0;

            this.province_Combobox.SelectedIndex = 0;
            if (city_Combobox.Items.Count > 0)
                this.city_Combobox.SelectedIndex = 0;
            if (county_Combobox.Items.Count > 0)
                this.county_Combobox.SelectedIndex = 0;

            this.message_Lable.Text = "标示 * 的为必填项！";
            //reset all filed's value
        }

        /// <summary>
        /// close current form 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void customerCancel_Btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void KeyPressNumber(object sender, KeyPressEventArgs e)
        {
            // base.KeyPressOnlyNum(sender, e);
            //联系方式 :电话号码
            if (this.contactType_comBox.SelectedIndex == 0 )
            {
                //设置最大长度
                this.contactInfo_Txt.MaxLength = 11;
                if (!((e.KeyChar >= '0' && e.KeyChar <= '9') || e.KeyChar == ' ') && e.KeyChar != '\b')//不输入输入除了数字之外的所有非法字符的判断
                {
                    e.Handled = true;
                }
            }
            //联系方式 QQ
            else if(this.contactType_comBox.SelectedIndex == 1)
            {
                //设置最大长度
                this.contactInfo_Txt.MaxLength = 14;
                if (!((e.KeyChar >= '0' && e.KeyChar <= '9') || e.KeyChar == ' ') && e.KeyChar != '\b')//不输入输入除了数字之外的所有非法字符的判断
                {
                    e.Handled = true;
                }
            }
            //其他联系方式
            else
                this.contactInfo_Txt.MaxLength = 40;

        }

        private void LabelsCheck_Click(object sender, EventArgs e)
        {
            Label lb = (sender as Label);
            if (lb.Tag.ToString() == "0")
            {

                lb.Image = PinkBus.OfflineCustomer.Properties.Resources.pick;
                lb.Tag = "1";
            }
            else
            {
                lb.Image = PinkBus.OfflineCustomer.Properties.Resources.unpick;
                lb.Tag = "0";
            }
        }

        #region Jason测试用，一键添加2000个新顾客
        //private void Add_2000_Click(object sender, EventArgs e)
        //{
        //    for (int i = 0; i < 2000; i++)
        //    {
        //        Customer customer = new Customer();
        //        customer.CustomerName = CreateRandomName();
        //        customer.CustomerPhone = CreateRandomPhone();
        //        customer.CustomerKey = Guid.NewGuid();
        //        customer.EventKey = EventKey.ToString();
        //        OfflineCustomerHelper.InsertCustomer(customer, EventKey);
        //    }
        //}

        //private static string CreateRandomPhone()
        //{
        //    StringBuilder sb = new StringBuilder();
        //    Random rd = new Random();
        //    sb.Append("139");
        //    for (int i = 0; i < 8; i++)
        //    {
        //        sb.Append(rd.Next(0, 9).ToString());
        //    }
        //    return sb.ToString();
        //}

        //private static string CreateRandomName()
        //{
        //    StringBuilder sb = new StringBuilder();
        //    Random rd = new Random();
        //    for (int j = 0; j < 6; j++)
        //    {
        //        sb.Append(Chars[rd.Next(0, Chars.Length)]);
        //    }
        //    return sb.ToString();
        //}

        //public static char[] Chars = { 'A', 'B', 'C', 'D', 'E',
        //    'F', 'G', 'H', 'R', 'J', 'K', 'L', 'M', 'N', 'O',
        //    'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y',
        //    'Z', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i',
        //    'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's',
        //    't', 'u', 'v', 'w', 'x', 'y', 'z' };
        #endregion


    }

    public class ComboBoxValue
    {
        public string Id { get; set; }
        public string Name { get; set; }

    }
}
