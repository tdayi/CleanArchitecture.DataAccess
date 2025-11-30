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
  "skip": 0,
  "take": 10,
  "orderByType": 1,
  "orderColumn": "Id",
  "parameters": [
    {
      "queryOperator": 6,
      "propertyName": "Name",
      "value": "e"
    },
    {
      "queryOperator": 2,
      "propertyName": "CreatedAt",
      "value": "2025-11-30"
    }
  ]
}
```

---

## Example Response

```json
{
  "totalCount": 5,
  "result": [
    {
      "id": "0cf5972e-0cfa-4519-8c58-45b13be7e7ba",
      "name": "Ayşe",
      "age": 21,
      "status": 1,
      "createdAt": "2025-11-30T17:54:53.789286",
      "isActive": false
    },
    {
      "id": "46b86195-ae74-4884-857a-fc1fdd603706",
      "name": "Veli",
      "age": 34,
      "status": 1,
      "createdAt": "2025-11-30T17:54:53.789286",
      "isActive": false
    },
    {
      "id": "5c5cbae5-ef27-4eb9-9357-9a1c838bc5b4",
      "name": "Selin",
      "age": 20,
      "status": 1,
      "createdAt": "2025-11-30T17:54:53.789287",
      "isActive": false
    },
    {
      "id": "6e63d725-6e78-4e25-85b4-dde465f5b6c9",
      "name": "Ahmet",
      "age": 25,
      "status": 1,
      "createdAt": "2025-11-30T17:54:53.78484",
      "isActive": false
    },
    {
      "id": "b2b40fbb-1d23-42d7-9d47-58c580616c49",
      "name": "Mehmet",
      "age": 20,
      "status": 1,
      "createdAt": "2025-11-30T17:54:53.789285",
      "isActive": false
    }
  ],
  "hasError": false,
  "messages": null
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
