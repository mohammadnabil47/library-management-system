-- Library Management System Database Scripts
-- Create Stored Procedures

USE LibraryManagementDB;
GO

-- Stored Procedure: GetAllBooksWithCategories
-- This procedure returns all books with their associated categories
-- Used by ADO.NET implementation as per requirements
CREATE PROCEDURE GetAllBooksWithCategories
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        b.Id AS BookId,
        b.Title AS BookTitle,
        b.Author AS BookAuthor,
        b.ISBN AS BookISBN,
        b.PublishedDate AS BookPublishedDate,
        b.Quantity AS BookQuantity,
        b.CreatedDate AS BookCreatedDate,
        b.UpdatedDate AS BookUpdatedDate,
        c.Id AS CategoryId,
        c.Name AS CategoryName,
        c.Description AS CategoryDescription,
        c.CreatedDate AS CategoryCreatedDate,
        c.UpdatedDate AS CategoryUpdatedDate
    FROM Books b
    LEFT JOIN BookCategories bc ON b.Id = bc.BookId
    LEFT JOIN Categories c ON bc.CategoryId = c.Id
    ORDER BY b.Title, c.Name;
END
GO

-- Stored Procedure: GetBooksByCategory
-- This procedure returns all books for a specific category
CREATE PROCEDURE GetBooksByCategory
    @CategoryId INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        b.Id AS BookId,
        b.Title AS BookTitle,
        b.Author AS BookAuthor,
        b.ISBN AS BookISBN,
        b.PublishedDate AS BookPublishedDate,
        b.Quantity AS BookQuantity,
        b.CreatedDate AS BookCreatedDate,
        b.UpdatedDate AS BookUpdatedDate,
        c.Id AS CategoryId,
        c.Name AS CategoryName,
        c.Description AS CategoryDescription,
        c.CreatedDate AS CategoryCreatedDate,
        c.UpdatedDate AS CategoryUpdatedDate
    FROM Books b
    INNER JOIN BookCategories bc ON b.Id = bc.BookId
    INNER JOIN Categories c ON bc.CategoryId = c.Id
    WHERE c.Id = @CategoryId
    ORDER BY b.Title;
END
GO

-- Stored Procedure: GetBookWithCategories
-- This procedure returns a specific book with its categories
CREATE PROCEDURE GetBookWithCategories
    @BookId INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        b.Id AS BookId,
        b.Title AS BookTitle,
        b.Author AS BookAuthor,
        b.ISBN AS BookISBN,
        b.PublishedDate AS BookPublishedDate,
        b.Quantity AS BookQuantity,
        b.CreatedDate AS BookCreatedDate,
        b.UpdatedDate AS BookUpdatedDate,
        c.Id AS CategoryId,
        c.Name AS CategoryName,
        c.Description AS CategoryDescription,
        c.CreatedDate AS CategoryCreatedDate,
        c.UpdatedDate AS CategoryUpdatedDate
    FROM Books b
    LEFT JOIN BookCategories bc ON b.Id = bc.BookId
    LEFT JOIN Categories c ON bc.CategoryId = c.Id
    WHERE b.Id = @BookId
    ORDER BY c.Name;
END
GO

-- Stored Procedure: SearchBooks
-- This procedure searches books by title, author, or ISBN
CREATE PROCEDURE SearchBooks
    @SearchTerm NVARCHAR(200)
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        b.Id AS BookId,
        b.Title AS BookTitle,
        b.Author AS BookAuthor,
        b.ISBN AS BookISBN,
        b.PublishedDate AS BookPublishedDate,
        b.Quantity AS BookQuantity,
        b.CreatedDate AS BookCreatedDate,
        b.UpdatedDate AS BookUpdatedDate,
        c.Id AS CategoryId,
        c.Name AS CategoryName,
        c.Description AS CategoryDescription,
        c.CreatedDate AS CategoryCreatedDate,
        c.UpdatedDate AS CategoryUpdatedDate
    FROM Books b
    LEFT JOIN BookCategories bc ON b.Id = bc.BookId
    LEFT JOIN Categories c ON bc.CategoryId = c.Id
    WHERE b.Title LIKE '%' + @SearchTerm + '%'
       OR b.Author LIKE '%' + @SearchTerm + '%'
       OR b.ISBN LIKE '%' + @SearchTerm + '%'
    ORDER BY b.Title, c.Name;
END
GO

-- Stored Procedure: GetBooksByAuthor
-- This procedure returns all books by a specific author
CREATE PROCEDURE GetBooksByAuthor
    @AuthorName NVARCHAR(200)
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        b.Id AS BookId,
        b.Title AS BookTitle,
        b.Author AS BookAuthor,
        b.ISBN AS BookISBN,
        b.PublishedDate AS BookPublishedDate,
        b.Quantity AS BookQuantity,
        b.CreatedDate AS BookCreatedDate,
        b.UpdatedDate AS BookUpdatedDate,
        c.Id AS CategoryId,
        c.Name AS CategoryName,
        c.Description AS CategoryDescription,
        c.CreatedDate AS CategoryCreatedDate,
        c.UpdatedDate AS CategoryUpdatedDate
    FROM Books b
    LEFT JOIN BookCategories bc ON b.Id = bc.BookId
    LEFT JOIN Categories c ON bc.CategoryId = c.Id
    WHERE b.Author LIKE '%' + @AuthorName + '%'
    ORDER BY b.Title, c.Name;
END
GO
