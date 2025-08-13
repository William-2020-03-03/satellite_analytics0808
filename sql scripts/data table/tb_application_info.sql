USE [lr_satellite]
GO

/****** Object:  Table [dbo].[application_info]    Script Date: 8/13/2025 3:17:56 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[application_info](
	[id] [nvarchar](50) NOT NULL,
	[build] [nvarchar](50) NOT NULL,
	[platform] [nvarchar](50) NOT NULL,
	[language] [nchar](10) NOT NULL,
	[created] [datetime] NOT NULL,
 CONSTRAINT [PK_application_info] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


