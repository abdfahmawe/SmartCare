# 🏥 SmartCare – Healthcare Management System

SmartCare is a Healthcare Management System built with **ASP.NET Core 9 Web API** that streamlines communication between **Patients**, **Doctors**, and **Administrators** through a secure and scalable platform.

The system provides authentication, appointment scheduling, medical records management, prescriptions, doctor working schedules, and invoice management while following a clean **Layered Architecture**.

The project is designed to simplify healthcare workflows and provide a secure RESTful API for healthcare applications.

---

# 🚀 Features

## 👨‍⚕️ Doctor

- Manage personal profile
- Set and update working hours
- View scheduled appointments
- View today's appointments
- Complete appointments
- Create medical records
- Update medical records
- Delete medical records
- Create prescriptions
- Update prescriptions
- Delete prescriptions
- View all created prescriptions

---

## 🧑 Patient

- Register and login
- Book appointments
- View available doctor schedules
- View appointment history
- Update personal profile
- Access medical records
- View prescriptions

---

## 👨‍💼 Admin

- Generate invoices
- View all invoices
- Retrieve invoice details
- Mark invoices as paid

---

## 🔐 Security

- JWT Authentication
- ASP.NET Identity
- Email Confirmation
- Password Reset
- Role-Based Authorization
- Protected Endpoints

---

# 🧠 Tech Stack

| Technology | Description |
|------------|-------------|
| ASP.NET Core 9 | Backend Framework |
| C# | Programming Language |
| SQL Server | Database |
| Entity Framework Core 9 | ORM |
| ASP.NET Identity | Identity Management |
| JWT Bearer Authentication | Authentication |
| Scalar OpenAPI | API Documentation |
| Layered Architecture | Application Design Pattern |

---

# 🏗️ Architecture

The project follows a **Layered Architecture** to improve maintainability, scalability, and separation of concerns.

```
SmartCare
│
├── SmartCare.PL
│      Presentation Layer
│      Controllers
│      Authentication
│
├── SmartCare.BLL
│      Business Logic
│      Services
│      Interfaces
│      Validation
│
├── SmartCare.DAL
│      Database
│      Models
│      DTOs
│      Repositories
│
└── SQL Server
```

---

# 📂 Project Structure

```
SmartCare
│
├── SmartCare.PL
│   ├── Areas
│   │   ├── Admin
│   │   ├── Doctor
│   │   ├── Patient
│   │   └── Identity
│   │
│   ├── Program.cs
│   └── appsettings.json
│
├── SmartCare.BLL
│   ├── Services
│   ├── Interfaces
│   ├── Exceptions
│   ├── Validation
│   └── Mapping
│
├── SmartCare.DAL
│   ├── Models
│   ├── DTO
│   ├── Data
│   ├── Repositories
│   └── Migrations
```

---

# 🗄️ Main Database Entities

- Users
- Roles
- Doctors
- Patients
- Appointments
- MedicalRecords
- Prescriptions
- WorkingTimes
- Invoices

---

# 🔑 User Roles

The system uses **Role-Based Authorization**.

### 👨‍💼 Admin

Responsible for:

- Invoice Management
- Payment Operations

---

### 👨‍⚕️ Doctor

Responsible for:

- Working Hours
- Appointments
- Medical Records
- Prescriptions
- Profile Management

---

### 🧑 Patient

Responsible for:

- Appointment Booking
- Profile Management
- Viewing Medical Records
- Viewing Prescriptions

---

# 🛠️ Installation & Setup

## ✅ Prerequisites

Before running the project make sure you have installed:

- .NET 9 SDK
- SQL Server
- Visual Studio 2022
- Git

---

## 📥 Clone Repository

```bash
git clone https://github.com/abdfahmawe/SmartCare.git
```

Move into the project folder

```bash
cd SmartCare
```

---

## ⚙️ Configure Database

Open

```text
appsettings.json
```

Update your SQL Server connection string.

Example

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=SmartCareDB;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

---

## 🗄️ Apply Database Migrations

Run

```bash
dotnet ef database update
```

---

## ▶️ Run the Project

Using Visual Studio:

```
Press F5
```

or

```bash
dotnet run
```

---

# 🌐 Base URL

After running the application

```
https://localhost:5001
```

or

```
https://localhost:{PORT}
```

---

# 🔐 Authentication

The application uses

- JWT Bearer Authentication
- ASP.NET Identity
- Role-Based Authorization

After logging in, every secured endpoint requires

