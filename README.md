# Bethany's Pie Shop

## Overview
Bethany's Pie Shop is an ASP.NET Core MVC web application designed to manage a fictional pie shop's inventory, orders, and customer information. This project was developed as part of the ASP.NET Core Fundamentals course by Plural Sight to learn and implement various technologies in the ASP.NET Core ecosystem.

## Features

### Technical Specifications

- Developed using Microsoft Visual Studio 2022.
- Frameworks and Technologies Used:
  - **ASP.NET Core MVC**
  - **ASP.NET Core Razor Pages**
  - **ASP.NET Core RESTful API**
  - **ASP.NET Core Blazor**
  - **Entity Framework Core**
  - **ASP.NET Core Identity**
  - **Unit Testing** with xUnit and Moq.
  - **JavaScript**, **Bootstrap**, **jQuery**
- Utilizes Entity Framework Core for database management.
- Implements a layered architecture with separation of concerns.
- Provides a user-friendly interface with responsive design.

### User Roles and Operations

1. **Customer**
   - Register and log in to the application using ASP.NET Core Identity.
   - Browse available pies.
   - Place orders.

### Business Rules

- Pies must have a name, description, price, and category.
- Orders must include customer information and details of the pies ordered.
- Customers must have a unique email address, name, and contact information.

### Functionalities

- **User Registration and Login:** Customers can register and log in to their accounts using ASP.NET Core Identity.
- **Pie Browsing:** Customers can browse the list of available pies.
- **Order Placement:** Customers can place orders for pies.

## Setup and Installation

1. Clone the repository.
2. Open the project in Microsoft Visual Studio 2022.
3. Restore NuGet packages.
4. Update the database connection string in the `appsettings.json` file.
5. Create the database using Entity Framework Core migrations:
   - Open the Package Manager Console in Visual Studio.
   - Run the command `Update-Database` to create the database schema and populate initial data.

## Database Setup

1. Ensure that you have SQL Server installed and running.
2. Update the `appsettings.json` file with your SQL Server connection string.
3. Open the Package Manager Console in Visual Studio.
4. Run the command `Update-Database` to create the database and tables, and to insert initial data.

## Usage

1. Launch the application.
2. Register as a new customer or log in with an existing account.
3. Browse the available pies and place orders.

## What I Learned

- **ASP.NET Core MVC:** Gained practical experience in building a web application using ASP.NET Core MVC.
- **Entity Framework Core:** Learned how to use Entity Framework Core for database management.
- **ASP.NET Core Identity:** Implemented user authentication and authorization using ASP.NET Core Identity.
- **JavaScript, Bootstrap, and jQuery:** Enhanced the user interface with JavaScript, Bootstrap, and jQuery for a responsive and interactive experience.
- **Layered Architecture:** Understood the importance of a layered architecture in maintaining separation of concerns.
- **Unit Testing:** Practiced writing unit tests with xUnit and Moq to ensure the reliability of the application.

## Conclusion

This project was a valuable learning experience in using various ASP.NET Core technologies for web development. It provided practical skills in building a full-fledged web application with user authentication, database management, and a responsive user interface. These skills are essential for future projects as a .NET developer.
