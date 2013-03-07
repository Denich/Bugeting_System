USE [MyCompany_Database];
GO

IF OBJECT_ID('[dbo].[usp_TargetBudgetInfoSelect]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[usp_TargetBudgetInfoSelect] 
END 
GO
CREATE PROC [dbo].[usp_TargetBudgetInfoSelect] 
    @ID INT
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [ID], [Name], [BudgetCategoryID], [Description], [IsDeleted] 
	FROM   [dbo].[TargetBudgetInfo] 
	WHERE  ([ID] = @ID OR @ID IS NULL) 

	COMMIT
GO
IF OBJECT_ID('[dbo].[usp_TargetBudgetInfoInsert]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[usp_TargetBudgetInfoInsert] 
END 
GO
CREATE PROC [dbo].[usp_TargetBudgetInfoInsert] 
    @Name nvarchar(150),
    @BudgetCategoryID int,
    @Description nvarchar(MAX),
    @IsDeleted bit
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[TargetBudgetInfo] ([Name], [BudgetCategoryID], [Description], [IsDeleted])
	SELECT @Name, @BudgetCategoryID, @Description, @IsDeleted
	
	-- Begin Return Select <- do not remove
	SELECT [ID], [Name], [BudgetCategoryID], [Description], [IsDeleted]
	FROM   [dbo].[TargetBudgetInfo]
	WHERE  [ID] = SCOPE_IDENTITY()
	-- End Return Select <- do not remove
               
	COMMIT
GO
IF OBJECT_ID('[dbo].[usp_TargetBudgetInfoUpdate]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[usp_TargetBudgetInfoUpdate] 
END 
GO
CREATE PROC [dbo].[usp_TargetBudgetInfoUpdate] 
    @ID int,
    @Name nvarchar(150),
    @BudgetCategoryID int,
    @Description nvarchar(MAX),
    @IsDeleted bit
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[TargetBudgetInfo]
	SET    [Name] = @Name, [BudgetCategoryID] = @BudgetCategoryID, [Description] = @Description, [IsDeleted] = @IsDeleted
	WHERE  [ID] = @ID
	
	-- Begin Return Select <- do not remove
	SELECT [ID], [Name], [BudgetCategoryID], [Description], [IsDeleted]
	FROM   [dbo].[TargetBudgetInfo]
	WHERE  [ID] = @ID	
	-- End Return Select <- do not remove

	COMMIT
GO
IF OBJECT_ID('[dbo].[usp_TargetBudgetInfoDelete]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[usp_TargetBudgetInfoDelete] 
END 
GO
CREATE PROC [dbo].[usp_TargetBudgetInfoDelete] 
    @ID int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[TargetBudgetInfo]
	WHERE  [ID] = @ID

	COMMIT
GO

----------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------

