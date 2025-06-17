SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- SP para actualizar un usuario por Id, incluyendo UserCode
CREATE PROCEDURE [dbo].[UPD_USER_PR]
    @P_Id         INT,
    @P_UserCode   NVARCHAR(30),
    @P_Name       NVARCHAR(50),
    @P_Email      NVARCHAR(30),
    @P_Password   NVARCHAR(50),
    @P_BirthDate  DATETIME,
    @P_Status     NVARCHAR(10)
AS
BEGIN
    UPDATE TBL_User
    SET 
        UserCode  = @P_UserCode,
        Name      = @P_Name,
        Email     = @P_Email,
        Password  = @P_Password,
        BirthDate = @P_BirthDate,
        Status    = @P_Status,
        Updated  = GETDATE()
    WHERE Id = @P_Id;
END
GO