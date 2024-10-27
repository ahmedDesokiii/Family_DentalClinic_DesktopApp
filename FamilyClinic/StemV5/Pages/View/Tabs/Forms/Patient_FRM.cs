using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.Sql;
using StemV5.Pages.Control;
using System.IO;

namespace StemV5.Pages.View.Tabs
{
    public partial class Patient_FRM : Form
    {
        int diagonIndex = -1;
        int re = 0; 
        //Objects ... 
        //
        Home_FRM h = new Home_FRM();
        AllPatients_FRM aa = new AllPatients_FRM("patient_c");
        Class_Patient patient = new Class_Patient();
       
        public Patient_FRM()
        {
            InitializeComponent();
        }
        // Constructor For New Patient
        //
        public Patient_FRM(string paType,int c)
        {
            InitializeComponent();
            patient.patientType = paType;
            txtCode.Text = c.ToString();
        }
        // Constructor For Basic Patient
        //
        public Patient_FRM(string paType, int code, string name, int age, string phone, string gender, string adress,string mh,string follow,string nextSess)
        {
            InitializeComponent();
            patient.patientType = paType;
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
                rbLong.Checked = false;
            }
            if (follow.Contains("Long"))
            {
                rbNormal.Checked = false;
                rbLong.Checked = true;
            }
            dateTimePicker1.Value = Convert.ToDateTime(nextSess);
            //txtDD.Text = diadd;
        }
        //Calc AccountantsOFPatients
        //
        public void CalcPatientAmounts()
        {
            int totalReq = patient.SumAmountRemaining(patient_TDataGridView, 3);
            int totalPaid = patient.SumAmountRemaining(patient_TDataGridView, 4);
            int totalDisc = patient.SumAmountRemaining(patient_TDataGridView, 5);
            int totalRemi = patient.SumAmountRemaining(patient_TDataGridView, 6);
            txtTTReq.Text = totalReq.ToString();
            txtTTPaid.Text = totalPaid.ToString();
            txtTTDiscount.Text = totalDisc.ToString();
            txtTTRemain.Text = totalRemi.ToString();

            txtAmoRequir.Text = (Convert.ToInt32(textBox12.Text) + Convert.ToInt32(txtTTRemain.Text)).ToString(); // total required
        }
        // TOOTH GRAPH
        //
        public void toothSelected(ComboBox cm, string ch, PictureBox pc)
        {
            if (cm.SelectedItem == ch)
            {
                p1.Visible = false; p2.Visible = false; p3.Visible = false; p4.Visible = false; p5.Visible = false; p6.Visible = false; p7.Visible = false; p8.Visible = false; p9.Visible = false; p10.Visible = false;
                p11.Visible = false; p12.Visible = false; p13.Visible = false; p14.Visible = false; p15.Visible = false; p16.Visible = false; p17.Visible = false; p18.Visible = false; p19.Visible = false; p20.Visible = false;
                p21.Visible = false; p22.Visible = false; p23.Visible = false; p24.Visible = false; p25.Visible = false; p26.Visible = false; p27.Visible = false; p28.Visible = false; p29.Visible = false; p30.Visible = false; p31.Visible = false; p32.Visible = false;
                pc.Visible = true;
            }
        }
        public void Readtooth(PictureBox pc1, PictureBox pc2, PictureBox pc3, PictureBox pc4, PictureBox pc5, PictureBox pc6, PictureBox pc7, PictureBox pc8, PictureBox pc9, PictureBox pc10, PictureBox pc11, PictureBox pc12
                                       , PictureBox pc13, PictureBox pc14, PictureBox pc15, PictureBox pc16, PictureBox pc17, PictureBox pc18, PictureBox pc19, PictureBox pc20, PictureBox pc21, PictureBox pc22, PictureBox pc23, PictureBox pc24
                                        , PictureBox pc25, PictureBox pc26, PictureBox pc27, PictureBox pc28, PictureBox pc29, PictureBox pc30, PictureBox pc31, PictureBox pc32)
        {
            //Uper Right
            toothSelected(comboBox5, "8", p1); toothSelected(comboBox5, "7", p2); toothSelected(comboBox5, "6", p3); toothSelected(comboBox5, "5", p4); toothSelected(comboBox5, "4", p5); toothSelected(comboBox5, "3", p6); toothSelected(comboBox5, "2", p7); toothSelected(comboBox5, "1", p8);
            //Uper Left
            toothSelected(comboBox3, "1", p9); toothSelected(comboBox3, "2", p10); toothSelected(comboBox3, "3", p11); toothSelected(comboBox3, "4", p12); toothSelected(comboBox3, "5", p13); toothSelected(comboBox3, "6", p14); toothSelected(comboBox3, "7", p15); toothSelected(comboBox3, "8", p16);
            //Lower Left
            toothSelected(comboBox6, "8", p17); toothSelected(comboBox6, "7", p18); toothSelected(comboBox6, "6", p19); toothSelected(comboBox6, "5", p20); toothSelected(comboBox6, "4", p21); toothSelected(comboBox6, "3", p22); toothSelected(comboBox6, "2", p23); toothSelected(comboBox6, "1", p24);
            //Lower Right
            toothSelected(comboBox7, "1", p25); toothSelected(comboBox7, "2", p26); toothSelected(comboBox7, "3", p27); toothSelected(comboBox7, "4", p28); toothSelected(comboBox7, "5", p29); toothSelected(comboBox7, "6", p30); toothSelected(comboBox7, "7", p31); toothSelected(comboBox7, "8", p32);
        }
 
