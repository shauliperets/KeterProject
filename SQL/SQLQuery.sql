
USE KeterDB

--DROP DATABASE KeterDB
CREATE DATABASE KeterDB  COLLATE Hebrew_CI_AS;  

---------------------------------- Tables -----------------------------------------

--DROP TABLE [dbo].[Products]
CREATE TABLE [dbo].[Products]
(
    [Id] INT IDENTITY(1,1),
    [Name] VARCHAR (100) NULL,
    [Price] REAL,
    [DepartmentID] SMALLINT FOREIGN KEY REFERENCES Departments(ID),
    [SubDepartmentID] SMALLINT FOREIGN KEY REFERENCES Departments(ID),
    [Image] VARCHAR (MAX) NULL,
    CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED ([Id] ASC)
);

insert into Products (Name, Price, DepartmentID, SubDepartmentID, Image) values ('שולחן גינה', 199, 1, 1, 'table.jpg') 
insert into Products (Name, Price, DepartmentID, SubDepartmentID, Image) values ('כיסא מתקפל', 89, 1, 1, 'char.jpg') 


SELECT * FROM [dbo].[Products]

--DROP TABLE Departments
CREATE TABLE Departments
(
    ID SMALLINT PRIMARY KEY IDENTITY(1,1),
    Name VARCHAR(100)
)
INSERT INTO Departments (Name) VALUES ('לגינה')

--DROP TABLE SubDepartments
CREATE TABLE SubDepartments
(
    ID SMALLINT PRIMARY KEY IDENTITY(1,1),
    Name VARCHAR(100)
)
INSERT INTO Departments (Name) VALUES ('שולחנות')
INSERT INTO Departments (Name) VALUES ('ארונות')

----------------- Procedures -----------------------------------
ALTER PROCEDURE GetProducts
AS
    SELECT [P].[Id], [P].[Name], [D].[Name] AS [Department], [SD].[Name] AS [SubDepartment], [P].[Price], [P].[Image]
    FROM [Products] AS [P]
    INNER JOIN [Departments] AS [D] ON [P].[DepartmentID] = [D].[ID]
    --INNER JOIN [SubDepartments] AS [SD] ON [P].[SubDepartmentID] = [SD].[ID]


ALTER PROCEDURE GetProduct
(
    @Id INT
)
AS
    SELECT * FROM Products
    WHERE Id = @Id


CREATE PROCEDURE AddProduct
(
    @Name VARCHAR(100),
    @Price FLOAT,
    @DepartmentID SMALLINT,
    @SubDepartmentID SMALLINT,
    @ImageName VARCHAR(200)

)
AS
    INSERT INTO Products (Name, Price, DepartmentID, SubDepartmentID, Image)
    VALUES (@Name, @Price, @DepartmentID, @SubDepartmentID, @ImageName) 



EXECUTE GetProducts

EXECUTE GetProduct @Id = 1

EXECUTE AddProduct @Name = 'ארון', @Price = 268, @DepartmentID = 1, @SubDepartmentID = 2, @ImageName = 'Closet.jpg'


