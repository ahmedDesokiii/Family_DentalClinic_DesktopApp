using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StemV5.Pages.Control
{
   public class Class_Ortho : Class_Data
    {
        //felids
        //
        public string orthoType = "new"; // new case only
        public int month, year;

        public string CheckedOrthoType()
        {
            if (orthoType == "new") { orthoType = "new"; }
            else if (orthoType == "edit") { orthoType = "edit"; }

            return orthoType;
        }

        //queries
        //
        public string que_deleteRecord_Ortho = "DELETE FROM Orthodontics_T WHERE orth_Code ='";
        // new patient Ortho
        public string que_newCode_Ortho = "SELECT Top 1(orth_Code) From Orthodontics_T Order by orth_Code desc;";
        public string que_newCode_OrthoRay = "select Top 1(orRay_Code) From OrthoRays_T Order by orRay_Code desc;";
        //Methods
        //
        // Delete Record Session
        public string DeleteRecSession(string patCo)
        {
            string que_deleteRecord_PaSession = "DELETE FROM Orthodontics_T WHERE  orth_Code = " + patCo + " AND orth_SessionCode ='";
            return que_deleteRecord_PaSession;
        }
        //Get Session New Code
        public int GetSessionNewCode_Ortho(string ppcode)
        {
            // get last code of session of this patient 
            string que_newNum_Session = "select Top 1(orth_SessionCode) From Orthodontics_T Where orth_Code = " + ppcode + " Order by orth_SessionCode desc;";
            int newSessNum = GetLastCode(que_newNum_Session, "orth_SessionCode") + 1; // New Session Num

            return newSessNum;
        }
        //Select Mini Ortho List in Selected Date
        public string MiniList(DateTimePicker nextday)
        {
            string que_displayMiniList_Ortho = "SELECT DISTINCT  orth_Name AS 'الاسم ' , orth_Phone As 'التليفون'  FROM Orthodontics_T  Where orth_NextSessionDate = '" + nextday.Value.Date + "'";
            return que_displayMiniList_Ortho;
        }
        //Select nearly Date Sorted 
        public string GetSortedDataByDate(DateTimePicker today)
        {
            //display all Ortho
            string que_displayAllOrtho = "SELECT DISTINCT orth_Code AS 'كود' , orth_Name AS 'الاسم' , orth_Age AS 'السن', orth_Gender AS 'النوع' , orth_Phone AS 'التليفون' , orth_Adress  AS  'العنوان'  ,orth_Follow  AS  'نوع المتابعة', orth_NextSessionDate  AS  'الزيارة القادمة'  FROM Orthodontics_T  Where orth_NextSessionDate > = '" + today.Value.Date + "'Order by orth_NextSessionDate";
            return que_displayAllOrtho;
        }
        //Search By Name
        public string SearchByName_Ortho(string searchTxt, DateTimePicker today)
        {
            string que_searchOrtho = "SELECT DISTINCT orth_Code AS 'كود' , orth_Name AS 'الاسم' , orth_Age AS 'السن', orth_Gender AS 'النوع' , orth_Phone AS 'التليفون' , orth_Adress  AS  'العنوان'  ,orth_Follow  AS  'نوع المتابعة', orth_NextSessionDate  AS  'الزيارة القادمة'   FROM Orthodontics_T  Where orth_Name like N'%" + searchTxt + "%'  AND orth_NextSessionDate > = '" + today.Value.Date + "'Order by orth_NextSessionDate";
            return que_searchOrtho;
        }
        public void ActiveControlsBasic_Ortho(TextBox name, TextBox age, TextBox phone, RadioButton m, RadioButton fe, TextBox adress, TextBox comment, RadioButton N, RadioButton L,DateTimePicker next, Color c, bool r)
        {
            name.BackColor = c; name.ReadOnly = r;
            age.BackColor = c; age.ReadOnly = r;
            adress.BackColor = c; adress.ReadOnly = r;
            phone.BackColor = c; phone.ReadOnly = r;
            comment.BackColor = c; comment.ReadOnly = r;
            m.Enabled = !r;
            fe.Enabled = !r;
            N.Enabled = !r;
            L.Enabled = !r;
            next.Enabled = !r;
        }
       

    }
}
