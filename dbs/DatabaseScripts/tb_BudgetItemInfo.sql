USE [MyCompany_Database]
GO

/****** Object:  Table [dbo].[TargetBudgetInfo]    Script Date: 03/06/2013 22:38:44 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TargetBudgetInfo]') AND type in (N'U'))
DROP TABLE [dbo].[TargetBudgetInfo]
GO

USE [MyCompany_Database]
GO

/****** Object:  Table [dbo].[TargetBudgetInfo]    Script Date: 03/06/2013 22:38:44 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TargetBudgetInfo](
	[ID] [int] NOT NULL,
	[Name] [nvarchar](150) NOT NULL,
	[BudgetCategoryID] [int] NOT NULL,
	[Description] [nvarchar](max) NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_TargetBudgetInfo] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

