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

## privilege table
| Endpoint         | Access        |
| ---------------- | ------------- |
| GET Categories   | Public        |
| POST Category    | Admin         |
| GET Events       | Public        |
| POST Event       | Admin	   |
| DELETE Event     | Admin         |
| GET Resources    | Public        |
| POST Resource    | Authenticated |
| APPROVE Resource | Admin         |

- /                → Landing page (home).
- /login           → Login.
- /register        → Register.
- /resources       → Public resources list (approved only).
- /events          → Public events list.
- /resources/new   → Suggest resource (Authenticated).
- /events/new      → Create event (Admin).
- /admin           → Admin dashboard (Admin only).

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
| **AutoMapper(13.0.1) PKG** | Used in _PLL.API_, _PLL.MVC_, and _SLL_ to simplify object-to-object mapping between DTOs, domain models, and view models. |

---





The project has transitioned from a monolithic data approach to a **N-Tier Architecture** using **DTOs (Data Transfer Objects)**. This shift separates the Data Access Layer (DAL) from the Presentation Layer (PLL), ensuring that database entities are never exposed directly to the UI or API consumers.

## Implementation Details

### 1. Data Transfer Objects (DTOs)

Specific classes were implemented in the **SLL (Service Logic Layer)** to handle data shaping:

* **ResourceReadDTO**: Optimized for data retrieval. It flattens relational data (e.g., bringing in `CategoryName`) to reduce complexity in the Views.
* **ResourceCreateDTO**: Tailored for form submissions. It includes only the necessary fields for data entry and the `Id` property for update operations.
* **EventDTO**: Standardized object for event-related operations across both MVC and API.

### 2. AutoMapper(13.0.1) Integration

**AutoMapper** was implemented to automate the object-to-object mapping between Domain Entities and DTOs.

* **Centralized Profiles**: All mapping rules are defined in `MappingProfile.cs`.
* **Flattening**: Automatic mapping of nested navigation properties (e.g., `Category.Name` to `CategoryName`).
* **Conversion**: Enabled mapping between `ReadDTO` and `CreateDTO` to facilitate the transition between viewing and editing data.

### 3. Layer Separation & Dependency Injection

* **Service Layer (SLL)**: Now acts as the sole orchestrator. It maps entities to DTOs before returning them to the controllers.
* **Constructor Injection**: `IMapper` is injected into controllers alongside services, following the Dependency Injection (DI) pattern.
* **Controller Logic**: Methods now work exclusively with DTOs, ensuring the Presentation Layer remains agnostic of the database schema.

### 4. UI and View Improvements

* **Strongly Typed Views**: All Razor views (`.cshtml`) were updated to bind to DTOs.
* **Form Binding**: Select lists for Categories are now handled via `SelectList` objects, ensuring correct binding between `CategoryId` and the UI dropdown.
* **Validation**: Implementation of `ModelState` validation on DTO-specific fields, improving user feedback on form errors.

### 5. Data Integrity Controls

* **Foreign Key Management**: Logic was improved to ensure that during creation or updates, only Foreign Key IDs (`CategoryId`) are processed. This prevents the accidental creation of duplicate parent records in the database.
* **Admin Filtering**: Conditional logic was added to the Service/Controller layer to differentiate between public views (Approved only) and administrative views (All records).

---