```
Authorization: Bearer YOUR_JWT_TOKEN
```

Example

```
GET /api/Patient/Patient/Profile

Authorization:
Bearer eyJhbGciOi...
```

---

# 📡 API Endpoints 
# 👨‍💼 Admin Endpoints

The Admin is responsible for invoice management and payment operations.

---

## 📌 Invoice Management

| Method | Endpoint | Description | Role |
|--------|----------|-------------|------|
| POST | `/api/Admin/Invoices/GenerateInvoice/{appointmentId}` | Generate a new invoice for a completed appointment | Admin |
| GET | `/api/Admin/Invoices/GetAll` | Retrieve all invoices | Admin |
| GET | `/api/Admin/Invoices/GetInvoiceByAppointmentId/{appointmentId}` | Retrieve invoice details using appointment ID | Admin |
| PATCH | `/api/Admin/Invoices/MarkAsPaid/{invoiceId}` | Mark an invoice as paid | Admin |

---

# 👨‍⚕️ Doctor Endpoints

The Doctor module allows healthcare providers to manage appointments, patient medical records, prescriptions, working schedules, and personal information.

---

## 📌 Doctor Profile

| Method | Endpoint | Description | Role |
|--------|----------|-------------|------|
| GET | `/api/Doctor/Doctors/Profile` | Retrieve doctor's profile information | Doctor |
| PATCH | `/api/Doctor/Doctors/UpdateProfile` | Update doctor's profile information | Doctor |

---

## 📌 Working Time Management

| Method | Endpoint | Description | Role |
|--------|----------|-------------|------|
| GET | `/api/Doctor/DoctorWorkingTime/GetWorkingHours` | Retrieve doctor's working schedule | Doctor |
| POST | `/api/Doctor/DoctorWorkingTime/SetWorkingHours` | Create doctor's working schedule | Doctor |
| PUT | `/api/Doctor/DoctorWorkingTime/UpdateWorkingHours` | Update working schedule | Doctor |
| DELETE | `/api/Doctor/DoctorWorkingTime/DeleteWorkingTime` | Delete working schedule | Doctor |

---

## 📌 Appointment Management

| Method | Endpoint | Description | Role |
|--------|----------|-------------|------|
| GET | `/api/Doctor/DoctorAppointments/GetAllAppointmentOnlyScheduled` | Retrieve all scheduled appointments | Doctor |
| GET | `/api/Doctor/DoctorAppointments/GetTodayAppointments` | Retrieve today's appointments | Doctor |
| PATCH | `/api/Doctor/DoctorAppointments/CompleteAppointment/{appointmentId}` | Mark an appointment as completed | Doctor |

---

## 📌 Medical Record Management

| Method | Endpoint | Description | Role |
|--------|----------|-------------|------|
| POST | `/api/Doctor/DoctorMedicalRecords/CreateMedicalRecord/{appointmentId}` | Create a medical record for an appointment | Doctor |
| GET | `/api/Doctor/DoctorMedicalRecords/GetMedicalRecord/{appointmentId}` | Retrieve a medical record by appointment ID | Doctor |
| GET | `/api/Doctor/DoctorMedicalRecords/GetAllMedicalRecords` | Retrieve all medical records created by the doctor | Doctor |
| PUT | `/api/Doctor/DoctorMedicalRecords/UpdateMedicalRecord/{appointmentId}` | Update a medical record | Doctor |
| DELETE | `/api/Doctor/DoctorMedicalRecords/DeleteMedicalRecord/{appointmentId}` | Delete a medical record | Doctor |

---

## 📌 Prescription Management

| Method | Endpoint | Description | Role |
|--------|----------|-------------|------|
| GET | `/api/Doctor/DoctorPrescriptions/AllPrescriptionsByDoctorId` | Retrieve all prescriptions created by the doctor | Doctor |
| GET | `/api/Doctor/DoctorPrescriptions/AllPrescriptionsByDoctorIdAndAppointmentId/{appointmentId}` | Retrieve prescriptions for a specific appointment | Doctor |
| POST | `/api/Doctor/DoctorPrescriptions/CreatePrescription` | Create a new prescription | Doctor |
| PUT | `/api/Doctor/DoctorPrescriptions/UpdatePrescription/{prescriptionId}` | Update an existing prescription | Doctor |
| DELETE | `/api/Doctor/DoctorPrescriptions/DeletePrescription/{prescriptionId}` | Delete a prescription | Doctor |

---

## 🔒 Doctor Authorization

All Doctor endpoints require:

