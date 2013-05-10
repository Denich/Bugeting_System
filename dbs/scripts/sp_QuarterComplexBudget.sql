USE [MyCompany_Database];
GO

IF OBJECT_ID('[dbo].[usp_QuarterComplexBudgetSelect]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[usp_QuarterComplexBudgetSelect] 
END 
GO
CREATE PROC [dbo].[usp_QuarterComplexBudgetSelect] 
    @ID INT
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT *
	FROM   [dbo].[QuarterComplexBudget] qbud
	INNER JOIN [dbo].[ComplexlBudget] cbud ON qbud.ComplexBudgetID = cbud.ID
	WHERE  (qbud.ComplexBudgetID = @ID OR @ID IS NULL) 
		AND NOT EXISTS 
		(SELECT ID FROM [dbo].[BudgetProject] prj 
			WHERE prj.ComplexBudgetID = qbud.ComplexBudgetID)
	COMMIT
GO
IF OBJECT_ID('[dbo].[usp_QuarterComplexBudgetInsert]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[usp_QuarterComplexBudgetInsert] 
END 
GO
CREATE PROC [dbo].[usp_QuarterComplexBudgetInsert] 
    @AdministrativeUnitID int,
    @IsFinal bit,
    @Year int,
    @QuarterNumber int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	DECLARE @ID [int];
	INSERT INTO [dbo].[ComplexlBudget] ([AdministrativeUnitID], [IsFinal])
	SELECT @AdministrativeUnitID, @IsFinal
	
	SELECT @ID = SCOPE_IDENTITY();
	
	INSERT INTO [dbo].[QuarterComplexBudget] ([ComplexBudgetID], [Year], [QuarterNumber])
	SELECT @ID, @Year, @QuarterNumber
	
	-- Begin Return Select <- do not remove
	SELECT *
	FROM   [dbo].[QuarterComplexBudget] qbud
	INNER JOIN [dbo].[ComplexlBudget] cbud ON qbud.ComplexBudgetID = cbud.ID
	WHERE  (qbud.ComplexBudgetID = @ID OR @ID IS NULL) 
		AND NOT EXISTS 
		(SELECT ID FROM [dbo].[BudgetProject] prj 
			WHERE prj.ComplexBudgetID = qbud.ComplexBudgetID)
	-- End Return Select <- do not remove
               
	COMMIT
GO
IF OBJECT_ID('[dbo].[usp_QuarterComplexBudgetUpdate]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[usp_QuarterComplexBudgetUpdate] 
END 
GO
CREATE PROC [dbo].[usp_QuarterComplexBudgetUpdate] 
    @ID int,
    @AdministrativeUnitID int,
    @IsFinal bit,
    @Year int,
    @QuarterNumber int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	UPDATE [dbo].[ComplexlBudget]
	SET    [AdministrativeUnitID] = @AdministrativeUnitID, [IsFinal] = @IsFinal
	WHERE  [ID] = @ID
	
	UPDATE [dbo].[QuarterComplexBudget]
	SET    [Year] = @Year, [QuarterNumber] = @QuarterNumber
	WHERE  [ComplexBudgetID] = @ID
	
	-- Begin Return Select <- do not remove
	SELECT *
	FROM   [dbo].[QuarterComplexBudget] qbud
	INNER JOIN [dbo].[ComplexlBudget] cbud ON qbud.ComplexBudgetID = cbud.ID
	WHERE  (qbud.ComplexBudgetID = @ID OR @ID IS NULL) 
		AND NOT EXISTS 
		(SELECT ID FROM [dbo].[BudgetProject] prj 
			WHERE prj.ComplexBudgetID = qbud.ComplexBudgetID)
	-- End Return Select <- do not remove

	COMMIT
GO
IF OBJECT_ID('[dbo].[usp_QuarterComplexBudgetDelete]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[usp_QuarterComplexBudgetDelete] 
END 
GO
CREATE PROC [dbo].[usp_QuarterComplexBudgetDelete] 
    @ID int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[QuarterComplexBudget]
	WHERE  [ComplexBudgetID] = @ID

	/*Can delete this when add foreign key*/
	DELETE
	FROM   [dbo].[YearComplexBudget]
	WHERE  [ComplexBudgetID] = @ID
	
	COMMIT
GO

----------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------

