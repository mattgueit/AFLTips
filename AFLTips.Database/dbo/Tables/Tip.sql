CREATE TABLE [dbo].[Tip]
(
	[MatchId]	INT NOT NULL,
	[PlayerId]	INT NOT NULL,
	[TeamId]	INT NOT NULL,

	CONSTRAINT [FK_Tip_MatchId]		FOREIGN KEY ([MatchId])		REFERENCES [dbo].[Match](MatchId),
	CONSTRAINT [FK_Tip_PlayerId]	FOREIGN KEY ([PlayerId])	REFERENCES [dbo].[Player](PlayerId),
	CONSTRAINT [FK_Tip_TeamId]		FOREIGN KEY ([TeamId])		REFERENCES [dbo].[Team] (TeamId)
)
