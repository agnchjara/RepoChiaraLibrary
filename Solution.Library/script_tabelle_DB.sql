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
UserRole NVARCHAR(12) NOT NULL CONSTRAINT userOrAdmin CHECK (UserRole='StandardUser' OR UserRole='Admin'),
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

INSERT INTO Users (UserRole, Username, Password) 
Values ('Admin', 'administrator', 'admin1')
INSERT INTO Users (UserRole, Username, Password) 
Values ('StandardUser', 'bob', 'userBob')
 INSERT INTO Users (UserRole, Username, Password) 
Values ('StandardUser', 'alice', 'userAlice')

INSERT INTO Books (Title, AuthorName, AuthorSurname, Publisher, Quantity, IsDeleted)
values ('Harry Potter and the Philosophers Stone', 'J. K', 'Rowling', 'Bloomsbury', 1, 'false')

SELECT * FROM Books;
UPDATE Books
SET Title = concat ('Harry Potter and the Philosoper', char(39), 's Stone')
where BookId = 1

INSERT INTO Books (Title, AuthorName, AuthorSurname, Publisher, Quantity, IsDeleted)
values ('Harry Potter and the Chamber of Secrets', 'J. K', 'Rowling', 'Bloomsbury', 2, 'false')

INSERT INTO Books (Title, AuthorName, AuthorSurname, Publisher, Quantity, IsDeleted)
values ('Harry Potter and the Prisoner of Azkaban', 'J. K', 'Rowling', 'Bloomsbury', 3, 'false')

INSERT INTO Books (Title, AuthorName, AuthorSurname, Publisher, Quantity, IsDeleted)
values ('Harry Potter and the Order of the Phoenix', 'J. K', 'Rowling', 'Bloomsbury', 5, 'false')

INSERT INTO Books (Title, AuthorName, AuthorSurname, Publisher, Quantity, IsDeleted)
values ('Le Rouge et le Noir', 'Henry Beyle', 'Stendhal', 'A. Levasseur', 4, 'false')

INSERT INTO Books (Title, AuthorName, AuthorSurname, Publisher, Quantity, IsDeleted)
values ('Call me by your name', 'André', 'Aciman', 'Farrar, Straus and Giroux', 9, 'false')

INSERT INTO Books (Title, AuthorName, AuthorSurname, Publisher, Quantity, IsDeleted)
values ('a', 'b', 'c', 'd', 1, 'false')


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

-- stored procedure DeleteBook
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
--UPDATE Books
--SET Quantity = (SELECT Quantity) -1
--WHERE BookId = @BookId
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
--UPDATE Books
--SET Quantity = (SELECT Quantity) + 1
--WHERE BookId = @BookId;

-- stored procedure UpdateBook
CREATE PROCEDURE up_UpdateBook
@BookId INT,
@Title NVARCHAR(350),
@AuthorName NVARCHAR(200),
@AuthorSurname NVARCHAR(200) ,
@Publisher NVARCHAR(200),
@Quantity INT,
@IsDeleted BIT
AS
UPDATE Books
SET Title = @Title, AuthorName = @AuthorName, AuthorSurname = @AuthorName, Publisher = @Publisher, Quantity = @Quantity, IsDeleted = @IsDeleted
WHERE BookId = @BookId


SELECT * FROM Books;
DELETE FROM Books WHERE BookId = 8;

DROP TABLE Reservations;
DROP TABLE Books;
DROP TABLE Users;

BEGIN TRANSACTION                                                     
DELETE FROM Books
WHERE BookId = 6;
ROLLBACK 


