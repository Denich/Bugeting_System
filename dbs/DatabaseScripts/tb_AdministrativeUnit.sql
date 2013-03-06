USE [MyCompany_Database]
GO

/****** Object:  Table [dbo].[AdministrativeUnit]    Script Date: 03/06/2013 16:19:08 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AdministrativeUnit]') AND type in (N'U'))
DROP TABLE [dbo].[AdministrativeUnit]
GO