```
Authorization: Bearer YOUR_JWT_TOKEN
```

Only authenticated users with the **Doctor** role can access these endpoints.

Example:

```http
GET https://localhost:5001/api/Doctor/DoctorAppointments/GetTodayAppointments

Authorization: Bearer YOUR_JWT_TOKEN
```

---

## 🔒 Admin Authorization

All Admin endpoints require:

```
Authorization: Bearer YOUR_JWT_TOKEN
```

Only authenticated users with the **Admin** role can access these endpoints.

Example:

```http
GET https://localhost:5001/api/Admin/Invoices/GetAll

Authorization: Bearer YOUR_JWT_TOKEN
```
# 🧑 Patient Endpoints

The Patient module allows users to manage their profile, book appointments, and access their healthcare information.

---

## 📌 Patient Profile

| Method | Endpoint | Description | Role |
|--------|----------|-------------|------|
| GET | `/api/Patient/Patient/Profile` | Retrieve patient profile information | Patient |
| PATCH | `/api/Patient/Patient/UpdateProfile` | Update patient profile | Patient |
| GET | `/api/Patient/Patient/MedicalRecords` | Retrieve all patient's medical records | Patient |
| GET | `/api/Patient/Patient/Prescriptions` | Retrieve all patient's prescriptions | Patient |

---

## 📌 Appointment Booking

| Method | Endpoint | Description | Role |
|--------|----------|-------------|------|
| GET | `/api/Patient/PatientAppointments/GetAllDoctorsWithIds` | Retrieve all available doctors | Patient |
| GET | `/api/Patient/PatientAppointments/AvailableSlots?doctorId={doctorId}&date={date}` | Retrieve available appointment slots for a doctor | Patient |
| GET | `/api/Patient/PatientAppointments/Appointments` | Retrieve patient's appointments | Patient |
| POST | `/api/Patient/PatientAppointments/BookAppointment` | Book a new appointment | Patient |

---

## 🔒 Patient Authorization

All Patient endpoints require:

```
Authorization: Bearer YOUR_JWT_TOKEN
```

Only authenticated users with the **Patient** role can access these endpoints.

Example:

```http
GET https://localhost:5001/api/Patient/Patient/Profile

Authorization: Bearer YOUR_JWT_TOKEN
```

---

# 🔐 Authentication Endpoints

Authentication is handled using **ASP.NET Identity** and **JWT Bearer Authentication**.

---

## 📌 Account Management

| Method | Endpoint | Description | Access |
|--------|----------|-------------|--------|
| POST | `/api/Identity/Account/register` | Register a new patient account | Public |
| POST | `/api/Identity/Account/login` | Login and receive JWT access token | Public |
| GET | `/api/Identity/Account/ConfirmEmail` | Confirm email address | Public |
| POST | `/api/Identity/Account/ResetPassword` | Reset forgotten password | Public |
| POST | `/api/Identity/Account/ChangePassword` | Change current password | Authenticated |

---

# 📖 HTTP Response Codes

The API returns standard HTTP status codes.

| Code | Meaning |
|------|---------|
| 200 OK | Request completed successfully |
| 201 Created | Resource created successfully |
| 400 Bad Request | Invalid request data |
| 401 Unauthorized | Authentication required |
| 403 Forbidden | Access denied |
| 404 Not Found | Requested resource was not found |
| 409 Conflict | Resource conflict |
| 500 Internal Server Error | Unexpected server error |

---

# 🧪 Testing the API

You can test the API using:

- Swagger / Scalar OpenAPI
- Postman
- Insomnia

For protected endpoints:

1. Register a new account.
2. Login using your credentials.
3. Copy the generated JWT token.
4. Add the following header to every secured request:

```
Authorization: Bearer YOUR_JWT_TOKEN
```

---

# 🚀 Future Improvements

- Email Notifications
- SMS Appointment Reminders
- Online Payment Gateway Integration
- PDF Invoice Export
- Medical Reports Export
- Doctor Dashboard
- Admin Dashboard
- Patient Dashboard
- Real-Time Notifications
- Video Consultation Support
- Appointment Cancellation & Rescheduling
- Multi-language Support

---

# 👨‍💻 Author

**Abd Al-Rahman Hamdan**

Software Engineer | Backend Developer

GitHub:

https://github.com/abdfahmawe

LinkedIn:

https://www.linkedin.com/

---

# ⭐ Support

If you found this project useful, consider giving it a ⭐ on GitHub.

Your support is greatly appreciated!
