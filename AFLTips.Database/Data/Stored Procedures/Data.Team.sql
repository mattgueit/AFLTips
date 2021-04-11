CREATE PROCEDURE [Data].[Team]

AS

BEGIN

MERGE INTO [dbo].[Team] AS TARGET
USING (VALUES
(1, 'ADE', 'Adelaide'),
(2, 'BRI', 'Brisbane Lions'),
(3, 'CAR', 'Carlton'),
(4, 'COL', 'Collingwood'),
(5, 'ESS', 'Essendon'),
(6, 'FRE', 'Fremantle'),
(7, 'GEE', 'Geelong'),
(8, 'GC', 'Gold Coast'),
(9, 'GWS', 'Greater Western Sydney'),
(10, 'HAW', 'Hawthorn'),
(11, 'MEL', 'Melbourne'),
(12, 'NOR', 'North Melbourne'),
(13, 'POR', 'Port Adelaide'),
(14, 'RIC', 'Richmond'),
(15, 'STK', 'St Kilda'),
(16, 'SYD', 'Sydney'),
(17, 'WCE', 'West Coast'),
(18, 'WBD', 'Western Bulldogs')
)
AS Source (TeamId, TeamAbbv, TeamName)
ON Target.TeamId = Source.TeamId

WHEN MATCHED 
AND (Target.TeamName <> Source.TeamName OR Target.TeamAbbv <> Source.TeamAbbv)
THEN
UPDATE SET 
	TeamName = Source.TeamName,
	TeamAbbv = Source.TeamAbbv


WHEN NOT MATCHED BY TARGET THEN
INSERT (TeamId, TeamAbbv, TeamName)
VALUES (TeamId, TeamAbbv, TeamName)

WHEN NOT MATCHED BY SOURCE THEN
DELETE;

END