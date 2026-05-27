# Reverse Auction API (ASP.NET Core)

This project is a Reverse Auction system where Buyers create auctions and Vendors place bids. Lowest bid wins.

---

# Tech Stack

- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- JWT Authentication
- Layered Architecture
- Repository Pattern
- Serilog Logging (Day wise logs)

---

## Setup Instructions
Step 1: Update Connection String
<img width="1161" height="79" alt="image" src="https://github.com/user-attachments/assets/2c5dc80d-85cf-498e-b556-4e13e4ab7bd2" />

Step 2: Run Migration by selecting the infrastructure layer.
<img width="1131" height="309" alt="image" src="https://github.com/user-attachments/assets/47cdc29a-0f4e-42f2-b1de-3559d4c50df2" />

Step 3: Run Project
<img width="1874" height="956" alt="image" src="https://github.com/user-attachments/assets/1db1b661-ecb6-4db3-aade-f4399b699585" />

---
# Architecture Used

I used Layered Architecture:

API Layer (Controllers)
↓
Application Layer (Services)
↓
Domain Layer (Entities, Interfaces)
↓
Infrastructure Layer (DbContext, Repositories)


---

# Dependency Flow

- API depends on Application
- Application depends on Domain
- Infrastructure depends on Domain

Domain is independent (no dependency)

---

# Project Structure
EnterpriceApp.API
EnterpriceApp.Application
EnterpriceApp.Domain
EnterpriceApp.Infrastructure

---

# Flow Diagram

User → API Controller → Service Layer → Repository → Database


---

# Roles

### Buyer
- Creates Auction
- Closes Auction
- Views bids

### Vendor
- Places bid
- Updates bid
- Views auctions

---

# Main Features

## Auth
- Register user
- Login user
- JWT token generated

## Auction (Buyer)
- Create auction
- Get all auctions
- Get auction by id
- Close auction

## Bids (Vendor)
- Place bid
- Update bid
- View bids

## Dashboard
- Bid comparison API
- Lowest bid
- Highest bid
- Total bids
- Winner info

---

# Database Tables

## Users

| Column | Type |
|------|------|
| Id | int |
| Name | string |
| Email | string |
| PasswordHash | string |
| Role | Buyer/Vendor |
| CreatedAt | datetime |

---

## Auctions

| Column | Type |
|------|------|
| Id | int |
| Title | string |
| Description | string |
| BasePrice | decimal |
| StartTime | datetime |
| EndTime | datetime |
| Status | string |
| CreatedBy | int |
| WinnerVendorId | int |
| WinningBid | decimal |

---

## Bids

| Column | Type |
|------|------|
| Id | int |
| AuctionId | int |
| VendorId | int |
| BidAmount | decimal |
| BidTime | datetime |

---

# JWT Authentication

- Token generated on login
- Role added in token
- Role based access used

Example:

- Buyer → Auction APIs
- Vendor → Bid APIs

---

# Generic API Response Format

All APIs follow this response:

```csharp
{
    StatusCode = 403,
    Success = false,
    Message = "Forbidden",
    Data = null,
    Errors = new List<string>
    {
        "You are not authorized to access this resource"
    }
};
```

Error Handling
Global exception middleware used
All errors return same format
No raw exceptions exposed

Logging
Serilog used
Logs stored day wise

Example:

Logs/log-2025-07-19.txt
Logs/log-2025-07-20.txt

Logs include:

API calls
Errors
Business actions


