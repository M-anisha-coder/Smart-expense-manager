# 📌 Smart Expense Manager

A .NET Core Console Application that helps individuals and small
business owners manage, categorize, and analyze their expenses from CSV
files.

------------------------------------------------------------------------

## 🚨 Problem

Managing expenses manually in Excel or notebooks becomes:

-   Time-consuming\
-   Error-prone\
-   Hard to analyze monthly & yearly trends\
-   Difficult to separate personal and business expenses

Many people download bank statements in CSV format but lack a
lightweight tool to process and analyze them efficiently.

------------------------------------------------------------------------

## 💡 Idea

Build a simple, fast, console-based expense manager that:

-   Imports CSV bank statements
-   Automatically categorizes transactions
-   Stores them locally in JSON
-   Generates monthly & yearly reports
-   Separates business expenses
-   Provides clean CLI-based reporting

------------------------------------------------------------------------

## ✅ Solution

Smart Expense Manager allows users to:

1.  Import transaction data from CSV files\
2.  Automatically categorize transactions\
3.  Store processed data in JSON format\
4.  Generate:
    -   Monthly reports
    -   Yearly summaries
5.  View category-wise breakdown
6.  Track business expenses separately

------------------------------------------------------------------------

## ✨ Features

-   📂 CSV Import
-   🏷 Automatic Categorization
-   💾 Persistent JSON Storage
-   📊 Monthly Report Generation
-   📈 Yearly Summary Report
-   💼 Business Expense Tracking
-   🎨 Clean CLI Output using Spectre.Console
-   🔐 Environment Variable Based Storage Path
-   🧩 Dependency Injection using
    Microsoft.Extensions.DependencyInjection

------------------------------------------------------------------------

## 🛠 Technologies Used

-   .NET 8 (Console Application)
-   C#
-   Spectre.Console
-   Microsoft Dependency Injection
-   JSON Serialization
-   Git & GitHub
-   SSH Authentication

------------------------------------------------------------------------

## 📁 Project Structure

    SmartExpenseManager/
    │
    ├── Models/
    ├── Services/
    │   ├── CsvImportService.cs
    │   ├── CategorizationService.cs
    │   ├── StorageService.cs
    │   └── ReportService.cs
    │
    ├── Program.cs
    ├── README.md
    └── SmartExpenseManager.csproj

------------------------------------------------------------------------

## ⚙️ Setup & Run Locally

### 1️⃣ Clone the repository

``` bash
git clone git@github.com:M-anisha-coder/Smart-expense-manager.git
```

### 2️⃣ Navigate into the project

``` bash
cd Smart-expense-manager
```

### 3️⃣ Restore dependencies

``` bash
dotnet restore
```

### 4️⃣ Run the application

``` bash
dotnet run
```

------------------------------------------------------------------------

## 🌍 Optional: Set Custom Storage Path (Environment Variable)

By default, the app stores data inside:

    /data/transactions.json

To change storage location:

### Windows (CMD)

``` bash
setx SMART_EXPENSE "D:\ExpenseData"
```

Restart terminal after setting.

If environment variable is not set, it falls back to default folder.

------------------------------------------------------------------------

## 📊 Sample CSV Format

    Date,Description,Amount
    2026-01-01,Salary,5000
    2026-01-02,Amazon Purchase,-1200
    2026-01-03,Uber Ride,-300

------------------------------------------------------------------------

## 🔮 Future Improvements

-   AI-based smart categorization
-   Budget limits & alerts
-   Export reports to PDF
-   SQLite integration
-   Web API version
-   Dashboard UI
-   Authentication & multi-user support

------------------------------------------------------------------------

## 👩‍💻 Skills Demonstrated

-   Clean Architecture Thinking
-   Service-Based Design
-   Dependency Injection
-   File Handling
-   JSON Serialization
-   Exception Handling
-   Git & Multi-Account SSH Setup
-   Console UI Enhancement
-   Environment Configuration
-   Problem-Solving & Debugging

------------------------------------------------------------------------

## 📌 Author

**Manisha**\
FullStack .NET Developer\
Passionate about building practical, real-world tools 🚀
