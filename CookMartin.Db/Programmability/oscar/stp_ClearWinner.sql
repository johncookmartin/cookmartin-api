CREATE PROCEDURE [oscar].[stp_ClearWinner]
    @NomineeId INT
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM [oscar].[Nominees] WHERE [NomineeId] = @NomineeId)
        THROW 50000, 'Nominee not found', 1;

    UPDATE [oscar].[Nominees]
    SET [IsWinner] = 0, [WonDate] = NULL
    WHERE [NomineeId] = @NomineeId;
END
