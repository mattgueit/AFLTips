CREATE PROCEDURE [dbo].[uspPlayer_Insert]
	@PlayerName INT
AS

BEGIN
	
	IF NOT EXISTS(SELECT PlayerId FROM [dbo].[Player] WHERE PlayerName = @PlayerName)
	BEGIN
		INSERT INTO [dbo].[Player]
		(PlayerName)
		VALUES
		(@PlayerName)
	END

END