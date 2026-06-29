# TicketSystem - Blazor and API Application

A fully data-driven ticketing system built with **.NET 6**, **Blazor WebAssembly**, and **SQL Server**.

---

## 📋 Project Overview

**Type:** Full-stack Web Application  
**API:** RESTful API with .NET 6  
**Frontend:** Blazor WebAssembly (WASM)  
**Database:** SQL Server (Entity Framework Core)  
**Authentication:** JWT (Json Web Token) with Claims  

### Description

This project is a complete ticketing system for managing client support requests. It includes a RESTful API built with .NET 6 that handles all data operations, and a Blazor WebAssembly frontend that consumes the API. The system allows clients to log tickets, support staff to manage them, and administrators to monitor SLA compliance.

---

## 🚀 Technologies Used

| Technology | Purpose |
|------------|---------|
| **.NET 6** | Backend framework |
| **Entity Framework Core** | ORM for database operations |
| **SQL Server** | Database |
| **Blazor WebAssembly (WASM)** | Client-side frontend |
| **JWT** | User authentication and authorization |
| **Claims** | User role-based access control |
| **NSwag** | HTTP client generation |
| **NuGet** | Package management |

---

## 🛠️ Key Features

### Client Account Management

| Feature | Description |
|---------|-------------|
| **Client Setup** | Manage client name, surname, company, contact details |
| **QuickBooks UID** | Unique identifier for client integration |
| **SLA Management** | Service Level Agreement settings per client |
| **Remote/On-site Hours** | Track allocated support hours |
| **Hardware Coverage** | Indicate if hardware is covered |
| **Response/Resolve Times** | Minimum SLA response and resolution times |
| **Warranty Tracking** | Warranty period management |

### Ticket Categories

| Category | Description |
|----------|-------------|
| Remote Support | Support provided remotely |
| Onsite Support | Support provided on-site |
| After Hours Remote | Remote support after business hours |
| After Hours Onsite | On-site support after business hours |
| Development | Development work |
| Training | Training services |
| Hardware | Hardware issues |
| Software | Software issues |
| Bug Report | Bug reporting |
| Suggestions | Feature suggestions |
| Warranty/Repair | Warranty claims and repairs |

### Ticket Management

| Feature | Description |
|---------|-------------|
| **Log Ticket** | Firstline support or client can log tickets |
| **Ticket Statuses** | Pending, Accepted, In Progress, Parked, Closed, Reopened |
| **Assign Ticket** | Assign tickets to support staff |
| **Accept Ticket** | Assignee accepts the ticket |
| **Time Logging** | Log time against tickets with comments |
| **Status History** | Start and end times with comments for each status change |
| **Client View** | Clients can view their own tickets |

### Communication

| Feature | Description |
|---------|-------------|
| **Email Notifications** | Automated emails to clients on status changes |
| **Exception Reports** | Automatic reports for SLA breaches |
| **SLA Alerts** | Notifications when response/resolve times are exceeded |
| **Escalation** | Administrator alerts for overdue tickets |

---

## 📁 Project Structure

