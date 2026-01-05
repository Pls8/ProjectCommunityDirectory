### Community Directory Management System

#### Project Overview

This project is a multi-layered ASP.NET Core MVC application designed to manage and display community-based data. It features three primary modules: Categories, Events, and Community Resources, integrated with a role-based approval system.

---

#### System Architecture

The project follows a Clean Architecture pattern to ensure scalability and maintainability:

1. **DAL (Data Access Layer):** * Contains the ApplicationDbContext and Entity Models.
* Defines the database schema for Categories, Events, and Resources.
* Implements the Repository Pattern for data persistence.


2. **BLL (Business Logic Layer):**
* Implements repositories to handle raw database queries.
* Manages specific logic such as data filtering and include statements for related entities.


3. **SLL (Service Logic Layer):**
* Orchestrates communication between the Controller and Repository.
* Handles role-based visibility rules (Admin vs. Public views).


4. **PLL (Presentation Logic Layer):**
* The MVC Web interface built with Razor Views, Tag Helpers, and Bootstrap.



---

#### Workflow and Modules

**1. Categories Management**

* Provides the classification framework for the entire system.
* Categories act as a central lookup table, allowing Events and Resources to be categorized for better filtering and user navigation.

**2. Events Module**

* Supports full CRUD (Create, Read, Update, Delete) operations.
* **Details Layout:** Features a structured sidebar for critical information like Event Date, Location, and Organizer, alongside an image-handling system that supports fallback visuals if a path is missing.

**3. Community Resources & Approval System**

* **Submission Workflow:** Allows for the collection of resource data including Name, City, Contact Email, Phone, and Website.
* **Role-Based Visibility:** * **Admin Role:** Authorized users can view all resources (Pending and Approved). Admins have the authority to toggle the "Approved" status via a checkbox.
* **Public Role:** Anonymous or regular users can only see resources that have been verified and approved by an administrator.


* **User Tracking:** Every resource is linked to the `SubmittedById` of the user who created it, ensuring a clear audit trail.

---

#### Technical Features

| Feature | Description |
| --- | --- |
| **Relational Data** | Uses Entity Framework Core with Fluent API/Data Annotations to manage foreign key relationships between Resources, Events, and Categories. |
| **Validation** | Implements server-side and client-side validation using jQuery Validation Unobtrusive to ensure data integrity. |
| **User Identity** | Integrated with ASP.NET Core Identity to manage user roles and unique identifiers for resource ownership. |
| **Seed Data** | Includes a DbInitializer class to automatically populate the database with default categories, users, and resources upon first launch. |
| **UI Design** | Utilizes Bootstrap 5 for a responsive, mobile-friendly design, featuring card-based layouts and badge-status indicators. |

---
