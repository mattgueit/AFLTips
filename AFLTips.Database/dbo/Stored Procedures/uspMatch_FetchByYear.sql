CREATE PROCEDURE [dbo].[uspMatch_FetchByYear]
	@Year INT
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
	WHERE YEAR(MatchDate) = @Year
END