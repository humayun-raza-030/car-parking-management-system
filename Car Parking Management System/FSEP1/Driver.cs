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
    public partial class Driver : Form
    {
        private DataGridView DriverDataGridView = new DataGridView();

        public Driver()
        {
            InitializeComponent();
            panel1.BackColor = Color.FromArgb(90, Color.White);
        }

        private void Driver_Load(object sender, EventArgs e)
        {
        }

        private void LoadDriverDataGrid()
        {
            DriverDataGridView.DataSource = GetDataFromDatabase();
        }

        private DataTable GetDataFromDatabase()
        {
            DataTable dt = new DataTable();
            string connectionString = "Data Source=test.db;Version=3;";
            string sql = "SELECT * FROM Driver_Information";

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

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SearchDriver(SearchBar.Text);

        }

        private void button3_Click(object sender, EventArgs e)
        {
            DriverEntryBox.Clear();
            DescriptionInput.Clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dataGridView1.ClearSelection();
            SearchBar.Clear();
        }
        private void DriverDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.menu.Show();
        }

        private void DescriptionInput_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(DriverEntryBox.Text))
            {
                MessageBox.Show("Driver Entry Can't Be Empty");
            }
            else
            {
                string driverType = DriverEntryBox.Text;
                string description = DescriptionInput.Text;

                InsertDataIntoDatabase(driverType, description);

                DriverEntryBox.Clear();
                DescriptionInput.Clear();

                LoadDriverDataGrid();

                MessageBox.Show("Driver information inserted successfully!");
            }
        }
        private void InsertDataIntoDatabase(string driverType, string description)
        {
            string connectionString = "Data Source=test.db;Version=3;";
            string sql = "INSERT INTO Driver_Information (driver_type, description) VALUES (@driverType, @description)";

            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@driverType", driverType);
                    cmd.Parameters.AddWithValue("@description", description);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }

        private void DriverEntryBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void DriverNameType_Click(object sender, EventArgs e)
        {

        }

        private void SearchBar_TextChanged(object sender, EventArgs e)
        {

        }
       /* private void SearchDriver(string searchText)
        {
            DataView dv = dataGridView1.DataSource as DataView;
            if (dv != null)
            {
                dv.RowFilter = $"driver_type LIKE '%{searchText}%' OR description LIKE '%{searchText}%'";
            }
        }*/
        private void SearchDriver(string searchTerm)
        {
            DataTable dt = new DataTable();
            string connectionString = "Data Source=test.db;Version=3;";
            string sql = "SELECT * FROM Driver_Information WHERE driver_id LIKE @searchTerm OR driver_type LIKE @searchTerm";

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

        private void button1_Click_1(object sender, EventArgs e)
        {
            SearchDriver(SearchBar.Text);
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
