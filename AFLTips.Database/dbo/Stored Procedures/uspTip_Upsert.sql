CREATE PROCEDURE [dbo].[uspTip_Upsert]
	@Tips [dbo].[udtTipTableType] READONLY
AS

BEGIN
	BEGIN TRY
		BEGIN TRANSACTION
			MERGE INTO [dbo].[Tip] AS TARGET
			USING (SELECT
					[MatchId],
					[PlayerId],
					[TeamId]
				FROM @Tips
			) AS Source ([MatchId], [PlayerId], [TeamId])
			ON Target.MatchId = Source.MatchId AND Target.PlayerId = Source.PlayerId

			WHEN MATCHED
			AND Target.TeamId <> Source.TeamId
			THEN
			UPDATE SET
				TeamId = Source.TeamId

			WHEN NOT MATCHED BY TARGET THEN
			INSERT ([MatchId], [PlayerId], [TeamId])
			VALUES ([MatchId], [PlayerId], [TeamId]);

		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION;

		DECLARE @ErrMsg VARCHAR(100) = ERROR_MESSAGE();
		RAISERROR(@ErrMsg, 16, 1);
	END CATCH

END