        //Display Data - loading page
        //
        private void NewPatient_Load(object sender, EventArgs e)
        {
            patient.GetDiagonsisDate(comboBox2);
            comboBox2.SelectedIndex = -1;textBox12.Text = "0";
            patient.patientType = patient.CheckedPatientType();
            patient.DisplayOldComplainsData(patient_TDataGridView, txtCode.Text);
            patient.DisplayDGVImages(dataGridView2, "select paRay_Code As 'Code',paRay_Date As Date , paRay_Img As 'X-Ray' From PatientRays_T Where patientt_Code = " + txtCode.Text + "order by paRay_Date desc ;");
            CalcPatientAmounts();
            if (patient.patientType == "new")
            {
                button1.Enabled = false;
                this.ActiveControl = txtName;
                label9.Text = txtCode.Text;
            }
            else if (patient.patientType == "edit")
            {
                label9.Text = txtCode.Text;
                //DeActive Feilds Controls
                patient.ActiveControlsBasic_Patient(txtName, txtAge, txtPhone, rbMale, rbFemale, txtAdress, txtMH, rbNormal,rbLong,txtDD, Color.Gainsboro, true);
                this.ActiveControl = comboBox2;
            }
        }
        // Save Btn - Update Data[Basic] & Insert Data [New]
        //
        private void button12_Click(object sender, EventArgs e)
        {
            patient.patientType = patient.CheckedPatientType();
            if( patient_TDataGridView.Rows.Count <= 0)
            {
                MessageBox.Show("Attention .. Check Diagonsis Data , Add Diagonsis... !", "Chief Complain Missing", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.ActiveControl = comboBox2;
            }
            else
            {
                //Update Date Patient : new or basic
                patient.UpdateData_Patient(patient_TDataGridView, txtCode.Text, txtName.Text.Trim(), txtAge.Text, patient.GetRadioButtonSelected(rbMale, rbFemale).Trim(), txtPhone.Text.Trim(), txtAdress.Text.Trim(), patient.SelectFollowType(rbNormal, rbLong).Trim(), dateTimePicker1);
                MessageBox.Show("  Patient Info Done ...  ! ", "Done!", MessageBoxButtons.OK);
                //DeActive Feilds Controls
                patient.ActiveControlsBasic_Patient(txtName, txtAge, txtPhone, rbMale, rbFemale, txtAdress, txtMH, rbNormal, rbLong,txtDD, Color.Gainsboro, true);
            }
        }
        //Calc Remaining Amount - Event  #TextBox TextChanged
        //
        private void textBox21_TextChanged(object sender, EventArgs e)
        {
            txtAmoRemain.Text = (Convert.ToInt32(txtAmoRequir.Text) - (Convert.ToInt32(txtAmoPaid.Text) + Convert.ToInt32(txtDiscount.Text))).ToString();
        }
        private void textBox20_TextChanged(object sender, EventArgs e)
        {
            if (txtAmoPaid.Text == "") { txtAmoPaid.Text = "0"; }
            txtAmoRemain.Text = (Convert.ToInt32(txtAmoRequir.Text) - (Convert.ToInt32(txtAmoPaid.Text) + Convert.ToInt32(txtDiscount.Text))).ToString();
        }
        private void textBox19_TextChanged(object sender, EventArgs e)
        {
            if (txtDiscount.Text == "") { txtDiscount.Text = "0"; }
            txtAmoRemain.Text = (Convert.ToInt32(txtAmoRequir.Text) - (Convert.ToInt32(txtAmoPaid.Text) + Convert.ToInt32(txtDiscount.Text))).ToString();
        }
        private void comboBox2_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            txtDD.BackColor = Color.White; txtDD.ReadOnly = false;
            for (int i = 0; i < comboBox2.Items.Count; i++)
            {
                textBox12.Text = patient.CalcDiagonsisCost(comboBox2, i).ToString();
            }
            patient.DisplayOldComplainsData(patient_TDataGridView, txtCode.Text);
            CalcPatientAmounts();
        }
        private void textBox12_TextChanged(object sender, EventArgs e)
        {
            txtAmoRequir.Text = (Convert.ToInt32(textBox12.Text) + Convert.ToInt32(txtTTRemain.Text)).ToString();
            txtAmoRemain.Text = (Convert.ToInt32(txtAmoRequir.Text) - (Convert.ToInt32(txtAmoPaid.Text) + Convert.ToInt32(txtDiscount.Text))).ToString();
        }
        //Tooth Gragh in controls
        //
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            Readtooth(p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p17, p28, p29, p30, p31, p32);
            comboBox5.Visible = false;
            comboBox6.Visible = false;
            comboBox7.Visible = false;
        }
        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            Readtooth(p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p17, p28, p29, p30, p31, p32);
            comboBox3.Visible = false;
            comboBox6.Visible = false;
            comboBox7.Visible = false;

        }
        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            Readtooth(p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p17, p28, p29, p30, p31, p32);
            comboBox5.Visible = false;
            comboBox3.Visible = false;
            comboBox7.Visible = false;
        }
        private void comboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
            Readtooth(p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p17, p28, p29, p30, p31, p32);
            comboBox5.Visible = false;
            comboBox6.Visible = false;
            comboBox3.Visible = false;
        }
        //Master Edit - Btn
        //
        private void button15_Click(object sender, EventArgs e)
        {
            // active controls patient data &  diagonas cost
           patient.ActiveControlsBasic_Patient(txtName, txtAge, txtPhone, rbMale, rbFemale, txtAdress, txtMH, rbNormal, rbLong,txtDD, Color.White, false);
            button12.Enabled = true;
           this.ActiveControl = txtName;
        }
        //Home - sub link
        //
        private void label6_Click(object sender, EventArgs e)
        {
            patient.gotoForm(h, this);
        }
        //All Patients - sub link
        //
        private void label3_Click(object sender, EventArgs e)
        {
            patient.gotoForm(aa,this);
        }
        // New Patient - btn
        //
        private void Button3_Click(object sender, EventArgs e)
        {
            //new  patient
            Patient_FRM np = new Patient_FRM("new", patient.GetLastCode(patient.que_newCode_Patient, "patient_Code") + 1);
            patient.gotoForm(np, this);
        }
        // Delete Session Record To DataBase
        //
        private void Button1_Click(object sender, EventArgs e)
        {

            if (patient_TDataGridView.CurrentRow.Cells[0].Value.ToString() == "0".Trim())
                { MessageBox.Show("Attention Please .. \nCan't have Permission To Delete this Record [Old Data] !", "", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                else
                {
                    //delete session record
                    patient.DeleteRecord(patient_TDataGridView, patient.DeleteRecSession(txtCode.Text));
                    //Display Sessions of  this Patient of DB
                    patient.DisplayOldComplainsData(patient_TDataGridView, txtCode.Text);
                    CalcPatientAmounts();
                    // Reset Controls of Session
                    patient.ResetControlsForSession(comboBox2, comboBox3, comboBox5, comboBox6, comboBox7, txtAmoRequir, txtAmoPaid, txtDiscount, txtAmoRemain, txtDD, textBox12);
                    this.ActiveControl = comboBox2;
                 }
        }
        // ADD Session Record To DataBase
        //
        private void Button4_Click(object sender, EventArgs e)
        {
            
            panelDia.Enabled = false;
            if (textBox12.Text == "0" && txtAmoPaid.Text == "0" && txtDD.Text == "")
            { MessageBox.Show(" Attenttion .. No Data inserted to patient ... !", "No Data Inserted", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            else
            {
                //add new session to DB
                patient.AddOldComplainsData(txtCode.Text, txtName.Text.Trim(), txtAge.Text, patient.GetRadioButtonSelected(rbMale, rbFemale).Trim(), txtPhone.Text.Trim(), txtAdress.Text.Trim(), txtMH.Text.Trim(), (patient.GetSessionNewCode(txtCode.Text)).ToString(), dateTimePicker2, dateTimePicker1, comboBox2.Text.ToString().Trim(), txtDD.Text.Trim(), textBox12.Text, patient.SelectedTeeth(comboBox3, comboBox5, comboBox6, comboBox7).Trim(), patient.SelectFollowType(rbNormal, rbLong).Trim(), textBox12.Text, txtAmoPaid.Text, txtDiscount.Text);
                comboBox2.SelectedIndex = -1;
                //Display Sessions of  this Patient of DB
                patient.DisplayOldComplainsData(patient_TDataGridView, txtCode.Text);
                CalcPatientAmounts();
                // Reset Controls of Session
                patient.ResetControlsForSession(comboBox2, comboBox3, comboBox5, comboBox6, comboBox7, txtAmoRequir, txtAmoPaid, txtDiscount, txtAmoRemain, txtDD, textBox12);

                this.ActiveControl = comboBox2;
            }
        }
        // Update Diagonsis Cost - Button
        //
        private void Button2_Click(object sender, EventArgs e)
        {
            if (comboBox2.DropDownStyle == ComboBoxStyle.DropDown) // new
            {
                // New Diagonas Date .
                patient.NewDiagonsis(textBox12.Text, (patient.GetLastCode("select Top 1(dia_Code) From Diagonsis_T Order by dia_Code desc; ", "dia_Code")+1), comboBox2.Text.Trim());
                textBox12.ReadOnly = true; textBox12.BackColor = Color.Gainsboro;
                comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
                patient.GetDiagonsisDate(comboBox2);
                comboBox2.SelectedIndex = comboBox2.Items.Count - 1;
                this.ActiveControl = txtDD;
            }
            else if (comboBox2.DropDownStyle == ComboBoxStyle.DropDownList)
            {
                // update diagosis cost
                if (comboBox2.SelectedIndex == -1)
                // // Handle Selected Item Diagonas Cost
                {
                    MessageBox.Show("Please .. Select Diagonsis TTT !", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.ActiveControl = comboBox2;
                }
                else
                {
                    re = patient_TDataGridView.Rows.Count;
                    // Update Diagonas Cost .
                    patient.UpateDiagonsisCost(textBox12.Text, diagonIndex);
                    txtAmoRequir.Text = (Convert.ToInt32(textBox12.Text) + Convert.ToInt32(txtTTRemain.Text)).ToString();
                }
                textBox12.ReadOnly = true; textBox12.BackColor = Color.Gainsboro;
            }
            panelDia.Enabled = false;
        }
        // Close - icon
        //
        private void PictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        // Home - icon
        //
        private void PictureBox1_Click(object sender, EventArgs e)
        {
            patient.gotoForm(aa,this);
        }
       
        //Handle .. Inputs Numeric Only ...
        //
        private void TxtAge_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtAge.Text == "") { txtAge.Text = "1"; }
            patient.NumericOnly(sender, e,txtAge);
        }
        private void TxtAmoPaid_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtAmoPaid.Text == null) { txtAmoPaid.Text = "0"; }
            patient.NumericOnly(sender, e, txtAmoPaid);
        }
        private void TxtDiscount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtDiscount.Text == "") { txtDiscount.Text = "0"; }
            patient.NumericOnly(sender, e,txtDiscount);
        }
        private void TextBox12_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (textBox12.Text == "") { textBox12.Text = "0"; }
            patient.NumericOnly(sender, e,textBox12);
        }

        private void DateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            patient.DisplayAll(dataGridView1, patient.MiniList(dateTimePicker1));
            groupBox3.Text = "List Patients  #  " + dataGridView1.Rows.Count;
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            if (patient.ActiveDiagonsisControls(comboBox2, textBox12, Color.White, false))
            { diagonIndex = comboBox2.SelectedIndex; this.ActiveControl = textBox12; }
            else { MessageBox.Show("Please .. Select Diagonsis TTT !", "", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex == -1)
            { MessageBox.Show("Please .. Select Diagonsis TTT !", "", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            else
            {
                patient.DeleteDiagonsis(comboBox2,panelDia);
                patient.GetDiagonsisDate(comboBox2);
                comboBox2.SelectedIndex = -1; comboBox2.SelectedItem = ""; textBox12.Text = "0";
            }
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            comboBox2.DropDownStyle = ComboBoxStyle.DropDown;
            textBox12.ReadOnly = false;textBox12.BackColor = Color.White;textBox12.Text = "0";
            this.ActiveControl = comboBox2;
        }

       

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            panelDia.Enabled = true;
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            if (panelRays.Visible == false) { panelRays.Visible = true; }
            else { panelRays.Visible = false; }
        }

        private void PictureBox39_Click(object sender, EventArgs e)
        {
            textBox2.Text = patient.BrowseImage(pictureBox39);
        }

        private void PictureBox40_Click(object sender, EventArgs e)
        {
            panelXRay.Visible = false;
        }

        private void BtnTestImg_Click(object sender, EventArgs e)
        {
            patient. InsertImage((patient.GetLastCode(patient.que_newCode_PatientRay, "paRay_Code")+1).ToString(),txtCode.Text,textBox2.Text.Trim(),dateTimePicker2,textBox1.Text.Trim(), patient.CovertImageToByte(pictureBox39.Image,70,95), patient.CovertImageToByte(pictureBox39.Image,550,600));
            patient.DisplayDGVImages(dataGridView2, "select paRay_Code As 'Code',paRay_Date As Date , paRay_Img As 'X-Ray' From PatientRays_T Where patientt_Code = "+txtCode.Text+" order by paRay_Date desc ;");
            
        }

        private void DataGridView2_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            panelXRay.Visible = true;
            patient.DisplayDGVImages(pictureBox38, "select paRay_SrcImg From PatientRays_T Where patientt_Code = " + txtCode.Text + " And paRay_Code = " + dataGridView2.SelectedRows[0].Cells[0].Value.ToString() + ";");
            textBox3.Text = txtCode.Text;
            textBox5.Text = txtName.Text;
            textBox6.Text = dataGridView2.SelectedRows[0].Cells[0].Value.ToString();
            dateTimePicker4.Value = Convert.ToDateTime(dataGridView2.SelectedRows[0].Cells[1].Value);
            textBox4.Text = patient.GetStudentFeild("Select paRay_Notes From PatientRays_T Where paRay_Code = ", textBox6.Text, "paRay_Notes");
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            if (dataGridView2.Rows.Count == 0)
            { MessageBox.Show("Attention .. No Records Selected ... !", "Select Record", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            else
            {
                patient.DeleteRecord(dataGridView2 , "DELETE FROM PatientRays_T WHERE patientt_Code = "+txtCode.Text+" AND paRay_Code = ' ");
            }
        }
    }
}
