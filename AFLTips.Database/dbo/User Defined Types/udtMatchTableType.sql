CREATE TYPE [dbo].[udtMatchTableType] AS TABLE
(
	[MatchId]		INT				NOT NULL,
	[RoundId]		INT				NOT NULL,
	[HomeTeamId]	INT				NULL,
	[AwayTeamId]	INT				NULL,
	[MatchDate]		DATETIME2		NOT NULL,
	[Venue]			VARCHAR(100)	NOT NULL,
	[Completed]		BIT				NOT NULL,
	[WinnerTeamId]	INT				NULL,
	[HomeGoals]		INT				NULL,
	[HomeBehinds]	INT				NULL,
	[HomeScore]		INT				NULL,
	[AwayGoals]		INT				NULL,
	[AwayBehinds]	INT				NULL,
	[AwayScore]		INT				NULL,
	[DateUpdated]	DATETIME2		NOT NULL
);