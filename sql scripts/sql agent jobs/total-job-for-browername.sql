
use msdb;
go

declare @JobName nvarchar(128) = 'UpdateBrowser_AllShards';
DECLARE @OwnerLoginName SYSNAME = SUSER_SNAME();

if exists (select 1 from msdb.dbo.sysjobs where name = @JobName)
begin
	exec msdb.dbo.sp_delete_job @job_name = @JobName;
end

exec msdb.dbo.sp_add_job
	@job_name = @JobName,
	@enabled = 1,
	@description = 'Start all 4 shard update jobs concurrently',
	@owner_login_name = @OwnerLoginName;

exec msdb.dbo.sp_add_jobstep
	@job_name = @JobName,
	@step_name = 'StartAllShards',
	@subsystem = 'TSQL',
	@command = '
		exec msdb.dbo.sp_start_job N''UpdateBrowser_Shard_0'';
		exec msdb.dbo.sp_start_job N''UpdateBrowser_Shard_1'';
		exec msdb.dbo.sp_start_job N''UpdateBrowser_Shard_2'';
		exec msdb.dbo.sp_start_job N''UpdateBrowser_Shard_3'';
	',
	@database_name = 'msdb',
	@on_success_action = 1,
	@on_fail_action = 2;


exec msdb.dbo.sp_add_jobserver
	@job_name = @JobName,
	@server_name = @@SERVERNAME;

GO

