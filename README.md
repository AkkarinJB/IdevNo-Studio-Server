# IdevNo Studio API

Backend API สำหรับ IdevNo Studio ที่ใช้ ASP.NET Core 8.0 พร้อม JWT Authentication และ MySQL Database

## คุณสมบัติ

- ✅ ASP.NET Core 8.0 Web API
- ✅ Swagger/OpenAPI Documentation
- ✅ MySQL Database (Pomelo.EntityFrameworkCore.MySql)
- ✅ JWT Authentication
- ✅ Login และ Register Endpoints
- ✅ Password Hashing ด้วย BCrypt

## การติดตั้งและรันโปรเจกต์

### 1. ติดตั้ง .NET SDK 8.0

ตรวจสอบว่ามี .NET SDK 8.0 ติดตั้งอยู่:
```bash
dotnet --version
```

### 2. Restore Dependencies

```bash
cd server
dotnet restore
```

### 3. สร้าง Database

โปรเจกต์จะสร้างตาราง Users อัตโนมัติเมื่อรันครั้งแรก (EnsureCreated)

หรือสามารถใช้ Migration:
```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### 4. รันโปรเจกต์

```bash
dotnet run
```

หรือใช้ HTTPS:
```bash
dotnet run --launch-profile https
```

API จะรันที่:
- HTTP: `http://localhost:5000`
- HTTPS: `https://localhost:5001`
- Swagger UI: `http://localhost:5000/swagger` หรือ `https://localhost:5001/swagger`

## API Endpoints

### Register
```
POST /api/auth/register
Content-Type: application/json

{
  "email": "user@example.com",
  "password": "password123",
  "firstName": "John",
  "lastName": "Doe"
}
```

### Login
```
POST /api/auth/login
Content-Type: application/json

{
  "email": "user@example.com",
  "password": "password123"
}
```

Response:
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "email": "user@example.com",
  "firstName": "John",
  "lastName": "Doe",
  "expiresAt": "2024-01-01T12:00:00Z"
}
```

## การใช้ JWT Token

หลังจาก Login หรือ Register สำเร็จ จะได้รับ JWT Token

ในการเรียก API ที่ต้องการ Authentication:
```
Authorization: Bearer {your_token_here}
```

ใน Swagger UI:
1. คลิกที่ปุ่ม "Authorize" ด้านบน
2. ใส่ `Bearer {your_token}` (รวมคำว่า Bearer ด้วย)
3. คลิก Authorize

## Database Configuration

การตั้งค่า Database อยู่ใน `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=thsv87.hostatom.com;Port=3306;Database=kitkh_geno;User=kitkh_geno;Password=genodev@kkk;CharSet=utf8mb4;"
  }
}
```

## JWT Configuration

การตั้งค่า JWT อยู่ใน `appsettings.json`:

```json
{
  "JwtSettings": {
    "SecretKey": "YourSuperSecretKeyForJWTTokenGenerationThatShouldBeAtLeast32CharactersLong",
    "Issuer": "IdevNoStudio",
    "Audience": "IdevNoStudioUsers",
    "ExpirationInMinutes": 60
  }
}
```

**⚠️ หมายเหตุ:** ควรเปลี่ยน SecretKey เป็นค่าที่ปลอดภัยและเก็บไว้เป็นความลับใน Production

## โครงสร้างโปรเจกต์

```
server/
├── Controllers/
│   └── AuthController.cs      # Login และ Register endpoints
├── Data/
│   └── ApplicationDbContext.cs # Database context
├── Models/
│   ├── User.cs                 # User entity
│   ├── LoginRequest.cs         # Login request model
│   ├── RegisterRequest.cs      # Register request model
│   └── AuthResponse.cs         # Authentication response model
├── Services/
│   ├── IAuthService.cs         # Auth service interface
│   ├── AuthService.cs          # Auth service implementation
│   ├── IJwtService.cs          # JWT service interface
│   └── JwtService.cs           # JWT service implementation
├── Properties/
│   └── launchSettings.json     # Launch settings
├── appsettings.json            # Application settings
├── Program.cs                  # Application entry point
└── IdevNoStudio.Api.csproj     # Project file
```

## Dependencies

- Microsoft.AspNetCore.Authentication.JwtBearer (8.0.0)
- Microsoft.EntityFrameworkCore (8.0.0)
- Pomelo.EntityFrameworkCore.MySql (8.0.0)
- BCrypt.Net-Next (4.0.3)
- Swashbuckle.AspNetCore (6.5.0)

## Development

### สร้าง Migration ใหม่
```bash
dotnet ef migrations add MigrationName
dotnet ef database update
```

### ตรวจสอบ Database Connection
```bash
dotnet ef database info
```

## License

MIT

