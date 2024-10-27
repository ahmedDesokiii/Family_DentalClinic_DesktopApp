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
    public partial class Finances_FRM : Form
    {
        Home_FRM h = new Home_FRM();
        Class_Patient patient = new Class_Patient();
        public Finances_FRM()
        {
            InitializeComponent();
        }

        public void PatientStation()
        {
            string quue_patient = "Select patient_Code As 'كود',patient_Name As 'الاسم',patient_Phone As 'تليفون' ,patient_AmountRequired As 'المبلغ المطلوب' ,patient_AmountPaid As 'المبلغ المدفوع',patient_Discount  As ' الخصم'  From Patient_T Where patient_SessionDate >= ' " + dateTimePicker3.Value.Date + " ' and patient_SessionDate <= ' " + dateTimePicker1.Value.Date + " '  ";
            if (cbLong.Checked == true && cbNormal.Checked == true)
            {
                quue_patient = quue_patient + " and ( patient_Follow = 'Long' or patient_Follow = 'Normal' )";
            }
           else if (cbLong.Checked == false && cbNormal.Checked == false)
            {
                quue_patient = quue_patient + " and ( patient_Follow = 'Long' and patient_Follow = 'Normal' )";
            }
           else if (cbLong.Checked == true && cbNormal.Checked == false)
            {
                quue_patient = quue_patient + " and patient_Follow = 'Long'  ";
            }
           else if (cbLong.Checked == false && cbNormal.Checked == true)
            {
                quue_patient = quue_patient + " and patient_Follow = 'Normal'  ";
            }
            //Get patients data
            patient.DisplayAll(patient_TDataGridView, quue_patient);
            //clac total amount 
            txtAmoTotal.Text = patient.SumAmountRemaining(patient_TDataGridView, 3).ToString();
            //clac total amount paid
            txtAmoPaid.Text = patient.SumAmountRemaining(patient_TDataGridView, 4).ToString();
            //clac total discount
            txtDisnt.Text = patient.SumAmountRemaining(patient_TDataGridView, 5).ToString();
            //clac total Amount Remaining
            txtTotalRemain.Text = ((Convert.ToInt32(txtAmoTotal.Text)) - ((Convert.ToInt32(txtAmoPaid.Text)) + (Convert.ToInt32(txtDisnt.Text)))).ToString();
            //count
            linkLabel1.Text = patient_TDataGridView.Rows.Count.ToString();
            this.ActiveControl = txtInSave;
        }
        public void OrthoStation()
        {

            string quue_patient = "Select  orth_Code As 'كود',orth_Name As 'الاسم',orth_Phone As 'تليفون' ,orth_TotalAmount As 'المبلغ المطلوب' ,orth_PaidAmount As 'المبلغ المدفوع',orth_Discount  As ' الخصم'  From Orthodontics_T Where orth_SessionDate >= ' " + dateTimePicker4.Value.Date + " ' and orth_SessionDate <= ' " + dateTimePicker5.Value.Date + " '  ";
            if (cbLongOr.Checked == true && cbNormalOr.Checked == true)
            {
                quue_patient = quue_patient + " and ( orth_Follow = 'Long' or orth_Follow = 'Normal' )";
            }
            else if (cbLongOr.Checked == false && cbNormalOr.Checked == false)
            {
                quue_patient = quue_patient + " and ( orth_Follow = 'Long' and orth_Follow = 'Normal' )";
            }
            else if (cbLongOr.Checked == true && cbNormalOr.Checked == false)
            {
                quue_patient = quue_patient + " and orth_Follow = 'Long'  ";
            }
            else if (cbLongOr.Checked == false && cbNormalOr.Checked == true)
            {
                quue_patient = quue_patient + " and orth_Follow = 'Normal'  ";
            }
            //Get patients data
            patient.DisplayAll(dataGridView1, quue_patient);
            DuplicateRecord(dataGridView1);
            //clac total amount 
            txtTotalOr.Text = patient.SumAmountRemaining(dataGridView1, 3).ToString();
            //clac total amount paid
            txtPaidOr.Text = patient.SumAmountRemaining(dataGridView1, 4).ToString();
            //clac total discount
            txtDiscountOr.Text = patient.SumAmountRemaining(dataGridView1, 5).ToString();
            //clac total Amount Remaining
            txtRemainOr.Text = ((Convert.ToInt32(txtTotalOr.Text)) - ((Convert.ToInt32(txtPaidOr.Text)) + (Convert.ToInt32(txtDiscountOr.Text)))).ToString();
            //count
            linkLabel2.Text = dataGridView1.Rows.Count.ToString();
            this.ActiveControl = txtInSave;
        }
        private void PictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {
            patient.gotoForm(h, this);
        }

        private void Finances_FRM_Load(object sender, EventArgs e)
        {
            // Patient Finance$
            PatientStation();

            // Ortho Finance$
            OrthoStation();
            
            // All count
            linkLabel10.Text = (patient_TDataGridView.Rows.Count + dataGridView1.Rows.Count).ToString();
        }

        private void DateTimePicker3_ValueChanged(object sender, EventArgs e)
        {
            PatientStation();
            
            // All count
            linkLabel10.Text = (patient_TDataGridView.Rows.Count + dataGridView1.Rows.Count).ToString();
        }

        private void DateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            PatientStation();
            // All count
            linkLabel10.Text = (patient_TDataGridView.Rows.Count + dataGridView1.Rows.Count).ToString();
        }

        private void DateTimePicker4_ValueChanged(object sender, EventArgs e)
        {
            OrthoStation();
            // All count
            linkLabel10.Text = (patient_TDataGridView.Rows.Count + dataGridView1.Rows.Count).ToString();
        }

        private void DateTimePicker5_ValueChanged(object sender, EventArgs e)
        {
            OrthoStation();
            // All count
            linkLabel10.Text = (patient_TDataGridView.Rows.Count + dataGridView1.Rows.Count).ToString();
        }

        private void TxtInSave_KeyPress(object sender, KeyPressEventArgs e)
        {
            patient.NumericOnly(sender, e, txtInSave);
        }

        private void TxtInSave_TextChanged(object sender, EventArgs e)
        {
            if (txtInSave.Text == "") { txtInSave.Text = "0"; }
            txtDiff.Text = ((Convert.ToInt32(txtInSave.Text)) - (Convert.ToInt32(txtAmoPaid.Text))).ToString();
        }

        private void TxtAmoPaid_TextChanged(object sender, EventArgs e)
        {
            if (txtInSave.Text == "") { txtInSave.Text = "0"; }
            txtDiff.Text = ((Convert.ToInt32(txtInSave.Text)) - (Convert.ToInt32(txtAmoPaid.Text))).ToString();
        }

        private void CbLong_CheckedChanged(object sender, EventArgs e)
        {
            PatientStation();
            // All count
            linkLabel10.Text = (patient_TDataGridView.Rows.Count + dataGridView1.Rows.Count).ToString();
        }

        private void CbNormal_CheckedChanged(object sender, EventArgs e)
        {
            PatientStation();
            // All count
            linkLabel10.Text = (patient_TDataGridView.Rows.Count + dataGridView1.Rows.Count).ToString();
        }

        private void CbLongOr_CheckedChanged(object sender, EventArgs e)
        {
            OrthoStation();
            // All count
            linkLabel10.Text = (patient_TDataGridView.Rows.Count + dataGridView1.Rows.Count).ToString();
        }

        private void CbNormalOr_CheckedChanged(object sender, EventArgs e)
        {
            OrthoStation();
            // All count
            linkLabel10.Text = (patient_TDataGridView.Rows.Count + dataGridView1.Rows.Count).ToString();
        }

        private void TxtInSaveOr_KeyPress(object sender, KeyPressEventArgs e)
        {
            patient.NumericOnly(sender, e, txtInSaveOr);
        }

        private void TxtInSaveOr_TextChanged(object sender, EventArgs e)
        {
            if (txtInSaveOr.Text == "") { txtInSaveOr.Text = "0"; }
            txtDiffOr.Text = ((Convert.ToInt32(txtInSaveOr.Text)) - (Convert.ToInt32(txtPaidOr.Text))).ToString();
        }

        private void TxtPaidOr_TextChanged(object sender, EventArgs e)
        {
            if (txtInSaveOr.Text == "") { txtInSaveOr.Text = "0"; }
            txtDiffOr.Text = ((Convert.ToInt32(txtInSaveOr.Text)) - (Convert.ToInt32(txtPaidOr.Text))).ToString();
        }


          public void DuplicateRecord(DataGridView dg)
        {
            // try
            // {
            //     int cou = 0;
            //         for (int i = 0; i < dg.Rows.Count ; i++)
            //         {
            //             for (int j = 0; j < dg.Rows.Count; j++)
            //             {
            //                 if (dg.Rows[i].Cells[0].Value == dg.Rows[j].Cells[0].Value)
            //                 {
            //                     // don't duplicate total
            //                     dg.Rows[i].Cells[3].Value = dg.Rows[j].Cells[3].Value; // total
            //                     dg.Rows[i].Cells[4].Value = Convert.ToInt32(dg.Rows[i].Cells[4].Value) + Convert.ToInt32(dg.Rows[j].Cells[4].Value); // paid
            //                     dg.Rows[i].Cells[5].Value = Convert.ToInt32(dg.Rows[i].Cells[5].Value) + Convert.ToInt32(dg.Rows[j].Cells[5].Value); // discount

            //                     if (cou != 0)
            //                         {
            //                             dg.Rows.RemoveAt(j);
            //                         }
            //                         cou++;
            //                 }
            //             }
            //        }
            // }
            // catch (Exception ex)
            // {
            //     MessageBox.Show(ex.Message.ToString());
            // }
            //// return true;
           
            //List<string> list = new List<string>();
            //for (int i = 0; i < dg.Rows.Count; i++)
            //{
            //    string str = dg.Rows[i].Cells[0].Value.ToString();
            //    if (!list.Contains(str))
            //    {
            //        //for (int j = 0; j < dg.Rows.Count; j++)
            //        //{
            //        //    dg.Rows[i].Cells[4].Value = Convert.ToInt32(dg.Rows[i].Cells[4].Value) + Convert.ToInt32(dg.Rows[j].Cells[4].Value); // paid
            //        //    dg.Rows[i].Cells[5].Value = Convert.ToInt32(dg.Rows[i].Cells[5].Value) + Convert.ToInt32(dg.Rows[j].Cells[5].Value); // discount
            //        //}
            //        list.Add(dg.Rows[i].Cells[0].Value.ToString());
            //    }
            //else
            //    {
            //        dg.Rows.Remove(dg.Rows[i]);
            //    }
            //}

        }




    }
}
