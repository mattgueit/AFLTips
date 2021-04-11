CREATE TABLE [dbo].[Player]
(
	[PlayerId]		INT			NOT NULL IDENTITY(1,1),
	[PlayerName]	VARCHAR(50) NOT NULL,

	CONSTRAINT [PK_Player_PlayerId] PRIMARY KEY CLUSTERED ([PlayerId] ASC)
)
