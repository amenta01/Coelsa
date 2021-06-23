IF NOT EXISTS (SELECT 1 FROM sysdatabases WHERE (name = 'Coelsa')) 
	CREATE DATABASE Coelsa;

GO
USE [Coelsa]
GO

/****** Object:  Table [dbo].[Contacts]    Script Date: 16/6/2021 22:40:46 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'dbo.Contacts')
	DROP TABLE [dbo].[Contacts]

GO

CREATE TABLE [dbo].[Contacts](
	[IdContact] [int] NOT NULL Identity,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Company] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[PhoneNumber] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Contacts] PRIMARY KEY CLUSTERED 
(
	[IdContact] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


