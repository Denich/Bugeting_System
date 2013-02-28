USE [MyCompany_Database]
GO

/****** Object:  Table [dbo].[AdministrativeUnit]    Script Date: 02/28/2013 20:34:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AdministrativeUnit]') AND type in (N'U'))
DROP TABLE [dbo].[AdministrativeUnit]
GO

USE [MyCompany_Database]
GO

/****** Object:  Table [dbo].[AdministrativeUnit]    Script Date: 02/28/2013 20:34:54 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[AdministrativeUnit](
	[ID] [nvarchar](100) NOT NULL,
	[Name] [nvarchar](150) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Adress] [nchar](150) NULL,
	[DirectorId] [nvarchar](100) NOT NULL,
	[Phone] [nchar](150) NULL,
 CONSTRAINT [PK_AdministrativeUnit] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

