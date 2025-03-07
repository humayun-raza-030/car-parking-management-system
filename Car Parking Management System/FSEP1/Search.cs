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
    public partial class Search : Form
    {
        private DataGridView SearchDataGridView = new DataGridView();

        public Search()
        {
            InitializeComponent();
            panel1.BackColor = Color.FromArgb(100, Color.White);
            LoadSearchDataGrid();
        }

        private void Search_Load(object sender, EventArgs e)
        {
            LoadSearchDataGrid();
        }

        private void LoadSearchDataGrid()
        {
            SearchDataGridView.DataSource = GetDataFromDatabase();
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

        private void button1_Click(object sender, EventArgs e)
        {
            SearchMember(SearchBox.Text);
        }
        private void SearchMember(string searchTerm)
        {
            DataTable dt = new DataTable();
            string connectionString = "Data Source=test.db;Version=3;";
            string sql = @"SELECT R.registration_id, D.driver_type, C.car_type, R.phone_number, R.cnic, R.car_number_plate, R.gender, R.description 
                           FROM Registration R 
                           INNER JOIN Driver_Information D ON R.driver_id = D.driver_id 
                           INNER JOIN Car_Information C ON R.car_id = C.car_id 
                           WHERE D.driver_type LIKE @searchTerm OR C.car_type LIKE @searchTerm OR R.phone_number LIKE @searchTerm OR R.cnic LIKE @searchTerm OR R.car_number_plate LIKE @searchTerm OR R.gender LIKE @searchTerm OR R.description LIKE @searchTerm";

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
            DataResultBox.DataSource = dt;
        }


        private void button3_Click(object sender, EventArgs e)
        {
            SearchBox.Clear();
            SearchDataGridView.ClearSelection();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.menu.Show();
        }

        private void DataResultBox_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void SearchBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
