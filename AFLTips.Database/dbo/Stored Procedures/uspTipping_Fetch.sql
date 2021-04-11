CREATE PROCEDURE [dbo].[uspTipping_Fetch]
	@MatchId INT,
	@PlayerId INT
AS

BEGIN

	SELECT
		MatchId,
		PlayerId,
		TeamId
	FROM [dbo].[Tipping]
	WHERE MatchId = @MatchId
		AND PlayerId = @PlayerId

END