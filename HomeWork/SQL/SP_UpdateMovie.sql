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
CREATE PROCEDURE SP_UpdateMovie_2025 
	-- Add the parameters for the stored procedure here
	@id int,
	@url nvarchar(max),
    @primaryTitle nvarchar(255),
    @description nvarchar(max),
    @primaryImage nvarchar(max),
    @year int,
    @releaseDate date,
    @language nvarchar(50),
    @budget decimal(18, 2),
    @grossWorldwide decimal(18, 2),
    @genres nvarchar(255),
    @isAdult bit,
    @runtimeMinutes int,
    @averageRating decimal(3, 1),
    @numVotes int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	-- SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE Movies_2025
SET [url] = @url, primaryTitle = @primaryTitle, [description]= @description,
	primaryImage = @primaryImage,
	[year] = @year, releaseDate = @releaseDate ,
	[language] = @language , budget = @budget, grossWorldwide = @grossWorldwide , genres = @genres,
	isAdult = @isAdult
	,runtimeMinutes = @runtimeMinutes, averageRating= @averageRating, numVotes = @numVotes
	
WHERE id = @id;
END
GO
