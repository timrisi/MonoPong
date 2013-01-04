using System;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Pong
{
	public class ActionScreen : GameScreen
	{
		KeyboardState oldKeyboardState;
		KeyboardState keyboardState;
		ContentManager content;
		SpriteFont spriteFont;
		Texture2D whiteSquare;

		Game game;
		
		Paddle paddleOne;
		Paddle paddleTwo;
		Ball ball;

		int playerOneScore = 0;
		int playerTwoScore = 0;
		public int NumPlayers;

		bool newGame = true;
		public bool GameOver = false;

		public ActionScreen (Game game, 
		                     SpriteBatch spriteBatch, 
		                     ContentManager content,
		                     SpriteFont spriteFont, 
		                     Texture2D whiteSquare)
			: base(game, spriteBatch)
		{
			this.game = game;
			this.content = content;
			this.whiteSquare = whiteSquare;
			this.spriteFont = spriteFont;
			NumPlayers = 1;
		}

		public override void Initialize ()
		{
			base.Initialize ();
		}

		protected override void LoadContent ()
		{
			base.LoadContent ();

			paddleOne = new Paddle (game, spriteBatch);
			paddleOne.Direction = new Vector2 (0, 0);
			paddleTwo = new Paddle (game, spriteBatch);
			paddleTwo.Direction = new Vector2 (0, 0);
			ball = new Ball (game, spriteBatch);
			ball.Direction = new Vector2 (0, 0);

			paddleOne.LoadContent (content);
			paddleOne.Position = new Vector2 (20, 20);
			
			paddleTwo.LoadContent (content);
			paddleTwo.Position = new Vector2 (GraphicsDevice.Viewport.Width - 40, GraphicsDevice.Viewport.Height - 100);
			
			ball.LoadContent (content);
			ball.Position = new Vector2 (GraphicsDevice.Viewport.Width + 10, 0);
		}

		public override void Update (GameTime gameTime)
		{
			keyboardState = Keyboard.GetState ();

			base.Update (gameTime);

			if (!GameOver) {
				// TODO: Add your update logic here		
				paddleOne.Update (gameTime);
				paddleTwo.Update (gameTime);
				
				if (ball.Position.X <= 0 && ball.Position.X != -20 && !newGame)
					playerTwoScore++;
				if (ball.Position.X + ball.Size.Width > GraphicsDevice.Viewport.Width && 
					ball.Position.X != GraphicsDevice.Viewport.Width + 10 && !newGame)
					playerOneScore++;
				
				if (playerOneScore == 10 || playerTwoScore == 10) {
					GameOver = true;
					return;
				}
				
				ball.Update (gameTime);
				
				if (keyboardState.IsKeyDown (Keys.W))
					paddleOne.Direction.Y = -1;
				else if (keyboardState.IsKeyDown (Keys.S))
					paddleOne.Direction.Y = 1;
				else
					paddleOne.Direction.Y = 0;

				if (NumPlayers == 2) {
					if (keyboardState.IsKeyDown (Keys.Up))
						paddleTwo.Direction.Y = -1;
					else if (keyboardState.IsKeyDown (Keys.Down))
						paddleTwo.Direction.Y = 1;
					else
						paddleTwo.Direction.Y = 0;
				} else {
					if (paddleTwo.Position.Y + 35 > ball.Position.Y)
						paddleTwo.Direction.Y = -1;
					else
						paddleTwo.Direction.Y = 1;
				}

				if (ball.intersects (paddleOne)) {
					ball.Direction.X = 2;
					ball.Speed = ball.Speed * new Vector2 (1.05f, 1.05f);
				}
				if (ball.intersects (paddleTwo)) {
					ball.Direction.X = -2;
					ball.Speed = ball.Speed * new Vector2 (1.05f, 1.05f);
				}

				if (ball.Position.X == -20 && NumPlayers == 1) {
					ball.Position = new Vector2 (GraphicsDevice.Viewport.Width - 200,
					                             GraphicsDevice.Viewport.Height - 100);
					ball.Direction = new Vector2 (-2, -1);
				}
				
				if (keyboardState.IsKeyDown (Keys.Space)) {
					newGame = false;
					if (ball.Position.X == GraphicsDevice.Viewport.Width + 10) {
						ball.Position = new Vector2 (200, 100);
						ball.Direction = new Vector2 (2, 1);
					} else if (ball.Position.X == -20) {
						ball.Position = new Vector2 (GraphicsDevice.Viewport.Width - 200,
						                             GraphicsDevice.Viewport.Height - 100);
						ball.Direction = new Vector2 (-2, -1);
					}
				}
			}

			oldKeyboardState = keyboardState;
		}

		public override void Draw (GameTime gameTime)
		{
			base.Draw (gameTime);

			paddleOne.Draw (spriteBatch);
			paddleTwo.Draw (spriteBatch);
			ball.Draw (spriteBatch);

			spriteBatch.DrawString (spriteFont, playerOneScore.ToString (), 
			                        new Vector2 (GraphicsDevice.Viewport.Width / 2 - 40, 15), Color.White);
			spriteBatch.DrawString (spriteFont, playerTwoScore.ToString (), 
			                        new Vector2 (GraphicsDevice.Viewport.Width / 2 + 22, 15), Color.White);
			if (!GameOver) {
				if (ball.Position.X == GraphicsDevice.Viewport.Width + 10)
					spriteBatch.DrawString (spriteFont, "Press Spacebar to serve",
					                        new Vector2 (15, 50), Color.White);
				if (ball.Position.X == -20)
					spriteBatch.DrawString (spriteFont, "Press Spacebar to serve",
					                        new Vector2 (GraphicsDevice.Viewport.Width / 2 + 15, 50), Color.White);
			} else {
				spriteBatch.DrawString (spriteFont, "Game Over", new Vector2 (50, 50), Color.White);
				if (playerOneScore == 10)
					spriteBatch.DrawString (spriteFont, "Player One Wins", new Vector2 (50, 100), Color.White);
				else
					spriteBatch.DrawString (spriteFont, "Player Two Wins", new Vector2 (50, 100), Color.White);
				spriteBatch.DrawString (spriteFont, "Spacebar to restart", new Vector2 (50, 150), Color.White);
			}
		}

		bool CheckKey(Keys theKey)
		{
			return keyboardState.IsKeyUp(theKey) && oldKeyboardState.IsKeyDown(theKey);
		}

		public void NewGame ()
		{
			newGame = true;
			GameOver = false;
			playerOneScore = 0;
			playerTwoScore = 0;
			ball.Position = new Vector2 (GraphicsDevice.Viewport.Width + 10, 0);
			ball.Direction = new Vector2 (2, 1);
		}
	}
}

