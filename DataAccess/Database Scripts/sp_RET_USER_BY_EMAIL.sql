
CREATE PROCEDURE [dbo].[RET_USER_BY_EMAIL_PR]
    @P_Email nvarchar(30)
AS
BEGIN
    SELECT *
    FROM TBL_User
    WHERE Email = @P_Email
END
GO;
