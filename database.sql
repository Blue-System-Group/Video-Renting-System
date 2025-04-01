CREATE DATABASE VideoRentingDB;
USE VideoRentingDB;

-- Customers Table
CREATE TABLE Customers (
    CustomerID INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Contact NVARCHAR(100) NOT NULL
);

-- Videos Table
CREATE TABLE Videos (
    VideoID INT PRIMARY KEY IDENTITY(1,1),
    Title NVARCHAR(100) NOT NULL,
    Genre NVARCHAR(50),
    ReleaseDate DATE,
    Availability BIT NOT NULL
);

-- Rentals Table
CREATE TABLE Rentals (
    RentalID INT PRIMARY KEY IDENTITY(1,1),
    CustomerID INT FOREIGN KEY REFERENCES Customers(CustomerID),
    VideoID INT FOREIGN KEY REFERENCES Videos(VideoID),
    RentalDate DATE,
    ReturnDate DATE,
    Status NVARCHAR(50)
);

-- User Table
CREATE TABLE Users (
    UserID INT PRIMARY KEY IDENTITY(1,1),
    Username NVARCHAR(100) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(255) NOT NULL,
    Role NVARCHAR(20) NOT NULL CHECK (Role IN ('Admin', 'Customer')),
    ReferenceID INT NOT NULL  -- Links to CustomerID or AdminID
);

SELECT * FROM Customers;
SELECT * FROM Videos;
SELECT * FROM Rentals;
SELECT * FROM Users;

INSERT INTO Users (Username, PasswordHash, Role,ReferenceID)
VALUES ('admin', 'Admin@123', 'Admin',0);

DELETE Users WHERE UserID = 2;