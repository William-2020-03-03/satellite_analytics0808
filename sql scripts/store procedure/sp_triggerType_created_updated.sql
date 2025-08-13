USE [lr_satellite]
GO

/****** Object:  StoredProcedure [dbo].[UpdateUserOperationLog_TriggerType_created]    Script Date: 8/13/2025 3:28:08 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE procedure [dbo].[UpdateUserOperationLog_TriggerType_created]
	@BatchSize int = 10000
as
begin
	set nocount on;

	declare @UpdatedRows int = 1;

	while @UpdatedRows > 0
	begin
		with cte as (
			select top (@BatchSize) *
			from [dbo].[user_operation_log] with(ROWLOCK READPAST UPDLOCK)
			where [trigger_type] is null
		)
		update cte
		set 
			[created] = DATEADD(SECOND, ABS(CHECKSUM(NEWID())) % DATEDIFF(SECOND, '2010-10-01', '2025-03-01'), '2010-10-01'),
			[trigger_type] = 
				case ABS(CHECKSUM(NEWID())) % 3
					when 0 then 'Shortcut'
					when 1 then 'Other'
					else 'Mouse'
				end;

		set @UpdatedRows = @@ROWCOUNT;
	    print concat('updated ', @UpdatedRows, ' line, time', convert(varchar, GETDATE(), 120));
	end
end;
GO


