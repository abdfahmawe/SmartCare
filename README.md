# 🏥 SmartCare – Healthcare Management System

SmartCare is a Healthcare Management System built with **ASP.NET Core 8** that streamlines communication between patients, doctors, and administrators.

The system provides secure authentication, appointment scheduling, medical records management, prescriptions, invoices, and doctor working schedules through a clean layered architecture.

---

# 🚀 Features

### 👨‍⚕️ Doctor

- Manage appointments
- View patient medical records
- Create and manage prescriptions
- Update working schedules
- Manage patient consultations

### 🧑‍🤝‍🧑 Patient

- Register and login securely
- Book appointments
- View appointment history
- Access medical records
- View prescriptions

### 👨‍💼 Admin

- Manage invoices
- Monitor system data
- Administrative dashboard

### 🔐 Security

- JWT Authentication
- ASP.NET Identity
- Role-Based Authorization
- Secure API Endpoints

---

# 🧠 Tech Stack

- **Backend:** ASP.NET Core 8
- **Language:** C#
- **Database:** SQL Server
- **ORM:** Entity Framework Core
- **Authentication:** JWT + ASP.NET Identity
- **API Documentation:** Scalar / OpenAPI
- **Architecture:** Layered Architecture (Presentation, Business Logic, Data Access)

---

# 🏗️ Project Architecture

```
SmartCare
│
├── SmartCare.PL        → Presentation Layer
├── SmartCare.BLL       → Business Logic Layer
├── SmartCare.DAL       → Data Access Layer
```

The project follows a clean layered architecture separating:

- Presentation Layer
- Business Logic
- Data Access
- Services
- Repositories
- Models

---

# 🗄️ Database Models

The system contains the following main entities:

- Users
- Doctors
- Patients
- Departments
- Appointments
- Medical Records
- Medical Files
- Prescriptions
- Working Times
- Invoices

---

# 📡 Main Modules

## 🔐 Authentication

- User Registration
- User Login
- JWT Token Generation
- Role Management

---

## 👨‍⚕️ Doctor Module

- Manage appointments
- Add medical records
- Create prescriptions
- Manage working hours

---

## 🧑 Patient Module

- Book appointments
- View appointments
- Access prescriptions
- View medical records

---

## 💳 Invoice Module

- Generate invoices
- View invoice details
- Manage billing information

---

# 📂 Project Structure

```
SmartCare
│
├── SmartCare.PL
│   ├── Areas
│   │   ├── Admin
│   │   ├── Doctor
│   │   ├── Identity
│   │   └── Patient
│   │
│   ├── Helpers
│   └── Program.cs
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
│   └── Utilities
```

---

# 🔑 Roles

The system supports multiple user roles:

- 👨‍💼 Admin
- 👨‍⚕️ Doctor
- 🧑 Patient

Each role has its own permissions and protected endpoints.

---

# 🛠️ Installation & Setup

## Prerequisites

- .NET 8 SDK
- SQL Server
- Visual Studio 2022 (Recommended)

---

## Clone Repository

```bash
git clone https://github.com/abdfahmawe/SmartCare.git
cd SmartCare
```

---

## Configure Database

Update the connection string inside:

```
appsettings.json
```

Then apply migrations:

```bash
dotnet ef database update
```

---

## Run the Project

```bash
dotnet run
```

or simply run the project using Visual Studio.

---

# 🔒 Authentication

SmartCare uses:

- ASP.NET Identity
- JWT Authentication
- Role-Based Authorization

All protected endpoints require a valid JWT token.

---

# 📚 API Documentation

The project includes OpenAPI support for testing and exploring endpoints.

After running the project, open the API documentation from your browser.

---

# ✨ Future Improvements

- Email Notifications
- Online Payment Integration
- Video Consultation
- Real-Time Chat
- Mobile Application
- Appointment Reminders
- Medical Reports Export (PDF)

---

# 👨‍💻 Author

**Abd Al-Rahman Hamdan**

Software Engineer | Backend Developer

GitHub:
https://github.com/abdfahmawe

---

## ⭐ If you like this project, don't forget to give it a Star!
