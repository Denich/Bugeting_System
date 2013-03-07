USE [MyCompany_Database];
GO

IF OBJECT_ID('[dbo].[usp_BudgetItemInfoSelect]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[usp_BudgetItemInfoSelect] 
END 
GO
CREATE PROC [dbo].[usp_BudgetItemInfoSelect] 
    @ID INT
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [ID], [Name], [Description], [TargetBudgetID], [IsDeleted] 
	FROM   [dbo].[BudgetItemInfo] 
	WHERE  ([ID] = @ID OR @ID IS NULL) 

	COMMIT
GO
IF OBJECT_ID('[dbo].[usp_BudgetItemInfoInsert]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[usp_BudgetItemInfoInsert] 
END 
GO
CREATE PROC [dbo].[usp_BudgetItemInfoInsert] 
    @Name nvarchar(150),
    @Description nvarchar(MAX),
    @TargetBudgetID int,
    @IsDeleted bit
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[BudgetItemInfo] ([Name], [Description], [TargetBudgetID], [IsDeleted])
	SELECT @Name, @Description, @TargetBudgetID, @IsDeleted
	
	-- Begin Return Select <- do not remove
	SELECT [ID], [Name], [Description], [TargetBudgetID], [IsDeleted]
	FROM   [dbo].[BudgetItemInfo]
	WHERE  [ID] = SCOPE_IDENTITY()
	-- End Return Select <- do not remove
               
	COMMIT
GO
IF OBJECT_ID('[dbo].[usp_BudgetItemInfoUpdate]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[usp_BudgetItemInfoUpdate] 
END 
GO
CREATE PROC [dbo].[usp_BudgetItemInfoUpdate] 
    @ID int,
    @Name nvarchar(150),
    @Description nvarchar(MAX),
    @TargetBudgetID int,
    @IsDeleted bit
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[BudgetItemInfo]
	SET    [Name] = @Name, [Description] = @Description, [TargetBudgetID] = @TargetBudgetID, [IsDeleted] = @IsDeleted
	WHERE  [ID] = @ID
	
	-- Begin Return Select <- do not remove
	SELECT [ID], [Name], [Description], [TargetBudgetID], [IsDeleted]
	FROM   [dbo].[BudgetItemInfo]
	WHERE  [ID] = @ID	
	-- End Return Select <- do not remove

	COMMIT
GO
IF OBJECT_ID('[dbo].[usp_BudgetItemInfoDelete]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[usp_BudgetItemInfoDelete] 
END 
GO
CREATE PROC [dbo].[usp_BudgetItemInfoDelete] 
    @ID int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[BudgetItemInfo]
	WHERE  [ID] = @ID

	COMMIT
GO

----------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------

