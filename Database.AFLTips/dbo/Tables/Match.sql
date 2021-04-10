CREATE TABLE [dbo].[Match]
(
	[MatchId]		INT				NOT NULL,
	[RoundId]		INT				NOT NULL,
	[HomeTeamId]	INT				NOT NULL,
	[AwayTeamId]	INT				NOT NULL,
	[MatchDate]		DATETIME		NOT NULL,
	[Venue]			VARCHAR(100)	NOT NULL,

	CONSTRAINT [PK_MatchId] PRIMARY KEY CLUSTERED ([MatchId] ASC)
)
