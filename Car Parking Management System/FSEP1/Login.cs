using System;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;

namespace FSEP1
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            BackgroundPanel.BackColor = Color.FromArgb(70, Color.White);
            FooterPanel.BackColor = Color.FromArgb(70, Color.White);
            label1.BackColor = Color.FromArgb(70, Color.White);
            linkLabel1.BackColor = Color.FromArgb(70, Color.White);
            gradientRectangle1.BackColor = Color.FromArgb(70, Color.White);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.youtube.com/@MenTalksbyAH");
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = UserNameBox.Text;
            string password = PasswordBox.Text;

            if (VerifyUser(username, password))
            {
                MessageBox.Show("Welcome Admin! You Have Been Logged In Successfully");
                this.Hide();
                Program.menu.Show();
            }
            else
            {
                MessageBox.Show("Invalid username or password!");
            }
        }

        private bool VerifyUser(string username, string password)
        {
            bool isValidUser = false;
            string connectionString = "Data Source=test.db;Version=3;";

            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                string sql = "SELECT * FROM users WHERE username = @username AND password = @password";
                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);

                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            isValidUser = true;
                        }
                    }
                }
                conn.Close();
            }

            return isValidUser;
        }

        private void Login_VisibleChanged(object sender, EventArgs e)
        {
            UserNameBox.Clear();
            PasswordBox.Clear();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                PasswordBox.UseSystemPasswordChar = false;
            }
            else
            {
                PasswordBox.UseSystemPasswordChar = true;
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            Program.signup.Show();
        }
    }
}
