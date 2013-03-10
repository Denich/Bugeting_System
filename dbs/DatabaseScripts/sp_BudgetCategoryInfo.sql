USE [MyCompany_Database];
GO

IF OBJECT_ID('[dbo].[usp_BudgetCategoryInfoSelect]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[usp_BudgetCategoryInfoSelect] 
END 
GO
CREATE PROC [dbo].[usp_BudgetCategoryInfoSelect] 
    @ID INT
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT *
	FROM   [dbo].[BudgetCategoryInfo] 
	WHERE  ([ID] = @ID OR @ID IS NULL) 

	COMMIT
GO
IF OBJECT_ID('[dbo].[usp_BudgetCategoryInfoInsert]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[usp_BudgetCategoryInfoInsert] 
END 
GO
CREATE PROC [dbo].[usp_BudgetCategoryInfoInsert] 
    @Name nvarchar(150),
    @Description nvarchar(MAX),
    @IsDeleted bit,
    @DateAdded date,
    @Source nvarchar(150)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[BudgetCategoryInfo] ([Name], [Description], [IsDeleted], [DateAdded], [Source])
	SELECT @Name, @Description, @IsDeleted, @DateAdded, @Source
	
	-- Begin Return Select <- do not remove
	SELECT *
	FROM   [dbo].[BudgetCategoryInfo]
	WHERE  [ID] = SCOPE_IDENTITY()
	-- End Return Select <- do not remove
               
	COMMIT
GO
IF OBJECT_ID('[dbo].[usp_BudgetCategoryInfoUpdate]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[usp_BudgetCategoryInfoUpdate] 
END 
GO
CREATE PROC [dbo].[usp_BudgetCategoryInfoUpdate] 
    @ID int,
    @Name nvarchar(150),
    @Description nvarchar(MAX),
    @IsDeleted bit,
    @DateAdded date,
    @Source nvarchar(150)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[BudgetCategoryInfo]
	SET    [Name] = @Name, [Description] = @Description, [IsDeleted] = @IsDeleted, [DateAdded] = @DateAdded, [Source] = @Source
	WHERE  [ID] = @ID
	
	-- Begin Return Select <- do not remove
	SELECT *
	FROM   [dbo].[BudgetCategoryInfo]
	WHERE  [ID] = @ID	
	-- End Return Select <- do not remove

	COMMIT
GO
IF OBJECT_ID('[dbo].[usp_BudgetCategoryInfoDelete]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[usp_BudgetCategoryInfoDelete] 
END 
GO
CREATE PROC [dbo].[usp_BudgetCategoryInfoDelete] 
    @ID int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[BudgetCategoryInfo]
	WHERE  [ID] = @ID

	COMMIT
GO

----------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------

