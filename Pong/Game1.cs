#region File Description
//-----------------------------------------------------------------------------
// PongGame.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

#endregion

namespace Pong
{
	/// <summary>
	/// Default Project Template
	/// </summary>
	public class Game1 : Game
	{

	#region Fields
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;
		SpriteFont font;
		Paddle paddleOne;
		Paddle paddleTwo;
		Ball ball;
		Texture2D rectangle;
	#endregion

	#region Initialization

		public Game1 ()
		{
			graphics = new GraphicsDeviceManager (this);
			
			Content.RootDirectory = "Content";

			graphics.IsFullScreen = false;
		}

		/// <summary>
		/// Overridden from the base Game.Initialize. Once the GraphicsDevice is setup,
		/// we'll use the viewport to initialize some values.
		/// </summary>
		protected override void Initialize ()
		{
			paddleOne = new Paddle (this);
			paddleOne.Direction = new Vector2 (0, 0);
			paddleTwo = new Paddle (this);
			paddleTwo.Direction = new Vector2 (0, 0);
			ball = new Ball (this);
			ball.Direction = new Vector2 (-2, 1);
			base.Initialize ();
		}


		/// <summary>
		/// Load your graphics content.
		/// </summary>
		protected override void LoadContent ()
		{
			// Create a new SpriteBatch, which can be use to draw textures.
			spriteBatch = new SpriteBatch (graphics.GraphicsDevice);

			rectangle = Content.Load<Texture2D> ("WhiteSquare");
			paddleOne.LoadContent (Content);
			paddleOne.Position = new Vector2 (20, 20);

			paddleTwo.LoadContent (Content);
			paddleTwo.Position = new Vector2 (GraphicsDevice.Viewport.Width - 40, GraphicsDevice.Viewport.Height - 100);

			ball.LoadContent (Content);
			ball.Position = new Vector2 (200, 100);

			font = Content.Load<SpriteFont> ("SpriteFont1");
		}

	#endregion

	#region Update and Draw

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update (GameTime gameTime)
		{
			KeyboardState keyboardState = Keyboard.GetState ();
			// TODO: Add your update logic here		
			paddleOne.Update (gameTime);
			paddleTwo.Update (gameTime);
			ball.Update (gameTime);

			if (keyboardState.IsKeyDown (Keys.W))
				paddleOne.Direction.Y = -1;
			else if (keyboardState.IsKeyDown (Keys.S))
				paddleOne.Direction.Y = 1;
			else
				paddleOne.Direction.Y = 0;
			/*if (ball.Position.Y - 30 > paddleOne.Position.Y)
				paddleOne.Direction.Y = 1;
			else
				paddleOne.Direction.Y = -1;*/

			if (keyboardState.IsKeyDown (Keys.Up))
			    paddleTwo.Direction.Y = -1;
			else if (keyboardState.IsKeyDown (Keys.Down))
				paddleTwo.Direction.Y = 1;
			else
				paddleTwo.Direction.Y = 0;
			/*if (ball.Position.Y - 30 > paddleTwo.Position.Y)
				paddleTwo.Direction.Y = 1;
			else
				paddleTwo.Direction.Y = -1;*/
			
			if (ball.intersects (paddleOne)) {
				ball.Direction.X = 2;
				ball.Speed = ball.Speed * new Vector2 (1.05f, 1.05f);
			}
			if (ball.intersects (paddleTwo)) {
				ball.Direction.X = -2;
				ball.Speed = ball.Speed * new Vector2 (1.05f, 1.05f);
			}

			base.Update (gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself. 
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw (GameTime gameTime)
		{
			// Clear the backbuffer
			graphics.GraphicsDevice.Clear (Color.Black);

			spriteBatch.Begin ();
			/*spriteBatch.Draw (rectangle, new Vector2 (20, 20), new Rectangle (0, 0, 20, 80), Color.White);
			spriteBatch.Draw (rectangle, 
			                  new Vector2 (GraphicsDevice.Viewport.Width - 40,
			                               GraphicsDevice.Viewport.Height - 100),
			                  new Rectangle (0, 0, 20, 80),
			                  Color.White);*/
			paddleOne.Draw (spriteBatch);
			paddleTwo.Draw (spriteBatch);
			ball.Draw (spriteBatch);

			for (var y = 5; y <= GraphicsDevice.Viewport.Height - 10; y += 20)
				spriteBatch.Draw (rectangle, new Vector2 (GraphicsDevice.Viewport.Width / 2 - 2, y),
				                  new Rectangle (0, 0, 4, 10), Color.White);
			spriteBatch.DrawString (font, "0", new Vector2 (GraphicsDevice.Viewport.Width / 2 - 40, 15), Color.White);
			spriteBatch.DrawString (font, "0", new Vector2 (GraphicsDevice.Viewport.Width / 2 + 22, 15), Color.White);
			spriteBatch.End ();
			//TODO: Add your drawing code here
			base.Draw (gameTime);
		}

	#endregion
	}
}
