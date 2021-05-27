CREATE PROCEDURE [dbo].[uspMatch_FetchAll]
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

END