```
TicketSystem/
├── TicketSystemWebApi/                  # REST API (backend)
│   ├── Configuration/                   # Configuration files
│   │   └── MapperConfig.cs              # AutoMapper configuration
│   ├── Constants/                       # Constant values
│   │   └── TicketStatusIds.cs           # Ticket status constants
│   ├── Controllers/                     # API endpoints
│   │   ├── ClientAccountCompanyAccessController.cs
│   │   ├── ClientAccountController.cs
│   │   ├── CompaniesController.cs
│   │   ├── ConsumebleItemController.cs
│   │   ├── ConsumebledUsedController.cs
│   │   ├── SLAController.cs
│   │   ├── TicketCategoryController.cs
│   │   ├── TicketController.cs
│   │   ├── TicketDetailController.cs
│   │   ├── TicketStatusController.cs
│   │   ├── UserController.cs
│   │   └── UserGroupsController.cs
│   ├── Data/                            # Database context and models
│   │   ├── ClientAccount.cs
│   │   ├── ClientAccountCompanyAccess.cs
│   │   ├── Company.cs
│   │   ├── ConsumebleItem.cs
│   │   ├── ConsumebledUsed.cs
│   │   ├── Sla.cs
│   │   ├── Ticket.cs
│   │   ├── TicketCategory.cs
│   │   ├── TicketDetail.cs
│   │   ├── TicketStatus.cs
│   │   ├── TicketSystemDbContext.cs
│   │   ├── User.cs
│   │   ├── UserGroup.cs
│   │   └── UserSeeder.cs
│   ├── Migrations/                      # Entity Framework migrations
│   │   ├── 20250724142148_InitialCreate.cs
│   │   ├── 20250922083112_AddUserIdToClientAccount.cs
│   │   ├── 20250922135016_MakeUserIdNullable.cs
│   │   └── TicketSystemDbContextModelSnapshot.cs
│   ├── Models/                          # DTOs (Data Transfer Objects)
│   │   ├── ClientAccount/
│   │   ├── ClientAccountCompanyAccess/
│   │   ├── Companies/
│   │   ├── ConsumebleItem/
│   │   ├── ConsumebledUsed/
│   │   ├── Sla/
│   │   ├── Ticket/
│   │   ├── TicketCategory/
│   │   ├── TicketDetail/
│   │   ├── TicketStatus/
│   │   ├── User/
│   │   └── UserGroup/
│   ├── Properties/
│   ├── Program.cs
│   ├── TicketSystemWebApi.csproj
│   ├── appsettings.Development.json
│   └── appsettings.json
│
├── TicketsSystemBlazorApp/              # Blazor WebAssembly frontend
│   ├── Pages/
│   │   ├── Assignee/
│   │   │   ├── AssigneeTicketDetail.razor
│   │   │   └── AssigneeTickets.razor
│   │   ├── ClientAccount/
│   │   │   ├── ApproveClients.razor
│   │   │   ├── ClientAccountClient.razor
│   │   │   ├── Create.razor
│   │   │   ├── Details.razor
│   │   │   ├── Index.razor
│   │   │   └── Update.razor
│   │   ├── ClientAccountCompanyAccess/
│   │   │   ├── Create.razor
│   │   │   └── Index.razor
│   │   ├── Company/
│   │   │   ├── Create.razor
│   │   │   ├── Details.razor
│   │   │   ├── Index.razor
│   │   │   └── Update.razor
│   │   ├── ConsumebleItem/
│   │   │   ├── Create.razor
│   │   │   ├── Details.razor
│   │   │   ├── Index.razor
│   │   │   └── Update.razor
│   │   ├── ConsumebledUsed/
│   │   │   ├── Create.razor
│   │   │   ├── Details.razor
│   │   │   ├── Index.razor
│   │   │   └── Update.razor
│   │   ├── Sla/
│   │   │   ├── Create.razor
│   │   │   ├── Details.razor
│   │   │   ├── Index.razor
│   │   │   └── Update.razor
│   │   ├── Ticket/
│   │   │   ├── ClientTicketDetsils.razor
│   │   │   ├── ClientTickets.razor
│   │   │   ├── Create.razor
│   │   │   ├── Details.razor
│   │   │   ├── Index.razor
│   │   │   ├── TicketApproval.razor
│   │   │   ├── TicketAssignment.razor
│   │   │   └── Update.razor
│   │   ├── TicketCategory/
│   │   │   ├── Create.razor
│   │   │   ├── Details.razor
│   │   │   ├── Index.razor
│   │   │   └── Update.razor
│   │   ├── TicketDetail/
│   │   │   ├── Create.razor
│   │   │   ├── Details.razor
│   │   │   ├── Index.razor
│   │   │   └── Update.razor
│   │   ├── TicketStatus/
│   │   │   ├── Create.razor
│   │   │   ├── Details.razor
│   │   │   ├── Index.razor
│   │   │   └── Update.razor
│   │   ├── User/
│   │   │   ├── ChangePassword.razor
│   │   │   ├── Login.razor
│   │   │   ├── Logout.razor
│   │   │   ├── Profile.razor
│   │   │   └── Register.razor
│   │   └── UserGroup/
│   │       ├── AdminDashboard.razor
│   │       └── Index.razor
│   ├── Providers/
│   │   └── ApiAuthenticationStateProvider.cs
│   ├── Service/
│   │   ├── Authentication/
│   │   │   ├── AuthenticationService.cs
│   │   │   └── IAuthenticationService.cs
│   │   ├── Base/
│   │   │   ├── ClientAccountService.cs
│   │   │   └── TicketServiceWrapper.cs
│   │   └── ticketsystem.nswag
│   ├── Shared/
│   │   ├── MainLayout.razor
│   │   ├── MainLayout.razor.css
│   │   ├── NavMenu.razor
│   │   ├── NavMenu.razor.css
│   │   └── SurveyPrompt.razor
│   ├── wwwroot/
│   ├── App.razor
│   ├── Program.cs
│   ├── TicketsSystemBlazorApp.csproj
│   ├── _Imports.razor
│   ├── appsettings.Development.json
│   └── appsettings.json
│
├── .gitattributes
├── .gitignore
└── README.md
```

---

## 🏗️ Architecture

### System Flow

