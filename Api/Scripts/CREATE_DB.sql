CREATE DATABASE CrudDapper

USE CrudDapper
       
CREATE TABLE Users (
      ID INT IDENTITY(1, 1),
      Name NVARCHAR(100),
      Email NVARCHAR(100),
      Password NVARCHAR(100),
      Role NVARCHAR(100),
      Salary decimal,
      CPF VARCHAR(11),
      Active bit,
);