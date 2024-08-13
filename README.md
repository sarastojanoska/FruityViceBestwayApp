# Fruit Vice Bestway App

## Overview

The Fruit Vice Bestway app is designed to manage and retrieve information about various fruits. It integrates with a repository and an external API to fetch fruit data and caches the results for improved performance. Additionally, it logs exceptions to a dedicated database.

## Features

- Retrieve fruit information by name
- Cache fruit data to reduce API calls and database queries
- Log exceptions to a separate database
- Map data between view models and entities using AutoMapper

## Prerequisites

- [.NET Core SDK](https://dotnet.microsoft.com/download) (version 6)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) or another supported database
- [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/)

## Setup

1. **Clone the repository**: 
git clone https://github.com/yourusername/fruit-vice-bestway.git
cd fruit-vice-bestway

2. **Configure the databases**:

    - Create two databases: one for the main application and one for logging exceptions.
    - Update the connection strings in `appsettings.json`:
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=your_server;Database=FruitViceDb;User Id=your_user;Password=your_password;",
    "ExceptionDatabase": "Server=your_server;Database=ExceptionDb;User Id=your_user;Password=your_password;"
  }
}

3. **Install dependencies**:
dotnet restore
    
4. **Apply migrations**:

    - For the main database: 
dotnet ef migrations add InitialCreate -c ApplicationDbContext
dotnet ef database update -c ApplicationDbContext

 - For the exception database:
dotnet ef migrations add InitialCreate -c ExceptionDbContext
dotnet ef database update -c ExceptionDbContext

## Usage

1. **Run the application**:
dotnet run


2. **Access the application**: Open your browser and navigate to `http://localhost:5000`.

## Code Overview

### `GetFruitByName` Method

This method retrieves fruit information by name, first checking the cache, then the database, and finally an external API if necessary.


### Exception Logging

Exceptions are logged to a separate database using the `ExceptionLoggerService`.


## Contributing

Contributions are welcome! Please open an issue or submit a pull request.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.


    
