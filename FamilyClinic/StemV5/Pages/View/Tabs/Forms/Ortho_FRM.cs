using StemV5.Pages.Control;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StemV5.Pages.View.Tabs.Forms
{
    public partial class Ortho_FRM : Form
    {

       public string chComm = "";

         //Objects
         //
         AllPatients_FRM ap = new AllPatients_FRM("ortho_c");
        Class_Ortho ortho = new Class_Ortho();
        Calendar clndr = new Calendar();
       

        DateTime now = DateTime.Now;
        public Ortho_FRM()
        {
            InitializeComponent();
        }
        //Constructor for new 
        //
        public Ortho_FRM(string orType,int co)
        {
            InitializeComponent();
            ortho.orthoType = orType;
            txtCode.Text = co.ToString();
        }
        public Ortho_FRM(string orType, int code, string name, int age, string phone, string gender, string adress, string mh, string follow, string nextSess)
        {
            InitializeComponent();
            ortho.orthoType = orType;
            txtCode.Text = code.ToString();
            txtName.Text = name.Trim();
            txtAge.Text = age.ToString();
            txtPhone.Text = phone.Trim();
            //Gender
            if (gender.Contains("Male"))
            {
                rbMale.Checked = true;
                rbFemale.Checked = false;
            }
            else if (gender.Contains("Female"))
            {
                rbMale.Checked = false;
                rbFemale.Checked = true;
            }
            txtAdress.Text = adress.Trim();
            txtMH.Text = mh.Trim();
            if (follow.Contains("Normal"))
            {
                rbNormal.Checked = true;
                rbLongg.Checked = false;
            }
            if (follow.Contains("Long"))
            {
                rbNormal.Checked = false;
                rbLongg.Checked = true;
            }
            dateTimePicker1.Value = Convert.ToDateTime(nextSess);
        }


        //Methods
        //

        //Update & Display
        public void UpdateAndDisplay()
        {
            //Update Date ortho : new or basic  ... pressing  Save btn
            ortho.UpdateData_Ortho(patient_TDataGridView, txtCode.Text, txtName.Text.Trim(), txtAge.Text, ortho.GetRadioButtonSelected(rbMale, rbFemale).Trim(), txtPhone.Text.Trim(), txtAdress.Text.Trim(), ortho.SelectFollowType(rbNormal, rbLongg).Trim(), dateTimePicker1, txtAmoTotal.Text);
            //Display Sessions of  this Ortho of DB
            ortho.DisplayOldFinanceData(patient_TDataGridView, txtCode.Text);

            MessageBox.Show("  Orthodontics Info Saved ...  ! ", "Orthodontics Data!", MessageBoxButtons.OK);
            //DeActive Feilds Controls
            ortho.ActiveControlsBasic_Ortho(txtName, txtAge, txtPhone, rbMale, rbFemale, txtAdress, txtMH, rbNormal, rbLongg, dateTimePicker1, Color.Gainsboro, true);
            txtAmoPaid.ReadOnly = true; txtAmoPaid.BackColor = Color.Gainsboro;
            txtDiscount.ReadOnly = true; txtDiscount.BackColor = Color.Gainsboro;
            dateTimePicker1.Enabled = false; 
        }

        public void CalcOrthoAmounts()
        {

            int total =Convert.ToInt32( ortho.GetSelectedRecordCellData(patient_TDataGridView, 3));//next session
            int totalPaid = ortho.SumAmountRemaining(patient_TDataGridView, 4);
            int totalDisc = ortho.SumAmountRemaining(patient_TDataGridView, 5);

            txtAmoTotal.Text = total.ToString(); ;
            txtTotalRemain.Text = (total - (totalPaid + totalDisc)).ToString();
        }
        //Delete Old Ortho Finance$ Record
        //
        public void DeleteOldFinancesRecord()
        {
            int _total = Convert.ToInt32(patient_TDataGridView.Rows[0].Cells[3].Value);
            int _paid = ortho.SumAmountRemaining(patient_TDataGridView, 4);
            int _discount = ortho.SumAmountRemaining(patient_TDataGridView, 5);
            //delete session record
            ortho.DeleteRecord(patient_TDataGridView, ortho.DeleteRecSession(txtCode.Text));
            txtAmoTotal.Text = _total.ToString();
            //Clac Total Remaining Amount : total - (paid+discount)
            txtTotalRemain.Text = (_total - (_paid + _discount)).ToString();
            this.ActiveControl = txtAmoPaid;
            button12.Enabled = true;
        }
        private void DisplayDays()
        {
            clndr.month = now.Month;
            clndr.year = now.Year;
        }
        //Exit - icon
        private void PictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Ortho_FRM_Load(object sender, EventArgs e)
        {
            //display calendar data
            DisplayDays();
            label20.Text = clndr.GetDataOfDays(FLP_dayscontainer);
            //////////////////////
            ///
            chComm = txtMH.Text.Trim();
            ortho.orthoType = ortho.CheckedOrthoType();
            ortho.DisplayOldFinanceData(patient_TDataGridView, txtCode.Text);
            label9.Text = txtCode.Text;
            txtAmoPaid.ReadOnly = false; txtAmoPaid.BackColor = Color.White;
            txtDiscount.ReadOnly = false; txtDiscount.BackColor = Color.White;
            ortho.DisplayDGVImages(dataGridView2, "select orRay_Code As 'Code',orRay_Date As Date , orRay_Img As 'X-Ray' From OrthoRays_T Where ortho_Code = " + txtCode.Text + "order by orRay_Date desc ;");
            ortho.orthoType = ortho.CheckedOrthoType();
            if (ortho.orthoType == "new")
            {
                button1.Enabled = false;
                txtAmoTotal.ReadOnly = false; txtAmoTotal.BackColor = Color.White;dateTimePicker1.Enabled = true;
                this.ActiveControl = txtName;
            }
            else if (ortho.orthoType == "edit")
            {
                CalcOrthoAmounts();
                //DeActive Feilds Controls
                ortho.ActiveControlsBasic_Ortho(txtName, txtAge, txtPhone, rbMale, rbFemale, txtAdress, txtMH,  rbNormal, rbLongg,dateTimePicker1, Color.Gainsboro, true);
                this.ActiveControl = txtAmoPaid;
            }
        }

        private void BtnPrev_Click(object sender, EventArgs e)
        {
            //clear container
            FLP_dayscontainer.Controls.Clear();
            //decremint month to go to next month 
            if (clndr.month == 1) { clndr.month = 1; }
            else { clndr.month--; }
            label20.Text = clndr.GetDataOfDays(FLP_dayscontainer);
        }

        private void BtnNext_Click(object sender, EventArgs e)
        {
            //clear container
            FLP_dayscontainer.Controls.Clear();
            //incremint month to go to next month 
            if (clndr.month == 12) { clndr.month = 12; }
            else { clndr.month++; }
            label20.Text = clndr.GetDataOfDays(FLP_dayscontainer);

        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {
           
            ortho.gotoForm(ap, this);
        }

        private void Label6_Click(object sender, EventArgs e)
        {
            Home_FRM h = new Home_FRM();
            ortho.gotoForm(h, this);
        }

        private void Label3_Click(object sender, EventArgs e)
        {
            ortho.gotoForm(ap, this);
        }

        private void DateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
           // dataGridView1.Columns[1].
            ortho.DisplayAll(dataGridView1, ortho.MiniList(dateTimePicker1));

            groupBox2.Text = "List Ortho Patients  #  " + dataGridView1.Rows.Count;
        }

        private void TxtAmoPaid_KeyPress(object sender, KeyPressEventArgs e)
        {
            ortho.NumericOnly(sender, e,txtAmoPaid);
        }

        private void TxtDiscount_KeyPress(object sender, KeyPressEventArgs e)
        {
            ortho.NumericOnly(sender, e,txtDiscount);
        }

        private void TxtAmoPaid_TextChanged(object sender, EventArgs e)
        {
            txtAmoRemain.Text = ((Convert.ToInt32(txtTotalRemain.Text)) - ((Convert.ToInt32(txtAmoPaid.Text)) + (Convert.ToInt32(txtDiscount.Text)))).ToString();

        }

        private void TxtDiscount_TextChanged(object sender, EventArgs e)
        {
            txtAmoRemain.Text = ((Convert.ToInt32(txtTotalRemain.Text)) - ((Convert.ToInt32(txtAmoPaid.Text)) + (Convert.ToInt32(txtDiscount.Text)))).ToString();

        }
        //Edit btn
        private void Button15_Click(object sender, EventArgs e)
        {
            //Active Feilds Controls
            ortho.ActiveControlsBasic_Ortho(txtName, txtAge, txtPhone, rbMale, rbFemale, txtAdress, txtMH, rbNormal, rbLongg,dateTimePicker1, Color.White, false);
            this.ActiveControl = txtMH;
            
            txtAmoPaid.ReadOnly = false; txtAmoPaid.BackColor = Color.White;
            txtDiscount.ReadOnly = false; txtDiscount.BackColor = Color.White;
        }
        //Update || Save btn Ortho Date
        private void Button12_Click(object sender, EventArgs e)
        {
            if (txtAmoPaid.Text == "0" && txtDiscount.Text == "0" && (txtMH.Text == "" || txtMH.Text.Trim() == ortho.GetStudentFeild("Select orth_Comments From Orthodontics_T Where orth_Code = ", txtCode.Text, "orth_Comments").Trim()))
            { UpdateAndDisplay(); }
            else 
            { 
                // Add next Session Record
                ortho.AddToOldFinancesRecord(txtCode.Text, txtName.Text.Trim(), txtAge.Text, ortho.GetRadioButtonSelected(rbMale, rbFemale).Trim(), txtPhone.Text.Trim(), txtAdress.Text.Trim(), txtMH.Text.Trim(), ortho.SelectFollowType(rbNormal, rbLongg).Trim(), (ortho.GetSessionNewCode_Ortho(txtCode.Text)).ToString(), dateTimePicker2, dateTimePicker1, txtAmoTotal.Text, txtAmoTotal.Text, txtAmoPaid.Text, txtDiscount.Text);
                UpdateAndDisplay();
            }
                
                        
        }

        private void TxtAmoTotal_TextChanged(object sender, EventArgs e)
        {
            txtTotalRemain.Text = txtAmoTotal.Text;
           
        }

        private void TxtAmoTotal_KeyPress(object sender, KeyPressEventArgs e)
        {
            ortho.NumericOnly(sender, e, txtAmoTotal);
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            //new ortho
            Ortho_FRM ort = new Ortho_FRM("new", ortho.GetLastCode(ortho.que_newCode_Ortho, "orth_Code") + 1);
            ortho.gotoForm(ort, this);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (patient_TDataGridView.CurrentRow.Cells[0].Value.ToString() == "0".Trim())
            { MessageBox.Show("Attention Please .. \nCan't have Permission To Delete this Record [Old Data] !", "", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            else
            {
                //delete selected record
                DeleteOldFinancesRecord();
                //Display Sessions of  this Patient of DB
                ortho.DisplayOldFinanceData(patient_TDataGridView, txtCode.Text);
                // Reset Controls of Session
                ortho.ActiveControlsBasic_Ortho(txtName, txtAge, txtPhone, rbMale, rbFemale, txtAdress, txtMH, rbNormal, rbLongg, dateTimePicker1, Color.Gainsboro, true);

            }
        }
        private void Button8_Click(object sender, EventArgs e)
        {
            if (panelRays.Visible == false) { panelRays.Visible = true; }
            else { panelRays.Visible = false; }
        }

        private void PictureBox39_Click(object sender, EventArgs e)
        {
            textBox2.Text = ortho.BrowseImage(pictureBox39);
        }

        private void BtnTestImg_Click(object sender, EventArgs e)
        {
            ortho.InsertImageOrtho((ortho.GetLastCode(ortho.que_newCode_OrthoRay, "orRay_Code") + 1).ToString(), txtCode.Text, textBox2.Text.Trim(), dateTimePicker2, textBox1.Text.Trim(), ortho.CovertImageToByte(pictureBox39.Image, 70, 95), ortho.CovertImageToByte(pictureBox39.Image, 550, 600));
            ortho.DisplayDGVImages(dataGridView2, "select orRay_Code As 'Code',orRay_Date As Date , orRay_Img As 'X-Ray' From OrthoRays_T Where ortho_Code = " + txtCode.Text + " order by orRay_Date desc ;");

        }

        private void DataGridView2_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            panelXRay.Visible = true;
            ortho.DisplayDGVImagesOrtho(pictureBox38, "select orRay_SrcImg From OrthoRays_T Where ortho_Code = " + txtCode.Text + " And orRay_Code = " + dataGridView2.SelectedRows[0].Cells[0].Value.ToString() + ";");
            textBox3.Text = txtCode.Text;
            textBox5.Text = txtName.Text;
            textBox6.Text = dataGridView2.SelectedRows[0].Cells[0].Value.ToString();
            dateTimePicker4.Value = Convert.ToDateTime(dataGridView2.SelectedRows[0].Cells[1].Value);
            textBox4.Text = ortho.GetStudentFeild("Select orRay_Notes From OrthoRays_T Where orRay_Code = ", textBox6.Text, "orRay_Notes");
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            if (dataGridView2.Rows.Count == 0)
            { MessageBox.Show("Attention .. No Records Selected ... !", "Select Record", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            else
            {
                ortho.DeleteRecord(dataGridView2, "DELETE FROM OrthoRays_T WHERE ortho_Code = " + txtCode.Text + " AND orRay_Code = ' ");
            }
        }

        private void PictureBox40_Click(object sender, EventArgs e)
        {
            panelXRay.Visible = false;
        }
    }
}
