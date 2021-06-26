CREATE PROCEDURE [dbo].[uspMatch_FetchByRound]
	@RoundId INT
AS

BEGIN

	SELECT
		MatchId,
		RoundId,
		HomeTeamId,
		AwayTeamId,
		MatchDate,
		Venue,
		Completed,
		WinnerTeamId,
		HomeGoals,
		HomeBehinds,
		HomeScore,
		AwayGoals,
		AwayBehinds,
		AwayScore,
		DateUpdated
	FROM [dbo].[Match]
	WHERE RoundId = @RoundId

END