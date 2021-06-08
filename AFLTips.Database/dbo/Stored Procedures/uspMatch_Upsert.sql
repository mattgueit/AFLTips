CREATE PROCEDURE [dbo].[uspMatch_Upsert]
		@Matches [dbo].[udtMatchTableType] READONLY
AS

BEGIN
	BEGIN TRY
		BEGIN TRANSACTION
			MERGE INTO [dbo].[Match] AS TARGET
			USING (SELECT
					[MatchId],
					[RoundId],
					[HomeTeamId],
					[AwayTeamId],
					[MatchDate],
					[Venue],
					[Completed],
					[WinnerTeamId],
					[HomeGoals],
					[HomeBehinds],
					[HomeScore],
					[AwayGoals],
					[AwayBehinds],
					[AwayScore],
					[DateUpdated]
				FROM @Matches
			) AS Source ([MatchId], [RoundId], [HomeTeamId], [AwayTeamId], [MatchDate], [Venue], [Completed], [WinnerTeamId], [HomeGoals], [HomeBehinds], [HomeScore], [AwayGoals], [AwayBehinds], [AwayScore], [DateUpdated])
			ON Target.MatchId = Source.MatchId

			WHEN MATCHED
			AND 
			(
				Target.RoundId <> Source.RoundId
				OR Target.HomeTeamId <> Source.HomeTeamId
				OR Target.AwayTeamId <> Source.AwayTeamId
				OR Target.MatchDate <> Source.MatchDate
				OR Target.Venue <> Source.Venue
				OR Target.Completed <> Source.Completed
				OR Target.WinnerTeamId <> Source.WinnerTeamId
				OR Target.HomeGoals <> Source.HomeGoals
				OR Target.HomeBehinds <> Source.HomeBehinds
				OR Target.HomeScore <> Source.HomeScore
				OR Target.AwayGoals <> Source.AwayGoals
				OR Target.AwayBehinds <> Source.AwayBehinds
				OR Target.AwayScore <> Source.AwayScore
				OR Target.DateUpdated <> Source.DateUpdated
			)
			THEN
			UPDATE SET
				RoundId = Source.RoundId,
				HomeTeamId = Source.HomeTeamId,
				AwayTeamId = Source.AwayTeamId,
				MatchDate = Source.MatchDate,
				Venue = Source.Venue,
				Completed = Source.Completed,
				WinnerTeamId = Source.WinnerTeamId,
				HomeGoals = Source.HomeGoals,
				HomeBehinds = Source.HomeBehinds,
				HomeScore = Source.HomeScore,
				AwayGoals = Source.AwayGoals,
				AwayBehinds = Source.AwayBehinds,
				AwayScore = Source.AwayScore,
				DateUpdated = Source.DateUpdated

			WHEN NOT MATCHED BY TARGET THEN
			INSERT ([MatchId], [RoundId], [HomeTeamId], [AwayTeamId], [MatchDate], [Venue], [Completed], [WinnerTeamId], [HomeGoals], [HomeBehinds], [HomeScore], [AwayGoals], [AwayBehinds], [AwayScore], [DateUpdated])
			VALUES ([MatchId], [RoundId], [HomeTeamId], [AwayTeamId], [MatchDate], [Venue], [Completed], [WinnerTeamId], [HomeGoals], [HomeBehinds], [HomeScore], [AwayGoals], [AwayBehinds], [AwayScore], [DateUpdated])

			WHEN NOT MATCHED BY SOURCE THEN
			DELETE;

		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION;

		DECLARE @ErrMsg VARCHAR(100) = ERROR_MESSAGE();
		RAISERROR(@ErrMsg, 16, 1);
	END CATCH

END