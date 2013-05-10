USE [MyCompany_Database];
GO

IF OBJECT_ID('[dbo].[usp_QuarterComplexBudgetProjectSelect]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[usp_QuarterComplexBudgetProjectSelect] 
END 
GO
CREATE PROC [dbo].[usp_QuarterComplexBudgetProjectSelect] 
    @ID INT
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN
	
	SELECT * 
	FROM   [dbo].[QuarterComplexBudget] qbud
	INNER JOIN [dbo].[ComplexlBudget] cbud ON qbud.ComplexBudgetID = cbud.ID
	INNER JOIN [dbo].[BudgetProject] prj ON prj.ComplexBudgetID = cbud.ID
	WHERE  (qbud.ComplexBudgetID = @ID OR @ID IS NULL) 

	COMMIT
GO
IF OBJECT_ID('[dbo].[usp_QuarterComplexBudgetProjectInsert]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[usp_QuarterComplexBudgetProjectInsert] 
END 
GO
CREATE PROC [dbo].[usp_QuarterComplexBudgetProjectInsert] 
    @AdministrativeUnitID int,
    @IsFinal bit,
    @Year int,
    @QuarterNumber int,
    @Revision int,
    @RevisionDate datetime,
    @UpdatePersonId int,
    @IsAccepted bit,
    @IsRejected bit
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
	
	INSERT INTO [dbo].[BudgetProject] ([ComplexBudgetID], [Revision], [RevisionDate], [UpdatePersonId], [IsAccepted], [IsRejected])
	SELECT @ID, @Revision, @RevisionDate, @UpdatePersonId, @IsAccepted, @IsRejected
	
	-- Begin Return Select <- do not remove
	SELECT * 
	FROM   [dbo].[QuarterComplexBudget] qbud
	INNER JOIN [dbo].[ComplexlBudget] cbud ON qbud.ComplexBudgetID = cbud.ID
	INNER JOIN [dbo].[BudgetProject] prj ON prj.ComplexBudgetID = cbud.ID
	WHERE  (qbud.ComplexBudgetID = @ID OR @ID IS NULL) 
	-- End Return Select <- do not remove
               
	COMMIT
GO
IF OBJECT_ID('[dbo].[usp_QuarterComplexBudgetProjectUpdate]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[usp_QuarterComplexBudgetProjectUpdate] 
END 
GO
CREATE PROC [dbo].[usp_QuarterComplexBudgetProjectUpdate] 
    @ID int,
    @AdministrativeUnitID int,
    @IsFinal bit,
    @Year int,
    @QuarterNumber int,
    @Revision int,
    @RevisionDate datetime,
    @UpdatePersonId int,
    @IsAccepted bit,
    @IsRejected bit
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
	
	UPDATE [dbo].[BudgetProject]
	SET    [Revision] = @Revision, [RevisionDate] = @RevisionDate, [UpdatePersonId] = @UpdatePersonId, [IsAccepted] = @IsAccepted, [IsRejected] = @IsRejected
	WHERE  [ComplexBudgetID] = @ID
	
	-- Begin Return Select <- do not remove
	SELECT * 
	FROM   [dbo].[QuarterComplexBudget] qbud
	INNER JOIN [dbo].[ComplexlBudget] cbud ON qbud.ComplexBudgetID = cbud.ID
	INNER JOIN [dbo].[BudgetProject] prj ON prj.ComplexBudgetID = cbud.ID
	WHERE  (qbud.ComplexBudgetID = @ID OR @ID IS NULL) 
	-- End Return Select <- do not remove

	COMMIT
GO
IF OBJECT_ID('[dbo].[usp_QuarterComplexBudgetProjectDelete]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[usp_QuarterComplexBudgetProjectDelete] 
END 
GO
CREATE PROC [dbo].[usp_QuarterComplexBudgetProjectDelete] 
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
	FROM   [dbo].[QuarterComplexBudget]
	WHERE  [ComplexBudgetID] = @ID
	
	DELETE
	FROM   [dbo].[BudgetProject]
	WHERE  [ComplexBudgetID] = @ID

	COMMIT
GO

----------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------

