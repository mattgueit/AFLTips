CREATE PROCEDURE [dbo].[uspPlayer_Fetch]
	@PlayerId INT
AS

BEGIN

	SELECT
		PlayerId,
		PlayerName
	FROM [dbo].[Player]
	WHERE PlayerId = @PlayerId

END