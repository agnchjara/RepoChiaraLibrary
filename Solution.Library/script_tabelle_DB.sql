CREATE TABLE Books(
BookId INT IDENTITY(1, 1) PRIMARY KEY,
Title NVARCHAR(350) NOT NULL,
AuthorName NVARCHAR(200) NOT NULL,
AuthorSurname NVARCHAR(200) NOT NULL,
Publisher NVARCHAR(200) NOT NULL,
Quantity INT NOT NULL,
IsDeleted BIT NOT NULL,
)

CREATE TABLE Users(
UserId INT IDENTITY(1, 1) PRIMARY KEY NOT NULL,
UserRole NVARCHAR(5) NOT NULL CONSTRAINT userOrAdmin CHECK (UserRole='user' OR UserRole='admin'),
Username NVARCHAR(20) NOT NULL,
Password VARCHAR(20) NOT NULL,
)

CREATE TABLE Reservations (
ReservationId INT IDENTITY(1, 1) PRIMARY KEY NOT NULL,
BookId INT NOT NULL,
CONSTRAINT FK_bookId FOREIGN KEY(BookId) REFERENCES Books(BookId),
UserId INT NOT NULL,
CONSTRAINT FK_userId FOREIGN KEY(UserId) REFERENCES Users(UserId),
StartDate DATETIME NOT NULL,
EndDate DATETIME, 
)

DROP TABLE Reservations;
DROP TABLE Books;
DROP TABLE Users;

INSERT INTO Books (Title, AuthorName, AuthorSurname, Publisher, Quantity, IsDeleted)
values ('Harry Potter and the Philosophers Stone', 'J. K', 'Rowling', 'Bloomsbury', 1, 'false')

SELECT * FROM Books;
UPDATE Books
SET Title = concat ('Harry Potter and the Philosoper', char(39), 's Stone')
where BookId = 1

-- stored procedure CreateBook
CREATE PROCEDURE Books_up_CreateBook
@Title VARCHAR(150),
@AuthorName VARCHAR(100),
@AuthorSurname VARCHAR(100),
@Publisher VARCHAR(100),
@Quantity INT,
@IsDeleted VARCHAR(5)

AS
INSERT INTO Books(Title, AuthorName, AuthorSurname, Publisher, Quantity, IsDeleted)
VALUES(@Title, @AuthorName, @AuthorSurname, @Publisher, @Quantity, @IsDeleted)

-- stored procedure DetleteBook
CREATE PROCEDURE up_DeleteBook
@BookId INT 
AS
UPDATE Books
SET Quantity = 0, IsDeleted = 1
WHERE BookId = @BookId;

-- stored procedure ReadAllReservations
CREATE PROCEDURE up_ReadAllReservations
AS 
SELECT * FROM Reservations; 

-- stored procedure CreateReservation
CREATE PROCEDURE up_CreateReservation
@BookId INT,
@UserId INT,
@StartDate DATETIME,
@EndDate DATETIME

AS
INSERT INTO Reservations(BookId, UserId, StartDate, EndDate)
VALUES (@BookId, @UserId, @StartDate, @EndDate)
UPDATE Books
SET Quantity = (SELECT Quantity) -1
WHERE BookId = @BookId
GO

-- stored procedure DeleteReservation
CREATE PROCEDURE up_DeleteReservation
@ReservationId INT,
@BookId INT ,
@EndDate DATETIME

AS
UPDATE Reservations
SET EndDate = @EndDate
WHERE ReservationId = @ReservationId
UPDATE Books
SET Quantity = (SELECT Quantity) + 1
WHERE BookId = @BookId;

SELECT * FROM Books;
DELETE FROM Books WHERE BookId = 8;

DROP TABLE Reservations;
DROP TABLE Books;
DROP TABLE Users;

BEGIN TRANSACTION                                                      
DELETE FROM Books
WHERE BookId = 6;
ROLLBACK 


