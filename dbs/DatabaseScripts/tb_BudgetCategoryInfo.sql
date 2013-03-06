USE [MyCompany_Database]
GO

/****** Object:  Table [dbo].[BudgetCategoryInfo]    Script Date: 03/06/2013 22:40:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BudgetCategoryInfo]') AND type in (N'U'))
DROP TABLE [dbo].[BudgetCategoryInfo]
GO

USE [MyCompany_Database]
GO

/****** Object:  Table [dbo].[BudgetCategoryInfo]    Script Date: 03/06/2013 22:40:54 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[BudgetCategoryInfo](
	[ID] [int] IDENTITY(100,1) NOT NULL,
	[Name] [nvarchar](150) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_BudgetCategoryInfo] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

