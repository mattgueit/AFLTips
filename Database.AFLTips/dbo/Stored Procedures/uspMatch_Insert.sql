CREATE PROCEDURE [dbo].[uspMatch_Insert]
		@MatchId INT,
		@RoundId INT,
		@HomeTeamId INT,
		@AwayTeamId INT,
		@MatchDate INT,
		@Venue VARCHAR(100)
AS

BEGIN
	
	IF NOT EXISTS (SELECT MatchId FROM [dbo].[Match] WHERE MatchId = @MatchId)
	BEGIN
		INSERT INTO [dbo].[Match]
		(MatchId , RoundId , HomeTeamId , AwayTeamId , MatchDate , Venue )
		VALUES
		(@MatchId, @RoundId, @HomeTeamId, @AwayTeamId, @MatchDate, @Venue)
	END

END