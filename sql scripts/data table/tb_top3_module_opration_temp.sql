USE [lr_satellite]
GO

/****** Object:  Table [dbo].[Top3ModuleOperation_Temp]    Script Date: 8/13/2025 3:18:58 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Top3ModuleOperation_Temp](
	[Id] [uniqueidentifier] NOT NULL,
	[Module] [nvarchar](50) NULL,
	[Operation] [nvarchar](50) NULL,
	[MyCount] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Top3ModuleOperation_Temp] ADD  DEFAULT (newid()) FOR [Id]
GO


