USE [MyCompany_Database];
GO

IF OBJECT_ID('[dbo].[usp_MonthComplexBudgetProjectSelect]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[usp_MonthComplexBudgetProjectSelect] 
END 
GO
CREATE PROC [dbo].[usp_MonthComplexBudgetProjectSelect] 
    @ID INT
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN
	
	SELECT * 
	FROM   [dbo].[MonthComplexBudget] mbud
	INNER JOIN [dbo].[ComplexlBudget] cbud ON mbud.ComplexBudgetID = cbud.ID
	INNER JOIN [dbo].[BudgetProject] prj ON prj.ComplexBudgetID = cbud.ID
	WHERE  (mbud.ComplexBudgetID = @ID OR @ID IS NULL) 

	COMMIT
GO
IF OBJECT_ID('[dbo].[usp_MonthComplexBudgetProjectInsert]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[usp_MonthComplexBudgetProjectInsert] 
END 
GO
CREATE PROC [dbo].[usp_MonthComplexBudgetProjectInsert] 
    @AdministrativeUnitID int,
    @MasterBudgetID int,
    @IsFinal bit,
    @Year int,
    @Month int,
    @QuarterBudgetID int,
    @Revision int,
    @RevisionDate datetime,
    @UpdatePersonId int,
    @Status int,
    @Comment nvarchar(250)
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
	
	INSERT INTO [dbo].[BudgetProject] ([ComplexBudgetID], [Revision], [RevisionDate], [UpdatePersonId], [Status], [Comment])
	SELECT @ID, @Revision, @RevisionDate, @UpdatePersonId, @Status, @Comment
	
	-- Begin Return Select <- do not remove
	SELECT * 
	FROM   [dbo].[MonthComplexBudget] mbud
	INNER JOIN [dbo].[ComplexlBudget] cbud ON mbud.ComplexBudgetID = cbud.ID
	INNER JOIN [dbo].[BudgetProject] prj ON prj.ComplexBudgetID = cbud.ID
	WHERE  (mbud.ComplexBudgetID = @ID OR @ID IS NULL) 
	-- End Return Select <- do not remove
               
	COMMIT
GO
IF OBJECT_ID('[dbo].[usp_MonthComplexBudgetProjectUpdate]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[usp_MonthComplexBudgetProjectUpdate] 
END 
GO
CREATE PROC [dbo].[usp_MonthComplexBudgetProjectUpdate] 
    @ID int,
    @AdministrativeUnitID int,
    @MasterBudgetID int,
    @IsFinal bit,
    @Year int,
    @Month int,
    @QuarterBudgetID int,
    @Revision int,
    @RevisionDate datetime,
    @UpdatePersonId int,
    @Status int,
    @Comment nvarchar(250)
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
	
	UPDATE [dbo].[BudgetProject]
	SET    [Revision] = @Revision, [RevisionDate] = @RevisionDate, [UpdatePersonId] = @UpdatePersonId, [Status] = @Status, [Comment] = @Comment
	WHERE  [ComplexBudgetID] = @ID
	
	-- Begin Return Select <- do not remove
	SELECT * 
	FROM   [dbo].[MonthComplexBudget] mbud
	INNER JOIN [dbo].[ComplexlBudget] cbud ON mbud.ComplexBudgetID = cbud.ID
	INNER JOIN [dbo].[BudgetProject] prj ON prj.ComplexBudgetID = cbud.ID
	WHERE  (mbud.ComplexBudgetID = @ID OR @ID IS NULL) 
	-- End Return Select <- do not remove

	COMMIT
GO
IF OBJECT_ID('[dbo].[usp_MonthComplexBudgetProjectDelete]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[usp_MonthComplexBudgetProjectDelete] 
END 
GO
CREATE PROC [dbo].[usp_MonthComplexBudgetProjectDelete] 
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
	FROM   [dbo].[MonthComplexBudget]
	WHERE  [ComplexBudgetID] = @ID
	
	DELETE
	FROM   [dbo].[BudgetProject]
	WHERE  [ComplexBudgetID] = @ID

	COMMIT
GO

----------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------

