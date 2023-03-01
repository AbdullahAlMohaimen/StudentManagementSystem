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

namespace StudentManagementSystem
{
    public partial class AddStudent : Form
    {
        //Connection String
        string connectionString = "Data Source = DESKTOP-S3JA9SK; Initial Catalog = StudentMS; User ID = sa; Password = 123456";
        DateTime dateTime = DateTime.UtcNow.Date;    //Current Date
        public AddStudent()
        {
            InitializeComponent();
        }

        private void AddStudent_Load(object sender, EventArgs e)
        {
            //PRINT CURRENT DATE
            txt_Date.Text = dateTime.ToString("dd/MM/yyyy");
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            txt_Name.Clear();
            txt_Department.Text = "";
            //txt_Semester.Clear();
            //txt_Cgpa.Clear();
            txt_Address.Clear();
            txt_Email.Clear();
            txt_Phone.Clear();
            txt_Gender.Text = "";
            txt_Religion.Text = "";
            txt_Password.Clear();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string name, department, gender, religion, address, email, dob, phoneNo,password;
            float semester, cgpa;

            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            name = txt_Name.Text;
            department = txt_Department.Text;
            dob = txt_Dob.Value.ToString("yyyy-MM-dd");
            gender = txt_Gender.Text;
            religion = txt_Religion.Text;
            address = txt_Address.Text;
            email = txt_Email.Text;
            phoneNo = txt_Phone.Text;
            password = txt_Password.Text;

            semester = (float)Convert.ToDecimal(txt_Semester.Text);
            cgpa = (float)Convert.ToDecimal(txt_Cgpa.Text);

            using (SqlTransaction insertTran=conn.BeginTransaction())
            {
                try
                {
                    SqlCommand command = new SqlCommand("Insert into Student(student_Name,student_Dept,student_Semester,student_CGPA,student_DOB,student_Gender,student_Religion,student_Address,student_Email,student_PhoneNo,student_Passeord) values('" + name + "','" + department + "','" + semester + "','" + cgpa + "','" + dob + "','" + gender + "','" + religion + "','" + address + "','" + email + "','" + phoneNo + "','" + password + "')", conn,insertTran);
                    command.ExecuteNonQuery();
                    insertTran.Commit();

                    MessageBox.Show("New student added, Successfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txt_Name.Clear();
                    txt_Department.Text = "";
                    //txt_Semester.Clear();
                    //txt_Cgpa.Clear();
                    txt_Address.Clear();
                    txt_Email.Clear();
                    txt_Phone.Clear();
                    txt_Gender.Text = "";
                    txt_Religion.Text = "";
                    txt_Password.Clear();
                }
                catch
                {
                    insertTran.Rollback();
                    MessageBox.Show("Something Problem", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    conn.Close();
                }
            }
                
        }
    }
}
