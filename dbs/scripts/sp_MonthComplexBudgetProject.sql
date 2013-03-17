USE [MyCompany_Database];
GO

IF OBJECT_ID('[dbo].[usp_MonthComplexBudgetProjectProjectSelect]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[usp_MonthComplexBudgetProjectProjectSelect] 
END 
GO
CREATE PROC [dbo].[usp_MonthComplexBudgetProjectProjectSelect] 
    @ID INT
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN
	
	SELECT * 
	FROM   [dbo].[MonthComplexBudgetProject] mbud
	INNER JOIN [dbo].[ComplexlBudget] cbud ON mbud.ComplexBudgetID = cbud.ID
	INNER JOIN [dbo].[BudgetProject] prj ON prj.ComplexBudgetID = cbud.ID
	WHERE  (mbud.ComplexBudgetID = @ID OR @ID IS NULL) 

	COMMIT
GO
IF OBJECT_ID('[dbo].[usp_MonthComplexBudgetProjectProjectInsert]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[usp_MonthComplexBudgetProjectProjectInsert] 
END 
GO
CREATE PROC [dbo].[usp_MonthComplexBudgetProjectProjectInsert] 
    @AdministrativeUnitID int,
    @Year int,
    @Month int,
    @Revision int,
    @RevisionDate datetime,
    @UpdatePersonId int,
    @IsAccepted bit
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	DECLARE @ID [int];
	INSERT INTO [dbo].[ComplexlBudget] ([AdministrativeUnitID])
	SELECT @AdministrativeUnitID
	
	SELECT @ID = SCOPE_IDENTITY();
		
	INSERT INTO [dbo].[MonthComplexBudgetProject] ([ComplexBudgetID], [Year], [Month])
	SELECT @ID, @Year, @Month
	
	INSERT INTO [dbo].[BudgetProject] ([ComplexBudgetID], [Revision], [RevisionDate], [UpdatePersonId], [IsAccepted])
	SELECT @ID, @Revision, @RevisionDate, @UpdatePersonId, @IsAccepted
	
	-- Begin Return Select <- do not remove
	SELECT * 
	FROM   [dbo].[MonthComplexBudgetProject] mbud
	INNER JOIN [dbo].[ComplexlBudget] cbud ON mbud.ComplexBudgetID = cbud.ID
	INNER JOIN [dbo].[BudgetProject] prj ON prj.ComplexBudgetID = cbud.ID
	WHERE  (mbud.ComplexBudgetID = @ID OR @ID IS NULL) 
	-- End Return Select <- do not remove
               
	COMMIT
GO
IF OBJECT_ID('[dbo].[usp_MonthComplexBudgetProjectProjectUpdate]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[usp_MonthComplexBudgetProjectProjectUpdate] 
END 
GO
CREATE PROC [dbo].[usp_MonthComplexBudgetProjectProjectUpdate] 
    @ID int,
    @AdministrativeUnitID int,
    @Year int,
    @Month int,
    @Revision int,
    @RevisionDate datetime,
    @UpdatePersonId int,
    @IsAccepted bit
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[ComplexlBudget]
	SET    [AdministrativeUnitID] = @AdministrativeUnitID
	WHERE  [ID] = @ID
	
	UPDATE [dbo].[MonthComplexBudgetProject]
	SET    [Year] = @Year, [Month] = @Month
	WHERE  [ComplexBudgetID] = @ID
	
	UPDATE [dbo].[BudgetProject]
	SET    [Revision] = @Revision, [RevisionDate] = @RevisionDate, [UpdatePersonId] = @UpdatePersonId, [IsAccepted] = @IsAccepted
	WHERE  [ComplexBudgetID] = @ID
	
	-- Begin Return Select <- do not remove
	SELECT * 
	FROM   [dbo].[MonthComplexBudgetProject] mbud
	INNER JOIN [dbo].[ComplexlBudget] cbud ON mbud.ComplexBudgetID = cbud.ID
	INNER JOIN [dbo].[BudgetProject] prj ON prj.ComplexBudgetID = cbud.ID
	WHERE  (mbud.ComplexBudgetID = @ID OR @ID IS NULL) 
	-- End Return Select <- do not remove

	COMMIT
GO
IF OBJECT_ID('[dbo].[usp_MonthComplexBudgetProjectProjectDelete]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[usp_MonthComplexBudgetProjectProjectDelete] 
END 
GO
CREATE PROC [dbo].[usp_MonthComplexBudgetProjectProjectDelete] 
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
	FROM   [dbo].[MonthComplexBudgetProject]
	WHERE  [ComplexBudgetID] = @ID
	
	DELETE
	FROM   [dbo].[BudgetProject]
	WHERE  [ComplexBudgetID] = @ID

	COMMIT
GO

----------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------

