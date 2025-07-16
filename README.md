# 📁 PortfolioAPI

**PortfolioAPI** is an API for managing a development team's portfolio with admin panel features and JWT authentication.

---

## 🚀 Features

- 🔄 CRUD operations for team members
- 🧠 Add projects and skills for each member
- 🔐 JWT authentication for admins
- 🧰 Detailed skill structure (programming languages, technologies, databases, etc.)
- 🗂️ MongoDB (Atlas) integration
- 📄 Swagger documentation
- 📦 Dockerfile for containerization

---

## 🛠 Technologies

- **Backend**: ASP.NET Core 8.0
- **Database**: MongoDB (cloud cluster via Atlas)
- **Authentication**: JWT + BCrypt
- **API Documentation**: Swagger UI
- **Object Mapping**: AutoMapper
- **Containerization**: Docker

---

## 📦 Project Structure

```
PortfolioAPI/
├── Controllers/
│   ├── AdminController.cs            # Admin functions (projects/skills)
│   ├── AuthController.cs             # Authentication
│   └── TeamMembersController.cs     # CRUD for team members
├── Data/
│   └── data.json                     # Sample team members
├── DTOs/                             # Data Transfer Objects
├── Models/                           # MongoDB models
├── Services/                         # Business logic
├── appsettings.json                 # Config (MongoDB, JWT)
└── Dockerfile                        # Docker configuration
```

---

## 🏁 Getting Started

### ✅ Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/)
- [MongoDB Atlas](https://www.mongodb.com/atlas) or a local MongoDB server
- [Docker](https://www.docker.com/) (optional)

### ▶️ Run with Docker

```bash
docker build -t portfolioapi .
docker run -p 5011:80 -e ASPNETCORE_ENVIRONMENT=Development portfolioapi
```

### ▶️ Run Locally

1. Update your MongoDB connection string in `appsettings.json`
2. Run the project:

```bash
dotnet run
```

3. Open [http://localhost:5011/swagger](http://localhost:5011/swagger) in your browser

---

## 🔐 Authentication

To access admin features:

1. Authenticate via `/api/auth/login`
2. Use the returned JWT in the header:

```
Authorization: Bearer <token>
```

📥 **Sample Request:**

```http
POST /api/auth/login
Content-Type: application/json

{
  "username": "admin",
  "password": "securePassword"
}
```

---

## 📌 API Request Examples

🔍 **Get all team members**

```http
GET /api/teammembers
```

➕ **Add a project to a team member (requires authorization)**

```http
POST /api/admin/team-member/{id}/projects
Content-Type: application/json
Authorization: Bearer <your_token>

{
  "title": "New Project",
  "type": "Web Development",
  "description": "Project description...",
  "links": ["https://github.com/example"]
}
```

🛠 **Update a team member's skills**

```http
POST /api/admin/team-member/{id}/skills
Content-Type: application/json
Authorization: Bearer <your_token>

{
  "category": "programming_languages",
  "skill": "Rust"
}
```

🌐 **Sample team member from data.json**

```json
{
  "full_name": "Yevhen Svitlichnyi",
  "position": "Full-Stack Developer",
  "skills": {
    "programming_languages": ["C#", "JavaScript", "Python"],
    "databases": ["MongoDB", "PostgreSQL"],
    "frameworks": [".NET", "React"]
  },
  "projects": [
    {
      "title": "Web application for accelerated aging assessment",
      "description": "Blazor web app with MS SQL database",
      "links": ["https://github.com/..."]
    }
  ]
}
```

---

## 👤 Author

**Yevhen Svitlichnyi**
📧 [evgen.svitlichnyi@gmail.com](mailto:evgen.svitlichnyi@gmail.com)
🔗 [LinkedIn](https://linkedin.com/in/yevhen-svitlychnyi)
