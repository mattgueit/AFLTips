CREATE TYPE [dbo].[udtMatchTableType] AS TABLE
(
	[MatchId]		INT				NOT NULL,
	[RoundId]		INT				NOT NULL,
	[HomeTeamId]	INT				NOT NULL,
	[AwayTeamId]	INT				NOT NULL,
	[MatchDate]		DATETIME		NOT NULL,
	[Venue]			VARCHAR(100)	NOT NULL,
	[DateUpdated]	DATETIME		NOT NULL
);