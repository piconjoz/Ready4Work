# Setup Guide

## Prerequisites

- .NET 8.0 SDK
- MySQL Server
- Git

## MySQL Setup Options

### Option 1: Docker (Recommended), unless you prefer native installation

**Install Docker:**
- Mac: Download Docker Desktop from [docker.com](https://www.docker.com/products/docker-desktop)
- Windows: Download Docker Desktop from [docker.com](https://www.docker.com/products/docker-desktop)

**Run MySQL Container:**
```bash
docker run --name mysql-ready4work -e MYSQL_ROOT_PASSWORD=your_password -p 3306:3306 -d mysql:8.0
docker ps  # verify container is running
```

### Option 2: Native Installation

**macOS:**
```bash
brew install mysql
brew services start mysql
mysql_secure_installation
```

**Windows:**
1. Download MySQL Installer from mysql.com
2. Run installer and choose "Developer Default"
3. Set root password during installation
4. Start MySQL service

**Linux, if you're using:**
```bash
sudo apt update
sudo apt install mysql-server
sudo mysql_secure_installation
sudo systemctl start mysql
```

## Project Setup

### 1. Clone and Navigate
```bash
git clone <your-repo-url>
cd project/backend
```

### 2. Install .NET 8 SDK
Download from [dotnet.microsoft.com](https://dotnet.microsoft.com)

### 3. Restore Dependencies
```bash
dotnet restore
```

### 4. Configure Database Connection
```bash
# Initialize user secrets
# This is quite important for local dev, to avoid hardcoding sensitive data, not requiring .env file as well, but remember this one just for local dev
dotnet user-secrets init

# Set connection string (replace YOUR_PASSWORD)
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=localhost;Port=3306;Database=<YOUR_DB_NAME>;Uid=<YOUR_USERNAME_FOR_MYSQL>;Pwd=<YOUR_PASSWORD>;"
```

### 5. Setup Database
```bash
# Install EF Tools
dotnet tool install --global dotnet-ef --version 8.0.8

# Create and apply migration
dotnet ef migrations add InitialCompanyMigration
dotnet ef database update
```

### 6. Run Application
```bash
dotnet run
```

Visit: http://localhost:5077/swagger

## Troubleshooting

**Connection Issues:**
- Check MySQL is running: `docker ps` or `brew services list mysql`
- Verify user secrets: `dotnet user-secrets list`

**Migration Errors:**
- Ensure correct EF Tools version: `dotnet ef --version` (should be 8.0.x)
- Reset database: `dotnet ef database drop` then `dotnet ef database update`

**Package Version Issues:**
- Verify .NET 8 compatibility: `dotnet list package`
- All Microsoft.* packages should be 8.0.x