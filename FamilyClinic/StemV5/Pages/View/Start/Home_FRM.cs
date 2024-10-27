using StemV5.Pages.Control;
using StemV5.Pages.View.Start;
using StemV5.Pages.View.Tabs.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StemV5.Pages.View.Tabs
{
    
    public partial class Home_FRM : Form
    {
        Class_Patient patient = new Class_Patient();
        public Home_FRM()
        {
            InitializeComponent();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AllPatients_FRM apf = new AllPatients_FRM("patient_c");
            patient.gotoForm(apf, this);  
        }
        private void pictureBox5_Click(object sender, EventArgs e)
        {
            AllPatients_FRM apf = new AllPatients_FRM("patient_c");
            patient.gotoForm(apf, this);
        }
       
        private void linkLabel7_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AllPatients_FRM apf = new AllPatients_FRM("ortho_c");
            patient.gotoForm(apf, this);
        }
        private void pictureBox11_Click(object sender, EventArgs e)
        {
            AllPatients_FRM apf = new AllPatients_FRM("ortho_c");
            patient.gotoForm(apf, this);
        }

        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Finances_FRM ff = new Finances_FRM();
            patient.gotoForm(ff, this);
        }

        private void PictureBox10_Click(object sender, EventArgs e)
        {
            Finances_FRM ff = new Finances_FRM();
            patient.gotoForm(ff, this);
        }
    }
}
