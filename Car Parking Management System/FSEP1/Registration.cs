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
    public partial class Registration : Form
    {
        private DataGridView RegistrationDataGridView = new DataGridView();

        public Registration()
        {
            InitializeComponent();
            panel1.BackColor = Color.FromArgb(130, Color.White);
            LoadCarTypes();
            LoadDriverTypes();

        }

        private void Registration_Load(object sender, EventArgs e)
        {
            LoadRegistrationDataGrid();
        }

        private void LoadRegistrationDataGrid()
        {
            RegistrationDataGridView.DataSource = GetDataFromDatabase();
        }

        private DataTable GetDataFromDatabase()
        {
            DataTable dt = new DataTable();
            string connectionString = "Data Source=test.db;Version=3;";
            string sql = "SELECT * FROM Registration";

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

        private void LoadCarTypes()
        {
            string connectionString = "Data Source=test.db;Version=3;";
            string sql = "SELECT DISTINCT car_type FROM Car_Information";

            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ComboBoxCarType.Items.Add(reader["car_type"].ToString());
                        }
                    }
                }
                conn.Close();
            }
        }

        private void LoadDriverTypes()
        {
            string connectionString = "Data Source=test.db;Version=3;";
            string sql = "SELECT DISTINCT driver_type FROM Driver_Information";

            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ComboBoxDriverType.Items.Add(reader["driver_type"].ToString());
                        }
                    }
                }
                conn.Close();
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox4.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.menu.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ClearInputFields();

        }

        private void DescriptionBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void ComboBoxDriverType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void NameEntryBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void ComboBoxCarType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void NumberPlateBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void PhoneNumberEntryBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void CNICEntryBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                InsertRegistrationData();
                LoadRegistrationDataGrid();
                MessageBox.Show("Registration Information Saved Successfully!");
                ClearInputFields();
            }
        }
        private bool ValidateInput()
        {
            if (string.IsNullOrEmpty(NameEntryBox.Text))
            {
                MessageBox.Show("Please enter the driver's name!");
                return false;
            }
            if (string.IsNullOrEmpty(PhoneNumberEntryBox.Text))
            {
                MessageBox.Show("Please enter the phone number!");
                return false;
            }
            if (string.IsNullOrEmpty(CNICEntryBox.Text))
            {
                MessageBox.Show("Please enter the CNIC number!");
                return false;
            }
            if (string.IsNullOrEmpty(NumberPlateBox.Text))
            {
                MessageBox.Show("Please enter the car number plate!");
                return false;
            }
            if (string.IsNullOrEmpty(ComboBoxDriverType.Text))
            {
                MessageBox.Show("Please select the driver type!");
                return false;
            }
            if (string.IsNullOrEmpty(ComboBoxCarType.Text))
            {
                MessageBox.Show("Please select the car type!");
                return false;
            }
            return true;
        }
        private void InsertRegistrationData()
        {
            string connectionString = "Data Source=test.db;Version=3;";
            string sql = @"INSERT INTO Registration (driver_id, car_id, phone_number, cnic, car_number_plate, gender, full_name, description) 
                   VALUES (@driver_id, @car_id, @phone_number, @cnic, @car_number_plate, @gender, @full_name, @description)";

            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@driver_id", GetDriverID());
                    cmd.Parameters.AddWithValue("@car_id", GetCarID());
                    cmd.Parameters.AddWithValue("@phone_number", PhoneNumberEntryBox.Text);
                    cmd.Parameters.AddWithValue("@cnic", CNICEntryBox.Text);
                    cmd.Parameters.AddWithValue("@car_number_plate", NumberPlateBox.Text);
                    cmd.Parameters.AddWithValue("@gender", radioButton1.Checked ? "Male" : "Female");
                    cmd.Parameters.AddWithValue("@full_name", NameEntryBox.Text); // Add full name parameter
                    cmd.Parameters.AddWithValue("@description", DescriptionBox.Text);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }

        private int GetDriverID()
        {
            int driverID = 0;
            string connectionString = "Data Source=test.db;Version=3;";
            string sql = "SELECT driver_id FROM Driver_Information WHERE driver_type = @driver_type";

            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@driver_type", ComboBoxDriverType.Text);
                    driverID = Convert.ToInt32(cmd.ExecuteScalar());
                }
                conn.Close();
            }

            return driverID;
        }
        private int GetCarID()
        {
            int carID = 0;
            string connectionString = "Data Source=test.db;Version=3;";
            string sql = "SELECT car_id FROM Car_Information WHERE car_type = @car_type";

            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@car_type", ComboBoxCarType.Text);
                    carID = Convert.ToInt32(cmd.ExecuteScalar());
                }
                conn.Close();
            }

            return carID;
        }

        private void ClearInputFields()
        {
            NameEntryBox.Clear();
            PhoneNumberEntryBox.Clear();
            CNICEntryBox.Clear();
            NumberPlateBox.Clear();
            DescriptionBox.Clear();
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            ComboBoxCarType.Text = string.Empty;
            ComboBoxDriverType.Text = string.Empty;
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string searchTerm = textBox4.Text;
            SearchRegistrationInformation(searchTerm);
        }
        private void SearchRegistrationInformation(string searchTerm)
        {
            DataTable dt = new DataTable();
            string connectionString = "Data Source=test.db;Version=3;";
            string sql = @"SELECT R.registration_id, R.phone_number, R.cnic, R.car_number_plate, R.gender, R.description 
                           FROM Registration R 
                           WHERE R.car_number_plate LIKE @searchTerm";

            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@searchTerm", "%" + searchTerm + "%");
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        dt.Load(reader);
                    }
                }
                conn.Close();
            }
            dataGridView1.DataSource = dt;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
