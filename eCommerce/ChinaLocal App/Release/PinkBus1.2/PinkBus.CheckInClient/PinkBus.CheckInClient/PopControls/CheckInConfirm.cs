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
    public partial class CheckInConfirm : Form
    {
        private CustomerTicket customerticket;
        private ConsultantTicket consultantticket;
        private int flag=0;
        public CheckInConfirm(CustomerTicket customer)
        {
            InitializeComponent();
            label4.Text = customer.CustomerName;
            label5.Text = customer.CustomerPhone;
            customerticket = customer;
            flag = 1;
        }

        public CheckInConfirm(ConsultantTicket consultant)
        {
            InitializeComponent();
            label4.Text = consultant.LastName+consultant.FirstName;
            label5.Text = consultant.PhoneNumber;
            consultantticket = consultant;
            flag = 2;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void MsgInfo(int msg)
        {
            MsgShow msgshow;
            switch (msg)
            {
                case 0:
                    msgshow = new MsgShow("提示", "签到成功！");
                    break;
                case 1:
                    msgshow = new MsgShow("提示", "该用户已作废,不能签到！");
                    break;
                case 2:
                    msgshow = new MsgShow("提示", "该用户今日已签到，请勿重复签到！");
                    break;
                default:
                    msgshow = new MsgShow("提示", "签到失败！");
                    break;
            }
            msgshow.Owner = this;
            msgshow.StartPosition = FormStartPosition.CenterParent;
            msgshow.ShowDialog();
            if (msgshow.DialogResult.ToString().Equals("Cancel"))
            {
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (flag == 1)
            {
                List<Ticket> tickets = EventDAL.QueryTickets(customerticket.CustomerKey.ToString());
                Ticket ticket = tickets.First();
                int msg =EventDAL.UpdateTicket(ticket);
                MsgInfo(msg);
            }
            else if (flag == 2)
            {
                int msg = EventDAL.InsertVolunteerCheckIn(consultantticket);
                MsgInfo(msg);
            }
            else {
                MsgInfo(3);
            }
        }
    }
}
