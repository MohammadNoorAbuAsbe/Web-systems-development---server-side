-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE SP_InsertMovies_2025
	-- Add the parameters for the stored procedure here
	@Movies dbo.MovieListType READONLY
AS
BEGIN
    --SET NOCOUNT ON;
    
    BEGIN TRY
        BEGIN TRANSACTION;
        
        INSERT INTO Movies_2025 (
            [url],
            primaryTitle,
            [description],
            primaryImage,
            [year],
            releaseDate,
            [language],
            budget,
            grossWorldwide,
            genres,
            isAdult,
            runtimeMinutes,
            averageRating,
            numVotes
        )
        SELECT 
            [url],
            primaryTitle,
            [description],
            primaryImage,
            [year],
            releaseDate,
            [language],
            budget,
            ISNULL(grossWorldwide, 0),
            genres,
            ISNULL(isAdult, 0),
            runtimeMinutes,
            ISNULL(averageRating, 0),
            ISNULL(numVotes, 0)
        FROM @Movies;
        
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO
