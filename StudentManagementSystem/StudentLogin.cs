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
    public partial class StudentLogin : Form
    {
        string connectionString = "Data Source = DESKTOP-S3JA9SK; Initial Catalog = StudentMS; User ID = sa; Password = 123456";
        public StudentLogin()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            First f = new First();
            f.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            String studentEmail, studentPassword;
            studentEmail = txt_Email.Text;
            studentPassword = txt_Password.Text;

            try
            {
                if (studentEmail == "" || studentPassword == "")
                {
                    MessageBox.Show("Please enter your email and password first!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    SqlCommand command = new SqlCommand("select * from Student where student_Email='" + studentEmail + "' and student_Passeord='" + studentPassword + "'", conn);
                    SqlDataReader dr = command.ExecuteReader();

                    if (dr.Read())
                    {
                        StudentInfo.Student s = new StudentInfo.Student();
                        s.SetEmail = txt_Email.Text;
                        s.Show();
                        this.Hide();
                        
                        /*StudentInformation st = new StudentInformation();
                        st.SetEmail = txt_Email.Text;
                        st.Show();
                        this.Hide();*/
                    }

                    else
                    {
                        MessageBox.Show("Invalid login details", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txt_Email.Clear();
                        txt_Password.Clear();

                        txt_Email.Focus();
                    }
                }
            }
            catch
            {
                MessageBox.Show("Error");
            }
            finally
            {
                conn.Close();
            }

        }

        private void Clear_Click(object sender, EventArgs e)
        {
            txt_Email.Clear();
            txt_Password.Clear();
        }

        private void txt_Password_TextChanged(object sender, EventArgs e)
        {
            txt_Password.PasswordChar = '*';
        }

        private void StudentLogin_Load(object sender, EventArgs e)
        {

        }
    }
}
