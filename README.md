# TodoList
## Description 
This project is a .NET Core and Angular application that demonstrates the integration of backend and frontend technologies.

### Prerequisites 
.NET Core SDK (version 7.0) 
Node.js 
Angular CLI 
Visual Studio 
Visual Studio Code (optional but recommended) 
SQL Server 
SSMS


# Tech Stack

- Angular version 17 (frontend)
- .net core 7.0 (backend)
- git (source control)
- mysql (database)

### Frontend

#### Directory Structure: 
a. Arranged files and folders logically. 
b. Common directories include "src" for source code, "service" for service files, “assets” for static files and "components" for UI components.

Separation of Concerns: Component-based architecture ensures a clear separation between UI components, services, and business logic.

Dependency Injection: Used in registering of services in components.

Form Validation: Leverage Angular Reactive Forms for robust form validation and handling.

### Backend

Project Structure: 
• Controllers: For API endpoints. 
• Services: Business logic and Data access. 
• Models: Define data models. 
• ViewModels: For separation of concerns by managing UI-related data and handling user interactions in a single, organized component.
• Middleware: Custom middleware for error handling

SQL Integration: Integrated SQL for data storage.

Error Handling and Validation: Implemented proper error handling and validation for API requests.

Middleware: Used middleware for exception handling

Dependency Injection: Registered services with Dependency Injection.

CORS (Cross-Origin Resource Sharing): Configure CORS settings in your backend to define which domains can access your API.

All globals like connection string, gmail credentials are set up in app-settings file, that are then set to variables in program.cs
