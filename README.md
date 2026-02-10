# Incident Management System

A lightweight, cloud-ready **Incident Management System** designed to demonstrate clean backend architecture, secure Azure integrations, and a simple React-based UI for managing incidents end to end.

---

## ✨ Features

* Create incidents with file attachments (screenshots, logs)
* List incidents with filtering and pagination
* Update incident status
* Secure file storage using Azure Blob Storage
* Secrets managed via Azure Key Vault (no secrets in code)
* Clean, layered backend architecture
* Simple, responsive UI built with React and Material UI

---

## 🏗️ Core Architecture Overview

### 1. Backend API

**ASP.NET Core Web API (.NET 7)**

**Responsibilities**

* REST endpoints for:

  * Creating incidents (with file upload)
  * Listing incidents (filters + pagination)
  * Updating incident status

**Technical Highlights**

* Entity Framework Core with SQLite for lightweight persistence
* Clean separation of concerns:

  * Controllers → Services → Data (EF Core)
* JSON serialization configured to avoid navigation cycles
* Swagger enabled for API discovery and testing

---

### 2. Azure Integrations

#### Azure Blob Storage

* Stores incident attachments (screenshots, logs)
* Database stores metadata only (URL, filename, size)
* Keeps API stateless and reduces storage overhead

#### Azure Key Vault

* Stores sensitive configuration:

  * Blob Storage connection string
  * Database connection string
* Accessed via:

  * Managed Identity (Azure)
  * Azure CLI authentication (local development)
* Eliminates secrets from `appsettings.json`

#### Azure App Service (Windows)

* Hosts the ASP.NET Core API
* Supports ZIP-based deployment
* Configuration managed via:

  * App Service environment variables
  * Key Vault references

---

### 3. Frontend Application

**React + Material UI**

**UI Capabilities**

* Tab-based layout:

  * Incidents List
  * Create Incident
  * Update Status
* Axios-based API client with environment-based API URL
* Supports:

  * Filtering by severity and status
  * File validation during incident creation
  * Direct attachment downloads from Blob Storage

---

## 🚀 Deployment Strategy

### Backend API Deployment

* Azure App Service (Windows, .NET 8 runtime)
* ZIP Deploy via Deployment Center
* Environment variables and Key Vault references configured in Azure Portal

### Frontend Deployment

* Azure App Service (Windows, Node runtime)
* React production build (`build/*`) zipped and deployed
* SPA routing enabled via rewrite rules:

  * `web.config` or `routes.json` (OS dependent)

---

## 🔐 Security Considerations

* No secrets stored in source control
* All sensitive values managed via Azure Key Vault
* Blob Storage access scoped to application needs
* Attachments accessed via secure URLs

---

## 🧠 Design Principles

* Keep infrastructure lightweight and cost-aware
* Favor clarity and separation over framework magic
* Treat the API as stateless
* Optimize for maintainability and cloud readiness

---

## 📌 Notes

This project is intended as a **reference implementation** showcasing backend architecture, Azure integrations, and a pragmatic full-stack setup rather than a production-hardened system.

---

Feel free to fork, explore, and adapt.
