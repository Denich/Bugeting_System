USE [MyCompany_Database];
GO

IF OBJECT_ID('[dbo].[usp_MonthComplexBudgetSelect]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[usp_MonthComplexBudgetSelect] 
END 
GO
CREATE PROC [dbo].[usp_MonthComplexBudgetSelect] 
    @ID INT
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT * 
	FROM   [dbo].[MonthComplexBudget] mbud
	INNER JOIN [dbo].[ComplexlBudget] cbud ON mbud.ComplexBudgetID = cbud.ID
	WHERE  (mbud.ComplexBudgetID = @ID OR @ID IS NULL) 
		AND NOT EXISTS 
		(SELECT ID FROM [dbo].[BudgetProject] prj 
			WHERE prj.ComplexBudgetID = mbud.ComplexBudgetID)
			
	COMMIT
GO
IF OBJECT_ID('[dbo].[usp_MonthComplexBudgetInsert]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[usp_MonthComplexBudgetInsert] 
END 
GO
CREATE PROC [dbo].[usp_MonthComplexBudgetInsert] 
    @AdministrativeUnitID int,
    @MasterBudgetID int,
    @IsFinal bit,
    @Year int,
    @Month int,
    @QuarterBudgetID int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	DECLARE @ID [int];
	INSERT INTO [dbo].[ComplexlBudget] ([AdministrativeUnitID], [IsFinal], [MasterBudgetID])
	SELECT @AdministrativeUnitID, @IsFinal, @MasterBudgetID
	
	SELECT @ID = SCOPE_IDENTITY();
	
	INSERT INTO [dbo].[MonthComplexBudget] ([ComplexBudgetID], [Year], [Month], [QuarterBudgetID])
	SELECT @ID, @Year, @Month, @QuarterBudgetID
	
	-- Begin Return Select <- do not remove
	SELECT * 
	FROM   [dbo].[MonthComplexBudget] mbud
	INNER JOIN [dbo].[ComplexlBudget] cbud ON mbud.ComplexBudgetID = cbud.ID
	WHERE  (mbud.ComplexBudgetID = @ID OR @ID IS NULL) 
		AND NOT EXISTS 
		(SELECT ID FROM [dbo].[BudgetProject] prj 
			WHERE prj.ComplexBudgetID = mbud.ComplexBudgetID)
	-- End Return Select <- do not remove
               
	COMMIT
GO
IF OBJECT_ID('[dbo].[usp_MonthComplexBudgetUpdate]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[usp_MonthComplexBudgetUpdate] 
END 
GO
CREATE PROC [dbo].[usp_MonthComplexBudgetUpdate] 
    @ID int,
    @AdministrativeUnitID int,
    @MasterBudgetID int,
    @IsFinal bit,
    @Year int,
    @Month int,
    @QuarterBudgetID int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[ComplexlBudget]
	SET    [AdministrativeUnitID] = @AdministrativeUnitID, [IsFinal] = @IsFinal, [MasterBudgetID] = @MasterBudgetID
	WHERE  [ID] = @ID
	
	UPDATE [dbo].[MonthComplexBudget]
	SET    [Year] = @Year, [Month] = @Month, [QuarterBudgetID] = @QuarterBudgetID
	WHERE  [ComplexBudgetID] = @ID
	
	-- Begin Return Select <- do not remove
	SELECT * 
	FROM   [dbo].[MonthComplexBudget] mbud
	INNER JOIN [dbo].[ComplexlBudget] cbud ON mbud.ComplexBudgetID = cbud.ID
	WHERE  (mbud.ComplexBudgetID = @ID OR @ID IS NULL) 
		AND NOT EXISTS 
		(SELECT ID FROM [dbo].[BudgetProject] prj 
			WHERE prj.ComplexBudgetID = mbud.ComplexBudgetID)
	-- End Return Select <- do not remove

	COMMIT
GO
IF OBJECT_ID('[dbo].[usp_MonthComplexBudgetDelete]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[usp_MonthComplexBudgetDelete] 
END 
GO
CREATE PROC [dbo].[usp_MonthComplexBudgetDelete] 
    @ID int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[MonthComplexBudget]
	WHERE  [ComplexBudgetID] = @ID

	/*Can delete this when add foreign key*/
	DELETE
	FROM   [dbo].[YearComplexBudget]
	WHERE  [ComplexBudgetID] = @ID
	
	COMMIT
GO

----------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------

