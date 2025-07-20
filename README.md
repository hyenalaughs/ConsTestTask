Задание:  
1.	Установить MS SQL Express Edition
2.	Cоздать БД . Например список книг в домашней библиотеке: Название, автор, год издания, … другие поля, оглавление в виде xml поля. Оглавление в виде xml файла.
3.	Создать хранимые процедуры для  insert, update, delete, select …
4.	Создать 2 типа проектов в MS Visual Studio: MVC, WEB-Forms
5.	Для каждого реализовать карточку, список, функции редактирования, создания, изменения, просмотра записей с помощью хранимых процедур.
6.	Также в карточке сделать форму для редактирования оглавления в виде HTML редактора (можно найти контрол в интернете), сохранять содержимое в xml поле.
7.	Реализовать примеры выборки данных из xml.

Как запустить:  
1. Создать БД BookingDb в SQLExpress.
2. Выполнить следующие команды в запросе:

CREATE TABLE Books(
ID UNIQUEIDENTIFIER PRIMARY KEY,
Title NVARCHAR(100),
Author NVARCHAR(100),
YearPublish INT,
Genre NVARCHAR(100),
Contents XML);

-- Процедуры insert, update, delete, select …
GO

-- Insert
CREATE PROCEDURE AddBook
@Id UNIQUEIDENTIFIER,
@Title NVARCHAR(100),
@Author NVARCHAR(100),
@YearPublish INT,
@Genre NVARCHAR(100),
@Contents XML
AS
BEGIN
	INSERT INTO Books (Id ,Title, Author, YearPublish, Genre, Contents)
	VALUES (@Id, @Title, @Author, @YearPublish, @Genre, @Contents)
END;

-- Update
GO

CREATE PROCEDURE UpdateBook 
@Id UNIQUEIDENTIFIER,
@Title NVARCHAR(100),
@Author NVARCHAR(100),
@YearPublish INT,
@Genre NVARCHAR(100),
@Contents XML
AS
BEGIN
	UPDATE Book
	SET Title = @Title, Author = @Author, YearPublish = @YearPublish, Genre = @Genre, Contents = @Contents 
	WHERE Id = @Id
END

-- Delete
GO

CREATE PROCEDURE DeleteBook
@Id UNIQUEIDENTIFIER
AS
BEGIN
	DELETE FROM Books 
	WHERE Id = @Id
END;

-- Get by Id
GO

CREATE PROCEDURE GetBookById
@Id UNIQUEIDENTIFIER
AS 
BEGIN
	SELECT * FROM Books
	WHERE Id = @Id
END;

-- Get All books
GO

CREATE PROCEDURE GetBooks
AS 
BEGIN
	SELECT * FROM Books
END;
