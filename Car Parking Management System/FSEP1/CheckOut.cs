using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.IO;

namespace FSEP1
{
    public partial class CheckOut : Form
    {
        private DataGridView CheckOutDataGridView = new DataGridView();

        public CheckOut()
        {
            InitializeComponent();
            panel1.BackColor = Color.FromArgb(100, Color.White);
        }

        private void CheckOut_Load(object sender, EventArgs e)
        {
            LoadCheckOutDataGrid();
        }

        private void LoadCheckOutDataGrid()
        {
            CheckOutDataGridView.DataSource = GetDataFromDatabase();
        }

        private DataTable GetDataFromDatabase()
        {
            DataTable dt = new DataTable();
            string connectionString = "Data Source=test.db;Version=3;";
            string sql = "SELECT * FROM Check_Out";

            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        dt.Load(reader);
                    }
                }
                conn.Close();
            }
            return dt;
        }

        private void PicturePanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.menu.Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }
        private bool VerifyCheckIn(string checkID)
        {
            string connectionString = "Data Source=test.db;Version=3;";
            string sql = @"SELECT COUNT(*) FROM Check_In WHERE checkin_id = @checkID";

            int count = 0;

            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@checkID", checkID);
                    count = Convert.ToInt32(cmd.ExecuteScalar());
                }
                conn.Close();
            }

            return count > 0;
        }

        private void SearchCheckIn(string checkinID)
        {
            if (VerifyCheckIn(checkinID))
            {
                DataTable dt = new DataTable();
                string connectionString = "Data Source=test.db;Version=3;";
                string sql = @"SELECT 
                                CI.full_name,
                                CI.phone_number,
                                CI.car_number_plate,
                                CI.car_type,
                                CI.driver_type,
                                CI.checkin_time 
                            FROM 
                                (SELECT 
                                    (SELECT full_name FROM Registration WHERE registration_id = CI.registration_id) AS full_name,
                                    (SELECT phone_number FROM Registration WHERE registration_id = CI.registration_id) AS phone_number,
                                    (SELECT car_number_plate FROM Registration WHERE registration_id = CI.registration_id) AS car_number_plate,
                                    (SELECT car_type FROM Car_Information WHERE car_id = (SELECT car_id FROM Registration WHERE registration_id = CI.registration_id)) AS car_type,
                                    (SELECT driver_type FROM Driver_Information WHERE driver_id = (SELECT driver_id FROM Registration WHERE registration_id = CI.registration_id)) AS driver_type,
                                    CI.checkin_time 
                                FROM 
                                    Check_In CI
                                WHERE 
                                    CI.checkin_id = @checkinID) CI";

                using (SQLiteConnection conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();
                    using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@checkinID", checkinID);
                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                label5.Text = reader["full_name"].ToString();
                                label7.Text = reader["phone_number"].ToString();
                                label10.Text = reader["car_number_plate"].ToString();
                                label12.Text = reader["car_type"].ToString();
                                label14.Text = reader["driver_type"].ToString();
                                label16.Text = reader["checkin_time"].ToString();
                            }
                            else
                            {
                                MessageBox.Show("No record found for the provided check-in ID.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                label5.Text = "";
                                label7.Text = "";
                                label10.Text = "";
                                label12.Text = "";
                                label14.Text = "";
                                label16.Text = "";
                            }
                        }
                    }
                    conn.Close();
                }
            }
            else
            {
                MessageBox.Show("No record found for the provided check-in ID.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                label5.Text = "";
                label7.Text = "";
                label10.Text = "";
                label12.Text = "";
                label14.Text = "";
                label16.Text = "";
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                SearchCheckIn(textBox1.Text);
            }
            else
            {
                MessageBox.Show("Please enter a check-in ID.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            label5.Text = "";
            label7.Text = "";
            label10.Text = "";
            label12.Text = "";
            label14.Text = "";
            label16.Text = "";
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }
        private void UpdateCheckOut(string checkinID, double parkingFee)
        {
            string connectionString = "Data Source=test.db;Version=3;";
            string updateCheckOutSql = @"INSERT INTO Check_Out (checkin_id, checkout_time, total_amount) 
                                    VALUES (@checkinID, @checkoutTime, @totalAmount)";

            DateTime checkoutTime = DateTime.Now;

            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(updateCheckOutSql, conn))
                {
                    cmd.Parameters.AddWithValue("@checkinID", checkinID);
                    cmd.Parameters.AddWithValue("@checkoutTime", checkoutTime);
                    cmd.Parameters.AddWithValue("@totalAmount", parkingFee);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }

            MessageBox.Show($"Parking fee: Rs. {parkingFee}", "Parking Fee", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private double CalculateParkingFee(string checkinID)
        {
            string connectionString = "Data Source=test.db;Version=3;";
            string sql = @"SELECT 
                    checkin_time 
                FROM 
                    Check_In 
                WHERE 
                    checkin_id = @checkinID";

            DateTime checkinTime;

            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@checkinID", checkinID);
                    checkinTime = Convert.ToDateTime(cmd.ExecuteScalar());
                }
                conn.Close();
            }

            TimeSpan duration = DateTime.Now - checkinTime;
            double hours = duration.TotalHours;
            double parkingFee = hours * 50;
            MessageBox.Show($"Parking fee: Rs. {parkingFee}", "Parking Fee", MessageBoxButtons.OK, MessageBoxIcon.Information);

            return parkingFee;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                string checkinID = textBox1.Text;
                double parkingFee = CalculateParkingFee(checkinID);
                UpdateCheckOut(checkinID, parkingFee);
            }
            else
            {
                MessageBox.Show("Please enter a check-in ID.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
