CREATE TABLE [dbo].[Match]
(
	[MatchId]		INT				NOT NULL,
	[RoundId]		INT				NOT NULL,
	[HomeTeamId]	INT				NOT NULL,
	[AwayTeamId]	INT				NOT NULL,
	[MatchDate]		DATETIME		NOT NULL,
	[Venue]			VARCHAR(100)	NOT NULL,
	[DateUpdated]	DATETIME		NOT NULL,

	CONSTRAINT [PK_Match_MatchId]		PRIMARY KEY CLUSTERED ([MatchId] ASC),
	CONSTRAINT [FK_Match_HomeTeamId]	FOREIGN KEY ([HomeTeamId]) REFERENCES [dbo].[Team](TeamId),
	CONSTRAINT [FK_Match_AwayTeamId]	FOREIGN KEY ([AwayTeamId]) REFERENCES [dbo].[Team](TeamId)
)
