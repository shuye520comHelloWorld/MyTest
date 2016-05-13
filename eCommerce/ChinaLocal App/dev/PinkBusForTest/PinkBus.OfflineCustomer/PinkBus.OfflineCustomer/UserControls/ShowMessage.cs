using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PinkBus.OfflineCustomer.UserControls
{
    public partial class ShowMessage : BaseForm
    {

        /// <summary>
        /// continue add customer button click handler
        /// </summary>
        public EventHandler AfterContinue;

        /// <summary>
        /// end add customer button cilck handler
        /// </summary>
        public EventHandler AfterEnd;

        public string Message { get; set; }

        public ShowMessage(string message)
            : base( )
        {
            InitializeComponent();
            SetClassLong(this.Handle, GCL_STYLE, GetClassLong(this.Handle, GCL_STYLE) | CS_DropSHADOW);
            Message=message;
            this.message_lable.Text = message;
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
          //  this.panel1.Focus();
        }

        private void lable1_MouseDown(object sender, MouseEventArgs e)
        {
            Drag_MouseDown(sender, e);
        }

        private void AssignCustomer_Load(object sender, EventArgs e)
        {
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);//解决闪烁
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);//解决闪烁
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void end_Btn_Click(object sender, EventArgs e)
        {
            if (AfterEnd != null)
                AfterEnd(sender, e);
            this.Close();
        }

        private void continue_Btn_Click(object sender, EventArgs e)
        {
            if (AfterContinue != null)
                AfterContinue(sender, e);
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

    }
}
