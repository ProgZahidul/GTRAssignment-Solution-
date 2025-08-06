
````markdown
# GTRAssignment-Solution-

This repository contains the full solution for the **INTERN ASP.NET** assignment at **Genuine Technology & Research Ltd.**  
It includes a backend Web API built with ASP.NET Core and a frontend MVC Client App using jQuery.

---

## Project Overview

### 🔷 Web API (Backend - ASP.NET Core Web API)

- Implements a login system using **ASP.NET Identity** with cookie-based authentication.
- Follows the **Repository Pattern** and clean architecture:  
  `Controller → Service Layer → Repository → DbContext`
- Handles **Master-Detail** data for:
  - EmployeeInfo
  - Department
  - Designation
- Supports **CRUD operations** with EF Core:
  - `POST /api/employees`
  - `PUT /api/employees/{id}`
  - `GET /api/employees`
  - `DELETE /api/employees/{id}`
- Uses **Entity Framework Core** exclusively.
- Designed for **SQL Server** with proper entity relationships.
- Migrations managed via EF Core.


### 🔶 MVC Client App (Frontend - ASP.NET Core MVC)

- Razor views using **jQuery / Vanilla JavaScript**.
- AJAX-based CRUD operations calling Web API endpoints.
- Master-detail form supports:
  - Employee Info
  - Department (dropdown)
  - Designation (dropdown)
- Consumes external API to display product list dynamically.
  `https://www.pqstec.com/InvoiceApps/values/GetProductListAll`
- No full page reloads—dynamic UI updates via AJAX.

---

## Technologies Used

- .NET 8  
- ASP.NET Core Web API  
- ASP.NET Core MVC  
- Entity Framework Core  
- SQL Server  
- ASP.NET Identity  
- jQuery / AJAX  

---

## Getting Started

### Prerequisites

* [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
* SQL Server (Express or full version) installed and running
  [Download SQL Server](https://aka.ms/sqlserverdownload)
* Visual Studio 2022 (17.7+) or Visual Studio Code with C# extensions

---

### Setup and Run Instructions

1. **Clone the repository**

```bash
git clone https://github.com/ProgZahidul/GTRAssignment-Solution-.git
cd GTRAssignment-Solution-
```

2. **Verify your database connection string**

* Open the Web API project’s `appsettings.json`.
* Confirm the connection string matches your SQL Server setup:

```json
"DefaultConnection": "Server=.;Database=GTRDB;Trusted_Connection=True;TrustServerCertificate=true"
```

Adjust if needed for your environment.

3. **Update the database**

Since migrations already exist, simply apply them to update the database schema:

```bash
dotnet ef database update
```

Run this command inside the **Web API** project directory.

4. **Run the Web API**

From the Web API project folder, run:

```bash
dotnet run
```

Or launch via Visual Studio (**F5**).
API will typically run at: `https://localhost:5001` or the port configured.

5. **Run the MVC Client app**

* Open the MVC client project.
* Ensure the AJAX calls in the client target the correct API base URL (`https://localhost:7125`).
* Run the MVC app (Visual Studio or CLI):

```bash
dotnet run
```

Client app will usually be available at: `https://localhost:5002` or configured port.

---

### Usage

* Register and log in users through the MVC frontend.
* Perform CRUD operations on Employees, Departments, and Designations.
* View product list fetched live from the external API.

---

### Additional Notes

* Ensure SQL Server service is running and accessible.
* Ports may vary; check the console output or launch settings.
* To reset database, delete `GTRDB` database and rerun `dotnet ef database update`.
* You do **not** need to create new migrations unless model changes occur.
* If `dotnet ef` commands are not recognized, install EF CLI tools:

```bash
dotnet tool install --global dotnet-ef
```

or update with:

```bash
dotnet tool update --global dotnet-ef
```

---

## Contact

**Sayed Zahidul Hoque**

