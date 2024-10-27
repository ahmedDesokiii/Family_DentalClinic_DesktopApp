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
using StemV5.Pages.View.Tabs.Forms;

namespace StemV5.Pages.View.Tabs
{
    public partial class AllPatients_FRM : Form
    {
      // Objects
      //
        Class_Patient patient = new Class_Patient();
        Class_Ortho ortho = new Class_Ortho();
        Home_FRM h = new Home_FRM();

       //Constructors
       //
        public AllPatients_FRM()
        {
            InitializeComponent();
        }
        //Constructor [All Patients] Or [All Ortho] passing Type
        //
        public AllPatients_FRM(string pp)
        {
            patient.check = pp;
            InitializeComponent();
        }

        //GoTo Basic Patient [edit]
        //
        public void GetBasictData_Patient()
        {
           
            Patient_FRM st = new Patient_FRM("edit",
                       (Convert.ToInt32(patient.GetSelectedRecordCellData(patient_TDataGridView, 0))),//code
                       (patient.GetSelectedRecordCellData(patient_TDataGridView, 1)),//Name
                       (Convert.ToInt32(patient.GetSelectedRecordCellData(patient_TDataGridView, 2))),//age
                       (patient.GetSelectedRecordCellData(patient_TDataGridView, 4)),//Phone
                      (patient.GetStudentFeild("Select patient_Gender From Patient_T Where patient_Code = ", patient.GetSelectedRecordCellData(patient_TDataGridView, 0), "patient_Gender")),// Gender
                       (patient.GetSelectedRecordCellData(patient_TDataGridView, 5)),//Adress
                      (patient.GetStudentFeild("Select patient_MedicalHistory From Patient_T Where patient_Code = ", patient.GetSelectedRecordCellData(patient_TDataGridView, 0), "patient_MedicalHistory")),// MH
                      (patient.GetStudentFeild("Select patient_Follow From Patient_T Where patient_Code = ", patient.GetSelectedRecordCellData(patient_TDataGridView, 0), "patient_Follow")),// Follow

                      (patient.GetSelectedRecordCellData(patient_TDataGridView, 7)) //next session
                      //(patient.GetStudentFeild("Select patient_DiagonsisDetails From Patient_T Where patient_Code = ", patient.GetSelectedRecordCellData(patient_TDataGridView, 0), "patient_DiagnosisDetails"))// DD
                      );
            patient.gotoForm(st, this);
        }

        //GoTo Basic Ortho [edit]
        //
        public void GetBasictData_Ortho()
        {
            
            Ortho_FRM st = new Ortho_FRM("edit",
                       (Convert.ToInt32(ortho.GetSelectedRecordCellData(patient_TDataGridView, 0))),//code
                       (ortho.GetSelectedRecordCellData(patient_TDataGridView, 1)),//Name
                       (Convert.ToInt32(ortho.GetSelectedRecordCellData(patient_TDataGridView, 2))),//age
                       (ortho.GetSelectedRecordCellData(patient_TDataGridView, 4)),//Phone
                      (ortho.GetStudentFeild("Select orth_Gender From Orthodontics_T Where orth_Code = ", ortho.GetSelectedRecordCellData(patient_TDataGridView, 0), "orth_Gender")),// Gender
                       (ortho.GetSelectedRecordCellData(patient_TDataGridView, 5)),//Adress
                      (ortho.GetStudentFeild("Select orth_Comments From Orthodontics_T Where orth_Code = ", ortho.GetSelectedRecordCellData(patient_TDataGridView, 0), "orth_Comments")),// comments
                      (ortho.GetStudentFeild("Select orth_Follow From Orthodontics_T Where orth_Code = ", ortho.GetSelectedRecordCellData(patient_TDataGridView, 0), "orth_Follow")),// Follow
                      (ortho.GetSelectedRecordCellData(patient_TDataGridView, 7))//next session
                      );
            ortho.gotoForm(st, this);
        }

