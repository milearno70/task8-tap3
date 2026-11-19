
IF DB_ID('SchoolDB') IS NULL CREATE DATABASE SchoolDB;
GO
USE SchoolDB;
GO

IF OBJECT_ID('Grades')   IS NOT NULL DROP TABLE Grades;
IF OBJECT_ID('Courses')  IS NOT NULL DROP TABLE Courses;
IF OBJECT_ID('Students') IS NOT NULL DROP TABLE Students;
GO

CREATE TABLE Students (
    Id        INT IDENTITY(1,1) PRIMARY KEY,
    FirstName NVARCHAR(50)  NOT NULL,
    LastName  NVARCHAR(50)  NOT NULL,
    Age       INT           NOT NULL CHECK (Age > 0),
    Email     NVARCHAR(100) NOT NULL,
    CONSTRAINT UQ_Students_Email UNIQUE (Email)     
);

CREATE TABLE Courses (
    Id      INT IDENTITY(1,1) PRIMARY KEY,
    Title   NVARCHAR(100) NOT NULL,
    Credits INT           NOT NULL CHECK (Credits BETWEEN 1 AND 10)
);

CREATE TABLE Grades (
    Id        INT IDENTITY(1,1) PRIMARY KEY,
    StudentId INT NOT NULL,
    CourseId  INT NOT NULL,
    Score     INT NOT NULL CHECK (Score BETWEEN 0 AND 100),
    CONSTRAINT FK_Grades_Students FOREIGN KEY (StudentId) REFERENCES Students(Id),
    CONSTRAINT FK_Grades_Courses  FOREIGN KEY (CourseId)  REFERENCES Courses(Id)   
);
GO

INSERT INTO Students (FirstName, LastName, Age, Email) VALUES
 (N'Fatimə', N'Movsumova', 19, N'fatime@mail.com'),
 (N'Aydan',  N'Məmmədli',  20, N'aydan@mail.com'),
 (N'Xanım',  N'Məmmədova', 19, N'xanim@mail.com');  

INSERT INTO Courses (Title, Credits) VALUES
 (N'Proqramlaşdırma', 6),
 (N'Verilənlər bazası', 5),
 (N'Riyaziyyat', 4),
 (N'İngilis dili', 3);                              

INSERT INTO Grades (StudentId, CourseId, Score) VALUES
 (1, 1, 85), (1, 2, 90), (1, 3, 70),
 (2, 1, 95), (2, 2, 88);
GO
