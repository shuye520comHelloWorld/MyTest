using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PinkBus.CheckInClient.PartialControls
{
    public partial class VolunteerCheckin : UserControl
    {
        public VolunteerCheckin()
        {
            InitializeComponent();
        }

        public string Title { set { volunteerexcel_title.Text = value; } }


        private void volunteerexcel_title_Click(object sender, EventArgs e)
        {
            this.Volunteercheckin_ratio.Checked = true;
            
        }

        private void Volunteercheckin_ratio_CheckedChanged(object sender, EventArgs e)
        {
            MessageBox.Show(this.Tag.ToString());

        }



       
    }
}
