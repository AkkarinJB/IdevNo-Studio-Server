# Railway Deployment Guide

คู่มือการ Deploy ASP.NET Core API ขึ้น Railway และตั้งค่า Auto-Deploy จาก GitHub

## 📋 สิ่งที่ต้องเตรียม

1. บัญชี Railway (https://railway.app)
2. GitHub Repository สำหรับโปรเจกต์
3. Database Connection String
4. JWT Secret Key

## 🚀 ขั้นตอนการ Deploy

### 1. เตรียม GitHub Repository

```bash
# สร้าง git repository (ถ้ายังไม่มี)
cd server
git init
git add .
git commit -m "Initial commit for Railway deployment"

# เพิ่ม remote repository
git remote add origin https://github.com/YOUR_USERNAME/YOUR_REPO.git
git branch -M main
git push -u origin main
```

### 2. สร้างโปรเจกต์ใน Railway

1. เข้าไปที่ https://railway.app
2. คลิก **"New Project"**
3. เลือก **"Deploy from GitHub repo"**
4. เลือก repository ที่ต้องการ deploy
5. Railway จะ auto-detect .NET project

### 3. ตั้งค่า Root Directory

ถ้าโปรเจกต์อยู่ใน folder `server/`:

1. ไปที่ **Settings** ของ service
2. ตั้งค่า **Root Directory** เป็น `server`
3. หรือใช้ **railway.json** ที่สร้างไว้แล้ว

### 4. ตั้งค่า Environment Variables

ไปที่ **Variables** tab ใน Railway และเพิ่ม:

#### Database Connection
```
DATABASE_URL=Server=thsv87.hostatom.com;Port=3306;Database=kitkh_geno;User=kitkh_geno;Password=genodev@kkk;CharSet=utf8mb4;
```

#### JWT Settings
```
JWT_SECRET_KEY=YourSuperSecretKeyForJWTTokenGenerationThatShouldBeAtLeast32CharactersLong
JWT_ISSUER=IdevNoStudio
JWT_AUDIENCE=IdevNoStudioUsers
JWT_EXPIRATION_MINUTES=60
```

#### Swagger (Optional)
```
EnableSwagger=true
```

**⚠️ หมายเหตุ:** 
- อย่า hardcode sensitive data ใน code
- ใช้ Environment Variables สำหรับ production
- JWT_SECRET_KEY ควรเป็น random string ที่ยาวและปลอดภัย

### 5. ตั้งค่า Auto-Deploy

1. ไปที่ **Settings** → **Source**
2. ตรวจสอบว่า **Auto Deploy** เปิดอยู่
3. เลือก branch ที่ต้องการ (ปกติเป็น `main` หรือ `master`)

### 6. Deploy

1. Railway จะเริ่ม build และ deploy อัตโนมัติ
2. ดู logs ใน **Deployments** tab
3. รอให้ deployment เสร็จ

### 7. ตั้งค่า Custom Domain (Optional)

1. ไปที่ **Settings** → **Networking**
2. คลิก **Generate Domain** หรือเพิ่ม custom domain
3. Railway จะให้ URL เช่น `https://your-app.railway.app`

## 🔄 Auto-Deploy จาก GitHub

เมื่อตั้งค่าเสร็จแล้ว:

1. **ทุกครั้งที่ push ไปยัง GitHub** → Railway จะ auto-deploy
2. **Pull Request** → สามารถตั้งค่าให้ deploy preview ได้
3. **Manual Deploy** → สามารถ deploy manual ได้จาก Railway dashboard

## 📝 ตรวจสอบ Deployment

### ดู Logs
- ไปที่ **Deployments** tab
- คลิกที่ deployment ล่าสุด
- ดู logs เพื่อตรวจสอบ errors

### ทดสอบ API
```bash
# ทดสอบ Health Check (ถ้ามี)
curl https://your-app.railway.app/swagger

# ทดสอบ Register
curl -X POST https://your-app.railway.app/api/auth/register \
  -H "Content-Type: application/json" \
  -d '{
    "email": "test@example.com",
    "password": "password123",
    "firstName": "Test",
    "lastName": "User"
  }'
```

## 🐛 Troubleshooting

### Build Fails
- ตรวจสอบว่า `.csproj` file ถูกต้อง
- ตรวจสอบ logs ใน Railway
- ตรวจสอบว่า dependencies ถูกต้อง

### Database Connection Error
- ตรวจสอบ `DATABASE_URL` environment variable
- ตรวจสอบว่า database server เปิดอยู่
- ตรวจสอบ firewall rules

### Port Error
- Railway จะให้ `PORT` environment variable อัตโนมัติ
- ตรวจสอบว่า app ใช้ `PORT` variable

### Swagger ไม่แสดง
- ตรวจสอบ `EnableSwagger` environment variable
- ตรวจสอบว่า Swagger middleware ถูก configure

## 📁 ไฟล์ที่สำคัญ

- `railway.json` - Railway configuration
- `nixpacks.toml` - Build configuration
- `.railwayignore` - ไฟล์ที่ Railway จะ ignore
- `appsettings.Production.json` - Production settings

## 🔒 Security Best Practices

1. **อย่า commit sensitive data** ลง GitHub
2. **ใช้ Environment Variables** สำหรับ secrets
3. **เปลี่ยน JWT Secret Key** เป็นค่าที่ปลอดภัย
4. **ใช้ HTTPS** เสมอ (Railway ให้ HTTPS อัตโนมัติ)
5. **ตั้งค่า CORS** ให้เหมาะสม (ตอนนี้เปิด AllowAll)

## 📚 Resources

- [Railway Documentation](https://docs.railway.app)
- [Railway .NET Guide](https://docs.railway.app/guides/dotnet)
- [Environment Variables](https://docs.railway.app/develop/variables)

## ✅ Checklist

- [ ] GitHub repository สร้างแล้ว
- [ ] Railway project สร้างแล้ว
- [ ] Environment variables ตั้งค่าแล้ว
- [ ] Auto-deploy เปิดใช้งานแล้ว
- [ ] Database connection ทำงาน
- [ ] API ทดสอบแล้ว
- [ ] Swagger เปิดใช้งานได้
- [ ] Custom domain ตั้งค่าแล้ว (ถ้าต้องการ)

