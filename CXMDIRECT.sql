CREATE DATABASE CXMDIRECT
GO

USE CXMDIRECT
GO

CREATE TABLE Nodes(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	ParentId INT,
	Name NVARCHAR(MAX) NOT NULL,
	Description NVARCHAR(MAX)
)
GO

CREATE TABLE ExceptionLogs(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	ExtensionType NVARCHAR(MAX),
	InstanceData DATETIME,
	Parameters NVARCHAR(MAX),
	Message NVARCHAR(MAX),
	StackTrace NVARCHAR(MAX)
)
GO

CREATE PROCEDURE DeleteNode (@id INT, @error NVARCHAR(MAX) = 'OK' OUT)
AS
	IF EXISTS (SELECT Id FROM Nodes WHERE ParentId = @id)
	BEGIN
		SET @error =  'First need to delete all child nodes'
	END
	ELSE
	BEGIN
		DELETE FROM Nodes WHERE Id = @id
	END
GO	


CREATE PROCEDURE AddNode(@parentId INT, @name NVARCHAR(MAX), @description NVARCHAR(MAX) = '', @error NVARCHAR(MAX) OUT)
AS
	IF EXISTS (SELECT ParentId FROM Nodes WHERE Id = @parentId) OR @parentId = 0
	BEGIN
		IF(@parentId < 0)
		BEGIN
			SET @error = 'Parrent Id must be grater or equel 0'
		END
		ELSE
		BEGIN
			INSERT INTO Nodes (ParentId, Name, Description) VALUES
			(@parentId, @name, @description)
			SET @error = @@IDENTITY
		END
	END
	ELSE
	BEGIN
		SET @error = 'The specified parent does not exist'
	END
GO

