# TheBookProject

## Overview
TheBookProject is a project designed to manage and interact with book data. It leverages various APIs to fetch book details and allows CRUD operations on book records.

## Features
- Fetch book details from Google Books API and GoodReads API.
- Add, update, and delete book records.
- List all books and view details of individual books.

## Technologies Used
- **C#**
- **ASP.NET Core**
- **Entity Framework Core**
- **SQLite**

## Getting Started

### Prerequisites
- .NET SDK 6.0 or later
- SQLite

### Installation
1. Clone the repository:
    ```bash
    git clone https://github.com/eveporras23/TheBookProject.git
    cd TheBookProject
    ```

2. Set up the database:
    ```bash
    dotnet ef database update
    ```

3. Build and run the project:
    ```bash
    dotnet build
    dotnet run
    ```

### Configuration
- Configure the connection string in `appsettings.json`:
    ```json
    {
      "ConnectionStrings": {
        "DefaultConnection": "Data Source=identifier.sqlite"
      }
    }
    ```

- Add API keys for GoodReads API in `appsettings.json`:
    ```json
    {
      "APIKeys": {
        "GoodReads": "your_goodreads_api_key"
      },
      "APIHost": {
        "GoodReads": "your_goodreads_api_host"
      }
    }
    ```

## Usage
- **GET** `/api/books`: Retrieve a list of all books.
- **GET** `/api/books/{isbn}`: Retrieve details of a book by ISBN.
- **POST** `/api/books`: Add a new book.
- **PUT** `/api/books/{isbn}`: Update an existing book.
- **DELETE** `/api/books/{isbn}`: Delete a book.
 
 
## Acknowledgements
- Google Books API
- GoodReads API
