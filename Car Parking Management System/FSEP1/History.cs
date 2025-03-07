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
    public partial class History : Form
    {
        private DataGridView HistoryDataGridView = new DataGridView();

        public History()
        {
            InitializeComponent();
            panel1.BackColor = Color.FromArgb(170, Color.DarkRed);
        }

        private void History_Load(object sender, EventArgs e)
        {
            LoadHistoryDataGrid();
        }

        private void LoadHistoryDataGrid()
        {
            ResultBox.DataSource = GetDataFromDatabase();
        }



        private DataTable GetDataFromDatabase()
        {
            DataTable dt = new DataTable();
            string connectionString = "Data Source=test.db;Version=3;";
            string sql = @"SELECT 
                    CI.checkin_id, 
                    CI.checkin_time, 
                    CO.checkout_time, 
                    CO.total_amount, 
                    H.action, 
                    H.timestamp 
                FROM 
                    History H
                LEFT JOIN 
                    Check_In CI ON H.checkin_id = CI.checkin_id
                LEFT JOIN 
                    Check_Out CO ON H.checkout_id = CO.checkout_id
                WHERE 
                    H.timestamp = (SELECT MAX(timestamp) FROM History)
                ORDER BY 
                    H.timestamp DESC";

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
            this.Hide();
            Program.menu.Show();
        }

        private void ResultBox_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            ResultBox.DataSource = GetDataFromDatabase();
        }
    }
}
