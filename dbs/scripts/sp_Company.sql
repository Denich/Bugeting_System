USE [MyCompany_Database];
GO

IF OBJECT_ID('[dbo].[usp_CompanySelect]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[usp_CompanySelect] 
END 
GO
CREATE PROC [dbo].[usp_CompanySelect] 
    @ID INT
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT * 
	FROM   [dbo].[Company] comp  
	INNER JOIN [dbo].[AdministrativeUnit] aunit ON comp.AdministrativeUnitID = aunit.ID
	WHERE  (comp.AdministrativeUnitID = @ID OR @ID IS NULL) 

	COMMIT
GO
IF OBJECT_ID('[dbo].[usp_CompanyInsert]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[usp_CompanyInsert] 
END 
GO
CREATE PROC [dbo].[usp_CompanyInsert] 
    @Name nvarchar(150),
    @Description nvarchar(MAX),
    @Adress nchar(150),
    @DirectorId int,
    @Phone nchar(150),
    @AccountNumber nvarchar(150),
    @EDRPOU nvarchar(150)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	DECLARE @ID [int];
	INSERT INTO [dbo].[AdministrativeUnit] ([Name], [Description], [Adress], [DirectorId], [Phone])
	SELECT @Name, @Description, @Adress, @DirectorId, @Phone
	
	SELECT @ID = SCOPE_IDENTITY();
	
	INSERT INTO [dbo].[Company] ([AdministrativeUnitID], [AccountNumber], [EDRPOU])
	SELECT @ID, @AccountNumber, @EDRPOU
	
	-- Begin Return Select <- do not remove
	SELECT * 
	FROM   [dbo].[Company] comp  
	INNER JOIN [dbo].[AdministrativeUnit] aunit ON comp.AdministrativeUnitID = aunit.ID
	WHERE  (comp.AdministrativeUnitID = @ID OR @ID IS NULL) 
	-- End Return Select <- do not remove
               
	COMMIT
GO
IF OBJECT_ID('[dbo].[usp_CompanyUpdate]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[usp_CompanyUpdate] 
END 
GO
CREATE PROC [dbo].[usp_CompanyUpdate] 
    @ID int,
    @Name nvarchar(150),
    @Description nvarchar(MAX),
    @Adress nchar(150),
    @DirectorId int,
    @Phone nchar(150),
    @AccountNumber nvarchar(150),
    @EDRPOU nvarchar(150)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[AdministrativeUnit]
	SET    [Name] = @Name, [Description] = @Description, [Adress] = @Adress, [DirectorId] = @DirectorId, [Phone] = @Phone
	WHERE  [ID] = @ID
	
	UPDATE [dbo].[Company]
	SET    [AdministrativeUnitID] = @ID, [AccountNumber] = @AccountNumber, [EDRPOU] = @EDRPOU
	WHERE  [AdministrativeUnitID] = @ID
	
	-- Begin Return Select <- do not remove
	SELECT * 
	FROM   [dbo].[Company] comp  
	INNER JOIN [dbo].[AdministrativeUnit] aunit ON comp.AdministrativeUnitID = aunit.ID
	WHERE  (comp.AdministrativeUnitID = @ID OR @ID IS NULL) 
	-- End Return Select <- do not remove

	COMMIT
GO
IF OBJECT_ID('[dbo].[usp_CompanyDelete]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[usp_CompanyDelete] 
END 
GO
CREATE PROC [dbo].[usp_CompanyDelete] 
    @ID int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[AdministrativeUnit]
	WHERE  [ID] = @ID
	
	/*Can delete this when add foreign key*/
	DELETE
	FROM   [dbo].[Company]
	WHERE  [AdministrativeUnitID] = @ID

	COMMIT
GO

----------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------

