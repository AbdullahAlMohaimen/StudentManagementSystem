using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace StudentManagementSystem
{
    public partial class StudentMS : Form
    {
        string connectionString = "Data Source = DESKTOP-S3JA9SK; Initial Catalog = StudentMS; User ID = sa; Password = 123456";
        
        public StudentMS()
        {
            InitializeComponent();
        }
        //Collect Login Email
        public string Email;
        public string SetEmail
        {
            get { return Email; }
            set { Email = value; }
        }


        
        private void StudentMS_Load(object sender, EventArgs e)
        {
            //CURRENT DATE
            DateTime dateTime = DateTime.UtcNow.Date;
            txt_Date.Text = dateTime.ToString("dd/MM/yyyy");

            
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            try
            {
                SqlCommand command = new SqlCommand("select Admin_Name from Admin where Admin_Email='" + Email + "'", conn);
                SqlDataReader dr = command.ExecuteReader();
                if(dr.Read())
                {
                    string adminName = dr.GetString(0);
                    txt_AdminName.Text = adminName;
                }
            }
            catch
            {
                MessageBox.Show("Something Problem!");
            }
            finally
            {
                conn.Close();
            }

            
            try
            {
                conn.Open();
                SqlCommand command = new SqlCommand("select Student_ID as ID,student_Name as Name,student_Dept as Department,student_Semester as Semester,student_CGPA as CGPA,student_Gender as Gender,student_Religion as Religion,student_Email as Email,student_PhoneNo as PhoneNo from Student", conn);
                SqlDataAdapter sda = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                all_Student.DataSource = dt;
            }
            catch
            {
                MessageBox.Show("All Student Information Problem");
            }
            finally
            {
                conn.Close();
            }

        }



        private void Clear_Click(object sender, EventArgs e)
        {
            AddStudent st = new AddStudent();
            st.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            AdminLogin ad = new AdminLogin();
            ad.Show();
            this.Hide();
        }


        private void button4_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            try
            {
                SqlCommand command = new SqlCommand("select Student_ID as ID,student_Name as Name,student_Dept as Department,student_Semester as Semester,student_CGPA as CGPA,student_Gender as Gender,student_Religion as Religion,student_Email as Email,student_PhoneNo as PhoneNo from Student", conn);
                SqlDataAdapter sda = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                all_Student.DataSource = dt;
            }
            catch
            {
                MessageBox.Show("All Student Information Problem");
            }
            finally
            {
                conn.Close();
            }
        }



        private void all_Student_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            //MessageBox.Show(Convert.ToString(all_Student.CurrentCell.Value));
            //MessageBox.Show(Convert.ToString(all_Student.CurrentRow.Cells[0].Value);
            //var id = Convert.ToInt32(all_Student.Rows[e.RowIndex].Cells[0].Value);

            int id = (int)Convert.ToDouble(all_Student.CurrentRow.Cells[0].Value);

            try
            {
                SqlCommand command = new SqlCommand("select * from Student where student_ID='"+id+"'", conn);
                SqlDataReader dr = command.ExecuteReader();

                if (dr.Read())
                {
                    int studentID = dr.GetInt32(0);
                    txt_ID.Text = Convert.ToString(studentID);

                    string studentName = dr.GetString(1);
                    txt_Name.Text = studentName;

                    string studentDept = dr.GetString(2);
                    txt_Department.Text = studentDept;

                    float studentSemester = (float)dr.GetDouble(3);
                    txt_Semester.Text = Convert.ToString(studentSemester);

                    float studentCgpa = (float)dr.GetDouble(4);
                    txt_Cgpa.Text = Convert.ToString(studentCgpa);

                    DateTime studentDob = dr.GetDateTime(5);
                    txt_Dob.Text = Convert.ToString(studentDob.ToString("dd/MMyyyy"));

                    string studentGender = dr.GetString(6);
                    txt_Gender.Text = studentGender;

                    string studentReligion = dr.GetString(7);
                    txt_Religion.Text = studentReligion;

                    string studentAddress = dr.GetString(8);
                    txt_Address.Text = studentAddress;
                    
                    string studentEmail = dr.GetString(9);
                    txt_Email.Text = studentEmail;

                    string studentPhoneNo = dr.GetString(10);
                    txt_PhoneNo.Text = studentPhoneNo;
                }
            }
            catch
            {
                MessageBox.Show("Invalied Student ID\nSelect Correct Student ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                conn.Close();
            }
        }



        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            txt_Semester.Enabled = true;
            txt_Cgpa.Enabled = true;
            txt_Address.Enabled = true;
            txt_Email.Enabled = true;
            txt_PhoneNo.Enabled = true;
        }



        private void button2_Click(object sender, EventArgs e)
        {
            string address, email, phoneNo;
            float semester, cgpa;
            
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            using (SqlTransaction updateTran = conn.BeginTransaction())
            {
                try
                {
                    int id = Convert.ToInt32(txt_ID.Text);

                    address = txt_Address.Text;
                    email = txt_Email.Text;
                    phoneNo = txt_PhoneNo.Text;
                    semester = (float)Convert.ToDecimal(txt_Semester.Text);
                    cgpa = (float)Convert.ToDecimal(txt_Cgpa.Text);

                    SqlCommand command = new SqlCommand("Update Student set student_Semester='" + semester + "',student_CGPA='" + cgpa + "',student_Address='" + address + "',student_Email='" + email + "',student_PhoneNo='" + phoneNo + "' where student_ID='" + id + "'", conn,updateTran);
                    command.ExecuteNonQuery();
                    updateTran.Commit();

                    MessageBox.Show("Update Successfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch
                {
                    updateTran.Rollback();
                    MessageBox.Show("Update Failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                finally
                {
                    conn.Close();
                }
            }
                
        }



        private void button3_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            using (SqlTransaction deleteTran = conn.BeginTransaction())
            {
                try
                {
                    int id = Convert.ToInt32(txt_ID.Text);
                    SqlCommand command = new SqlCommand("Delete Student where student_ID='" + id + "'", conn, deleteTran);
                    command.ExecuteNonQuery();
                    deleteTran.Commit();

                    MessageBox.Show("Deleted Successfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txt_ID.Clear();
                    txt_Name.Clear();
                    txt_Department.Clear();
                    txt_Semester.Clear();
                    txt_Cgpa.Clear();
                    txt_Dob.Clear();
                    txt_Gender.Clear();
                    txt_Religion.Clear();
                    txt_Address.Clear();
                    txt_Email.Clear();
                    txt_PhoneNo.Clear();

                }
                catch
                {
                    deleteTran.Rollback();
                    MessageBox.Show("Delete Failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                finally
                {
                    conn.Close();
                }
            }
                
        }

        private void button6_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            int searchID = Convert.ToInt32(txt_SearchID.Text);

            try
            {
                SqlCommand command = new SqlCommand("select * from Student where student_ID='" + searchID + "'", conn);
                SqlDataReader dr = command.ExecuteReader();

                if (dr.Read())
                {
                    int studentID = dr.GetInt32(0);
                    txt_ID.Text = Convert.ToString(studentID);

                    string studentName = dr.GetString(1);
                    txt_Name.Text = studentName;

                    string studentDept = dr.GetString(2);
                    txt_Department.Text = studentDept;

                    float studentSemester = (float)dr.GetDouble(3);
                    txt_Semester.Text = Convert.ToString(studentSemester);

                    float studentCgpa = (float)dr.GetDouble(4);
                    txt_Cgpa.Text = Convert.ToString(studentCgpa);

                    DateTime studentDob = dr.GetDateTime(5);
                    txt_Dob.Text = Convert.ToString(studentDob.ToString("dd/MMyyyy"));

                    string studentGender = dr.GetString(6);
                    txt_Gender.Text = studentGender;

                    string studentReligion = dr.GetString(7);
                    txt_Religion.Text = studentReligion;

                    string studentAddress = dr.GetString(8);
                    txt_Address.Text = studentAddress;

                    string studentEmail = dr.GetString(9);
                    txt_Email.Text = studentEmail;

                    string studentPhoneNo = dr.GetString(10);
                    txt_PhoneNo.Text = studentPhoneNo;
                }
            }
            catch
            {
                MessageBox.Show("Invalied Student ID\nSelect Correct Student ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                conn.Close();
            }

        }

        private void button7_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string search = txt_search.Text; //for department, gender, Religion, Address
            //float searchBySemester = (float)Convert.ToDouble(txt_search.Text);
            try
            {
                SqlCommand command = new SqlCommand("select Student_ID as ID,student_Name as Name,student_Dept as Department,student_Semester as Semester,student_CGPA as CGPA,student_Gender as Gender,student_Religion as Religion,student_Email as Email,student_PhoneNo as PhoneNo from Student where student_Dept='"+search+ "' or student_Gender='"+search+ "' or student_Religion='"+search+ "'", conn);
                SqlDataAdapter sda = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                all_Student.DataSource = dt;
            }
            catch
            {
                MessageBox.Show("Search Student Information Problem");
            }
            finally
            {
                conn.Close();
            }


        }
    }
}
