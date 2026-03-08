CREATE PROCEDURE [note].[stp_GetNotecardsByQuizId]
	@QuizId int
AS
BEGIN
	SET NOCOUNT ON;

	SELECT 
		n.[NotecardId],
		n.[CollectionId],
		q.[QuizInstanceId],
		n.[FrontDescription],
		n.[BackDescription],
		n.[CreatedDate],
		n.[UpdatedDate],
		n.[IsDeleted]
	FROM [note].[Notecards] n
	INNER JOIN [note].[QuizInstances] q ON n.[NotecardId] = q.[NotecardId]
	WHERE q.[QuizId] = @QuizId AND n.[IsDeleted] = 0 AND q.[AnsweredDate] IS NULL; 

END
