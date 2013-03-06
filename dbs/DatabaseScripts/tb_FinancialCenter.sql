USE [MyCompany_Database]
GO

/****** Object:  Table [dbo].[FinancialCenter]    Script Date: 03/06/2013 10:26:44 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FinancialCenter]') AND type in (N'U'))
DROP TABLE [dbo].[FinancialCenter]
GO

USE [MyCompany_Database]
GO

/****** Object:  Table [dbo].[FinancialCenter]    Script Date: 03/06/2013 10:26:44 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[FinancialCenter](
	[AdministrativeUnitID] [int] NOT NULL,
	[Type] [int] NOT NULL,
	[CompanyId] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_FinancialCenter] PRIMARY KEY CLUSTERED 
(
	[AdministrativeUnitID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

