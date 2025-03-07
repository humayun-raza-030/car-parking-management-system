using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FSEP1
{
    static class Program
    {
        public static Login login;
        public static Menu menu;
        public static Driver driver;
        public static Car car;
        public static History history;
        public static Search search;
        public static Registration registration;
        public static CheckIn checkin;
        public static CheckOut checkout;
        public static SignUp signup;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]

        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            signup = new SignUp();
            login = new Login();
            menu = new Menu();
            driver = new Driver();
            car = new Car();
            history = new History();
            search = new Search();
            registration = new Registration();
            checkin = new CheckIn();
            checkout = new CheckOut();
            Application.Run(Program.signup);
        }
    }
}
