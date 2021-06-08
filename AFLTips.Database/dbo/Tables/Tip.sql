CREATE TABLE [dbo].[Tip]
(
	[MatchId]	INT NOT NULL,
	[PlayerId]	INT NOT NULL,
	[TeamId]	INT NOT NULL,

	CONSTRAINT [FK_Tip_MatchId_TeamId]		FOREIGN KEY ([MatchId])		REFERENCES [dbo].[Match](MatchId),
	CONSTRAINT [FK_Tip_PlayerId]			FOREIGN KEY ([PlayerId])	REFERENCES [dbo].[Player](PlayerId),
	CONSTRAINT [UQ_Tip_MatchId_PlayerId]	UNIQUE						(MatchId, PlayerId),
	CONSTRAINT [CH_Tip_MatchId_TeamId]		CHECK						(dbo.HomeOrAwayTeamExists(MatchId, TeamId) = 1)
)
