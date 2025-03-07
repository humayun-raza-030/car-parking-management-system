using System.Data.SQLite;
using System.IO;

namespace FSEP1
{
    public static class DatabaseInitializer
    {
        public static void CreateTables()
        {
            if (!File.Exists("test.db"))
            {
                SQLiteConnection.CreateFile("test.db");
                using (SQLiteConnection conn = new SQLiteConnection("Data Source=test.db;Version=3;"))
                {
                    conn.Open();
                    string sql = @"CREATE TABLE Users (
                                    user_id INTEGER PRIMARY KEY AUTOINCREMENT, 
                                    username VARCHAR(255) NOT NULL, 
                                    email VARCHAR(255) NOT NULL, 
                                    password VARCHAR(255) NOT NULL
                                );
                                CREATE TABLE Driver_Information (
                                    driver_id INTEGER PRIMARY KEY AUTOINCREMENT, 
                                    user_id INT, 
                                    driver_type VARCHAR(255), 
                                    description TEXT, 
                                    FOREIGN KEY (user_id) REFERENCES Users(user_id)
                                );
                                CREATE TABLE Car_Information (
                                    car_id INTEGER PRIMARY KEY AUTOINCREMENT, 
                                    car_type VARCHAR(255), 
                                    description TEXT
                                );
                                CREATE TABLE Registration (
                                    registration_id INTEGER PRIMARY KEY AUTOINCREMENT, 
                                    driver_id INT, 
                                    car_id INT, 
                                    phone_number VARCHAR(15), 
                                    cnic VARCHAR(15), 
                                    car_number_plate VARCHAR(20), 
                                    gender VARCHAR(10), 
                                    description TEXT,
                                    full_name VARCHAR(255),
                                    FOREIGN KEY (driver_id) REFERENCES Driver_Information(driver_id), 
                                    FOREIGN KEY (car_id) REFERENCES Car_Information(car_id)
                                );
                                CREATE TABLE Check_In (
                                    checkin_id INTEGER PRIMARY KEY AUTOINCREMENT, 
                                    registration_id INT, 
                                    full_name VARCHAR(255), 
                                    cnic VARCHAR(15), 
                                    checkin_time DATETIME DEFAULT CURRENT_TIMESTAMP, 
                                    car_number VARCHAR(20), 
                                    serial_number VARCHAR(50), 
                                    slip_printed BIT DEFAULT 0, 
                                    FOREIGN KEY (registration_id) REFERENCES Registration(registration_id)
                                );
                                CREATE TABLE Check_Out (
                                    checkout_id INTEGER PRIMARY KEY AUTOINCREMENT, 
                                    checkin_id INT, 
                                    checkout_time DATETIME DEFAULT CURRENT_TIMESTAMP, 
                                    total_amount DECIMAL(10, 2), 
                                    FOREIGN KEY (checkin_id) REFERENCES Check_In(checkin_id)
                                );
                                CREATE TABLE History (
                                    history_id INTEGER PRIMARY KEY AUTOINCREMENT, 
                                    checkin_id INT, 
                                    checkout_id INT, 
                                    action VARCHAR(20), 
                                    timestamp DATETIME DEFAULT CURRENT_TIMESTAMP, 
                                    FOREIGN KEY (checkin_id) REFERENCES Check_In(checkin_id), 
                                    FOREIGN KEY (checkout_id) REFERENCES Check_Out(checkout_id)
                                );";
                    using (SQLiteCommand cmmd = new SQLiteCommand(sql, conn))
                    {
                        cmmd.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
