SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- SP para actualizar una película por Id
CREATE PROCEDURE [dbo].[UPD_MOVIE_PR]
    @P_Id           INT,
    @P_Title        NVARCHAR(75),
    @P_Description  NVARCHAR(225),
    @P_ReleaseDate  DATETIME,
    @P_Genre        NVARCHAR(20),
    @P_Director     NVARCHAR(30)
AS
BEGIN
    UPDATE TBL_Movie
    SET
        Title       = @P_Title,
        Description = @P_Description,
        ReleaseDate = @P_ReleaseDate,
        Genre       = @P_Genre,
        Director    = @P_Director,
        Updated     = GETDATE()
    WHERE Id = @P_Id;
END
GO