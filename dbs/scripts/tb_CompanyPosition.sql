USE [MyCompany_Database]
GO

/****** Object:  Table [dbo].[CompanyPosition]    Script Date: 04/14/2013 17:11:28 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CompanyPosition]') AND type in (N'U'))
DROP TABLE [dbo].[CompanyPosition]
GO

