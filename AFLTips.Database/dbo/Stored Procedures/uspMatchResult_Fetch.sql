CREATE PROCEDURE [dbo].[uspMatchResult_Fetch]
	@MatchId INT
AS

BEGIN

	SELECT
		MatchId,
		WinnerTeamId,
		HomeGoals,
		HomeBehinds,
		HomeScore,
		AwayGoals,
		AwayBehinds,
		AwayScore
	FROM [dbo].[MatchResult]
	WHERE MatchId = @MatchId

END