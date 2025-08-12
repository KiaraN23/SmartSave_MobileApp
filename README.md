# SmartSave – Personal Finance Mobile App with Predictive Artificial Intelligence

## 📌 Project Overview
The **SmartSave** project aims to develop a cross-platform mobile application that helps users manage their personal finances intelligently.  
Through the analysis of financial habits and the use of artificial intelligence models, the app provides personalized recommendations to promote savings, reduce unnecessary expenses, and achieve specific financial goals.

This project was motivated by the lack of financial literacy and accessible tools for personal money management, especially among young people and adults with variable income.  
**SmartSave** seeks to fill this gap by offering an intuitive platform that assists users in making financial decisions based on reliable data and predictions.

---

## 🛠 Tech Stack
- **Frontend:** React Native (separate repository)
- **Backend:** C# (ASP.NET Core 8)
- **Database:** SQL Server
- **AI Integration:** OpenRouter API with `mistralai/mistral-7b-instruct`

---

## 🚀 Features (First Functional Version)
- **User Authentication:** Registration and login using a custom backend in C#.
- **Financial Records:** Manual entry of income, expenses, debts, and goals.
- **AI Recommendations:** Predictions and suggestions via integration with the OpenRouter API.

---

## 📡 Main Endpoints
```
POST /api/auth/register   -> Register a new user  
POST /api/auth/login      -> Authenticate user and obtain token  

GET  /api/goal/all        -> Retrieve all user goals  
POST /api/goal/create     -> Create a new financial goal  

GET  /api/debt/all        -> Retrieve all user debts  
POST /api/debt/create     -> Register a new debt  

GET  /api/transaction/all -> Retrieve all financial transactions  
POST /api/transaction/create -> Create a new transaction  

POST /api/suggestion      -> Get AI-based financial suggestions  
POST /api/prediction      -> Get AI-based financial predictions
```

---

## 📦 Installation & Setup
```
# Clone the repository
git clone https://github.com/your-username/smartsave-api.git

# Navigate to the project folder
cd smartsave-api

# Restore dependencies
dotnet restore

# Update database
dotnet ef database update

# Run the application
dotnet run
```

---

## 📄 License
This project is licensed under the MIT License.
