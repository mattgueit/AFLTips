CREATE PROCEDURE [dbo].[uspMatchResult_Insert]
	@MatchId INT,
	@WinnerTeamId INT,
	@HomeGoals INT,
	@HomeBehinds INT,
	@HomeScore INT,
	@AwayGoals INT,
	@AwayBehinds INT,
	@AwayScore INT
AS

BEGIN
	
	IF NOT EXISTS(SELECT MatchId FROM [dbo].[MatchResult] WHERE MatchId = @MatchId)
	BEGIN
		INSERT INTO [dbo].[MatchResult]
		(MatchId , WinnerTeamId , HomeGoals , HomeBehinds , HomeScore , AwayGoals , AwayBehinds , AwayScore )
		VALUES
		(@MatchId, @WinnerTeamId, @HomeGoals, @HomeBehinds, @HomeScore, @AwayGoals, @AwayBehinds, @AwayScore)
	END

END