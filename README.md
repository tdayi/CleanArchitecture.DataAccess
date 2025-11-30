# Dynamic Query API with EF Core & MediatR

This project demonstrates a clean architecture setup using **EF Core**, **SQLite**, **MediatR**, **AutoMapper**, and **Repository + UnitOfWork patterns**. It supports **dynamic filtering, sorting, and pagination** for queries, allowing flexible API requests.

---

## Project Purpose

The goal of this project is to provide a ready-to-use backend template that supports:

* Repository and UnitOfWork design pattern
* Dynamic IQueryable filtering
* Pagination and sorting
* AutoMapper integration
* MediatR for request/response handling
* SQLite database with seed data

It can be used as a base for CRUD APIs or more complex data access scenarios.

---

## Installation

1. Clone the repository:

   ```bash
   git clone https://github.com/tdayi/CleanArchitecture.DataAccess.git
   cd CleanArchitecture.DataAccess
   ```

2. Build and run the project:

   ```bash
   dotnet build
   dotnet run --project WebApi
   ```

   The API will start at [http://localhost:5218](http://localhost:5218)

---

## Example Request

**POST** `/api/User`

```json
{
  "skipCount": 0,
  "takeCount": 10,
  "orderByType": 1,
  "orderColumn": "Id",
  "queryParameters": [
    {
      "queryOperator": 6,
      "propertyName": "Name",
      "value": "h"
    }
  ]
}
```

---

## Example Response

```json
{
  "totalCount": 2,
  "result": [
    {
      "id": "96c945c8-bd07-4757-bad0-06434f9bb9b4",
      "name": "Mehmet",
      "age": 32,
      "status": 0,
      "createdAt": "2025-11-25T12:36:29.807515",
      "isActive": false
    },
    {
      "id": "9ed07a67-9a93-4087-895e-5c9107a3fcb7",
      "name": "Ahmet",
      "age": 25,
      "status": 1,
      "createdAt": "2025-11-20T12:36:29.807472",
      "isActive": true
    }
  ],
  "hataDurumu": false,
  "hataMesajlar": null
}
```

---

## Supported Query Operators

| Operator Name      | Value | Description                           |
| ------------------ | ----- | ------------------------------------- |
| Equals             | 0     | Matches exact value                   |
| NotEquals          | 1     | Excludes exact value                  |
| GreaterThan        | 2     | Greater than comparison               |
| LessThan           | 3     | Less than comparison                  |
| GreaterThanOrEqual | 4     | Greater than or equal                 |
| LessThanOrEqual    | 5     | Less than or equal                    |
| Contains           | 6     | Value contains the provided string    |
| StartsWith         | 7     | Value starts with the provided string |
| EndsWith           | 8     | Value ends with the provided string   |
