SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DEL_MOVIE_PR]
    @P_Id INT
AS
BEGIN
    DELETE FROM TBL_Movie
    WHERE Id = @P_Id;
END
GO