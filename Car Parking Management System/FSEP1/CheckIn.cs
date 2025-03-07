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
    public partial class CheckIn : Form
    {
        private DataGridView CheckInDataGridView = new DataGridView();
        private TextBox SearchBar = new TextBox();
        private Button SearchButton = new Button();
        private Button ClearButton = new Button();
        private Button HomeButton = new Button();
        public CheckIn()
        {
            InitializeComponent();
            panel1.BackColor = Color.FromArgb(100, Color.White);
            LoadCheckInDataGrid();
        }

        private void CheckIn_Load(object sender, EventArgs e)
        {
            LoadCheckInDataGrid();
        }

        private void LoadCheckInDataGrid()
        {
            CheckInDataGridView.DataSource = GetDataFromDatabase();
        }

        private DataTable GetDataFromDatabase()
        {
            DataTable dt = new DataTable();
            string connectionString = "Data Source=test.db;Version=3;";
            string sql = "SELECT * FROM Check_In";

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

        private void button2_Click(object sender, EventArgs e)
        {
            FullNameBox.Clear();
            CNICBox.Clear();
            CarNumberBox.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.menu.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button4.Enabled = true;
            InsertCheckInData();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            PrintSlip();
        }
        private void PrintSlip()
        {
            string fullName = FullNameBox.Text;
            string cnic = CNICBox.Text;
            string carNumber = CarNumberBox.Text;
            string checkInTime = DateTime.Now.ToString();

            SlipForm slipForm = new SlipForm(fullName, cnic, carNumber, checkInTime);
            slipForm.Show();
        }

        private void SearchMember(string carNumber)
        {
            string connectionString = "Data Source=test.db;Version=3;";
            string sql = @"SELECT COUNT(*) 
                   FROM Check_In 
                   WHERE car_number = @carNumber";

            int count = 0;

            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@carNumber", carNumber);
                    count = Convert.ToInt32(cmd.ExecuteScalar());
                }
                conn.Close();
            }

            if (count !=1)
            {
                string sql2 = @"SELECT full_name, cnic 
                FROM Registration 
                WHERE car_number_plate = @carNumber";

                using (SQLiteConnection conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();

                    using (SQLiteCommand cmd = new SQLiteCommand(sql2, conn))
                    {
                        cmd.Parameters.AddWithValue("@carNumber", carNumber);
                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                FullNameBox.Text = reader["full_name"].ToString();
                                CNICBox.Text = reader["cnic"].ToString();
                                CarNumberBox.Text = carNumber;
                                MessageBox.Show("User exists!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                        }
                    }
                    conn.Close();
                }

            }
            MessageBox.Show("No record found for the provided car number.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
         private void SearchCheckIn(string carNum)
         {
             DataTable dt = new DataTable();
             string connectionString = "Data Source=test.db;Version=3;";
             string sql = @"SELECT full_name, cnic, car_number_plate 
             FROM Registration 
             WHERE car_number_plate = @carNum";

             using (SQLiteConnection conn = new SQLiteConnection(connectionString))
             {
                 conn.Open();
                 using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                 {
                     cmd.Parameters.AddWithValue("@carNum", "%" + carNum + "%");
                     using (SQLiteDataReader reader = cmd.ExecuteReader())
                     {
                         dt.Load(reader);
                     }
                 }
                 conn.Close();
             }
             dataGridView1.DataSource = dt;
         }


        private void InsertCheckInData()
        {
            string connectionString = "Data Source=test.db;Version=3;";
            string sql = @"INSERT INTO Check_In (full_name, cnic, car_number) 
                   VALUES (@fullName, @cnic, @carNumber)";

            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@fullName", FullNameBox.Text);
                    cmd.Parameters.AddWithValue("@cnic", CNICBox.Text);
                    cmd.Parameters.AddWithValue("@carNumber", CarNumberBox.Text);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }

            MessageBox.Show("Check-in data saved successfully!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void FullNameBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void CarNumberBox_TextChanged(object sender, EventArgs e)
        {

        }


        private void CNICBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            SearchCheckIn(textBox4.Text);
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            string carNumber = CarNumberBox.Text;
            SearchMember(carNumber);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
