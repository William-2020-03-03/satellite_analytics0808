USE [lr_satellite]
GO

/****** Object:  Table [dbo].[user_operation_log]    Script Date: 8/13/2025 3:18:26 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[user_operation_log](
	[id] [nvarchar](50) NOT NULL,
	[user_id] [nvarchar](50) NOT NULL,
	[application_id] [nvarchar](50) NOT NULL,
	[module] [nvarchar](50) NOT NULL,
	[operation] [nvarchar](50) NOT NULL,
	[trigger_type] [nvarchar](50) NOT NULL,
	[browser] [nvarchar](50) NOT NULL,
	[created] [nvarchar](50) NOT NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[user_operation_log]  WITH NOCHECK ADD  CONSTRAINT [FK_Application_Id] FOREIGN KEY([application_id])
REFERENCES [dbo].[application_info] ([id])
GO

ALTER TABLE [dbo].[user_operation_log] NOCHECK CONSTRAINT [FK_Application_Id]
GO