```
[Client] → [Blazor WASM] → [Web API] → [Database]
              ↓               ↓
          [JWT Token]    [EF Core]
```

### Ticket Lifecycle

```
1. Client logs ticket → Status: Pending
2. Firstline accepts ticket → Status: Accepted
3. Assignee assigned → Status: In Progress
4. Work in progress → Status: Parked (optional)
5. Ticket completed → Status: Closed
6. If issue returns → Status: Reopened
```

### Authentication Flow

```
1. User logs in with credentials
2. API validates and generates JWT token with claims
3. Token returned to Blazor WASM client
4. Client includes token in subsequent requests
5. API validates token on each request
6. Claims determine user permissions
```

---

## 🔧 How to Run the Project

### Prerequisites

- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0) installed
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (or SQL Server Express)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or later

### Step 1: Clone the Repository

```bash
git clone https://github.com/your-username/ticketsystem.git
cd ticketsystem
```

### Step 2: Configure Database

1. Open `appsettings.json` in the API project
2. Update the connection string:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=TicketSystemDB;Trusted_Connection=True;MultipleActiveResultSets=true"
}
```

### Step 3: Apply Migrations

```bash
cd TicketSystemWebApi
dotnet ef database update
```

### Step 4: Seed Initial Data

The project includes a `UserSeeder.cs` file that will seed default users and roles.

### Step 5: Run the API

```bash
dotnet run
```

The API will be available at: `https://localhost:5001`

### Step 6: Run the Blazor WASM App

```bash
cd TicketsSystemBlazorApp
dotnet run
```

The Blazor app will be available at: `https://localhost:5002`

---

## 📡 API Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/ClientAccount` | Get all clients |
| GET | `/api/ClientAccount/{id}` | Get client by ID |
| POST | `/api/ClientAccount` | Create client |
| PUT | `/api/ClientAccount/{id}` | Update client |
| DELETE | `/api/ClientAccount/{id}` | Delete client |
| GET | `/api/Ticket` | Get all tickets |
| GET | `/api/Ticket/{id}` | Get ticket by ID |
| POST | `/api/Ticket` | Create ticket |
| PUT | `/api/Ticket/{id}` | Update ticket |
| DELETE | `/api/Ticket/{id}` | Delete ticket |
| GET | `/api/TicketStatus` | Get all ticket statuses |
| GET | `/api/SLA` | Get all SLA configurations |
| POST | `/api/User/login` | User login (JWT) |
| POST | `/api/User/register` | User registration |

---

## 📸 Screenshots

### Home Page

*Landing page of the Ticket System*

<img width="1579" height="619" alt="Home page Ticket system" src="https://github.com/user-attachments/assets/586550be-f933-4e62-b7cf-25c2256fa862" />

---

### Registration

*Creating a new account*

<img width="1569" height="734" alt="Creating a new account" src="https://github.com/user-attachments/assets/ca9bdd49-cf7f-4e95-a5a1-59799763e59d" />

---

### Client Approval Flow

*Client waiting for admin to approve the account*

<img width="1014" height="429" alt="Client waiting for admin to approve the account" src="https://github.com/user-attachments/assets/3dcc49af-4301-48ea-ad9d-50080e45b903" />

*Admin Home Page*

<img width="1502" height="621" alt="Admin Home Page" src="https://github.com/user-attachments/assets/cab565b6-3158-4e2e-8027-125f1704d3be" />

*Admin approving the client account*

<img width="1451" height="707" alt="Admin approve the client account" src="https://github.com/user-attachments/assets/a6327b2d-e117-4bcd-8a3d-65de2c19e10a" />

*Temporary password notification (expires in 24 hours)*

<img width="899" height="300" alt="Temporary password that admin needs to send to client" src="https://github.com/user-attachments/assets/81580bb2-b6c0-4689-87dc-b403d4731cfa" />

*Password reset page*

<img width="665" height="376" alt="Password reset page" src="https://github.com/user-attachments/assets/6f36efb0-62b3-4649-9eb5-77e9b1460d7a" />

---

### Ticket Creation

*Client creating a ticket*

<img width="1599" height="524" alt="Client create a ticket" src="https://github.com/user-attachments/assets/8b27bd73-1805-4422-a996-670a1e922480" />

*Ticket displayed on client dashboard after creation*

<img width="1145" height="495" alt="How the ticket shows on the client's dashboard" src="https://github.com/user-attachments/assets/5998692f-2cf9-4e5b-9560-49af64e709d8" />

---

### Ticket Approval & Assignment

*Support approving the ticket*

<img width="1585" height="485" alt="Support approving the ticket" src="https://github.com/user-attachments/assets/0df57aa0-ecaa-4b5f-b360-094e1c2a6be6" />

*Support assigning the ticket to an assignee*

