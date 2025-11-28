# 🔐 วิธีสร้าง GitHub Personal Access Token

## ⚠️ ปัญหา
GitHub ไม่รองรับ password authentication แล้ว ต้องใช้ **Personal Access Token (PAT)**

## 📋 ขั้นตอนการสร้าง Token

### 1. เปิดเบราว์เซอร์
ไปที่: **https://github.com/settings/tokens**

### 2. Login (ถ้ายังไม่ได้ login)
- ใส่ Username/Email และ Password
- ผ่าน 2FA (ถ้ามี)

### 3. สร้าง Token
1. คลิก **"Generate new token"** → **"Generate new token (classic)"**
2. ตั้งชื่อ: `IdevNo-Studio-Server`
3. เลือก **Expiration**: 
   - `90 days` (แนะนำ)
   - หรือ `No expiration` (ถ้าต้องการ)
4. เลือก **Scopes**:
   - ✅ **`repo`** (Full control of private repositories)
     - นี้จะรวม `repo:status`, `repo_deployment`, `public_repo`, `repo:invite`, `security_events`
5. เลื่อนลงไปด้านล่าง
6. คลิก **"Generate token"** (สีเขียว)

### 4. ⚠️ คัดลอก Token ทันที!
- Token จะแสดงแค่ **ครั้งเดียว**
- หน้าตาเหมือน: `ghp_xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx`
- **คัดลอกเก็บไว้ก่อน!**

## 🚀 วิธีใช้ Token

### วิธีที่ 1: ใช้ใน Terminal (แนะนำ)
```bash
cd /Users/akkarin/Documents/IdevNo-Studio/server
git push -u origin main
```

เมื่อถาม:
- **Username**: `AkkarinJB`
- **Password**: ใส่ **token** ที่คัดลอกมา (ไม่ใช่ password จริง)

ระบบจะเก็บ token ไว้ใน Keychain และใช้ในครั้งต่อไปโดยไม่ต้องใส่ใหม่

### วิธีที่ 2: ใช้ใน URL (ไม่แนะนำ - ไม่ปลอดภัย)
```bash
git remote set-url origin https://YOUR_TOKEN@github.com/AkkarinJB/IdevNo-Studio-Server.git
git push -u origin main
```

⚠️ **ไม่แนะนำ** เพราะ token จะเห็นใน git config

## ✅ ตรวจสอบว่า Token ทำงาน

หลังจาก push สำเร็จ:
```bash
git push -u origin main
```

ควรเห็น:
```
Enumerating objects: ...
Writing objects: ...
To https://github.com/AkkarinJB/IdevNo-Studio-Server.git
 * [new branch]      main -> main
Branch 'main' set up to track remote branch 'main' from 'origin'.
```

## 🔒 Security Tips

1. **อย่า share token** กับใคร
2. **อย่า commit token** ลง code
3. **ลบ token** ถ้าไม่ใช้แล้ว (ไปที่ https://github.com/settings/tokens)
4. **ใช้ expiration** เพื่อความปลอดภัย

## 🆘 Troubleshooting

### Token ไม่ทำงาน
- ตรวจสอบว่าเลือก scope `repo` แล้ว
- ตรวจสอบว่า token ยังไม่หมดอายุ
- ลองสร้าง token ใหม่

### ยังถาม password
- ลบ credentials เก่า:
  ```bash
  printf "host=github.com\nprotocol=https\n\n" | git credential-osxkeychain erase
  ```
- ลอง push ใหม่

### Token หมดอายุ
- สร้าง token ใหม่
- ใช้ token ใหม่ในการ push

## 📚 เอกสารเพิ่มเติม

- [GitHub Docs: Creating a personal access token](https://docs.github.com/en/authentication/keeping-your-account-and-data-secure/creating-a-personal-access-token)
- [GitHub Docs: Using a personal access token](https://docs.github.com/en/authentication/keeping-your-account-and-data-secure/using-a-personal-access-token)

