# CRM (Customer Relationship Management)  - ABP Framework & Blazor

![.NET](https://img.shields.io/badge/.NET-9.0-blue)
![ABP](https://img.shields.io/badge/ABP-Framework-green)
![Blazor](https://img.shields.io/badge/Blazor-UI-purple)
![Docker](https://img.shields.io/badge/Docker-Containerized-blue)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-Database-blue)
![License](https://img.shields.io/badge/License-MIT-success)

A modern, modular, and scalable **Customer Relationship Management (CRM)** application developed using **ABP Framework** and **Blazor**.

The project leverages ABP’s **layered and modular architecture**, providing a clean, maintainable, and enterprise-ready codebase suitable for real-world business scenarios.

---

## 🚀 Features

- 👤 Customer management (CRUD operations)
- 🔐 Authentication & authorization (ABP Identity)
- 📧 Mailto integration for email communication
- 🔔 Real-time updates with SignalR
- 🔁 AutoMapper for DTO ↔ Entity mapping
- 🎨 Blazorise for responsive and modern UI
- 🧩 Modular & layered architecture
- 🐳 Fully Dockerized environment

---

## 🛠️ Tech Stack

- ABP Framework
- Blazor
- Blazorise
- Entity Framework Core
- PostgreSQL
- SignalR
- AutoMapper
- Docker & Docker Compose
- .NET 9.0

---

## 🧱 Project Architecture

```text
├── src
│   ├── Crm.Application
│   ├── Crm.Domain
│   ├── Crm.EntityFrameworkCore
│   ├── Crm.HttpApi
│   ├── Crm.HttpApi.Client
│   └── Crm.Blazor
├── docker-compose.yml
├── migrator-compose.yml
└── README.md
```
## 🧩 Design Patterns & Architecture

The project applies enterprise design patterns aligned with ABP Framework best practices:

### ✔ Core Patterns

#### - Layered Architecture
Separation of Domain, Application, Infrastructure, and Presentation layers.

#### - Repository Pattern
Abstracts data access logic using IRepository<TEntity, TKey>.

#### - Unit of Work Pattern
Automatically managed by ABP to ensure transactional consistency.

#### - Application Service Pattern
Use-case–oriented services orchestrating domain logic.

#### - Domain Service Pattern
Encapsulation of complex business rules (e.g. ActivityManager, EmployeeManager).

#### - DTO Pattern
Isolates domain entities from external layers with AutoMapper support.

#### - Dependency Injection (DI)
Loose coupling via ABP’s built-in IoC container.

#### - Soft Delete Pattern
Logical deletion using ABP’s soft delete mechanism.

#### - CQRS (Lightweight)
Clear separation of read and write operations at service level.

---
## 📬 Automated Activity Email Notifications
 
The application integrates **Hangfire** as a background job processor to deliver automated email notifications whenever a new activity is created.
 
### How It Works
 
When a new activity (e.g. a **meeting** or **call**) is added to the system, Hangfire enqueues a background job that automatically sends an informational email to **all associated customers and employees** whose email addresses are registered in the system.
 
```
New Activity Created
        │
        ▼
 Hangfire Job Enqueued
        │
        ▼
 Resolve Recipients
 (Customers & Employees linked to the activity)
        │
        ▼
 Send Email Notification
 (Activity details delivered to each recipient)
```
 
### Email Content
 
Each notification email contains the key details of the newly created activity, including:
 
- Activity type (Meeting, Call, etc.)
- Activity title and description
- Scheduled date and time
- Linked customer and employee information
### Key Benefits
 
| Benefit | Description |
|---|---|
| **Non-blocking** | Email delivery runs in the background without affecting the user experience |
| **Reliable** | Hangfire persists jobs to the database, ensuring delivery even after restarts |
| **Scalable** | Background processing is decoupled from the main request pipeline |
| **Automatic** | No manual action needed — notifications are triggered on every activity creation |
 
> 💡 Hangfire's built-in dashboard can be used to monitor job status, retry failed jobs, and inspect execution history.
 
---

## ⚙️ Setup & Installation

### Prerequisites
- .NET SDK 9.0 or higher
- Docker & Docker Compose

---

### Installation Steps

#### 1. Clone the Repository
```sh
git clone https://github.com/berfin-t/Crm.git
cd Crm

```

#### 2.Create Docker Network

```sh
docker network create crm-backend
```

#### 3.Start Docker Containers

```sh
docker compose up -d 
```

#### 4.Run Database Migrations

```sh
docker compose -f migrator-compose.yml run --rm -d migrator 
```

#### 🌐 Access the Application
Once the services are running, open your browser and navigate to:
- **Blazor UI:** [http://localhost:3232/](http://localhost:44376/)

---

## 🔐 Default User Credentials

The application comes with predefined users for testing and development purposes.

### 👑 Admin User
- **Username:** admin
- **Password:** 1q2w3E*

**Permissions:**
- Full system access
- User & role management
- Customer and employee management

---

### 👤 Employee User
- **Username:** employee_berfin
- **Password:** 1q2w3E*

**Permissions:**
- Customer management
- Limited system access based on role

> ⚠️ **Security Notice:**  
> These credentials are intended for **development and testing only**.  
> Make sure to change default passwords before deploying to a production environment.


## 🔔 Real-Time Communication

The application uses **SignalR** to enable real-time communication between the server and clients.

- Live data updates without page refresh
- Real-time notifications
- Improved user experience with instant UI synchronization

---

## 📧 Email Integration

**Mailto integration** is used to allow users to send emails directly from the application interface.

- Fast and simple customer communication
- Opens the default mail client with pre-filled data
- No additional email server configuration required

---

## 🔄 Object Mapping

**AutoMapper** is used to manage object-to-object mappings across application layers.

- Clean separation between Entities and DTOs
- Reduced boilerplate code
- Centralized and maintainable mapping configuration

---

## 📄 License

This project is licensed under the **MIT License**.  
See the `LICENSE` file for details.

---

## 👩‍💻 Author

**Berfin Tek**  
GitHub: https://github.com/berfin-t
