USE [MyCompany_Database];
GO

IF OBJECT_ID('[dbo].[usp_YearComplexBudgetSelect]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[usp_YearComplexBudgetSelect] 
END 
GO
CREATE PROC [dbo].[usp_YearComplexBudgetSelect] 
    @ID INT
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT * 
	FROM   [dbo].[YearComplexBudget] ybud
	INNER JOIN [dbo].[ComplexlBudget] cbud ON ybud.ComplexBudgetID = cbud.ID
	WHERE  (ybud.ComplexBudgetID = @ID OR @ID IS NULL) 
		AND NOT EXISTS 
		(SELECT ID FROM [dbo].[BudgetProject] prj 
			WHERE prj.ComplexBudgetID = ybud.ComplexBudgetID)

	COMMIT
GO
IF OBJECT_ID('[dbo].[usp_YearComplexBudgetInsert]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[usp_YearComplexBudgetInsert] 
END 
GO
CREATE PROC [dbo].[usp_YearComplexBudgetInsert] 
    @AdministrativeUnitID int,
    @MasterBudgetID int,
    @IsFinal bit,
    @Year int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	DECLARE @ID [int];
	INSERT INTO [dbo].[ComplexlBudget] ([AdministrativeUnitID], [IsFinal], [MasterBudgetID])
	SELECT @AdministrativeUnitID, @IsFinal, @MasterBudgetID
	
	SELECT @ID = SCOPE_IDENTITY();
		
	INSERT INTO [dbo].[YearComplexBudget] ([ComplexBudgetID], [Year])
	SELECT @ID, @Year
	
	-- Begin Return Select <- do not remove
	SELECT * 
	FROM   [dbo].[YearComplexBudget] ybud
	INNER JOIN [dbo].[ComplexlBudget] cbud ON ybud.ComplexBudgetID = cbud.ID
	WHERE  (ybud.ComplexBudgetID = @ID OR @ID IS NULL) 
		AND NOT EXISTS 
		(SELECT ID FROM [dbo].[BudgetProject] prj 
			WHERE prj.ComplexBudgetID = ybud.ComplexBudgetID)
	-- End Return Select <- do not remove
               
	COMMIT
GO
IF OBJECT_ID('[dbo].[usp_YearComplexBudgetUpdate]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[usp_YearComplexBudgetUpdate] 
END 
GO
CREATE PROC [dbo].[usp_YearComplexBudgetUpdate] 
    @ID int,
    @MasterBudgetID int,
    @AdministrativeUnitID int,
    @IsFinal bit,
    @Year int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	UPDATE [dbo].[ComplexlBudget]
	SET    [AdministrativeUnitID] = @AdministrativeUnitID, [IsFinal] = @IsFinal, [MasterBudgetID] = @MasterBudgetID
	WHERE  [ID] = @ID
	
	UPDATE [dbo].[YearComplexBudget]
	SET    [Year] = @Year
	WHERE  [ComplexBudgetID] = @ID
	
	-- Begin Return Select <- do not remove
	SELECT * 
	FROM   [dbo].[YearComplexBudget] ybud
	INNER JOIN [dbo].[ComplexlBudget] cbud ON ybud.ComplexBudgetID = cbud.ID
	WHERE  (ybud.ComplexBudgetID = @ID OR @ID IS NULL) 
		AND NOT EXISTS 
		(SELECT ID FROM [dbo].[BudgetProject] prj 
			WHERE prj.ComplexBudgetID = ybud.ComplexBudgetID)
	-- End Return Select <- do not remove

	COMMIT
GO
IF OBJECT_ID('[dbo].[usp_YearComplexBudgetDelete]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[usp_YearComplexBudgetDelete] 
END 
GO
CREATE PROC [dbo].[usp_YearComplexBudgetDelete] 
    @ID int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	DELETE
	FROM   [dbo].[ComplexlBudget]
	WHERE  [ID] = @ID
	
	/*Can delete this when add foreign key*/
	DELETE
	FROM   [dbo].[YearComplexBudget]
	WHERE  [ComplexBudgetID] = @ID

	COMMIT
GO

----------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------

