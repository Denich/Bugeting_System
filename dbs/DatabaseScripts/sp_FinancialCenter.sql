USE [MyCompany_Database];
GO

IF OBJECT_ID('[dbo].[usp_FinancialCenterSelect]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[usp_FinancialCenterSelect] 
END 
GO
CREATE PROC [dbo].[usp_FinancialCenterSelect] 
    @ID INT
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT * 
	FROM   [dbo].[FinancialCenter] finc  
	INNER JOIN [dbo].[AdministrativeUnit] aunit ON finc.AdministrativeUnitID = aunit.ID
	WHERE  (finc.AdministrativeUnitID = @ID OR @ID IS NULL) 

	COMMIT
GO
IF OBJECT_ID('[dbo].[usp_FinancialCenterInsert]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[usp_FinancialCenterInsert] 
END 
GO
CREATE PROC [dbo].[usp_FinancialCenterInsert] 
    @Name nvarchar(150),
    @Description nvarchar(MAX),
    @Adress nchar(150),
    @DirectorId int,
    @Phone nchar(150),
    @Type int,
    @CompanyId nvarchar(100)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	
	DECLARE @ID [int];
	INSERT INTO [dbo].[AdministrativeUnit] ([Name], [Description], [Adress], [DirectorId], [Phone])
	SELECT @Name, @Description, @Adress, @DirectorId, @Phone
	SELECT @ID = SCOPE_IDENTITY();
	INSERT INTO [dbo].[FinancialCenter] ([AdministrativeUnitID], [Type], [CompanyId])
	SELECT @ID, @Type, @CompanyId
	
	
	
	-- Begin Return Select <- do not remove
	SELECT * 
	FROM   [dbo].[FinancialCenter] finc  
	INNER JOIN [dbo].[AdministrativeUnit] aunit ON finc.AdministrativeUnitID = aunit.ID
	WHERE  finc.AdministrativeUnitID = @ID 
	-- End Return Select <- do not remove
               
	COMMIT
GO
IF OBJECT_ID('[dbo].[usp_FinancialCenterUpdate]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[usp_FinancialCenterUpdate] 
END 
GO
CREATE PROC [dbo].[usp_FinancialCenterUpdate] 
    @ID int,
    @Name nvarchar(150),
    @Description nvarchar(MAX),
    @Adress nchar(150),
    @DirectorId int,
    @Phone nchar(150),
    @Type int,
    @CompanyId nvarchar(100)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	UPDATE [dbo].[AdministrativeUnit]
	SET    [Name] = @Name, [Description] = @Description, [Adress] = @Adress, [DirectorId] = @DirectorId, [Phone] = @Phone
	WHERE  [ID] = @ID
	
	UPDATE [dbo].[FinancialCenter]
	SET    [Type] = @Type, [CompanyId] = @CompanyId
	WHERE  [AdministrativeUnitID] = @ID
	
	-- Begin Return Select <- do not remove
	SELECT * 
	FROM   [dbo].[FinancialCenter] finc  
	INNER JOIN [dbo].[AdministrativeUnit] aunit ON finc.AdministrativeUnitID = aunit.ID
	WHERE  finc.AdministrativeUnitID = @ID 
	-- End Return Select <- do not remove

	COMMIT
GO
IF OBJECT_ID('[dbo].[usp_FinancialCenterDelete]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[usp_FinancialCenterDelete] 
END 
GO
CREATE PROC [dbo].[usp_FinancialCenterDelete] 
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
	FROM   [dbo].[FinancialCenter]
	WHERE  [AdministrativeUnitID] = @ID

	COMMIT
GO

----------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------

