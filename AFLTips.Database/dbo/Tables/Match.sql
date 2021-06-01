CREATE TABLE [dbo].[Match]
(
	[MatchId]		INT																NOT NULL,
	[RoundId]		INT																NOT NULL,
	[HomeTeamId]	INT																NOT NULL,
	[AwayTeamId]	INT																NOT NULL,
	[MatchDate]		DATETIME2														NOT NULL,
	[Venue]			VARCHAR(100)													NOT NULL,
	[Completed]		BIT																NOT NULL,
	[WinnerTeamId]	INT																NULL,
	[HomeGoals]		INT																NULL,
	[HomeBehinds]	INT																NULL,
	[HomeScore]		INT																NULL,
	[AwayGoals]		INT																NULL,
	[AwayBehinds]	INT																NULL,
	[AwayScore]		INT																NULL,
	[DateUpdated]	DATETIME2 CONSTRAINT [DF_Match_DateUpdated] DEFAULT GETDATE()	NOT NULL,

	CONSTRAINT [PK_Match_MatchId]		PRIMARY KEY CLUSTERED ([MatchId] ASC),
	CONSTRAINT [FK_Match_HomeTeamId]	FOREIGN KEY ([HomeTeamId]) REFERENCES [dbo].[Team](TeamId),
	CONSTRAINT [FK_Match_AwayTeamId]	FOREIGN KEY ([AwayTeamId]) REFERENCES [dbo].[Team](TeamId),
)
