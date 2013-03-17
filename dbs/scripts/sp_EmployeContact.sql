USE [MyCompany_Database];
GO

IF OBJECT_ID('[dbo].[usp_EmployeContactSelect]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[usp_EmployeContactSelect] 
END 
GO
CREATE PROC [dbo].[usp_EmployeContactSelect] 
    @ID INT
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [EmployeID], [Email], [WorkPhone], [MobilePhone], [Skype], [Adress] 
	FROM   [dbo].[EmployeContact] 
	WHERE  ([EmployeID] = @ID OR @ID IS NULL) 

	COMMIT
GO
IF OBJECT_ID('[dbo].[usp_EmployeContactInsert]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[usp_EmployeContactInsert] 
END 
GO
CREATE PROC [dbo].[usp_EmployeContactInsert] 
    @ID int,
    @Email nvarchar(150),
    @WorkPhone nvarchar(150),
    @MobilePhone nvarchar(150),
    @Skype nvarchar(150),
    @Adress nvarchar(150)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[EmployeContact] ([EmployeID], [Email], [WorkPhone], [MobilePhone], [Skype], [Adress])
	SELECT @ID, @Email, @WorkPhone, @MobilePhone, @Skype, @Adress
	
	-- Begin Return Select <- do not remove
	SELECT [EmployeID], [Email], [WorkPhone], [MobilePhone], [Skype], [Adress]
	FROM   [dbo].[EmployeContact]
	WHERE  [EmployeID] = @ID
	-- End Return Select <- do not remove
               
	COMMIT
GO
IF OBJECT_ID('[dbo].[usp_EmployeContactUpdate]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[usp_EmployeContactUpdate] 
END 
GO
CREATE PROC [dbo].[usp_EmployeContactUpdate] 
    @ID int,
    @Email nvarchar(150),
    @WorkPhone nvarchar(150),
    @MobilePhone nvarchar(150),
    @Skype nvarchar(150),
    @Adress nvarchar(150)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[EmployeContact]
	SET    [EmployeID] = @ID, [Email] = @Email, [WorkPhone] = @WorkPhone, [MobilePhone] = @MobilePhone, [Skype] = @Skype, [Adress] = @Adress
	WHERE  [EmployeID] = @ID
	
	-- Begin Return Select <- do not remove
	SELECT [EmployeID], [Email], [WorkPhone], [MobilePhone], [Skype], [Adress]
	FROM   [dbo].[EmployeContact]
	WHERE  [EmployeID] = @ID	
	-- End Return Select <- do not remove

	COMMIT
GO
IF OBJECT_ID('[dbo].[usp_EmployeContactDelete]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[usp_EmployeContactDelete] 
END 
GO
CREATE PROC [dbo].[usp_EmployeContactDelete] 
    @ID int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[EmployeContact]
	WHERE  [EmployeID] = @ID

	COMMIT
GO

----------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------

