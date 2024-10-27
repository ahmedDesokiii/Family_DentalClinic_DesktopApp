using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace StemV5.Pages.Control
{
    public class Class_Data
    {
        //feilds
        //
        public string check = "";
        public int code = 0;
        public string cellData = "";
        public string patientField = "";
        public int diaCost = 0;
        public int remin = 0;

        //connection string path
        SqlConnection con = new SqlConnection(@"Data Source = (LocalDB)\v11.0; AttachDbFilename=F:\Work2024\AspNetProjects\DesktopApps\FamilyClinic\StemV5\Pages\Model\FamilyDB.mdf;Integrated Security = True");


        //Checked Type of Class
        //
        public string CheckedType()
        {
            if (check == "patient_c") { check = "patient_c"; }
            else if (check == "ortho_c") { check = "ortho_c"; }

            return check;
        }
        // Go to any Form and hide another
        //
        public void gotoForm(Form frm, Form hideF)
        {
            frm.Show();
            hideF.Hide();
        }
        //Numeric only
        //
        public void NumericOnly(object sender, KeyPressEventArgs e, TextBox txt)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                txt.Text = "0";
            }
        }
        public static Image ResizeImage(Image imgToResize, Size size)
        {
            return (Image)(new Bitmap(imgToResize, size));
        }
        //Display Datagrid view Images 
        public void DisplayDGVImages(DataGridView dg, string displayQuery)
        {
            try
            {
                con.Open();
                dg.Refresh(); dg.Update();
                DataTable dtt = new DataTable();
                SqlDataAdapter ad = new SqlDataAdapter(displayQuery, con);//  
                ad.Fill(dtt);
                dg.DataSource = dtt;
                con.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        //Display Datagrid view Images 
        public void DisplayDGVImages(PictureBox pic, string displayQuery)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = (displayQuery);
                //cmd.Parameters.AddWithValue("@EntryID", Convert.ToInt32(textBox1.Text));
                var da = new SqlDataAdapter(cmd);
                var ds = new DataSet();
                da.Fill(ds, "PatientRays_T");
                int count = ds.Tables["PatientRays_T"].Rows.Count;
                if (count > 0)
                {
                    var data = (Byte[])ds.Tables["PatientRays_T"].Rows[count - 1]["paRay_SrcImg"];
                    var stream = new MemoryStream(data);
                    pic.Image = Image.FromStream(stream);
                }
                con.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        //Display Datagrid view Images 
        public void DisplayDGVImagesOrtho(PictureBox pic, string displayQuery)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = (displayQuery);
                //cmd.Parameters.AddWithValue("@EntryID", Convert.ToInt32(textBox1.Text));
                var da = new SqlDataAdapter(cmd);
                var ds = new DataSet();
                da.Fill(ds, "OrthoRays_T");
                int count = ds.Tables["OrthoRays_T"].Rows.Count;
                if (count > 0)
                {
                    var data = (Byte[])ds.Tables["OrthoRays_T"].Rows[count - 1]["orRay_SrcImg"];
                    var stream = new MemoryStream(data);
                    pic.Image = Image.FromStream(stream);
                }
                con.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        //Convert Image To Byte ...
        public byte[] CovertImageToByte(Image img, int w, int h)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                img = ResizeImage(img, new Size(w, h));
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return ms.ToArray();
            }
        }
        //Convert ByteArray To Image ...
        public Image CovertByteArrayToImage(byte[] data, Image img)
        {
            using (MemoryStream ms = new MemoryStream(data))
            {
                img = ResizeImage(img, new Size(1000, 1000));
                return Image.FromStream(ms);
            }
        }
        //Browse Image ... 
        public string BrowseImage(PictureBox pb)
        {
            // open file dialog   
            OpenFileDialog open = new OpenFileDialog();
            // image filters  
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if (open.ShowDialog() == DialogResult.OK)
            {
                // display image in picture box  
                // pb.Image = new Bitmap(open.FileName);
                pb.Image = Image.FromFile(open.FileName);
                // System.Drawing.Image i = ResizeImage(b, new Size(500, 500));
            }
            return open.FileName;
        }
        // Insert Images To DB ...
        public void InsertImage(string rayCode, string patientCode, string fileName, DateTimePicker rayDate, string notes, byte[] img, byte[] srcImg)
        {
            try
            {
                con.Open();
                string insert_addPatient = "INSERT INTO PatientRays_T (paRay_Code,patientt_Code,paRay_FileName,paRay_Date,paRay_Notes,paRay_Img,paRay_SrcImg) VALUES (@paRay_Code,@patientt_Code,@paRay_FileName,@paRay_Date,@paRay_Notes,@paRay_Img,@paRay_SrcImg)";

                SqlCommand command = new SqlCommand(insert_addPatient, con);
                command.Parameters.AddWithValue("@paRay_Code", rayCode);
                command.Parameters.AddWithValue("@patientt_Code", patientCode);
                command.Parameters.AddWithValue("@paRay_FileName", fileName.Trim());
                command.Parameters.AddWithValue("@paRay_Date", rayDate.Value.Date);
                command.Parameters.AddWithValue("@paRay_Notes", notes.Trim());
                command.Parameters.AddWithValue("@paRay_Img", img);
                command.Parameters.AddWithValue("@paRay_SrcImg", srcImg);

                command.ExecuteNonQuery();

                con.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        // Insert Images To DB ...
        public void InsertImageOrtho(string rayCode, string patientCode, string fileName, DateTimePicker rayDate, string notes, byte[] img, byte[] srcImg)
        {
            try
            {
                con.Open();
                string insert_addPatient = "INSERT INTO OrthoRays_T (orRay_Code,ortho_Code,orRay_FileName,orRay_Date,orRay_Notes,orRay_Img,orRay_SrcImg) VALUES (@orRay_Code,@ortho_Code,@orRay_FileName,@orRay_Date,@orRay_Notes,@orRay_Img,@orRay_SrcImg)";

                SqlCommand command = new SqlCommand(insert_addPatient, con);
                command.Parameters.AddWithValue("@orRay_Code", rayCode);
                command.Parameters.AddWithValue("@ortho_Code", patientCode);
                command.Parameters.AddWithValue("@orRay_FileName", fileName.Trim());
                command.Parameters.AddWithValue("@orRay_Date", rayDate.Value.Date);
                command.Parameters.AddWithValue("@orRay_Notes", notes.Trim());
                command.Parameters.AddWithValue("@orRay_Img", img);
                command.Parameters.AddWithValue("@orRay_SrcImg", srcImg);

                command.ExecuteNonQuery();

                con.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        //Decimal & Numeric Only
        //
        public void DecimalNumericOnly(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;

            }
            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }
        // Check RadioButton Selected 
        public string GetRadioButtonSelected(RadioButton rb_male, RadioButton rb_female)
        {
            string selectedRadioButton = "";
            if (rb_male.Checked == true) { selectedRadioButton = rb_male.Text; }
            else { selectedRadioButton = rb_female.Text; }
            return selectedRadioButton;
        }
        //Select Follow Type
        public string SelectFollowType(RadioButton rbN, RadioButton rbL)
        {
            string followType = "Normal";
            if (rbN.Checked == true) { followType = "Normal"; }
            if (rbL.Checked == true) { followType = "Long"; }
            //else { followType = "Normal"; }
            return followType;
        }

        public int SumAmountRemaining(DataGridView dgv, int cel)
        {
            int sum = 0;
            for (int i = 0; i < dgv.RowCount; i++)
            {
                sum += Convert.ToInt32(dgv.Rows[i].Cells[cel].Value);
            }
            return sum;
        }

        //Display All Records 
        //
        public string DisplayAll(DataGridView dg, string displayQuery)
        {
            try
            {
                check = CheckedType();
                con.Open();
                dg.Refresh(); dg.Update();
                DataTable dtt = new DataTable();
                SqlDataAdapter ad = new SqlDataAdapter(displayQuery, con);//  
                ad.Fill(dtt);
                dg.DataSource = dtt;
                con.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            return check;
        }
        //Search Record 
        //
        public string SearchRecord(DataGridView dg, string searchQuery)
        {
            try
            {
                check = CheckedType();
                con.Open();
                dg.Refresh(); dg.Update();
                DataTable dtt = new DataTable();
                SqlDataAdapter adabter = new SqlDataAdapter(searchQuery, con);
                adabter.Fill(dtt);
                dg.DataSource = dtt;
                con.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            return check;
        }
        //Delete Record DataGridView... 
        //
        public string DeleteRecord(DataGridView dg, string deleteBasicQuery)
        {
            check = CheckedType();
            SqlCommand delComm;
            try
            {
                DialogResult d = MessageBox.Show("Are you Sure Delete Code :   " + dg.SelectedRows[0].Cells[0].Value.ToString(), "  Delete ? ", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);
                if (d == DialogResult.Yes)
                {
                    foreach (DataGridViewRow item in dg.SelectedRows)
                    {
                        con.Open();
                        delComm = con.CreateCommand();
                        int code = Convert.ToInt32(dg.SelectedRows[0].Cells[0].Value);
                        // Delete Basic data
                        delComm.CommandText = deleteBasicQuery + code + "'";
                        MessageBox.Show(" Delete Done ... ! ", MessageBoxIcon.Hand.ToString());
                        dg.Rows.RemoveAt(dg.SelectedRows[0].Index);
                        delComm.ExecuteNonQuery();
                        dg.Refresh();
                        dg.Update();
                        con.Close();
                    }
                }
                else if (d == DialogResult.No)
                {
                    con.Close();
                }

            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            return check;
        }

        //Delete Record DataGridView... 
        //
        public string DeleteRecord(DataGridView dg, string deleteBasicQuery, string quDelRays)
        {
            check = CheckedType();
            SqlCommand delComm;
            try
            {
                DialogResult d = MessageBox.Show("Are you Sure Delete Code :   " + dg.SelectedRows[0].Cells[0].Value.ToString(), "  Delete ? ", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);
                if (d == DialogResult.Yes)
                {
                    foreach (DataGridViewRow item in dg.SelectedRows)
                    {
                        con.Open();
                        delComm = con.CreateCommand();
                        int code = Convert.ToInt32(dg.SelectedRows[0].Cells[0].Value);
                        // Delete Basic data
                        delComm.CommandText = quDelRays + code + "'";
                        delComm.CommandText = deleteBasicQuery + code + "'";


                        MessageBox.Show(" Delete Done ... ! ", MessageBoxIcon.Hand.ToString());
                        dg.Rows.RemoveAt(dg.SelectedRows[0].Index);
                        delComm.ExecuteNonQuery();
                        dg.Refresh();
                        dg.Update();
                        con.Close();
                    }
                }
                else if (d == DialogResult.No)
                {
                    con.Close();
                }

            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            return check;
        }
        // Delete Diagonsis Data in FRM
        //
        public void DeleteDiagonsis(ComboBox dia, Panel p)
        {
            SqlCommand delComm;
            try
            {
                DialogResult d = MessageBox.Show("Are You Sure Delete Diagonsis :  " + dia.Text, "Delete Diagonsis !  ", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);
                if (d == DialogResult.Yes)
                {
                    con.Open();
                    delComm = con.CreateCommand();
                    // Delete Basic data
                    delComm.CommandText = "Delete From Diagonsis_T Where dia_Code = " + Convert.ToInt32(dia.SelectedIndex) + ";";
                    delComm.ExecuteNonQuery();
                    dia.SelectedIndex = -1;
                    con.Close();
                    p.Enabled = false;
                }
                else if (d == DialogResult.No)
                {
                    con.Close();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        // Add To New Patient Data || Upate Basic Patient Data
        //
        public void UpdateData_Patient(DataGridView dg, string paCode, string paName, string paAge, string paGender, string paPhone, string paAdress, string paFollow, DateTimePicker nextSess)
        {
            try
            {
                int rr = dg.Rows.Count;
                con.Open();
                //foreach (DataGridViewRow r in dg.Rows)
                ////for (int i = 0; i < rr; i++)
                //{

                string q_updatePatientData = " Update Patient_T  Set  patient_Name=@patient_Name,patient_Age=@patient_Age,patient_Gender=@patient_Gender,patient_Phone=@patient_Phone,patient_Adress=@patient_Adress,patient_Follow = @patient_Follow,patient_SessionNextDate = @patient_SessionNextDate Where patient_Code=@patient_Code ";
                SqlCommand command = new SqlCommand(q_updatePatientData, con);
                command.Parameters.AddWithValue("@patient_Code", paCode);
                command.Parameters.AddWithValue("@patient_Name", paName.Trim());
                command.Parameters.AddWithValue("@patient_Age", paAge);
                command.Parameters.AddWithValue("@patient_Gender", paGender.Trim());
                command.Parameters.AddWithValue("@patient_Phone", paPhone.Trim());
                command.Parameters.AddWithValue("@patient_Adress", paAdress.Trim());
                command.Parameters.AddWithValue("@patient_Follow", paFollow.Trim());
                command.Parameters.AddWithValue("@patient_SessionNextDate", nextSess.Value.Date);


                ////data grid view data
                //command.Parameters.AddWithValue("@patient_SessionDate", r.Cells[1].Value);
                //command.Parameters.AddWithValue("@patient_SessionCode", r.Cells[0].Value);
                //command.Parameters.AddWithValue("@patient_MedicalHistory", r.Cells[9].Value.ToString().Trim());
                //command.Parameters.AddWithValue("@patient_DiagonsisName", r.Cells[2].Value.ToString().Trim());
                //command.Parameters.AddWithValue("@patient_DiagonsisDetails", r.Cells[8].Value.ToString().Trim());
                //command.Parameters.AddWithValue("@patient_DiagonsisCost", r.Cells[3].Value.ToString());
                //command.Parameters.AddWithValue("@patient_TeethChart", r.Cells[7].Value.ToString().Trim());
                //command.Parameters.AddWithValue("@patient_AmountRequired", r.Cells[3].Value.ToString());
                //command.Parameters.AddWithValue("@patient_AmountPaid", r.Cells[4].Value.ToString());
                //command.Parameters.AddWithValue("@patient_Discount", r.Cells[5].Value.ToString());
                command.ExecuteNonQuery();
                //}//for

                con.Close();
            }//try
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        // Display Old Complains
        //
        public void DisplayOldComplainsData(DataGridView dg, string pCode)
        {
            var bindingSource = new BindingSource();
            string query = "Select DISTINCT patient_SessionCode AS 'Code',patient_SessionDate AS 'Date',patient_DiagonsisName AS 'Diagonsis TTT',patient_AmountRequired AS 'Required',patient_AmountPaid AS 'Paid ',patient_Discount AS 'Discount ',paient_RemainingAmount AS 'Remining ',patient_TeethChart AS 'Teeth',patient_DiagonsisDetails AS 'Diagonsis Details',patient_MedicalHistory AS 'Medical History'  From Patient_T Where patient_Code=" + pCode + ";";
            using (SqlDataAdapter dAdapter = new SqlDataAdapter(query, con))
            {
                try
                {
                    SqlCommandBuilder commBuilder = new SqlCommandBuilder(dAdapter);
                    DataTable table = new DataTable();
                    dAdapter.Fill(table);
                    bindingSource.DataSource = table;
                    dg.ReadOnly = true;
                    dg.DataSource = bindingSource;
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
                finally
                {
                    //con.Close();
                }
            }

        }
        // Add To Old Complains
        //
        public void AddOldComplainsData(string paCode, string paName, string paAge, string paGender, string paPhone, string paAdress, string paMh, string paSessionCode, DateTimePicker sessDate, DateTimePicker sessNextDate, string paDiaTTT, string paDiaDetails, string paDiaCost, string tee, string paFollow, string req, string paid, string discount)
        {
            try
            {
                con.Open();
                string insert_addPatient = "INSERT INTO Patient_T (patient_Code,patient_Name,patient_Age,patient_Gender,patient_Phone,patient_Adress,patient_SessionCode,patient_SessionDate,patient_SessionNextDate,patient_MedicalHistory,patient_DiagonsisName,patient_DiagonsisDetails,patient_DiagonsisCost,patient_TeethChart,patient_Follow,patient_AmountRequired,patient_AmountPaid,patient_Discount)"
                                                    + "VALUES (@patient_Code,@patient_Name,@patient_Age,@patient_Gender,@patient_Phone,@patient_Adress,@patient_SessionCode,@patient_SessionDate,@patient_SessionNextDate,@patient_MedicalHistory,@patient_DiagonsisName,@patient_DiagonsisDetails,@patient_DiagonsisCost,@patient_TeethChart,@patient_Follow,@patient_AmountRequired,@patient_AmountPaid,@patient_Discount)";

                SqlCommand command = new SqlCommand(insert_addPatient, con);
                command.Parameters.AddWithValue("@patient_Code", paCode);
                command.Parameters.AddWithValue("@patient_Name", paName.Trim());
                command.Parameters.AddWithValue("@patient_Age", paAge);
                command.Parameters.AddWithValue("@patient_Gender", paGender.Trim());
                command.Parameters.AddWithValue("@patient_Phone", paPhone.Trim());
                command.Parameters.AddWithValue("@patient_Adress", paAdress.Trim());

                command.Parameters.AddWithValue("@patient_SessionDate", sessDate.Value.Date);
                command.Parameters.AddWithValue("@patient_SessionNextDate", sessNextDate.Value.Date);
                command.Parameters.AddWithValue("@patient_SessionCode", paSessionCode);
                command.Parameters.AddWithValue("@patient_MedicalHistory", paMh.Trim());
                command.Parameters.AddWithValue("@patient_DiagonsisName", paDiaTTT.Trim());
                command.Parameters.AddWithValue("@patient_DiagonsisDetails", paDiaDetails.Trim());
                command.Parameters.AddWithValue("@patient_DiagonsisCost", paDiaCost);
                command.Parameters.AddWithValue("@patient_TeethChart", tee.Trim());
                command.Parameters.AddWithValue("@patient_Follow", paFollow.Trim());
                command.Parameters.AddWithValue("@patient_AmountRequired", req);
                command.Parameters.AddWithValue("@patient_AmountPaid", paid);
                command.Parameters.AddWithValue("@patient_Discount", discount);
                command.ExecuteNonQuery();
                con.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        //Add DiaName, DiaCost,DiaCode
        //
        public void GetDiagonsisDate(ComboBox dia)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Diagonsis_T ", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            cmd.ExecuteNonQuery();
            con.Close();
            dia.DataSource = ds.Tables[0];
            dia.DisplayMember = "dia_Name";
            dia.ValueMember = "Id";
        }


        //Calc Select Diagonsis Cost
        //
        public int CalcDiagonsisCost(ComboBox cm, int index)
        {
            try
            {
                if (cm.SelectedIndex == index)
                {
                    if (index == -1) { cm.SelectedIndex = -1; cm.SelectedItem = ""; diaCost = 0; }
                    else
                    {
                        con.Open();
                        using (SqlCommand cmd = new SqlCommand("select dia_Cost,dia_Name From Diagonsis_T where dia_Code  = " + cm.SelectedIndex + ";", con))
                        {
                            using (SqlDataReader rdr = cmd.ExecuteReader())
                            {
                                if (rdr.Read())
                                {
                                    diaCost = rdr.GetInt32(rdr.GetOrdinal("dia_Cost"));
                                }//if
                            }//using
                        }//using
                    }
                }

                con.Close();
            }
            catch (SqlException ex)
            { MessageBox.Show(ex.Message.ToString()); }
            return diaCost;
        }
        //Update Diagonsis Cost
        //
        public void UpateDiagonsisCost(string cost, int index)
        {
            try
            {
                con.Open();
                string updateDiagonsis = "Update Diagonsis_T SET dia_Cost=@cost  WHERE dia_Code=@code ;";
                SqlCommand command = new SqlCommand(updateDiagonsis, con);
                command.Parameters.AddWithValue("@cost", cost);
                command.Parameters.AddWithValue("@code", index);
                command.ExecuteNonQuery();
                con.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message.ToString());
                con.Close();
            }
        }
        //New Diagonsis 
        //
        public void NewDiagonsis(string cost, int index, string name)
        {
            try
            {
                con.Open();
                string updateDiagonsis = "Insert Into Diagonsis_T (dia_Cost,dia_Code,dia_Name) Values (@cost,@code,@name ) ;";
                SqlCommand command = new SqlCommand(updateDiagonsis, con);
                command.Parameters.AddWithValue("@cost", cost);
                command.Parameters.AddWithValue("@code", index);
                command.Parameters.AddWithValue("@name", name.Trim());

                command.ExecuteNonQuery();
                con.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message.ToString());
                con.Close();
            }
        }
        //Select Feild Of DataGridView Record
        //
        public string GetSelectedRecordCellData(DataGridView dg, int cellIndex)
        {
            try
            {
                cellData = Convert.ToString(dg.SelectedRows[0].Cells[cellIndex].Value);
            }
            catch (SqlException ex)
            { MessageBox.Show(ex.Message.ToString()); }
            return cellData;
        }
        // Get Any Feild DB
        //
        public string GetStudentFeild(string selectFeildQuery, string txCode, string stFeild)
        {
            try
            {
                con.Open();
                using (SqlCommand cmd1 = new SqlCommand(selectFeildQuery + txCode, con))
                {
                    using (SqlDataReader rdr = cmd1.ExecuteReader())
                    {
                        if (rdr.Read())
                        {
                            patientField = rdr.GetString(rdr.GetOrdinal(stFeild));
                        }//if
                    }//using
                }//using
                con.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message.ToString());
                con.Close();
            }
            return patientField;
        }

        //GetLastCode DB
        //
        public int GetLastCode(string querey, string last)
        {
            try
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(querey, con))
                {
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        if (rdr.Read())
                        {
                            code = rdr.GetInt32(rdr.GetOrdinal(last));
                        }//if
                    }//using
                }//using
                con.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            return code;
        }

        // Display Old Finances
        //
        public void DisplayOldFinanceData(DataGridView dg, string orCode)
        {
            var bindingSource = new BindingSource();
            string query = "Select DISTINCT  orth_SessionCode AS 'Session Code' , orth_SessionDate AS 'Session Date',orth_Comments AS 'Comments',orth_TotalAmount As 'Total',orth_PaidAmount  AS 'Paid',orth_Discount AS 'Discount'   From Orthodontics_T Where orth_Code=" + orCode + " order by orth_SessionDate  ;";
            using (SqlDataAdapter dAdapter = new SqlDataAdapter(query, con))
            {
                try
                {
                    SqlCommandBuilder commBuilder = new SqlCommandBuilder(dAdapter);
                    DataTable table = new DataTable();
                    dAdapter.Fill(table);
                    bindingSource.DataSource = table;
                    dg.ReadOnly = true;
                    dg.DataSource = bindingSource;
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
                finally
                {
                    //con.Close();
                }
            }
        }
        // Add To New Patient Data || Upate Basic Patient Data 
        // 

        public void UpdateData_Ortho(DataGridView dg, string orCode, string orName, string orAge, string orGender, string orPhone, string orAdress, string orFollow, DateTimePicker nextSess, string total)
        {
            try
            {
                int rr = dg.Rows.Count;
                con.Open();
                //foreach (DataGridViewRow r in dg.Rows)
                //{
                string q_updateOrthoData = " Update Orthodontics_T  Set       orth_Name=@orth_Name,orth_Age=@orth_Age,orth_Gender=@orth_Gender,orth_Phone=@orth_Phone,orth_Adress=@orth_Adress,orth_Follow = @orth_Follow,"
                                                                                                                + "orth_NextSessionDate=@orth_NextSessionDate Where orth_Code=@orth_Code ";
                SqlCommand command = new SqlCommand(q_updateOrthoData, con);
                command.Parameters.AddWithValue("@orth_Code", orCode);
                command.Parameters.AddWithValue("@orth_Name", orName.Trim());
                command.Parameters.AddWithValue("@orth_Age", orAge);
                command.Parameters.AddWithValue("@orth_Gender", orGender.Trim());
                command.Parameters.AddWithValue("@orth_Phone", orPhone.Trim());
                command.Parameters.AddWithValue("@orth_Adress", orAdress.Trim());
                command.Parameters.AddWithValue("@orth_Follow", orFollow.Trim());
                command.Parameters.AddWithValue("@orth_NextSessionDate", nextSess.Value.Date); // next session
                command.Parameters.AddWithValue("@orth_TotalAmount", total); // total




                ////data grid view data
                //command.Parameters.AddWithValue("@orth_SessionCode", r.Cells[0].Value); // session code
                //command.Parameters.AddWithValue("@orth_SessionDate", r.Cells[1].Value); // session date
                //command.Parameters.AddWithValue("@orth_Comments", r.Cells[2].Value.ToString().Trim());//comments
                //command.Parameters.AddWithValue("@orth_RequiredAmount", r.Cells[3].Value); //required
                //command.Parameters.AddWithValue("@orth_PaidAmount", r.Cells[4].Value);//paid
                //command.Parameters.AddWithValue("@orth_Discount", r.Cells[5].Value);//discount

                command.ExecuteNonQuery();
                //}//for

                con.Close();
            }//try
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }


        // Add To Old Finances Records
        //
        public void AddToOldFinancesRecord(string orCode, string orName, string orAge, string orGender, string orPhone, string orAdress, string comment, string orFollow, string sessCode, DateTimePicker sessDate, DateTimePicker sessNextDate, string total, string req, string paid, string discount)
        {
            try
            {
                con.Open();
                string insert_addPatient = "INSERT INTO Orthodontics_T (orth_Code,orth_Name,orth_Age,orth_Gender,orth_Phone,orth_Adress,orth_Follow,orth_SessionCode,orth_SessionDate,orth_Comments,orth_TotalAmount,orth_RequiredAmount,orth_PaidAmount,orth_Discount,orth_NextSessionDate)"
                                                    + "VALUES (@orth_Code,@orth_Name,@orth_Age,@orth_Gender,@orth_Phone,@orth_Adress,@orth_Follow,@orth_SessionCode,@orth_SessionDate,@orth_Comments,@orth_TotalAmount,@orth_RequiredAmount,@orth_PaidAmount,@orth_Discount,@orth_NextSessionDate)";

                SqlCommand command = new SqlCommand(insert_addPatient, con);
                command.Parameters.AddWithValue("@orth_Code", orCode);
                command.Parameters.AddWithValue("@orth_Name", orName.Trim());
                command.Parameters.AddWithValue("@orth_Age", orAge);
                command.Parameters.AddWithValue("@orth_Gender", orGender.Trim());
                command.Parameters.AddWithValue("@orth_Phone", orPhone.Trim());
                command.Parameters.AddWithValue("@orth_Adress", orAdress.Trim());
                command.Parameters.AddWithValue("@orth_Comments", comment.Trim());
                command.Parameters.AddWithValue("@orth_Follow", orFollow.Trim());
                command.Parameters.AddWithValue("@orth_SessionCode", sessCode);
                command.Parameters.AddWithValue("@orth_SessionDate", sessDate.Value.Date);
                command.Parameters.AddWithValue("@orth_NextSessionDate", sessNextDate.Value.Date);
                command.Parameters.AddWithValue("@orth_TotalAmount", total);
                command.Parameters.AddWithValue("@orth_RequiredAmount", req);
                command.Parameters.AddWithValue("@orth_PaidAmount", paid);
                command.Parameters.AddWithValue("@orth_Discount", discount);
                command.ExecuteNonQuery();
                con.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

    }
}