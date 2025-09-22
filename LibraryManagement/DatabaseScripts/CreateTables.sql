-- Library Management System Database Scripts
-- Create Tables

USE LibraryManagementDB;
GO

-- Create Categories table
CREATE TABLE Categories (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Description NVARCHAR(500) NULL,
    CreatedDate DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    UpdatedDate DATETIME2 NULL
);
GO

-- Create Books table
CREATE TABLE Books (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Title NVARCHAR(200) NOT NULL,
    Author NVARCHAR(200) NOT NULL,
    ISBN NVARCHAR(50) NULL,
    PublishedDate DATETIME2 NOT NULL,
    Quantity INT NOT NULL DEFAULT 0,
    CreatedDate DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    UpdatedDate DATETIME2 NULL
);
GO

-- Create BookCategories junction table (Many-to-Many relationship)
CREATE TABLE BookCategories (
    BookId INT NOT NULL,
    CategoryId INT NOT NULL,
    PRIMARY KEY (BookId, CategoryId),
    FOREIGN KEY (BookId) REFERENCES Books(Id) ON DELETE CASCADE,
    FOREIGN KEY (CategoryId) REFERENCES Categories(Id) ON DELETE CASCADE
);
GO

-- Create indexes for better performance
CREATE INDEX IX_Books_Title ON Books(Title);
CREATE INDEX IX_Books_Author ON Books(Author);
CREATE INDEX IX_Books_ISBN ON Books(ISBN);
CREATE INDEX IX_Categories_Name ON Categories(Name);
CREATE INDEX IX_BookCategories_BookId ON BookCategories(BookId);
CREATE INDEX IX_BookCategories_CategoryId ON BookCategories(CategoryId);
GO

-- Insert sample data
INSERT INTO Categories (Name, Description) VALUES
('Fiction', 'Fictional literature including novels, short stories, and plays'),
('Non-Fiction', 'Factual and informative literature'),
('Science Fiction', 'Speculative fiction dealing with futuristic concepts'),
('Mystery', 'Fiction dealing with the solution of a crime'),
('Romance', 'Fiction focusing on romantic relationships'),
('Biography', 'Non-fiction accounts of people''s lives'),
('History', 'Non-fiction accounts of past events'),
('Technology', 'Books about technology and programming');
GO

INSERT INTO Books (Title, Author, ISBN, PublishedDate, Quantity) VALUES
('The Great Gatsby', 'F. Scott Fitzgerald', '978-0-7432-7356-5', '1925-04-10', 5),
('To Kill a Mockingbird', 'Harper Lee', '978-0-06-112008-4', '1960-07-11', 3),
('1984', 'George Orwell', '978-0-452-28423-4', '1949-06-08', 4),
('Pride and Prejudice', 'Jane Austen', '978-0-14-143951-8', '1813-01-28', 6),
('The Hobbit', 'J.R.R. Tolkien', '978-0-547-92822-7', '1937-09-21', 7),
('Clean Code', 'Robert C. Martin', '978-0-13-235088-4', '2008-08-01', 2),
('Design Patterns', 'Gang of Four', '978-0-201-63361-0', '1994-10-21', 3),
('The Pragmatic Programmer', 'David Thomas', '978-0-201-61622-4', '1999-10-20', 4);
GO

-- Insert sample book-category relationships
INSERT INTO BookCategories (BookId, CategoryId) VALUES
(1, 1), -- The Great Gatsby -> Fiction
(2, 1), -- To Kill a Mockingbird -> Fiction
(3, 3), -- 1984 -> Science Fiction
(3, 1), -- 1984 -> Fiction
(4, 1), -- Pride and Prejudice -> Fiction
(4, 5), -- Pride and Prejudice -> Romance
(5, 1), -- The Hobbit -> Fiction
(5, 3), -- The Hobbit -> Science Fiction
(6, 2), -- Clean Code -> Non-Fiction
(6, 8), -- Clean Code -> Technology
(7, 2), -- Design Patterns -> Non-Fiction
(7, 8), -- Design Patterns -> Technology
(8, 2), -- The Pragmatic Programmer -> Non-Fiction
(8, 8); -- The Pragmatic Programmer -> Technology
GO
