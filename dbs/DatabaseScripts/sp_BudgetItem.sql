USE [MyCompany_Database];
GO

IF OBJECT_ID('[dbo].[usp_BudgetItemSelect]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[usp_BudgetItemSelect] 
END 
GO
CREATE PROC [dbo].[usp_BudgetItemSelect] 
    @ID INT
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [ID], [InfoId], [Value], [TargetBudgetId] 
	FROM   [dbo].[BudgetItem] 
	WHERE  ([ID] = @ID OR @ID IS NULL) 

	COMMIT
GO
IF OBJECT_ID('[dbo].[usp_BudgetItemInsert]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[usp_BudgetItemInsert] 
END 
GO
CREATE PROC [dbo].[usp_BudgetItemInsert] 
    @InfoId int,
    @Value float,
    @TargetBudgetId int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[BudgetItem] ([InfoId], [Value], [TargetBudgetId])
	SELECT @InfoId, @Value, @TargetBudgetId
	
	-- Begin Return Select <- do not remove
	SELECT [ID], [InfoId], [Value], [TargetBudgetId]
	FROM   [dbo].[BudgetItem]
	WHERE  [ID] = SCOPE_IDENTITY()
	-- End Return Select <- do not remove
               
	COMMIT
GO
IF OBJECT_ID('[dbo].[usp_BudgetItemUpdate]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[usp_BudgetItemUpdate] 
END 
GO
CREATE PROC [dbo].[usp_BudgetItemUpdate] 
    @ID int,
    @InfoId int,
    @Value float,
    @TargetBudgetId int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[BudgetItem]
	SET    [InfoId] = @InfoId, [Value] = @Value, [TargetBudgetId] = @TargetBudgetId
	WHERE  [ID] = @ID
	
	-- Begin Return Select <- do not remove
	SELECT [ID], [InfoId], [Value], [TargetBudgetId]
	FROM   [dbo].[BudgetItem]
	WHERE  [ID] = @ID	
	-- End Return Select <- do not remove

	COMMIT
GO
IF OBJECT_ID('[dbo].[usp_BudgetItemDelete]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[usp_BudgetItemDelete] 
END 
GO
CREATE PROC [dbo].[usp_BudgetItemDelete] 
    @ID int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[BudgetItem]
	WHERE  [ID] = @ID

	COMMIT
GO

----------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------

