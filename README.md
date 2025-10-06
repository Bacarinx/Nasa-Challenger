# 🌍 NASA Air Quality Health Alert App 🚀

A full-stack application developed for the **NASA Space Apps Challenge**, this solution helps protect individuals—especially those with **respiratory conditions**—from dangerous air quality. It leverages **real-time sensor data from NASA's TEMPO mission**, **ground-based sensors**, and **weather data** to provide **personalized alerts and forecasts**.

---

## 🧠 About the Project

### 🎯 Challenge Overview

This project responds to the challenge:

> **"TEMPO: Air Quality from Space"**
> Build an app that forecasts air quality by integrating real-time TEMPO data with ground-based air quality measurements and weather data. Notify users—especially vulnerable groups—about potential health risks.

**TEMPO (Tropospheric Emissions: Monitoring of Pollution)** enables hourly measurements of pollutants over North America. Combined with OpenAQ and weather data, this app provides relevant, real-time health alerts to the public.

---

## 👥 Target Users

- Adults with respiratory diseases (asthma, COPD, etc.)
- Elderly individuals (registered by caregivers)
- Families concerned about air health for vulnerable loved ones
- General public wanting to monitor local air conditions

---

## 🧩 How It Works

1. **User Registration**
    - Users create a profile with health conditions and address.
    - Caregivers can register dependents (e.g., elderly parents).

2. **Air Quality Monitoring**
    - Air quality is monitored in real-time via OpenAQ and NASA’s TEMPO data.
    - Weather APIs may be used to improve prediction accuracy.

3. **Alert System**
    - If poor air quality is detected in a user’s area, push notifications are sent.
    - Alerts are based on a risk model that considers personal health data.
    - Caregivers are notified for their dependents.

4. **Location & Visualization**
    - Users can enable GPS or search by address to view local air quality.
    - Google Maps displays air quality using a **color-coded risk scale**.

---

## 📲 Key Features

- 👤 **Health-Based Alerts**: Personalized notifications based on respiratory risk
- 🗺️ **Interactive Map**: Color-coded zones (Good → Hazardous) using Google Maps
- 📞 **Contact Management**: Caregivers linked to dependents
- 📉 **Historical & Forecast Data**: Trends and predictions from sensor data
- 🔐 **Secure Data**: Privacy-first, following best practices for sensitive data

---

## 🏛️ Tech Stack

| Layer    | Technology                      |
| -------- | ------------------------------- |
| Frontend | Ionic + Angular                 |
| Backend  | ASP.NET Core 9                  |
| Database | SQL Server                      |
| Maps     | Google Maps API                 |
| Air Data | NASA TEMPO, OpenAQ              |
| Weather  | External Weather API (optional) |

---

## 🗃️ Project Structure

```bash
/
├── /app   # Frontend - Ionic/Angular
└── /back  # Backend - .NET 9 API
```

---

## ✅ Prerequisites

- Make sure you have the following installed:

- .NET SDK 9

- Node.js (LTS)

- Ionic CLI: npm install -g @ionic/cli

- Angular CLI: npm install -g @angular/cli

- SQL Server (2019+)

---

## ⚙️ Setup & Installation

Follow these steps to get your development environment set up.

### 1\. Database Setup

First, make sure your **MSSQL Server instance is running** or try using our Docker compose file in the root. You need to create the database manually.

```SQL
CREATE DATABASE nasadb;
```

### 2\. Backend Setup (`/back`)

The backend is a .NET API that connects to the `nasadb` database.

1.  **Navigate to the backend directory**:

```bash
cd back
```

2.  **Configure the Connection String**:
    Open the `appsettings.Development.json` and `appsettings.json` file. Find the `ConnectionStrings` section and update the `DefaultConnection` value with your MSSQL Server credentials. The database name should be `nasadb`.

```json
{
	"ConnectionStrings": {
		"DefaultConnection": "Server=YOUR_SERVER_NAME;Database=nasadb;User Id=YOUR_USER;Password=YOUR_PASSWORD;Trusted_Connection=False;TrustServerCertificate=True;"
	}
}
```

_Replace `YOUR_SERVER_NAME`, `YOUR_USER`, and `YOUR_PASSWORD` with your own details._

3.  **Apply Database Migrations**:
    Run the following command to create the `nasadb` database and apply the table schemas using Entity Framework.

```bash
dotnet tool install dotnet-ef
```

```bash
dotnet ef database update
```

### 3\. Frontend Setup (`/app`)

The frontend is an Ionic/Angular application that consumes the backend API and uses Google Maps.

1.  **Navigate to the frontend directory**:

```bash
cd app
```

2.  **Install Dependencies**:
    Run the following command to install all the required Node.js packages.

```bash
npm install
```

---

## ▶️ Running the Application

You need to run both the backend and frontend servers simultaneously.

### Start the Backend API

In the `/back` directory, run:

```bash
dotnet run
```

The API will typically start on `https://localhost:7001` or a similar port. Check the terminal output for the exact URL.

### Start the Frontend App

In a **new terminal**, navigate to the `/app` directory and run:

```bash
ionic serve
```

This will launch the application in your default web browser, usually at `http://localhost:8100`.

---

### 🧪 Testing the Full Flow

Once both servers are running:

1. 🌐 Visit [`http://localhost:8100`](http://localhost:8100) in your browser.
2. 📝 Register a new user with a respiratory condition.
3. 📍 Grant location permission (or manually enter an address).
4. 📊 The app will display air quality for that region using color-coded indicators.
5. 🚨 If conditions are poor, alerts will be generated and shown as notifications for the user (and their caregiver, if registered).

---

### 🛠️ Troubleshooting

- ❌ **Backend can't connect to the database?**
  Double-check the connection string in your `appsettings.json` file. Make sure:
    - The SQL Server instance is running
    - The database name, username, and password are correct

> Still stuck? Open an issue in the repo and we'll try to help!

---

## 📡 Data Sources

- OpenAQ – Aggregated real-time air quality data from ground-based sensors.

## 🚀 Built for NASA Space Apps Challenge

This app was created for the **NASA Space Apps Challenge**, with the mission to:

> 🛰️ **Help individuals monitor air quality and reduce exposure to pollution**
> by combining space-based data, ground sensors, and user health profiles
> into an intuitive and actionable application.

> 💙 Together, we can make the invisible threat of air pollution visible and actionable.
