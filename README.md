# Car Parking Management System

## Overview
The **Car Parking Management System** is a Windows Forms application developed in **C#** using **SQLite** as a database. It allows users to register, check-in vehicles, check-out vehicles, and maintain a parking history.

## Features
- **User Authentication**: Sign-up and login with verification.
- **Driver and Car Registration**: Store driver and car details.
- **Check-In & Check-Out System**: Track vehicle entry and exit.
- **Parking Fee Calculation**: Automated fee calculation based on duration.
- **Search Functionality**: Find records using various filters.
- **History Management**: Maintain logs of all parking activity.
- **Slip Generation**: Print slips upon check-in.
- **User-Friendly Interface**: Simple navigation with a menu.

## Technologies Used
- **C# (.NET Framework)** - Windows Forms Application
- **SQLite** - Database management
- **Gmail SMTP** - Email verification during signup

## Installation Guide
### Prerequisites
- **Windows OS**
- **.NET Framework Installed**
- **SQLite Database (test.db will be auto-generated)**

### Steps to Run the Application
1. **Clone the Repository**
   ```bash
   git clone https://github.com/humayun-raza-030/car-parking-system.git
   ```
2. **Open in Visual Studio**
   - Open `FSEP1.sln` in Visual Studio.
3. **Build and Run the Project**
   - Press `Ctrl + F5` to run the application.
4. **Database Initialization**
   - The database `test.db` will be created automatically on first run.

## Database Schema
The system includes the following tables:
- **Users**: Stores user credentials.
- **Driver_Information**: Stores driver details.
- **Car_Information**: Stores different car types.
- **Registration**: Links drivers to cars with additional details.
- **Check_In**: Tracks vehicle entry times.
- **Check_Out**: Stores exit times and calculates fees.
- **History**: Maintains logs of parking activity.

## Security Improvements Needed
- Implement **password hashing** instead of storing plain text passwords.
- Validate **duplicate entries** in registration, check-in, and check-out.
- Implement **proper error handling** for database queries.
- Add **indexing** for improved search performance.

## Contribution Guidelines
1. Fork the repository.
2. Create a new branch: `git checkout -b feature-branch`.
3. Make your changes and commit: `git commit -m 'Add new feature'`.
4. Push the changes: `git push origin feature-branch`.
5. Open a pull request.

## License
This project is open-source under the **MIT License**.

## Contact
For queries, contact **humayunraza030@example.com** or open an issue in the repository.

