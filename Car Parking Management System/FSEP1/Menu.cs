using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FSEP1
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void Menu_Load(object sender, EventArgs e)
        {
            gradientRectangle1.BackColor = Color.FromArgb(150, Color.Black);
        }

        private void gradientRectangle1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.driver.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.car.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.registration.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.search.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.checkin.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.checkout.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.history.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            MessageBox.Show("You have Logged Out Successfully!");
            this.Hide();
            Program.login.Show();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
