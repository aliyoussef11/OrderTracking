## Full-Stack Mini Platform

A sample full-stack application built using .NET microservices architecture with a modern frontend. This project demonstrates:

* API Gateway with YARP (JWT authentication, rate limiting, Serilog logging)
* Backend microservices: Auth Service, Product Service, and Order Service (.Net Core Web API)
* Frontend application (.Net Core Web MVC)
* Shared layer for DTOs 
* Containerized deployment via Docker Compose

---

### Table of Contents

1. [Tech Stack](#tech-stack)
2. [Prerequisites](#prerequisites)
3. [Getting Started](#getting-started)

   * [Clone the Repository](#clone-the-repository)
   * [Configuration](#configuration)
   * [Running with Docker Compose](#running-with-docker-compose)
4. [Microservice Endpoints](#microservice-endpoints)

   * [Auth Service](#auth-service)
   * [API Gateway](#api-gateway)
   * [Product Service](#product-service)
   * [Order Service](#order-service)
5. [Frontend](#frontend)
6. [Health Checks & Logging](#health-checks--logging)
7. [Form Validation & Localization](#form-validation--localization)
8. [Folder Structure](#folder-structure)
9. [Contributing](#contributing)
10. [License](#license)

---

## Tech Stack

* **Backend**: .NET 8, .NET Core Web API
* **Gateway**: YARP (Yet Another Reverse Proxy)
* **Frontend**: .Net Core Web (MVC)
* **Authentication**: Auth Service issuing JWT tokens 
* **Logging**: Serilog
* **Data Storage**: PostgreSQL  
* **Containerization**: Docker & Docker Compose

---

## Prerequisites

* [.NET 8 SDK](https://dotnet.microsoft.com/download)
* [Docker](https://www.docker.com/)
* [Docker Compose](https://docs.docker.com/compose/)

---

## Getting Started

### Clone the Repository

```bash
git clone https://github.com/aliyoussef11/OrderTracking.git
```

### Configuration

### Running with Docker Compose

```bash
docker-compose up --build
```

This will start:

* **authservice** 
* **apigateway** 
* **productservice** 
* **orderservice** 
* **frontend** 
* **postgres** 

Access the frontend at `http://localhost:3000`. + Only gateway will be accessed

---

## Microservice Endpoints

### Auth Service

* **POST** `/api/auth/login` : Authenticate user and issue JWT token
* **POST** `/api/auth/register` : Register a new user 

### API Gateway

| Route             | Description                          |
| ----------------- | ------------------------------------ |
| `/api/products/*` | Proxies to Product Service endpoints |
| `/api/orders/*`   | Proxies to Order Service endpoints   |
| `/api/auth/*`     | Proxies to Auth Service endpoints    |

### Product Service

* **GET** `/api/products` : List all products
* **GET** `/api/products/{id}` : Get product by ID
* **POST** `/api/products` : Create new product
* **PUT** `/api/products/{id}` : Update product
* **DELETE** `/api/products/{id}` : Delete product

### Order Service

* **GET** `/api/orders` : List orders by current user
* **GET** `/api/orders/{id}` : Get order by ID
* **POST** `/api/orders` : Create new order
* **PUT** `/api/orders/{id}` : Update order
* **DELETE** `/api/orders/{id}` : Delete order

## Contributing

1. Fork the repo
2. Create feature branch
3. Commit changes
4. Open pull request

---

## License

This project is licensed under the MIT License. See `LICENSE` file for details.
