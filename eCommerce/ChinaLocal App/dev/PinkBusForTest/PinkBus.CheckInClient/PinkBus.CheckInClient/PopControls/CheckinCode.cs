using PinkBus.CheckInClient.DAL;
using PinkBus.CheckInClient.Entitys;
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
    public partial class CheckinCode : BaseForm
    {
        public CheckinCode():base()
        {
            //SetClassLong(this.Handle, GCL_STYLE, GetClassLong(this.Handle, GCL_STYLE) | CS_DropSHADOW);
            InitializeComponent();

        }

        private void checkincode_close_btn_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

      

        private void checkincode_submit_btn_Click(object sender, EventArgs e)
        {
            string err = "";
            Guid CustomerKey = Guid.Empty;
            string customerName = "";
            TicketType type=TicketType.Normal;
            bool res = EventDAL.PassCodeCheckin((this.Owner as MainForm).SelectEventKey, 
                this.checkincode_phone.Text, 
                this.checkincode_smstoken.Text,
                ref CustomerKey,
                ref customerName,
                ref type,
                out err);
           

            if (res)
            {
                if (type == TicketType.Normal) { customerName = "来宾 " + customerName; } else { customerName = "贵宾 " + customerName; }
                (this.Owner as MainForm).SelectCustomerKey = CustomerKey;
                (this.Owner as MainForm).DataGridViewRefresh();
                (this.Owner as MainForm).CustomerInfoShow(CustomerKey.ToString());
                if ((this.Owner as MainForm).extendForm != null)
                {
                    (this.Owner as MainForm).extendForm.CustomerTitle = customerName;
                }

                this.Close();
            }

            (this.Owner as MainForm).ShowMsgBox(err);
        }

        private void checkincode_title_panel_MouseDown(object sender, MouseEventArgs e)
        {
            Drag_MouseDown(sender, e);
        }

        private void checkincode_title_MouseDown(object sender, MouseEventArgs e)
        {
            Drag_MouseDown(sender, e);
        }

        private void CheckinCode_Load(object sender, EventArgs e)
        {
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);//解决闪烁
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);//解决闪烁 

        }


        private void KeyPressNumber(object sender, KeyPressEventArgs e)
        {
            KeyPressOnlyNum(sender, e);
        }
       
    }
}
