CREATE DATABASE CXMDIRECT
GO


USE CXMDIRECT
GO

CREATE TABLE Nodes(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	ParentId INT,
	NodeId HIERARCHYID,
	NodeLvl AS NodeId.GetLevel(),
	Name NVARCHAR(MAX) NOT NULL
)
GO

CREATE TABLE ExceptionLogs(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	ExtensionId BIGINT,
	ExtensionType NVARCHAR(MAX),
	InstanceData DATETIME,
	Parameters NVARCHAR(MAX),
	Message NVARCHAR(MAX),
	StackTrace NVARCHAR(MAX)
)
GO

CREATE INDEX Nodes_BreadthFirst ON Nodes(Id, NodeId, NodeLvl)
GO

CREATE PROCEDURE DeleteNode (@id INT, @error NVARCHAR(MAX) = 'OK' OUT)
AS
	DECLARE @NodeId HIERARCHYID = (SELECT NodeId FROM Nodes WHERE Id = @id)


	IF (((SELECT @NodeId.GetDescendant(null, null)) IS NOT NULL) AND (SELECT Id FROM Nodes WHERE NodeId = @NodeId.GetDescendant(null, null)) IS NOT NULL )
	BEGIN
		SET @error =  'First need to delete all child nodes'
	END
	ELSE
	BEGIN
		DELETE FROM Nodes WHERE Id = @id
	END
GO	


CREATE PROCEDURE AddNode(@parentId INT, @name NVARCHAR(MAX), @error NVARCHAR(MAX) OUT)
AS
	DECLARE @parentHierarchyId HIERARCHYID

	IF EXISTS (SELECT ParentId FROM Nodes WHERE Id = @parentId) OR @parentId = 0
	BEGIN
		IF(@parentId = 0)
		BEGIN
			SET @parentHierarchyId = '/'
		END
		ELSE
		BEGIN
			SET @parentHierarchyId = (SELECT NodeId FROM Nodes WHERE Id = @parentId)
		END

		DECLARE @lastChild HIERARCHYID;

		INS_ER:

			SELECT @lastChild = MAX(NodeId) 
				FROM Nodes 
				WHERE NodeId.GetAncestor(1) = @parentHierarchyId

			INSERT INTO Nodes (NodeId, Name, ParentId) 
				SELECT @parentHierarchyId.GetDescendant(@lastChild, null), @name, @parentId

			SET @error = @@IDENTITY

		IF @@ERROR <> 0 GOTO INS_ER
	END
	ELSE
	BEGIN
		SET @error = 'The specified parent does not exist'
	END
GO

