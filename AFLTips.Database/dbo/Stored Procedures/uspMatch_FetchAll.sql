CREATE PROCEDURE [dbo].[uspMatch_FetchAll]
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

END