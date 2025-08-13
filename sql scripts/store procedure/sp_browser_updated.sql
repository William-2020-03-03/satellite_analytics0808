USE [lr_satellite]
GO

/****** Object:  StoredProcedure [dbo].[UpdateUserOperationLogBrowser_Batch_Sharded]    Script Date: 8/13/2025 3:25:30 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



create procedure [dbo].[UpdateUserOperationLogBrowser_Batch_Sharded]
	@Shard int
as
begin
	set nocount on;

	declare @BatchSize int = 10000;
	declare @UpdatedRows int = 1;

	while @UpdatedRows > 0
	begin
		with cte as (
			select top (@BatchSize) *
			from [dbo].[user_operation_log]
			where browser is null
				AND ABS(CAST(CAST(HASHBYTES('MD5', CAST(id AS NVARCHAR(36))) AS BINARY(4)) AS INT)) % 4 = @Shard
		)
		update cte
		set browser = 
			case ABS(CHECKSUM(NEWID())) % 3
				when 0 then 'Chrome'
				when 1 then 'Firefox'
				else 'IE'
			end;

			set @UpdatedRows = @@ROWCOUNT;
			PRINT 'Shard ' + CAST(@Shard AS VARCHAR) + ': ' + CAST(@UpdatedRows AS VARCHAR) + ' rows updated.';
	end
end;
GO


