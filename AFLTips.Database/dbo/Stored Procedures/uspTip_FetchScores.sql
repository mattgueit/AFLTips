CREATE PROCEDURE [dbo].[uspTip_FetchScores]
	@Year INT
AS

BEGIN

	SELECT
	P.PlayerName,
	[PlayerScore] = SUM(
		CASE WHEN T.TeamId = M.WinnerTeamId
		THEN
			1
		ELSE
			0
		END
	)
	FROM dbo.[Tip] T
	INNER JOIN dbo.[Match] M
		ON M.MatchId = T.MatchId
	INNER JOIN dbo.[Player] P
		ON T.PlayerId = P.PlayerId
	WHERE DATEPART(YEAR, M.MatchDate) = @Year
	GROUP BY P.PlayerName

END