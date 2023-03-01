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
    public partial class AdminLogin : Form
    {
        string connectionString = "Data Source = DESKTOP-S3JA9SK; Initial Catalog = StudentMS; User ID = sa; Password = 123456";
        public AdminLogin()
        {
            InitializeComponent();
        }


        //CLEAR BUTTON
        private void button2_Click(object sender, EventArgs e)
        {
            txt_Email.Clear();
            txt_Password.Clear();

            txt_Email.Focus();
        }


        //LOGIN BUTTON
        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            String adminEmail, adminPassword;
            adminEmail = txt_Email.Text;
            adminPassword = txt_Password.Text;

            try
            {
                if(adminEmail == "" || adminPassword == "")
                {
                    MessageBox.Show("Please enter your email and password first!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    SqlCommand command = new SqlCommand("select * from Admin where Admin_Email='" + adminEmail + "' and Admin_Password='" + adminPassword + "'", conn);
                    SqlDataReader dr = command.ExecuteReader();

                    if (dr.Read())
                    {
                        StudentMS form2 = new StudentMS();
                        form2.SetEmail = txt_Email.Text;
                        form2.Show();
                        this.Hide();
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

        private void AdminLogin_Load(object sender, EventArgs e)
        {

        }

        private void txt_Password_TextChanged(object sender, EventArgs e)
        {
            txt_Password.PasswordChar = '*';
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            First f = new First();
            f.Show();
            this.Hide();
        }
    }
}












//SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-S3JA9SK\SQLEXPRESS;Initial Catalog=StudentMS;Integrated Security=True");
/*String querry = "select * from Admin where Admin_Email='" + adminEmail + "' and Admin_Password='" + adminPassword + "'";
                SqlDataAdapter sda = new SqlDataAdapter(querry, conn);

                DataTable dtable = new DataTable();
                sda.Fill(dtable);*/
//dtable.Rows.Count > 0