USE [MyCompany_Database]
GO

/****** Object:  Table [dbo].[BudgetItemInfo]    Script Date: 03/10/2013 22:18:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BudgetItemInfo]') AND type in (N'U'))
DROP TABLE [dbo].[BudgetItemInfo]
GO

USE [MyCompany_Database]
GO

/****** Object:  Table [dbo].[BudgetItemInfo]    Script Date: 03/10/2013 22:18:32 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[BudgetItemInfo](
	[ID] [int] IDENTITY(100,1) NOT NULL,
	[Name] [nvarchar](150) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[TargetBudgetID] [int] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[DateAdded] [date] NULL,
	[Source] [nvarchar](150) NULL,
 CONSTRAINT [PK_BudgetItemInfo] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

