USE [lr_satellite]
GO

/****** Object:  StoredProcedure [dbo].[UpdateUserOperationLogWithRandomAppId]    Script Date: 8/13/2025 3:30:19 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[UpdateUserOperationLogWithRandomAppId]
    @BatchSize INT = 20000,
    @MaxRetries INT = 3
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @AppCount INT;
    DECLARE @RetryCount INT;
    DECLARE @ErrorNumber INT;

    DECLARE @AppIds TABLE (
        row_num INT IDENTITY(1,1),
        app_id UNIQUEIDENTIFIER
    );

    INSERT INTO @AppIds (app_id)
    SELECT id FROM application_info;

    SELECT @AppCount = COUNT(*) FROM @AppIds;

    DECLARE @RowsAffected INT = 1;

    WHILE @RowsAffected > 0
    BEGIN
        SET @RetryCount = 0;

        WHILE @RetryCount <= @MaxRetries
        BEGIN
            BEGIN TRY
                BEGIN TRANSACTION;

                IF OBJECT_ID('tempdb..#BatchToUpdate') IS NOT NULL DROP TABLE #BatchToUpdate;
                SELECT TOP (@BatchSize) id, ABS(CHECKSUM(NEWID())) % @AppCount + 1 AS rnd
                INTO #BatchToUpdate
                FROM user_operation_log WITH (ROWLOCK, READPAST, UPDLOCK)
                WHERE application_id IS NULL

                UPDATE u
                SET u.application_id = a.app_id
                FROM user_operation_log u
                INNER JOIN #BatchToUpdate b ON u.id = b.id
                INNER JOIN @AppIds a ON a.row_num = b.rnd;

                SET @RowsAffected = @@ROWCOUNT;
                if @RowsAffected > 0
                begin
                    print concat('updated ', @RowsAffected, ' line, time', convert(varchar, GETDATE(), 120));
                end

                COMMIT TRANSACTION;
                BREAK;
            END TRY
            BEGIN CATCH
                ROLLBACK TRANSACTION;

                IF ERROR_NUMBER() = 1205
                BEGIN
                    SET @RetryCount += 1;
                    if @RetryCount > @MaxRetries
                    begin
                        THROW;
                    end
                    else
                    begin
                        WAITFOR DELAY '00:00:00.500';
                    end
                END
                ELSE
                BEGIN
                    THROW;
                END
            END CATCH
        END
    END
END;
GO


