
# GTRAssignment-Solution

Complete solution for **INTERN ASP.NET** assignment at **Genuine Technology & Research Ltd.**
Includes an ASP.NET Core Web API backend and an MVC Client App with jQuery frontend.

---

## Project Overview

### Backend: ASP.NET Core Web API

* ASP.NET Identity login with cookie authentication
* Clean architecture: Controller → Service → Repository → DbContext
* Master-detail CRUD for EmployeeInfo, Department, Designation
* EF Core with SQL Server, migrations managed
* API endpoints:

  * `POST /api/employees`
  * `PUT /api/employees/{id}`
  * `GET /api/employees`
  * `DELETE /api/employees/{id}`

### Frontend: ASP.NET Core MVC Client

* Razor views with jQuery/AJAX (no full page reloads)
* CRUD operations via Web API calls
* Master-detail form with dropdowns for Department & Designation
* Dynamic product list from external API:
  `https://www.pqstec.com/InvoiceApps/values/GetProductListAll`

---

## Technologies

* .NET 8
* ASP.NET Core Web API & MVC
* Entity Framework Core
* SQL Server
* ASP.NET Identity
* jQuery / AJAX

---

## Setup

### Prerequisites

* [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
* SQL Server installed & running
* Visual Studio 2022 (17.7+) or VS Code with C# extensions

### Steps

1. Clone repo:

   ```bash
   git clone https://github.com/ProgZahidul/GTRAssignment-Solution-.git
   cd GTRAssignment-Solution-
   ```

2. Configure DB connection in `appsettings.json`:

   ```json
   "DefaultConnection": "Server=.;Database=GTRDB;Trusted_Connection=True;TrustServerCertificate=true"
   ```

3. Apply migrations:

   ```bash
   dotnet ef database update
   ```

4. Run Web API:

   ```bash
   dotnet run
   ```

   (Usually at `https://localhost:5001`)

5. Run MVC Client:

   ```bash
   dotnet run
   ```

   Ensure API base URL matches Web API port (default `https://localhost:7125`).

---

## Usage

* Register/login via MVC frontend
* Manage Employees, Departments, Designations
* View live product list from external API

---

## Notes

* Ensure SQL Server service is running
* Ports may vary—check console or launch settings
* To reset DB: delete `GTRDB` and rerun migrations
* Install EF CLI if needed:

  ```bash
  dotnet tool install --global dotnet-ef
  ```

---

## Contact

**Sayed Zahidul Hoque**
