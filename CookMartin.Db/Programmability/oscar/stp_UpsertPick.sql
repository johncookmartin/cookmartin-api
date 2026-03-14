CREATE PROCEDURE [oscar].[stp_UpsertPick]
    @UserName  NVARCHAR(200),
    @NomineeId INT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @CategoryId INT;
    SELECT @CategoryId = [CategoryId] FROM [oscar].[Nominees] WHERE [NomineeId] = @NomineeId;

    IF EXISTS (SELECT 1 FROM [oscar].[Picks] WHERE [UserName] = @UserName AND [CategoryId] = @CategoryId)
        UPDATE [oscar].[Picks]
        SET [NomineeId] = @NomineeId, [PickedDate] = GETUTCDATE()
        WHERE [UserName] = @UserName AND [CategoryId] = @CategoryId;
    ELSE
        INSERT INTO [oscar].[Picks] ([UserName], [CategoryId], [NomineeId])
        VALUES (@UserName, @CategoryId, @NomineeId);
END
