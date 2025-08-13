USE [lr_satellite]
GO

/****** Object:  StoredProcedure [dbo].[FillModuleColumnInBatches_Random_WithRetry]    Script Date: 8/13/2025 3:29:37 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE   procedure [dbo].[FillModuleColumnInBatches_Random_WithRetry]
	@BatchSize int = 5000,
	@MaxRetries int = 5
as
begin
	set nocount on;

	declare @RetryCount int = 0;
	declare @RowsAffected int = 1;

	declare @Modules table(module nvarchar(50));
	insert into @Modules (module)
	values
		('Toolbar'), ('Contextmenu'), ('Steps'), ('Actions'), ('Runlogic'), ('Libraries'), ('General settings'), ('Event handlers'),
		('Transactions'), ('Spy'), ('Coded'), ('Search'), ('Snapshot viewer'), ('Debugger viewer');


	while @RowsAffected > 0
	begin
		set @RetryCount = 0;

		while @RetryCount <= @MaxRetries
		begin
			begin try
				begin transaction;

				if object_id('tempdb..#BatchToUpdate') is not null drop table #BatchToUpdate;
				select top (@BatchSize) id, NEWID() as row_num
				into #BatchToUpdate
				from [dbo].[user_operation_log] with (ROWLOCK,READPAST, UPDLOCK)
				where module is null;


				;with NumberedModules as (
					select module, ROW_NUMBER() over (order by (select null)) as row_num
					from @Modules
				)
				update u
				set u.module = m.module
				from [dbo].[user_operation_log] u
				inner join #BatchToUpdate b on u.id = b.id
				inner join NumberedModules m on (ABS(CHECKSUM(b.row_num)) % 14) + 1 = m.row_num

				set @RowsAffected = @@ROWCOUNT;
				if @RowsAffected > 0
				begin
					print concat('updated ', @RowsAffected, ' line, time ', convert(varchar, getdate(), 120));
				end

				commit transaction;
				break;
			end try
			begin catch
				rollback transaction;

				if ERROR_NUMBER() = 1205
				begin
					set @RetryCount += 1;
					if @RetryCount > @MaxRetries
					begin
						throw;
					end
					else
					begin
						waitfor delay '00:00:00.500';
					end
				end
				else
				begin
					throw;
				end
			end catch
		end
	end
end;
GO


