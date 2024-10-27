using StemV5.Pages.View.Tabs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StemV5.Pages.View.Start
{
    public partial class UsersLogin_FRM : Form
    {
       public  void resetLoginFRM() {
            txt_UserLogin.Text = "User Name";
            txt_pswLogin.Text = "Password";
            txt_UserLogin.ForeColor = Color.LightGray;
            txt_pswLogin.ForeColor = Color.LightGray;
            txt_pswLogin.PasswordChar = '\0';
            txt_UserLogin.Focus();
        }
        // login pic circle 
        public UsersLogin_FRM()
        {
            InitializeComponent();
        }
        private void txt_UserLogin_TextChanged(object sender, EventArgs e) {txt_UserLogin.ForeColor = Color.Tomato;}
        private void txt_pswLogin_TextChanged(object sender, EventArgs e){txt_pswLogin.ForeColor = Color.Tomato;txt_pswLogin.PasswordChar= '*' ; }
        private void closedLogin_FRM_PIC_Click(object sender, EventArgs e){this.Close();}
       
        private void button1_Click(object sender, EventArgs e)
        {
            Home_FRM home = new Home_FRM();
            if (txt_UserLogin.Text == "desokii" && txt_pswLogin.Text == "1234")
            {
                home.Show();
                this.Hide();
            }
            else if (txt_UserLogin.Text == "Dent" && txt_pswLogin.Text == "yayaroma")
            {
                home.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("invisable User Name Or Password ... !", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                resetLoginFRM();
            }

        }
        private void Txt_UserLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                this.SelectNextControl(txt_pswLogin, true, true, true, true);
            }
        }

        private void Chk_showPsw_CheckedChanged(object sender, EventArgs e)
        {
            txt_pswLogin.PasswordChar = chk_showPsw.Checked ? '\0' : '*';
        }

        private void Linklbl_LoginClear_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            resetLoginFRM();
        }

        private void ClosedLogin_FRM_PIC_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
