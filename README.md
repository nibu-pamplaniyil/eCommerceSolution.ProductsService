# 🛍️ eCommerceSolution.ProductsService

**ProductsService** is a microservice within an e-commerce solution built using **C#**, **.NET Core**, and **Docker**.  
It manages product-catalog functionality — storing product data, providing APIs to retrieve product details.

---

## 🧱 Architecture & Structure 
- **ProductsMicroService.API** – ASP.NET Core Web API exposing endpoints for product operations (GET, POST, PUT, DELETE).  
- **BusinessLogicLayer** – contains core business rules and services for product handling.  
- **DataAccessLayer** – implements repository patterns, database context, and persistence logic.  
- Solution file: `eCommerceSolution.ProductsService.sln` connects all layers.

---

## 💡 Key Technologies  
- C# / .NET Core  
- Layered architecture (API → Business → Data)  
- Docker  
- Relational database 
- RESTful API design

---

## ✅ Features  
- Create, Read, Update, Delete (CRUD) operations for products  
- Clean separation of concerns (Data, Business, API layers)  
- Ready for integration in a larger e-commerce ecosystem  
- Easily extensible for features like filtering, search, and categories  
