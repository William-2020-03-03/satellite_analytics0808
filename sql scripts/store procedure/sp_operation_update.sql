USE [lr_satellite]
GO

/****** Object:  StoredProcedure [dbo].[FillOperationColumnInBatches_TrueRandom_WithRetry]    Script Date: 8/13/2025 3:29:04 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[FillOperationColumnInBatches_TrueRandom_WithRetry]
    @BatchSize INT = 5000,
    @MaxRetries INT = 5
AS
BEGIN
    set nocount on;

    declare @RetryCount int = 0;
    declare @RowsAffected  int = 1;

    DECLARE @Operations TABLE (operation NVARCHAR(50));
    INSERT INTO @Operations (operation)
    VALUES 
        ('Copy'), ('Cut'), ('Paste'), ('Delete'), ('Undo'), ('Redo'), ('Open'), ('Close'),
        ('Start'), ('End'), ('Group'), ('UnGroup'), ('Rename'), ('Convert'), ('UnConvert'),
        ('Download'), ('Upload'), ('Surround'), ('Add'), ('Toggle'), ('Replay'), ('Record'),
        ('Pause'), ('Resume'), ('Stop'), ('Edit'), ('Highlight'), ('Replace'), ('Import'),
        ('Export'), ('Find'), ('Goto');


    while @RowsAffected > 0
    begin
        set @RetryCount = 0;

        while @RetryCount <= @MaxRetries
        begin
            begin try
                IF OBJECT_ID('tempdb..#BatchToUpdate') IS NOT NULL DROP TABLE #BatchToUpdate;
                SELECT TOP (@BatchSize) ID, NEWID() AS rnd
                INTO #BatchToUpdate
                FROM user_operation_log WITH (READPAST)
                WHERE operation IS NULL;

               ;WITH NumberedOps AS (
                    SELECT operation, ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) AS RowNum
                    FROM @Operations
                )
                update u
                set u.operation = o.operation
                from [dbo].[user_operation_log] u
                inner join #BatchToUpdate b on u.id = b.ID
                inner join NumberedOps o on (ABS(CHECKSUM(b.rnd)) % 32) + 1 = o.RowNum;

                set @RowsAffected = @@ROWCOUNT;
                if @RowsAffected > 0
                begin
                    print concat('updated', @RowsAffected, 'line, time', convert(varchar, GETDATE(), 120));
                end

                break;
            end TRY
            begin catch
                if ERROR_NUMBER() = 1205
                begin
                    set @RetryCount += 1;
                    if @RetryCount > @MaxRetries
                    begin
                        THROW;
                    end
                    ELSE 
                    begin
                         WAITFOR DELAY '00:00:00.500';
                    end
                end
                ELSE
                begin
                    THROW;
                end
            END CATCH
        end
    end
END;
GO


