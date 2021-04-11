CREATE TABLE [dbo].[MatchResult]
(
	[MatchId]		INT		NOT NULL,
	[WinnerTeamId]	INT		NULL,
	[HomeGoals]		INT		NOT NULL,
	[HomeBehinds]	INT		NOT NULL,
	[HomeScore]		INT		NOT NULL,
	[AwayGoals]		INT		NOT NULL,
	[AwayBehinds]	INT		NOT NULL,
	[AwayScore]		INT		NOT NULL,

	CONSTRAINT [FK_MatchResult_MatchId] FOREIGN KEY ([MatchId]) REFERENCES [dbo].[Match](MatchId)
)
