--GO
--USE master

--GO
--CREATE DATABASE LibraryShop

--GO
--USE LibraryShop

--GO
--CREATE TABLE Authors 
--(
--	[Id] INT PRIMARY KEY IDENTITY,
--	[FirstName] NVARCHAR(100) NOT NULL,
--	[LastName] NVARCHAR(100) NOT NULL
--)

--GO
--CREATE TABLE Genres 
--(
--	[Id] INT PRIMARY KEY IDENTITY,
--	[GenreType] NVARCHAR(100) NOT NULL,
--)

--GO
--Alter Table Genres ADD CONSTRAINT UK_Unique_Genre UNIQUE (GenreType)

--GO
--CREATE TABLE Publishers
--(
--	[Id] INT PRIMARY KEY IDENTITY,
--	[Name] NVARCHAR(100) NOT NULL,
--)

--GO
--Alter Table Publishers ADD CONSTRAINT UK_Unique_Name UNIQUE (Name)

--GO
--CREATE TABLE Books
--(
--	[Id] INT PRIMARY KEY IDENTITY,
--	[Name] NVARCHAR(100) UNIQUE NOT NULL,
--	[PublisherId] INT FOREIGN KEY REFERENCES Publishers(Id) NOT NULL,
--	[AmountOfPages] INT NOT NULL, 
--	[Year] DATE NOT NULL,
--	[PrimeCost] DECIMAL NOT NULL, 
--	[DiscountPercent] INT NOT NULL, 
--	[Price] DECIMAL,
--	[AmountOfBook] INT NOT NULL, 
--	[IsSequel] BIT NOT NULL,

--	CONSTRAINT CK_CHECK_AMOUNT_OF_PAGES CHECK ([AmountOfPages] > 0),
--	CONSTRAINT CK_CHECK_PRIME_COST CHECK ([PrimeCost] > 0),
--	CONSTRAINT CK_CHECK_DISCOUNT_PERCENT_COST CHECK ([DiscountPercent] >= 0 AND [DiscountPercent] <=100),
--	CONSTRAINT CK_CHECK_AMOUNT_OF_BOOK CHECK ([AmountOfBook] >= 0),
--)



																	
																	-- Triggers
--GO
--CREATE TRIGGER BooksUpdateTrigger
--ON Books
--FOR UPDATE
--AS
--	BEGIN
--		DECLARE @_new_Price DECIMAL
--		IF UPDATE(DiscountPercent)
--				BEGIN
--				     DECLARE @_old_Prime_Cost DECIMAL
--				     DECLARE @_new_Discount_Percent DECIMAL
					 

--					 SET @_old_Prime_Cost = (SELECT deleted.PrimeCost FROM deleted)
--					 SET @_new_Discount_Percent = (SELECT inserted.DiscountPercent FROM inserted)

--					 SET @_new_Price = (@_old_Prime_Cost - (@_old_Prime_Cost * @_new_Discount_Percent/100) )

--					 UPDATE Books SET Price = @_new_Price WHERE Id = (SELECT inserted.Id From inserted)

--				END
--		IF UPDATE (PrimeCost)	
--				BEGIN
--					 DECLARE @_old_Discount_Percent DECIMAL
--				     DECLARE @_new_Prime_Cost DECIMAL
					 
					

--					 SET @_old_Discount_Percent = (SELECT deleted.DiscountPercent FROM deleted)
--					 SET @_new_Prime_Cost = (SELECT inserted.PrimeCost FROM inserted)

--					  SET @_new_Price = (@_new_Prime_Cost - (@_new_Prime_Cost * @_old_Discount_Percent/100) )
--					  UPDATE Books SET Price = @_new_Price WHERE Id = (SELECT inserted.Id From inserted)
--				END

--	END


--GO
--CREATE TRIGGER BookInsertionTrigger
--ON Books
--AFTER INSERT 
--AS	
--	BEGIN
--		DECLARE @_amountPercent DECIMAL
--		DECLARE @_price DECIMAL
--		SET @_amountPercent = (SELECT inserted.DiscountPercent FROM inserted)
--		SET @_price =( (SELECT inserted.PrimeCost FROM inserted) - (SELECT inserted.PrimeCost FROM inserted) * (@_amountPercent/100))

--		UPDATE Books
--		SET Price = @_price WHERE Id = (SELECT inserted.Id From inserted)
--	END


--GO
--CREATE TABLE AuthorBooks
--(
--	[Id] INT PRIMARY KEY IDENTITY,
--	[BookdId] INT FOREIGN KEY REFERENCES Books(Id),
--	[AuthorId] INT FOREIGN KEY REFERENCES Authors(Id),
--)


																			--Cascade DELETE UPDATE Addition 
