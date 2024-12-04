# TheBookProject

## Overview

TheBookProject is a project designed to manage and interact with book data. It leverages various APIs to fetch book details and allows CRUD operations on book records.

## Features

- Fetch book details from Google Books API and GoodReads API.
- Add, update, and delete book records.
- List all books and view details of individual books.
- Manage book reviews.

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
  
  **NOTE** inside TheBookProject.Db project folder there is an example of the database `TheBookProject.sqlite`

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
  **NOTE** To get a Good reads API host and Key, you can go to website `https://rapidapi.com/UnitedAPI/api/goodreads12/playground`, login is needed.

## Usage

If prefer, the repository has a **POSTMAN collection** `TheBookProject.postman_collection.json`

**BOOKS**

[AllowAnonymous]

- **GET** `/api/books`: Retrieve a list of all books.
   AllowAnonymous 
- **GET** `/api/books/{isbn}`: Retrieve details of a book by ISBN.
   Authorize 
- **POST** `/api/books`: Add a new book.
   Authorize 
- **PUT** `/api/books/{isbn}`: Update an existing book.
  Authorize 
- **DELETE** `/api/books/{isbn}`: Delete a book.

  **REVIEWS**

 AllowAnonymous 
- **GET** `/api/review`: Retrieve a list of all reviews for a book by ISBN.
  AllowAnonymous 
- **GET** `/api/review/{id}`: Retrieve details of a review by ID.
  Authorize 
- **POST** `/api/review`: Add a new review.
  Authorize 
- **PUT** `/api/review/{id}`: Update an existing review.
   Authorize 
- **DELETE** `/api/review/{id}`: Delete a review.

  **USING GOOGLE BOOKS REVERSE PROXY**

 AllowAnonymous 
- **GET** `/api/GoogleBooks/{isbn}`: Retrieve information about a book by ISBN from Google books API.
   Authorize 
- **POST** `/api/GoogleBooks/{isbn}`: Retrieve information about a book by ISBN and add the new book, if doesn't exist in the database.
   Authorize 
- **PUT** `/api/GoogleBooks/{isbn}`: Retrieve information about a book by ISBN and update the book, if exists in the database.

  **USING GOOD READS REVERSE PROXY**

To get the book Id of the Good reads books, go to website `https://www.goodreads.com/`, select a book from thousend of options for example `https://www.goodreads.com/book/show/7094569-feed`- The book id is **7094569**. Another example is `https://www.goodreads.com/book/show/199531723-the-author-s-guide-to-murder` the book Id is **199531723**

[AllowAnonymous]

- **GET** `/api/GoodReads/{bookId}`: Retrieve information about a book by book Id from Google good reads API.
  [Authorize]
- **POST** `/api/GoodReads/{bookId}`: Retrieve information about a book by book Id and add the new book if the retrieve data has an ISBN of 13 digits and if the book data has reviews add them in the database too.
  [Authorize]
- **PUT** `/api/GoodReads/{bookId}`: Retrieve information about a book by book Id and update the book data info, if exists in the database. No reviews are added only in POST

**Note:** The options with [Authorize] needs a jwts token to get one, open the terminal in the TheBookProject/TheBookProject project and execute the command:

  ```dotnet user-jwts create --claim "LastName=Porras"  ```

Copy and paste the token in a notepad to remove any space, copy and use it on swagger directly or in POSTMAN.

## Acknowledgements

- Google Books API
- GoodReads API
