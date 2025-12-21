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

## 🤝 Contributing

Contributions are welcome and appreciated.

1. Fork the repository  
2. Create a new branch (`feature/new-feature`)  
3. Commit your changes  
4. Open a Pull Request  

---

## 📄 License

This project is licensed under the **MIT License**.  
See the `LICENSE` file for details.

---

## 👩‍💻 Author

**Berfin Tek**  
GitHub: https://github.com/berfin-t
