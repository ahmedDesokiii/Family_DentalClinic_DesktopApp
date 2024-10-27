using StemV5.Pages.View.Start;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IWshRuntimeLibrary;
using System.Reflection;

namespace StemV5
{
    public partial class StartLoading_FRM : Form
    {
        
        public StartLoading_FRM()
        {
            InitializeComponent();
            //string folder = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            //creatShortCut(folder);

            this.Timer_StartLoading_FRM.Start();
        }
        // Start Loading load design
        private void Timer_StartLoading_FRM_Tick(object sender, EventArgs e)
        {
            UsersLogin_FRM login = new UsersLogin_FRM();
            if (progressBar_StartLoading_FRM.Value == 100)
            {
                Timer_StartLoading_FRM.Stop();
                this.Hide();
                login.Show();
            }
            else
            { progressBar_StartLoading_FRM.Value += 2; }

        }

        //private void creatShortCut(string sav) {
        //    WshShellClass wshShell = new WshShellClass();
        //    string file = sav + "\\" + ProductName + ".lnk";
        //    IWshShortcut shortCut = (IWshShortcut)wshShell.CreateShortcut(file);
        //    shortCut.TargetPath = Application.ExecutablePath;
        //    shortCut.Save();
        //}
        private void StartLoading_FRM_Load(object sender, EventArgs e)
        {

        }

        
    }
}
