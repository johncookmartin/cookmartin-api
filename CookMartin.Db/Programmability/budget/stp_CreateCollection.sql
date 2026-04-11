CREATE PROCEDURE [budget].[stp_CreateCollection]
    @OwnerId       NVARCHAR(450),
    @Name          NVARCHAR(255),
    @EmergencyFund DECIMAL(18, 2),
    @CollectionId  INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO [budget].[Collections] ([OwnerId], [Name], [EmergencyFund], [CreatedDate], [IsDeleted])
    VALUES (@OwnerId, @Name, @EmergencyFund, GETUTCDATE(), 0);
    SET @CollectionId = SCOPE_IDENTITY();
    SELECT @CollectionId AS CollectionId;
END
