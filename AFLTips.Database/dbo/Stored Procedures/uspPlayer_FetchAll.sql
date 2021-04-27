CREATE PROCEDURE [dbo].[uspPlayer_FetchAll]
AS

BEGIN

	SELECT
		PlayerId,
		PlayerName
	FROM [dbo].[Player]

END