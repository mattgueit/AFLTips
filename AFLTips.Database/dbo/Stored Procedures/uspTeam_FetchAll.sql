CREATE PROCEDURE [dbo].[uspTeam_FetchAll]
AS
BEGIN

	SELECT
		TeamId,
		TeamAbbv,
		TeamName
	FROM dbo.Team

END