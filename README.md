# ❄️ CSMS API (Cold Storage Movement System API)

---

## 🚀 Technologies & Tools Used

### ✅ Core Frameworks

* **.NET 6 Web API**
* **Entity Framework Core** (`Microsoft.EntityFrameworkCore`, `SqlServer`, `Tools`)

### ✅ Authentication & Security

* **JWT Authentication** (`Microsoft.AspNetCore.Authentication.JwtBearer`)
* **Password Hashing** using `BCrypt.Net-Next`
* **CORS** enabled via `Microsoft.AspNetCore.Cors`

### ✅ Object Mapping

* **AutoMapper** (`AutoMapper.Extensions.Microsoft.DependencyInjection`) for DTO ↔ Entity mapping

### ✅ File Import/Export Support

* **Excel Readers**:
  * `ExcelDataReader`
  * `ExcelDataReader.DataSet`
* **Excel Writers**:
  * `EPPlus`
  * `ClosedXML`
* **CSV Support**:
  * `CsvHelper`

---

## 📂 Features

* 🔐 **JWT-based Authentication** with hashed passwords
* 👥 **Role-Based Access Control** (RBAC)
* 🧊 **Product Movement Management** (Receiving, Dispatching)
* 📦 **Inventory Tracking**
* 📤 **Excel & CSV Import/Export** for bulk data operations
* 🧭 **AutoMapper** for clean data transformation
* 🔄 **Entity Framework Core** for efficient DB interactions

---

## 📁 Project Folder Structure
```
CSMS-api/
│
├── Controllers/ # API Controllers 
│
├── Extensions/ # ServiceCollection extensions 
│
├── Helpers/ # General reusable helper methods
│ ├── Excel/ # Excel/CSV import-export utilities & templates
│ └── Queries/ # Reusable query expressions and LINQ logic
│
├── Interfaces/ # Interfaces for services 
│
├── Migration/ # Entity Framework Core migration files
│
├── Models/ # Domain models and DTOs
│
├── Services/ # Business logic and service implementations
│
└── Validators/ # Request model validators 

```
