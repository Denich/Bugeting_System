USE [MyCompany_Database]
GO

/****** Object:  Table [dbo].[BudgetCategory]    Script Date: 03/06/2013 22:51:51 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BudgetCategory]') AND type in (N'U'))
DROP TABLE [dbo].[BudgetCategory]
GO

USE [MyCompany_Database]
GO

/****** Object:  Table [dbo].[BudgetCategory]    Script Date: 03/06/2013 22:51:51 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[BudgetCategory](
	[ID] [int] IDENTITY(100,1) NOT NULL,
	[InfoId] [int] NOT NULL,
	[Value] [float] NOT NULL,
	[ComplexBudgetId] [int] NOT NULL,
	[ResponsibleEmployeeId] [int] NULL,
 CONSTRAINT [PK_BudgetCategory] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

