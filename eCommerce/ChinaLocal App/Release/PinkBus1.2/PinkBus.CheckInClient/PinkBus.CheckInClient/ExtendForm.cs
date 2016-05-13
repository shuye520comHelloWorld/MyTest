using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace PinkBus.CheckInClient
{
    public partial class ExtendForm : Form
    {
        public ExtendForm()
        {
            InitializeComponent();
            this.BackgroundImage = Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "\\Content\\ExtendBG.jpg");

        }


        public string EventTitle { get { return eventTitle.Text; } set { eventTitle.Text = value; } }

        public string CustomerTitle { get { return this.CustomerName.Text; } set { CustomerName.Text = value; } }
        private int ExtendFormWidth { get; set; }
        private int ExtendFormHeight { get; set; }

        public delegate void SetTextCallback();
        private Thread ToggleTitleThread = null;

        // private delegate void setEventTitle(string text);

        private void ExtendForm_Load(object sender, EventArgs e)
        {
            SetExtendFormSize();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);//解决闪烁
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);//解决闪烁 
            this.CustomerName.Hide();
            this.welcome.Hide();
            //eventTitle.Text = EventTitle;

            //eventTitle.Width = ExtendFormWidth / 10 * 8;
            eventTitle.Location = new System.Drawing.Point((ExtendFormWidth - eventTitle.Width - ExtendFormWidth / 10) / 2, ExtendFormHeight *18/100);
            //eventTitle.Width = 800;

            //CustomerName.Location = new System.Drawing.Point((ExtendFormWidth - CustomerName.Width) / 2, eventTitle.Location.Y + eventTitle.Height + 100);
            //this.welcome.Location = new System.Drawing.Point((ExtendFormWidth - CustomerName.Width) / 2 + (CustomerName.Width / 3) * 2, CustomerName.Location.Y + CustomerName.Height);

        }

        private void SetExtendFormSize()
        {
            ExtendFormWidth = Screen.AllScreens[1].Bounds.Width;
            ExtendFormHeight = Screen.AllScreens[1].Bounds.Height;

        }




        private void CustomerName_TextChanged(object sender, EventArgs e)
        {

            this.ToggleTitleThread = new Thread(new ThreadStart(() =>
            {
               
                this.CustomerName.Show();
                this.welcome.Show();
                Thread.Sleep(4000);

                this.CustomerName.Hide();
                this.welcome.Hide();
                ToggleTitleThread.Abort();
            }));
            this.ToggleTitleThread.Start();
        }

        private void eventTitle_Paint(object sender, PaintEventArgs e)
        {
            eventTitle.Location = new System.Drawing.Point((ExtendFormWidth - eventTitle.Width - ExtendFormWidth / 10) / 2, ExtendFormHeight *18/100);
           // CustomerName.Location = new System.Drawing.Point((ExtendFormWidth - CustomerName.Width) / 2, eventTitle.Location.Y + eventTitle.Height + 100);
           // this.welcome.Location = new System.Drawing.Point((ExtendFormWidth - CustomerName.Width) / 2 + (CustomerName.Width / 3) * 2, CustomerName.Location.Y + CustomerName.Height);

        }

        private void CustomerName_Paint(object sender, PaintEventArgs e)
        {
            CustomerName.Location = new System.Drawing.Point((ExtendFormWidth - CustomerName.Width) / 2, eventTitle.Location.Y + eventTitle.Height + 100);
            this.welcome.Location = new System.Drawing.Point((ExtendFormWidth - CustomerName.Width) / 2 + (CustomerName.Width / 2), CustomerName.Location.Y + CustomerName.Height + 30);

        }

        //public void customerNameChange(string name)
        //{
        //    CustomerName.Text = name;
        //    this.CustomerName.Show();
        //    this.welcome.Show();
        //    //Thread.Sleep(4000);
        //    this.ToggleTitleThread = new Thread(new ThreadStart(() =>
        //    {
        //        //SetTextCallback stc = new SetTextCallback(showCustomer);
        //        //this.Invoke(stc);
        //        this.CustomerName.Show();
        //        this.welcome.Show();
        //        Thread.Sleep(4000);

        //        this.CustomerName.Hide();
        //        this.welcome.Hide();
        //        ToggleTitleThread.Abort();
        //    }));
        //    //this.ToggleTitleThread.Start();

        //}
        //private void showCustomer()
        //{
        //    this.CustomerName.Show();
        //    this.welcome.Show();
        //    Thread.Sleep(4000);
        //    this.CustomerName.Hide();
        //    this.welcome.Hide();
        //}


    }
}
