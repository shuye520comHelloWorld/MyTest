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
    public partial class AssignCustomer : BaseForm
    {
        public AssignCustomer()
            : base()
        {
            InitializeComponent();
            SetClassLong(this.Handle, GCL_STYLE, GetClassLong(this.Handle, GCL_STYLE) | CS_DropSHADOW);
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            this.panel1.Focus();
        }

        private void lable1_MouseDown(object sender, MouseEventArgs e)
        {
            Drag_MouseDown(sender, e);
        }

        private void AssignCustomer_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
