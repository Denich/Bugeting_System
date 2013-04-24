USE [MyCompany_Database];
GO

IF OBJECT_ID('[dbo].[usp_CompanyPositionSelect]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[usp_CompanyPositionSelect] 
END 
GO
CREATE PROC [dbo].[usp_CompanyPositionSelect] 
    @Id INT
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [Id], [Name], [CanApproveBudget] 
	FROM   [dbo].[CompanyPosition] 
	WHERE  ([Id] = @Id OR @Id IS NULL) 

	COMMIT
GO
IF OBJECT_ID('[dbo].[usp_CompanyPositionInsert]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[usp_CompanyPositionInsert] 
END 
GO
CREATE PROC [dbo].[usp_CompanyPositionInsert] 
    @Id int,
    @Name nvarchar(150),
    @CanApproveBudget bit
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[CompanyPosition] ([Id], [Name], [CanApproveBudget])
	SELECT @Id, @Name, @CanApproveBudget
	
	-- Begin Return Select <- do not remove
	SELECT [Id], [Name], [CanApproveBudget]
	FROM   [dbo].[CompanyPosition]
	WHERE  [Id] = @Id
	-- End Return Select <- do not remove
               
	COMMIT
GO
IF OBJECT_ID('[dbo].[usp_CompanyPositionUpdate]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[usp_CompanyPositionUpdate] 
END 
GO
CREATE PROC [dbo].[usp_CompanyPositionUpdate] 
    @Id int,
    @Name nvarchar(150),
    @CanApproveBudget bit
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[CompanyPosition]
	SET    [Id] = @Id, [Name] = @Name, [CanApproveBudget] = @CanApproveBudget
	WHERE  [Id] = @Id
	
	-- Begin Return Select <- do not remove
	SELECT [Id], [Name], [CanApproveBudget]
	FROM   [dbo].[CompanyPosition]
	WHERE  [Id] = @Id	
	-- End Return Select <- do not remove

	COMMIT
GO
IF OBJECT_ID('[dbo].[usp_CompanyPositionDelete]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[usp_CompanyPositionDelete] 
END 
GO
CREATE PROC [dbo].[usp_CompanyPositionDelete] 
    @Id int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[CompanyPosition]
	WHERE  [Id] = @Id

	COMMIT
GO

----------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------

