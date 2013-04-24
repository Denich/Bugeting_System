USE [MyCompany_Database];
GO

IF OBJECT_ID('[dbo].[usp_TargetBudgetSelect]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[usp_TargetBudgetSelect] 
END 
GO
CREATE PROC [dbo].[usp_TargetBudgetSelect] 
    @ID INT
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [ID], [InfoId], [Value], [BudgetCategoryId] 
	FROM   [dbo].[TargetBudget] 
	WHERE  ([ID] = @ID OR @ID IS NULL) 

	COMMIT
GO
IF OBJECT_ID('[dbo].[usp_TargetBudgetInsert]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[usp_TargetBudgetInsert] 
END 
GO
CREATE PROC [dbo].[usp_TargetBudgetInsert] 
    @InfoId int,
    @Value float,
    @BudgetCategoryId int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[TargetBudget] ([InfoId], [Value], [BudgetCategoryId])
	OUTPUT INSERTED.ID
	SELECT @InfoId, @Value, @BudgetCategoryId
	
	-- Begin Return Select <- do not remove
	SELECT [ID], [InfoId], [Value], [BudgetCategoryId]
	FROM   [dbo].[TargetBudget]
	WHERE  [ID] = SCOPE_IDENTITY()
	-- End Return Select <- do not remove
               
	COMMIT
GO
IF OBJECT_ID('[dbo].[usp_TargetBudgetUpdate]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[usp_TargetBudgetUpdate] 
END 
GO
CREATE PROC [dbo].[usp_TargetBudgetUpdate] 
    @ID int,
    @InfoId int,
    @Value float,
    @BudgetCategoryId int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[TargetBudget]
	SET    [InfoId] = @InfoId, [Value] = @Value, [BudgetCategoryId] = @BudgetCategoryId
	WHERE  [ID] = @ID
	
	-- Begin Return Select <- do not remove
	SELECT [ID], [InfoId], [Value], [BudgetCategoryId]
	FROM   [dbo].[TargetBudget]
	WHERE  [ID] = @ID	
	-- End Return Select <- do not remove

	COMMIT
GO
IF OBJECT_ID('[dbo].[usp_TargetBudgetDelete]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[usp_TargetBudgetDelete] 
END 
GO
CREATE PROC [dbo].[usp_TargetBudgetDelete] 
    @ID int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[TargetBudget]
	WHERE  [ID] = @ID

	COMMIT
GO

----------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------