        //Display All Records [All Patients] Or [All Ortho] passing Type
        //
        private void AllPatients_FRM_Load(object sender, EventArgs e)
        {
            //patient.HandleButtons(patient_TDataGridView,button15,button2);
            this.ActiveControl = textBox2;
            if (patient.check == "patient_c")
            {
                label3.Text = "All Patients";
                //display all patients
                patient.DisplayAll(patient_TDataGridView, patient.GetSortedDataByDate(dateTimePicker2));
            }
            else if (patient.check == "ortho_c")
            {
                label3.Text = "All Orthodontics";
                //display all ortho
                patient.DisplayAll(patient_TDataGridView, ortho.GetSortedDataByDate(dateTimePicker2));
            }
            else { MessageBox.Show(patient.check); }
            //Follow Up Color [Long]
            foreach (DataGridViewRow row in patient_TDataGridView.Rows)
            {
                if (row.Cells[6].Value.ToString() == "Long") { row.DefaultCellStyle.BackColor = Color.LightSalmon; }
                else { row.DefaultCellStyle.BackColor = Color.White; }
            }
            //count
            linkLabel10.Text = (patient_TDataGridView.Rows.Count).ToString();
            }
        // Search - Button
        //
        private void button1_Click(object sender, EventArgs e)
        {
            //patient.HandleButtons(patient_TDataGridView, button15, button2);
            if (patient_TDataGridView.Rows.Count == 0) { button15.Enabled = false; button2.Enabled = false; }
            else { button15.Enabled = true; button2.Enabled = true; }
            
            if (patient.check == "patient_c")
            {
                //search patients
                patient.SearchRecord(patient_TDataGridView, patient.SearchByName_Patient(textBox2.Text,dateTimePicker2));
                //Follow Up Color [Long]
                foreach (DataGridViewRow row in patient_TDataGridView.Rows)
                {
                    if (row.Cells[6].Value.ToString() == "Long") { row.DefaultCellStyle.BackColor = Color.LightSalmon; }
                    else { row.DefaultCellStyle.BackColor = Color.White; }
                }
            }
            if (patient.check == "ortho_c")
            {
                //patient.HandleButtons(patient_TDataGridView, button15, button2);
                //search ortho
                patient.SearchRecord(patient_TDataGridView, ortho.SearchByName_Ortho(textBox2.Text, dateTimePicker2));
                //Follow Up Color [Long]
                foreach (DataGridViewRow row in patient_TDataGridView.Rows)
                {
                    if (row.Cells[6].Value.ToString() == "Long") { row.DefaultCellStyle.BackColor = Color.LightSalmon; }
                    else { row.DefaultCellStyle.BackColor = Color.White; }
                }
            }
            //count
            linkLabel10.Text = (patient_TDataGridView.Rows.Count).ToString();
        }
        //Search - TextBox & DateTime
        //
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
                if (patient.check == "patient_c")
                {
                   // patient.HandleButtons(patient_TDataGridView, button15, button2);
                    //search patients
                    patient.SearchRecord(patient_TDataGridView, patient.SearchByName_Patient(textBox2.Text, dateTimePicker2));
                //Follow Up Color [Long]
                foreach (DataGridViewRow row in patient_TDataGridView.Rows)
                {
                    if (row.Cells[6].Value.ToString() == "Long") { row.DefaultCellStyle.BackColor = Color.LightSalmon; }
                    else { row.DefaultCellStyle.BackColor = Color.White; }
                }
                }
                if (patient.check == "ortho_c")
                {
                   // patient.HandleButtons(patient_TDataGridView, button15, button2);
                    //search ortho
                     patient.SearchRecord(patient_TDataGridView, ortho.SearchByName_Ortho(textBox2.Text, dateTimePicker2));
                //Follow Up Color [Long]
                foreach (DataGridViewRow row in patient_TDataGridView.Rows)
                {
                    if (row.Cells[6].Value.ToString() == "Long") { row.DefaultCellStyle.BackColor = Color.LightSalmon; }
                    else { row.DefaultCellStyle.BackColor = Color.White; }
                }
            }
            //count
            linkLabel10.Text = (patient_TDataGridView.Rows.Count).ToString();
        }       
        // New Record [Patient] Or [Ortho] Passing NewCode
        //
        private void Button3_Click(object sender, EventArgs e)
        {
            if (patient.check == "patient_c")
            {
                //new  patient
                Patient_FRM np = new Patient_FRM("new", patient.GetLastCode(patient.que_newCode_Patient, "patient_Code") + 1);
                patient.gotoForm(np, this);
            }
            else if (patient.check == "ortho_c")
            {
                //new ortho
                Ortho_FRM ort = new Ortho_FRM("new",ortho.GetLastCode(ortho.que_newCode_Ortho,"orth_Code")+1);
                ortho.gotoForm(ort, this);

            }
        }
        //Edit - Button
        //
        private void Button15_Click(object sender, EventArgs e)
        {
            if (patient_TDataGridView.Rows.Count == 0)
            { MessageBox.Show("No Records Selected ... !", "Select Record", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            else
            {
                if (patient.check == "patient_c")
                {
                    //basic patient data
                    GetBasictData_Patient();
                }
                else if (patient.check == "ortho_c")
                {
                    //basic ortho data
                    GetBasictData_Ortho();
                }
            }
        } 
        // Edit Patient - Double Cilck in Data Grid View
        // 
        private void patient_TDataGridView_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (patient.check == "patient_c")
            {
                //basic patient data
                GetBasictData_Patient();
            }
            else if (patient.check == "ortho_c")
            {
                //basic ortho data
                GetBasictData_Ortho();
            }
        }
        // Delete Record For [All Patients] O r [All Ortho]
        //
        private void Button2_Click(object sender, EventArgs e)
        {
            if (patient_TDataGridView.Rows.Count == 0)
            { MessageBox.Show("Attention .. No Records Selected ... !", "Select Record", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            else
            {
                if (patient.check == "patient_c")
                {
                    // delete selected patient
                    patient.DeleteRecord(patient_TDataGridView, patient.que_deleteRecord_Patient, "DELETE FROM PatientRays_T WHERE patientt_Code = '");
                }
                else if (patient.check == "ortho_c")
                {
                    // delete selected ortho
                    patient.DeleteRecord(patient_TDataGridView, ortho.que_deleteRecord_Ortho, "DELETE FROM OrthoRays_T WHERE ortho_Code = '");

                }
                linkLabel10.Text = (patient_TDataGridView.Rows.Count).ToString();
            }
        }
       
        // Home - Sub Link
        //
        private void Label6_Click(object sender, EventArgs e)
        {
            patient.gotoForm(h, this);
        } 
        // Home - Icon
        //
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            patient.gotoForm(h, this);
        }
        //Exit - Icon
        //
        private void PictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //DateTimePicker / Search By Date
        //
        private void DateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            if (patient.check == "patient_c")
            {
                //patient.HandleButtons(patient_TDataGridView, button15, button2);
                //search patients
                patient.SearchRecord(patient_TDataGridView, patient.SearchByName_Patient(textBox2.Text, dateTimePicker2));
                //Follow Up Color [Long]
                foreach (DataGridViewRow row in patient_TDataGridView.Rows)
                {
                    if (row.Cells[6].Value.ToString() == "Long") { row.DefaultCellStyle.BackColor = Color.LightSalmon; }
                    else { row.DefaultCellStyle.BackColor = Color.White; }
                }
            }
            if (patient.check == "ortho_c")
            {
               // patient.HandleButtons(patient_TDataGridView, button15, button2);
                //search ortho
             patient.SearchRecord(patient_TDataGridView, ortho.SearchByName_Ortho(textBox2.Text, dateTimePicker2));
                //Follow Up Color [Long]
                foreach (DataGridViewRow row in patient_TDataGridView.Rows)
                {
                    if (row.Cells[6].Value.ToString() == "Long") { row.DefaultCellStyle.BackColor = Color.LightSalmon; }
                    else { row.DefaultCellStyle.BackColor = Color.White; }
                }
            }
            //count
            linkLabel10.Text = (patient_TDataGridView.Rows.Count).ToString();
        }
        
    }
}
