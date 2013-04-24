USE [MyCompany_Database];
GO

IF OBJECT_ID('[dbo].[usp_BudgetCategorySelect]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[usp_BudgetCategorySelect] 
END 
GO
CREATE PROC [dbo].[usp_BudgetCategorySelect] 
    @ID INT
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [ID], [InfoId], [Value], [ComplexBudgetId], [ResponsibleEmployeeId] 
	FROM   [dbo].[BudgetCategory] 
	WHERE  ([ID] = @ID OR @ID IS NULL) 

	COMMIT
GO
IF OBJECT_ID('[dbo].[usp_BudgetCategoryInsert]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[usp_BudgetCategoryInsert] 
END 
GO
CREATE PROC [dbo].[usp_BudgetCategoryInsert] 
    @InfoId int,
    @Value float,
    @ComplexBudgetId int,
    @ResponsibleEmployeeId int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[BudgetCategory] ([InfoId], [Value], [ComplexBudgetId], [ResponsibleEmployeeId])
	OUTPUT INSERTED.ID
	SELECT @InfoId, @Value, @ComplexBudgetId, @ResponsibleEmployeeId
	
	-- Begin Return Select <- do not remove
	SELECT [ID], [InfoId], [Value], [ComplexBudgetId], [ResponsibleEmployeeId]
	FROM   [dbo].[BudgetCategory]
	WHERE  [ID] = SCOPE_IDENTITY()
	-- End Return Select <- do not remove
               
	COMMIT
GO
IF OBJECT_ID('[dbo].[usp_BudgetCategoryUpdate]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[usp_BudgetCategoryUpdate] 
END 
GO
CREATE PROC [dbo].[usp_BudgetCategoryUpdate] 
    @ID int,
    @InfoId int,
    @Value float,
    @ComplexBudgetId int,
    @ResponsibleEmployeeId int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[BudgetCategory]
	SET    [InfoId] = @InfoId, [Value] = @Value, [ComplexBudgetId] = @ComplexBudgetId, [ResponsibleEmployeeId] = @ResponsibleEmployeeId
	WHERE  [ID] = @ID
	
	-- Begin Return Select <- do not remove
	SELECT [ID], [InfoId], [Value], [ComplexBudgetId], [ResponsibleEmployeeId]
	FROM   [dbo].[BudgetCategory]
	WHERE  [ID] = @ID	
	-- End Return Select <- do not remove

	COMMIT
GO
IF OBJECT_ID('[dbo].[usp_BudgetCategoryDelete]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[usp_BudgetCategoryDelete] 
END 
GO
CREATE PROC [dbo].[usp_BudgetCategoryDelete] 
    @ID int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[BudgetCategory]
	WHERE  [ID] = @ID

	COMMIT
GO

----------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------

