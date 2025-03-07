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
    public partial class Car : Form
    {
        private DataGridView CarDataGridView = new DataGridView();


        public Car()
        {
            InitializeComponent();
            PicturePanel.BackColor = Color.FromArgb(100, Color.White);
            panel1.BackColor = Color.FromArgb(100, Color.White);
        }

        private void Car_Load(object sender, EventArgs e)
        {
            LoadCarDataGrid();
        }

        private void LoadCarDataGrid()
        {
            CarDataGridView.DataSource = GetDataFromDatabase();
        }

        private DataTable GetDataFromDatabase()
        {
            DataTable dt = new DataTable();
            string connectionString = "Data Source=test.db;Version=3;";
            string sql = "SELECT * FROM Car_Information";

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
            SearchBar.Clear();
        }
        private void SearchCarInformation(string searchTerm)
        {
            DataTable dt = new DataTable();
            string connectionString = "Data Source=test.db;Version=3;";
            string sql = "SELECT * FROM Car_Information WHERE car_type LIKE @searchTerm OR description LIKE @searchTerm";

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
        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.menu.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Car Type Can't Be Empty");
            }
            else
            {
                string connectionString = "Data Source=test.db;Version=3;";
                string sql = "INSERT INTO Car_Information (car_type, description) VALUES (@car_type, @description)";

                using (SQLiteConnection conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();
                    using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@car_type", textBox2.Text);
                        cmd.Parameters.AddWithValue("@description", textBox1.Text);
                        cmd.ExecuteNonQuery();
                    }
                    conn.Close();
                }
                LoadCarDataGrid();
                MessageBox.Show("Car Information Saved Successfully!");
                textBox1.Clear();
                textBox2.Clear();
            }
        }

        private void SearchBar_TextChanged(object sender, EventArgs e)
        {

        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = CarDataGridView.Rows[e.RowIndex];
                textBox2.Text = row.Cells["car_type"].Value.ToString();
                textBox1.Text = row.Cells["description"].Value.ToString();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            SearchCarInformation(SearchBar.Text);

        }

    }
}