--GO
--Alter Table Books
--DROP Constraint FK__Books__Publisher__5DCAEF64

--Alter Table Books
--Add Constraint FK_Books_Publishers FOREIGN KEY (PublisherId)  REFERENCES Publishers(Id) ON DELETE CASCADE ON UPDATE CASCADE

--ALTER Table AuthorBooks
--ADD Constraint FK_AuthorBooks_BookId Foreign KEY (BookdId) REFERENCES Books(Id) ON DELETE CASCADE ON UPDATE CASCADE

--ALTER Table AuthorBooks
--ADD Constraint FK_AuthorBooks_AuthorsId Foreign KEY (AuthorId) REFERENCES Authors(Id) ON DELETE CASCADE ON UPDATE CASCADE

--GO
--CREATE TABLE GenresBooks
--(
--	[Id] INT PRIMARY KEY IDENTITY,
--	[BookdId] INT FOREIGN KEY REFERENCES Books(Id),
--	[GenreId] INT FOREIGN KEY REFERENCES Genres(Id),
--)

--GO
--ALTER Table GenresBooks
--ADD Constraint FK_GenresBooks_GenreId Foreign KEY (GenreId) REFERENCES Genres(Id) ON DELETE CASCADE ON UPDATE CASCADE

--GO
--SELECT Genres.GenreType, Books.Name, ([Authors].FirstName + [Authors].LastName) as [Author] FROM Books
--JOIN AuthorBooks ON AuthorBooks.BookdId = Books.Id
--JOIN GenresBooks ON GenresBooks.BookdId = Books.Id
--JOIN Authors ON AuthorBooks.AuthorId = Authors.Id
--JOIN Genres ON GenresBooks.GenreId= Genres.Id

--GO
--CREATE TABLE Users 
--(
--	[Id] INT PRIMARY KEY IDENTITY,
--	[FirstName] NVARCHAR(100) NOT NULL,
--	[LastName] NVARCHAR(100) NOT NULL,
--	[Login] NVARCHAR(100) NOT NULL UNIQUE,
--	[Password] NVARCHAR(100) NOT NULL,
--	[IsAdmin] BIT NOT NULL DEFAULT 0
--)

--GO
--INSERT INTO [Users] VALUES(N'Admin',N'Admin', N'dbAdmin',N'admin',1)


--GO
--CREATE TABLE Orders 
--(
--	[Id] INT PRIMARY KEY IDENTITY,
--	[BookId] INT NOT NULL FOREIGN KEY REFERENCES Books(Id),
--	[UserId] INT NOT NULL FOREIGN KEY REFERENCES Users(Id),
--	[OrderDate] DATE NOT NULL
--) 


--GO
--CREATE TABLE Stocks
--(
--	[Id] INT PRIMARY KEY IDENTITY,
--	[BookId] INT NOT NULL FOREIGN KEY REFERENCES Books(Id) ON DELETE CASCADE ON UPDATE CASCADE,
--)


--GO
--SELECT Books.[Id], Books.[Name], Books.[PublisherId],Books.[AmountOfPages], Books.[Year],
--Books.[PrimeCost], Books.[DiscountPercent], Books.[Price], Books.[AmountOfBook], Books.[IsSequel],
--(Authors.[FirstName] + ' ' + Authors.[LastName])  AS [Author],
--Genres.GenreType
--FROM Books
--JOIN AuthorBooks ON Books.Id = AuthorBooks.Id
--JOIN GenresBooks ON Books.Id = GenresBooks.BookdId
--JOIN Authors ON Authors.Id = AuthorBooks.AuthorId
--JOIN Genres ON Genres.Id = GenresBooks.GenreId

--GO
--CREATE TRIGGER OrderAddTrigger
--ON Orders
--AFTER INSERT
--AS
--	BEGIN
--		DECLARE @_orderedBookId INT
--		SET @_orderedBookId = (SELECT inserted.BookId FROM inserted)
--		UPDATE Books SET AmountOfBook = AmountOfBook - 1 WHERE Id = @_orderedBookId
--	END

--GO
--CREATE TRIGGER OrderDeleteTrigger
--ON Orders
--AFTER DELETE
--AS
--	BEGIN
--		DECLARE @_orderedBookId INT
--		SET @_orderedBookId = (SELECT deleted.BookId FROM deleted)
--		UPDATE Books SET AmountOfBook = AmountOfBook + 1 WHERE Id = @_orderedBookId
--	END

