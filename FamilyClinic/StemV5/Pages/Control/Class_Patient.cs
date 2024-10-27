using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StemV5.Pages.Control
{
    public class Class_Patient : Class_Data
    {
        //felids
        //
        public string patientType = "new"; // new case only
        public int row = 0;
    

        public string CheckedPatientType()
        {
            if (patientType == "new") { patientType = "new"; }
            else if (patientType == "edit") { patientType = "edit"; }

            return patientType;
        }

        //queries
        //
        //display all patients
        // delete record from patients
        public string que_deleteRecord_Patient = "DELETE FROM Patient_T WHERE patient_Code ='";
        // new patient code
        public string que_newCode_Patient = "select Top 1(patient_Code) From Patient_T Order by patient_Code desc;";
        public string que_newCode_PatientRay = "select Top 1(paRay_Code) From PatientRays_T Order by paRay_Code desc;";
        //Methods
        //


        //Select Mini Patients List in Selected Date
        public string MiniList(DateTimePicker nextday)
        {
            string que_displayMiniList_Patients = "SELECT DISTINCT  patient_Name AS 'الاسم ' , patient_Phone As 'التليفون'  FROM Patient_T  Where patient_SessionNextDate  = '" + nextday.Value.Date + "'";
            return que_displayMiniList_Patients;
        }
        //Select nearly Date Sorted 
        public string GetSortedDataByDate(DateTimePicker today)
        {
              //display all patients
              string que_displayAllPatients = "SELECT DISTINCT patient_Code  AS 'كود ' , patient_Name AS 'الاسم ', patient_Age  AS ' السن ' , patient_Gender AS ' النوع ' , patient_Phone As 'التليفون',patient_Adress AS 'العنوان' ,  patient_Follow AS 'نوع المتابعة', patient_SessionNextDate AS 'الزيارة القادمة'  FROM Patient_T  Where patient_SessionNextDate > = '" + today.Value.Date + "'Order by patient_SessionNextDate"; 
            return que_displayAllPatients;
        }

        //Search By Name & Nearly Date
        public string SearchByName_Patient(string searchTxt, DateTimePicker today)
        {
            string que_searchPatient = "SELECT DISTINCT patient_Code  AS 'كود ' , patient_Name AS 'الاسم ', patient_Age  AS ' السن ' , patient_Gender AS ' النوع ' , patient_Phone As 'التليفون',patient_Adress AS ' العنوان ' ,  patient_Follow AS 'نوع المتابعة', patient_SessionNextDate AS 'الزيارة القادمة' FROM Patient_T  Where patient_Name like N'%" + searchTxt+ "%'  AND patient_SessionNextDate > = '" + today.Value.Date + "'Order by patient_SessionNextDate"; 
            return que_searchPatient;
        }
        //Active Diagonsis 
        public bool ActiveDiagonsisControls(ComboBox diaTTT,TextBox diaCost, Color c, bool r)
        {
            bool diaSelected = false;
            if (diaTTT.SelectedIndex == -1)
            { diaSelected = false; diaCost.BackColor = Color.Gainsboro; diaCost.ReadOnly = !r; }
            else
            { diaSelected = true; diaCost.BackColor = c; diaCost.ReadOnly = r; }
            return diaSelected;
        }
        //Active Controls Color & Read Only
        public void ActiveControlsBasic_Patient(TextBox name, TextBox age, TextBox phone, RadioButton m, RadioButton fe, TextBox adress, TextBox mh, RadioButton N, RadioButton L, TextBox DD, Color c, bool r)
        {
            name.BackColor = c; name.ReadOnly = r;
            age.BackColor = c; age.ReadOnly = r;
            adress.BackColor = c; adress.ReadOnly = r;
            phone.BackColor = c; phone.ReadOnly = r;
            DD.BackColor = c; DD.ReadOnly = r;
            mh.BackColor = c; mh.ReadOnly = r;
            m.Enabled = !r;
            fe.Enabled = !r;
            N.Enabled = !r;
            L.Enabled = !r;
        }
        //Get Session New Code
        public int GetSessionNewCode(string ppcode)
        {
            // get last code of session of this patient 
            string que_newNum_Session = "select Top 1(patient_SessionCode) From Patient_T Where patient_Code = " + ppcode + " Order by patient_SessionCode desc;";
            int newSessNum = GetLastCode(que_newNum_Session, "patient_SessionCode") + 1; // New Session Num

            return newSessNum;
        }
        // Delete Record Session
        public string DeleteRecSession(string patCo)
        {
            string que_deleteRecord_PaSession = "DELETE FROM Patient_T WHERE  patient_Code = "+patCo+" AND patient_SessionCode ='";
            return que_deleteRecord_PaSession;
        }
        // Teeth # Select
        public string SelectedTeeth(ComboBox c_ul, ComboBox c_ur, ComboBox c_ll, ComboBox c_lr)
        {
            string tee = "";
            if (c_ul.Visible == true && c_ur.Visible == false && c_ll.Visible == false && c_lr.Visible == false)
            {
                tee = "U.L # " + c_ul.SelectedItem.ToString();
            }
            else if (c_ul.Visible == false && c_ur.Visible == true && c_ll.Visible == false && c_lr.Visible == false)
            {
                tee = "U.R # " + c_ur.SelectedItem.ToString();
            }
            else if (c_ul.Visible == false && c_ur.Visible == false && c_ll.Visible == true && c_lr.Visible == false)
            {
                tee = "L.L # " + c_ll.SelectedItem.ToString();
            }
            else if (c_ul.Visible == false && c_ur.Visible == false && c_ll.Visible == false && c_lr.Visible == true)
            {
                tee = "L.R # " + c_lr.SelectedItem.ToString();
            }
            else
            {
                tee = "No Tooth";
            }
            return tee;
        }
         //Reset Controls For Session
        public void ResetControlsForSession(ComboBox c1, ComboBox c_ul, ComboBox c_ur, ComboBox c_ll, ComboBox c_lr,TextBox t1, TextBox t2, TextBox t3, TextBox t4, TextBox t5,TextBox t6)
        {
            c1.SelectedIndex = -1;
            c_ul.SelectedIndex = -1; c_ur.SelectedIndex = -1; c_ll.SelectedIndex = -1; c_lr.SelectedIndex = -1;
            c_ul.Visible = true; c_ur.Visible = true; c_ll.Visible = true; c_lr.Visible = true;
            t2.Text = "0"; t3.Text = "0";  t5.Text = ""; t6.Text = "0";
        }
      
    }
}
