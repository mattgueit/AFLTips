CREATE TABLE [dbo].[Tipping]
(
	[MatchId]	INT NOT NULL,
	[PlayerId]	INT NOT NULL,
	[TeamId]	INT NOT NULL,

	CONSTRAINT [FK_Tipping_MatchId]		FOREIGN KEY ([MatchId])		REFERENCES [dbo].[Match](MatchId),
	CONSTRAINT [FK_Tipping_PlayerId]	FOREIGN KEY ([PlayerId])	REFERENCES [dbo].[Player](PlayerId),
	CONSTRAINT [FK_Tipping_TeamId]		FOREIGN KEY ([TeamId])		REFERENCES [dbo].[Team] (TeamId)
)
