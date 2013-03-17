USE [MyCompany_Database];
GO

IF OBJECT_ID('[dbo].[usp_EmployeSelect]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[usp_EmployeSelect] 
END 
GO
CREATE PROC [dbo].[usp_EmployeSelect] 
    @ID INT
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [ID], [FirstName], [SecondName], [MiddleName], [Position] 
	FROM   [dbo].[Employe] 
	WHERE  ([ID] = @ID OR @ID IS NULL) 

	COMMIT
GO
IF OBJECT_ID('[dbo].[usp_EmployeInsert]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[usp_EmployeInsert] 
END 
GO
CREATE PROC [dbo].[usp_EmployeInsert] 
    @FirstName nvarchar(150),
    @SecondName nvarchar(150),
    @MiddleName nvarchar(150),
    @Position nvarchar(150)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[Employe] ([FirstName], [SecondName], [MiddleName], [Position])
	SELECT @FirstName, @SecondName, @MiddleName, @Position
	
	-- Begin Return Select <- do not remove
	SELECT [ID], [FirstName], [SecondName], [MiddleName], [Position]
	FROM   [dbo].[Employe]
	WHERE  [ID] = SCOPE_IDENTITY()
	-- End Return Select <- do not remove
               
	COMMIT
GO
IF OBJECT_ID('[dbo].[usp_EmployeUpdate]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[usp_EmployeUpdate] 
END 
GO
CREATE PROC [dbo].[usp_EmployeUpdate] 
    @ID int,
    @FirstName nvarchar(150),
    @SecondName nvarchar(150),
    @MiddleName nvarchar(150),
    @Position nvarchar(150)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[Employe]
	SET    [FirstName] = @FirstName, [SecondName] = @SecondName, [MiddleName] = @MiddleName, [Position] = @Position
	WHERE  [ID] = @ID
	
	-- Begin Return Select <- do not remove
	SELECT [ID], [FirstName], [SecondName], [MiddleName], [Position]
	FROM   [dbo].[Employe]
	WHERE  [ID] = @ID	
	-- End Return Select <- do not remove

	COMMIT
GO
IF OBJECT_ID('[dbo].[usp_EmployeDelete]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[usp_EmployeDelete] 
END 
GO
CREATE PROC [dbo].[usp_EmployeDelete] 
    @ID int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[Employe]
	WHERE  [ID] = @ID

	COMMIT
GO

----------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------

