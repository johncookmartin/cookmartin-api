CREATE PROCEDURE [budget].[stp_CreateRecurringItem]
    @CollectionId    INT,
    @UserId          NVARCHAR(450),
    @Label           NVARCHAR(255),
    @Amount          DECIMAL(18, 2),
    @Type            NVARCHAR(100),
    @IsShared        BIT,
    @RecurringItemId INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO [budget].[RecurringItems]
        ([CollectionId], [UserId], [Label], [Amount], [Type], [IsShared], [CreatedDate], [IsDeleted])
    VALUES
        (@CollectionId, @UserId, @Label, @Amount, @Type, @IsShared, GETUTCDATE(), 0);
    SET @RecurringItemId = SCOPE_IDENTITY();
    SELECT @RecurringItemId AS RecurringItemId;
END
