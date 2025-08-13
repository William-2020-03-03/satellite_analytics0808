USE [lr_satellite]
GO

/****** Object:  StoredProcedure [dbo].[InsertUserOperationLogData_forUserId]    Script Date: 8/13/2025 3:24:14 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





CREATE    PROCEDURE [dbo].[InsertUserOperationLogData_forUserId]
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @BatchSize INT = 10000;
    DECLARE @TotalRows INT = 3000000;
    DECLARE @InsertedRows INT = 0;

    WHILE @InsertedRows < @TotalRows
    BEGIN
        ;WITH RandomGuids AS (
            SELECT TOP (@BatchSize)
                NEWID() AS id,
                ABS(CHECKSUM(NEWID())) % 100 + 1 AS rn
            FROM sys.all_objects a
            CROSS JOIN sys.all_objects b
        ),
        NumberedUserIds AS (
            SELECT user_id,
                   ROW_NUMBER() OVER (ORDER BY NEWID()) AS rn
            FROM dbo.user_id_pool
        )
        INSERT INTO dbo.user_operation_log (id, user_id)
        SELECT NEWID(), u.user_id
        FROM RandomGuids g
        JOIN NumberedUserIds u ON g.rn = u.rn;

        SET @InsertedRows += @BatchSize;
        
        PRINT CONCAT('Inserted ', @InsertedRows, ' rows...');
    END
END;

GO


