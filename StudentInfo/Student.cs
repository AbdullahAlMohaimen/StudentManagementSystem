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

namespace StudentInfo
{
    public partial class Student : Form
    {
        string connectionString = "Data Source = DESKTOP-S3JA9SK; Initial Catalog = StudentMS; User ID = sa; Password = 123456";

        public Student()
        {
            InitializeComponent();
        }

        public string Email;
        public string SetEmail
        {
            get { return Email; }
            set { Email = value; }
        }

        private void Student_Load(object sender, EventArgs e)
        {
            DateTime dateTime = DateTime.UtcNow.Date;
            txt_Date.Text = dateTime.ToString("dd/MM/yyyy");

            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            try
            {
                SqlCommand command = new SqlCommand("select * from Student where student_Email='" + Email + "'", conn);
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

                    string studentPassword = dr.GetString(11);
                    txt_Password.Text = studentPassword;
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
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            try
            {
                SqlCommand command = new SqlCommand("select * from Student where student_Email='" + Email + "'", conn);
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

                    string studentPassword = dr.GetString(11);
                    txt_Password.Text = studentPassword;
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
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txt_PhoneNo.Enabled = true;
            txt_Password.Enabled = true;
            txt_Address.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string address, password, phoneNo;


            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            using (SqlTransaction updateTran = conn.BeginTransaction())
            {
                try
                {
                    int id = Convert.ToInt32(txt_ID.Text);

                    address = txt_Address.Text;
                    phoneNo = txt_PhoneNo.Text;
                    password = txt_Password.Text;

                    SqlCommand command = new SqlCommand("Update Student set student_Address='" + address + "',student_PhoneNo='" + phoneNo + "', student_Passeord='" + password + "' where student_ID='" + id + "'", conn,updateTran);
                    command.ExecuteNonQuery();
                    updateTran.Commit();

                    MessageBox.Show("Successfully Updated", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
    }
}
