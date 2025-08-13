use msdb;
go

declare @DatabaseName nvarchar(128) = 'lr_satellite';
DECLARE @OwnerLoginName SYSNAME = SUSER_SNAME();

declare @i int = 0;

while @i < 4
begin
	declare @JobName nvarchar(128) = 'UpdateBrowser_Shard_' + cast(@i as nvarchar);
	declare @StepName nvarchar(128) = 'Step_Update_Shard_' + cast(@i as nvarchar);
	declare @Command nvarchar(MAX) = 'EXEC ' + quotename(@DatabaseName) + '.dbo.UpdateUserOperationLogBrowser_Batch_Sharded @Shard = ' + cast(@i as nvarchar);
	DECLARE @JobDescription NVARCHAR(255) = 'Update browser column for shard ' + CAST(@i AS NVARCHAR);

	if exists (select 1 from msdb.dbo.sysjobs where name = @JobName)
	begin
		exec msdb.dbo.sp_delete_job @Job_name = @JobName;
	end

	exec msdb.dbo.sp_add_job @job_name = @JobName, @enabled = 1, @description = @JobDescription, @owner_login_name = @OwnerLoginName;

	exec msdb.dbo.sp_add_jobstep
		@job_name = @JobName,
		@step_name = @StepName,
		@subsystem = 'TSQL',
		@database_name = @DatabaseName,
		@command = @Command,
		@on_success_action = 1,
		@on_fail_action = 2;

	exec msdb.dbo.sp_add_jobserver
		@job_name = @JobName,
		@server_name = @@SERVERNAME;


	set @i = @i + 1;

end
go