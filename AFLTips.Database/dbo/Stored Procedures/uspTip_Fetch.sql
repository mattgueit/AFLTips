CREATE PROCEDURE [dbo].[uspTip_Fetch]
	@MatchId INT,
	@PlayerId INT
AS

BEGIN

	SELECT
		MatchId,
		PlayerId,
		TeamId
	FROM [dbo].[Tip]
	WHERE MatchId = @MatchId
		AND PlayerId = @PlayerId

END