<img width="1566" height="482" alt="Support assigning the ticket to an assignee" src="https://github.com/user-attachments/assets/af23d4cc-750a-411c-9832-67fc92a1e616" />

*Assignee working on and closing the ticket*

<img width="1095" height="435" alt="Assignee working on and closing the ticket" src="https://github.com/user-attachments/assets/5d2f33b4-62e2-4ad5-b616-3bc513c6314e" />

---

### API Documentation (Swagger)

*JWT Authentication - Authorize with Bearer token*

<img width="697" height="338" alt="JWT token authentication in Swagger" src="https://github.com/user-attachments/assets/e21a5c06-b302-44b9-9bde-7b9aa999804e" />

*ClientAccount API endpoints*

<img width="1482" height="430" alt="ClientAccount API endpoints" src="https://github.com/user-attachments/assets/86e2e7f0-5a5a-44d8-8ec7-39fed03d0d25" />

*ClientAccountCompanyAccess and Companies API endpoints*

<img width="1487" height="576" alt="ClientAccountCompanyAccess and Companies API endpoints" src="https://github.com/user-attachments/assets/b53b64bc-6009-467b-9073-cd54933cb5a5" />

*Consumable Used and Consumable Items API endpoints*

<img width="1479" height="682" alt="Consumable Used and Items API endpoints" src="https://github.com/user-attachments/assets/4fe8bd9e-041b-4cc0-b48e-e54b4f6d0232" />

*SLA API endpoints*

<img width="1451" height="340" alt="SLA API endpoints" src="https://github.com/user-attachments/assets/266544bf-8c44-4979-8bff-c632febc368f" />

*Ticket API endpoints*

<img width="1460" height="732" alt="Ticket API endpoints" src="https://github.com/user-attachments/assets/49cf6a7e-e826-4b78-9aa8-fd199f8e7901" />

*TicketCategory and TicketStatus API endpoints*

<img width="1464" height="659" alt="TicketCategory and TicketStatus API endpoints" src="https://github.com/user-attachments/assets/ef9e17ca-6c57-4cc3-bc69-c8e6454c212d" />

*TicketStatus API endpoints*

<img width="1442" height="331" alt="TicketStatus API endpoints" src="https://github.com/user-attachments/assets/84f30939-0bf0-49a2-b858-c9448afdad26" />

*User API endpoints*

<img width="1453" height="555" alt="User API endpoints" src="https://github.com/user-attachments/assets/b28bc005-981a-4098-b9e3-bb0bbad5655b" />

*UserGroups API endpoints*

<img width="1454" height="432" alt="UserGroups API endpoints" src="https://github.com/user-attachments/assets/1bccff70-bbc8-4c13-b8c4-26a010943f69" />

---

## 📦 NuGet Packages Used

### API Project

- `Microsoft.EntityFrameworkCore.SqlServer`
- `Microsoft.EntityFrameworkCore.Tools`
- `AutoMapper.Extensions.Microsoft.DependencyInjection`
- `Microsoft.AspNetCore.Authentication.JwtBearer`
- `NSwag.AspNetCore`

### Blazor Project

- `NSwag.MSBuild`
- `Microsoft.AspNetCore.Components.WebAssembly.Authentication`

---

## 📝 Features Checklist

| Feature | Status |
|---------|--------|
| REST API with .NET 6 | ✅ |
| Entity Framework Core + SQL Server | ✅ |
| Blazor WebAssembly Frontend | ✅ |
| JWT Authentication | ✅ |
| Claims-based Authorization | ✅ |
| Client Account Management | ✅ |
| SLA Management | ✅ |
| Ticket Categories | ✅ |
| Ticket Lifecycle Management | ✅ |
| Time Logging | ✅ |
| Email Notifications | ✅ |
| Exception Reports | ✅ |
| SLA Breach Alerts | ✅ |
| NSwag HTTP Client Generation | ✅ |

---

## 📊 Ticket Status Flow

```
                    ┌─────────────┐
                    │   Pending   │
                    └──────┬──────┘
                           │
                           ▼
                    ┌─────────────┐
                    │   Accepted  │
                    └──────┬──────┘
                           │
                           ▼
              ┌────────────┴────────────┐
              │                         │
              ▼                         ▼
    ┌─────────────────┐       ┌─────────────────┐
    │   In Progress   │◄─────►│    Parked       │
    └────────┬────────┘       └─────────────────┘
             │
             ▼
    ┌─────────────────┐
    │     Closed      │
    └────────┬────────┘
             │
             ▼
    ┌─────────────────┐
    │   Reopened      │
    └─────────────────┘
```

---

**Made with .NET 6 and Blazor WebAssembly**
