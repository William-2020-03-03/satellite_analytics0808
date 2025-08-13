-- 备份业务数据库
BACKUP DATABASE [lr_satellite]
TO DISK = N'C:\Backup\lr_satellite_full.bak'
WITH FORMAT, INIT, COMPRESSION, 
     NAME = N'lr_satellite-Full Database Backup',
     STATS = 10;

-- 备份 msdb 数据库（包含 SQL Agent Jobs 等元数据）
BACKUP DATABASE [msdb]
TO DISK = N'c:\Backup\msdb_full.bak'
WITH FORMAT, INIT, COMPRESSION, 
     NAME = N'msdb-Full Backup',
     STATS = 10;
