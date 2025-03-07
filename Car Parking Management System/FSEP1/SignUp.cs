using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;
using System.Data.SQLite;
using System.IO;
using System.Text.RegularExpressions;

namespace FSEP1
{
    public partial class SignUp : Form
    {
        string RandomCode;
        public static String to;

        public SignUp()
        {
            InitializeComponent();
            DatabaseInitializer.CreateTables();
            panel1.BackColor = Color.FromArgb(100, Color.White);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(UsernameInput.Text))
            {
                MessageBox.Show("Username Can't Be Empty");
            }
            else if (!Regex.IsMatch(UsernameInput.Text, @"^[a-zA-Z0-9]+$"))
            {
                MessageBox.Show("Username should only contain alphanumeric characters");
            }
            else if (string.IsNullOrEmpty(PasswordInputBox.Text))
            {
                MessageBox.Show("Password Can't Be Empty");
            }
            else if (PasswordInputBox.Text.Length < 4 || PasswordInputBox.Text.Length > 16)
            {
                MessageBox.Show("Password must be between 8 and 16 characters long");
            }
            else if (string.IsNullOrEmpty(PasswordInputConfirmBox.Text))
            {
                MessageBox.Show("Your Passwords Didn't match");
            }
            else if (PasswordInputBox.Text != PasswordInputConfirmBox.Text)
            {
                MessageBox.Show("Passwords didn't match!");
            }
            else
            {
                if (string.IsNullOrEmpty(txtGmail.Text))
                {
                    MessageBox.Show("Your Gmail is Empty");
                }
                else
                {
                    string from, pass, messagebody;
                    Random rand = new Random();
                    RandomCode = (rand.Next(999999)).ToString();
                    MailMessage message = new MailMessage();
                    string to = txtGmail.Text;
                    from = "bloodmachine300@gmail.com";
                    pass = "xsifnqdlxxrjxvbt";
                    messagebody = "Your Confirmation Code is " + RandomCode + "\nYour UserName is : " + "\"" + UsernameInput.Text + "\"" + "\nYour Password is : " + "\"" + PasswordInputBox.Text + "\"";
                    message.To.Add(to);
                    message.From = new MailAddress(from);
                    message.Body = messagebody;
                    message.Subject = "Car Parking Management System - Verification Code";
                    SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                    smtp.EnableSsl = true;
                    smtp.Port = 587;
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Credentials = new NetworkCredential(from, pass);
                    try
                    {
                        smtp.Send(message);
                        MessageBox.Show("Verification Code has been Sent!, Check your mail", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        UsernameInput.Enabled = false;
                        PasswordInputBox.Enabled = false;
                        PasswordInputConfirmBox.Enabled = false;
                        txtGmail.Enabled = false;
                        txtConfirm.Enabled = true;
                        button2.Enabled = true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }
        private void SignUp_Load(object sender, EventArgs e)
        {

        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            if (RandomCode == (txtConfirm.Text).ToString())
            {
                to = txtGmail.Text;
                this.Hide();

                using (SQLiteConnection conn = new SQLiteConnection("Data Source=test.db;Version=3;"))
                {
                    conn.Open();
                    string sql = "INSERT INTO users (username, email, password) VALUES (@username, @email, @password)";
                    using (SQLiteCommand cmmd = new SQLiteCommand(sql, conn))
                    {
                        cmmd.Parameters.AddWithValue("@username", UsernameInput.Text);
                        cmmd.Parameters.AddWithValue("@email", txtGmail.Text);
                        cmmd.Parameters.AddWithValue("@password", PasswordInputBox.Text);
                        cmmd.ExecuteNonQuery();
                    }
                    conn.Close();
                }
                //LoadUserDataGridView(UserDataGridView1);
                MessageBox.Show("User Registered Successfully!");

                Program.login.Show();

            }
            else
            {
                MessageBox.Show("Wrong Code!");
            }

        }

        private void gradientRectangle1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void PasswordInputBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void PasswordInputConfirmBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void UsernameInput_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
