CREATE PROCEDURE [dbo].[uspMatch_Fetch]
	@MatchId INT
AS

BEGIN

	SELECT
		MatchId,
		RoundId
		HomeTeamId,
		AwayTeamId,
		MatchDate,
		Venue
	FROM [dbo].[Match]
	WHERE MatchId = @MatchId

END