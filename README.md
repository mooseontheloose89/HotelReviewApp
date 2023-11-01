# HotelReviewApp

Hotel Review App is a RESTful API service designed to manage hotels and user reviews. It allows users to create, read, update, and delete hotel listings and reviews, as well as user accounts and their related operations.

## Features

- **Hotel Management:** Add new hotels, edit information, and remove listings.
- **Review System:** Users can post reviews for hotels and rate them.
- **User Profiles:** Manage user accounts including registration, updating profiles, and password management.
- **API Versioning:** Supports multiple API versions for future-proofing the application.
- **Swagger Documentation:** API endpoints are documented and testable through Swagger UI.

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes.

### Prerequisites

- [.NET 6 SDK](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/sql-server/sql-server-downloads)

### Installing

1. Clone the repository:

```bash
git clone https://github.com/mooseontheloose89/HotelReviewApp/.git
cd hotel-review-app
2. Restore the .NET packages:
dotnet restore
Update the DefaultConnection string in appsettings.json with your SQL Server details.

3.Run the application:
dotnet run

4.Usage
After running the project, you can access the API through:

Local: http://localhost:5000/swagger

Built With
.NET 6 - The framework used
Entity Framework Core - ORM used for database management
AutoMapper - Object-Object mapper
Swagger - API documentation and exploration tool
