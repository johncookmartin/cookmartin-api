CREATE PROCEDURE [oscar].[stp_SetWinner]
    @NomineeId INT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @CategoryId INT;
    SELECT @CategoryId = [CategoryId] FROM [oscar].[Nominees] WHERE [NomineeId] = @NomineeId;

    IF @CategoryId IS NULL
        THROW 50000, 'Nominee not found', 1;

    UPDATE [oscar].[Nominees]
    SET [IsWinner] = 0, [WonDate] = NULL
    WHERE [CategoryId] = @CategoryId AND [IsWinner] = 1;

    UPDATE [oscar].[Nominees]
    SET [IsWinner] = 1, [WonDate] = GETUTCDATE()
    WHERE [NomineeId] = @NomineeId;
END
