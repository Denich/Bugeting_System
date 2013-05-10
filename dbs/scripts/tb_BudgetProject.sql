USE [MyCompany_Database]
GO

/****** Object:  Table [dbo].[BudgetProject]    Script Date: 03/16/2013 16:37:00 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BudgetProject]') AND type in (N'U'))
DROP TABLE [dbo].[BudgetProject]
GO

USE [MyCompany_Database]
GO

/****** Object:  Table [dbo].[BudgetProject]    Script Date: 03/16/2013 16:37:00 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[BudgetProject](
	[ComplexBudgetID] [int] NOT NULL,
	[Revision] [int] NOT NULL,
	[RevisionDate] [datetime] NOT NULL,
	[UpdatePersonId] [int] NOT NULL,
	[IsAccepted] [bit] NOT NULL,
	[IsRejected] [bit] NOT NULL,
 CONSTRAINT [PK_BudgetProject] PRIMARY KEY CLUSTERED 
(
	[ComplexBudgetID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


