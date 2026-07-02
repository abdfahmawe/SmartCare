# 🏥 SmartCare – Healthcare Management System

SmartCare is a comprehensive Healthcare Management System developed using **ASP.NET Core 8**. The system simplifies healthcare operations by connecting **Patients**, **Doctors**, and **Administrators** through a secure and organized platform.

It provides appointment scheduling, medical record management, prescriptions, invoices, authentication, and doctor working schedules using a clean layered architecture.

---

# 🚀 Features

## 👨‍⚕️ Doctor Portal

- View daily appointments
- Manage patient appointments
- Create and update medical records
- Write prescriptions
- Manage working schedule
- View patient history

---

## 🧑 Patient Portal

- Secure registration and login
- Book appointments
- View appointment history
- Access medical records
- View prescriptions

---

## 👨‍💼 Admin Portal

- Manage invoices
- Monitor healthcare operations
- Administrative management tools

---

## 🔐 Authentication & Security

- JWT Authentication
- ASP.NET Identity
- Role-Based Authorization
- Protected API Endpoints

---

# 🧠 Tech Stack

| Technology | Description |
|------------|-------------|
| ASP.NET Core 9 | Backend Framework |
| C# | Programming Language |
| SQL Server | Database |
| Entity Framework Core | ORM |
| ASP.NET Identity | User Management |
| JWT | Authentication |
| Layered Architecture | Project Structure |

---

# 🏗️ Project Architecture

The project follows a **Layered Architecture** to ensure maintainability and scalability.

```
Presentation Layer (PL)
        │
Business Logic Layer (BLL)
        │
Data Access Layer (DAL)
        │
SQL Server
```

---

# 🗄️ Main Modules

### 🔐 Identity

- Login
- Register
- JWT Authentication
- User Management

---

### 👨‍⚕️ Doctors

- Doctor Profile
- Working Time Management
- Appointments
- Medical Records
- Prescriptions

---

### 🧑 Patients

- Patient Profile
- Appointment Booking
- Medical History
- Prescriptions

---

### 📅 Appointments

- Book Appointment
- Cancel Appointment
- View Upcoming Appointments
- Appointment Status Management

---

### 📋 Medical Records

- Create Medical Record
- Update Medical Record
- View Patient History

---

### 💊 Prescriptions

- Create Prescription
- View Prescriptions
- Patient Prescription History

---

### 💰 Invoices

- Create Invoice
- View Invoice Details
- Invoice Management

---

# 📡 API Modules

## 🔐 Authentication

| Method | Endpoint | Description |
|--------|----------|-------------|
| POST | /api/Account/Register | Register new account |
| POST | /api/Account/Login | User Login |

---

## 👨‍⚕️ Doctor APIs

- Doctor Profile
- Working Time
- Appointments
- Medical Records
- Prescriptions

---

## 🧑 Patient APIs

- Patient Profile
- Appointment Booking
- Appointment History
- Medical Records

---

## 👨‍💼 Admin APIs

- Invoice Management
- Administrative Operations

---

# 📂 Project Structure

```
SmartCare
│
├── SmartCare.PL
│   ├── Areas
│   │   ├── Identity
│   │   ├── Doctor
│   │   ├── Patient
│   │   └── Admin
│
├── SmartCare.BLL
│   ├── Services
│   ├── Interfaces
│   ├── BackgroundJobs
│   └── Exceptions
│
├── SmartCare.DAL
│   ├── Models
│   ├── Repositories
│   ├── Data
│   └── Migrations
```

---

# 🛠️ Installation

## Requirements

- .NET 8 SDK
- SQL Server
- Visual Studio 2022

---

## Clone Repository

```bash
git clone https://github.com/abdfahmawe/SmartCare.git
cd SmartCare
```

---

## Configure Database

Update your connection string inside:

```json
appsettings.json
```

Run migrations:

```bash
dotnet ef database update
```

---

## Run

```bash
dotnet run
```

or simply press **F5** in Visual Studio.

---

# 🔑 User Roles

### 👨‍💼 Admin

- Manage invoices
- Administrative operations

### 👨‍⚕️ Doctor

- Manage appointments
- Create prescriptions
- Update medical records
- Manage working hours

### 🧑 Patient

- Book appointments
- View prescriptions
- Access medical records

---

# 🔒 Authentication

SmartCare secures all protected endpoints using:

- ASP.NET Identity
- JWT Bearer Authentication
- Role-Based Authorization

---

# 📈 Future Enhancements

- Email Notifications
- SMS Appointment Reminders
- Online Payments
- Medical Report PDF Export
- Video Consultation
- Dashboard Analytics

---

# 👨‍💻 Developer

**Abd Al-Rahman Hamdan**

Software Engineer

GitHub:
https://github.com/abdfahmawe

---

⭐ If you found this project useful, consider giving it a star!
