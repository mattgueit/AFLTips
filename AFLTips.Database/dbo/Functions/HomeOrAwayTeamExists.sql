CREATE FUNCTION [dbo].[HomeOrAwayTeamExists]
(
	@MatchId INT,
	@TeamId INT
)
RETURNS BIT
AS
BEGIN
	IF EXISTS (
		SELECT 0 FROM 
		dbo.[Match]
		WHERE MatchId = @MatchId
			AND (HomeTeamId = @TeamId OR AwayTeamId = @TeamId)
	)
	BEGIN
		RETURN 1
	END

	RETURN 0
END
