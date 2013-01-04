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

		KeyboardState keyboardState;
		KeyboardState oldKeyboardState;

		GameScreen activeScreen;
		StartScreen startScreen;
		ActionScreen actionScreen;

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
			base.Initialize ();
		}

		/// <summary>
		/// Load your graphics content.
		/// </summary>
		protected override void LoadContent ()
		{
			// Create a new SpriteBatch, which can be use to draw textures.
			spriteBatch = new SpriteBatch (graphics.GraphicsDevice);

			font = Content.Load<SpriteFont> ("SpriteFont1");
			rectangle = Content.Load<Texture2D> ("WhiteSquare");

			startScreen = new StartScreen (this, spriteBatch, font);
			startScreen.Hide ();
			Components.Add (startScreen);

			actionScreen = new ActionScreen (this, spriteBatch, Content, font, rectangle);
			actionScreen.Hide ();
			Components.Add (actionScreen);

			activeScreen = startScreen;
			activeScreen.Show ();

			base.LoadContent ();
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
			keyboardState = Keyboard.GetState ();

			if (activeScreen == startScreen) {
				if (CheckKey (Keys.Enter)) {
					activeScreen.Hide ();
					actionScreen.NumPlayers = startScreen.SelectedIndex + 1;
					actionScreen.NewGame ();
					activeScreen = actionScreen;
					activeScreen.Show ();
				}
			}

			if (activeScreen == actionScreen) {
				if (CheckKey (Keys.Escape) || (CheckKey (Keys.Space) && actionScreen.GameOver)) {
					activeScreen.Hide ();
					activeScreen = startScreen;
					activeScreen.Show ();
				}
			}

			base.Update (gameTime);

			oldKeyboardState = keyboardState;
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
			for (var y = 5; y <= GraphicsDevice.Viewport.Height - 10; y += 20) {
				spriteBatch.Draw (rectangle, new Vector2 (GraphicsDevice.Viewport.Width / 2 - 2, y),
				                  new Rectangle (0, 0, 4, 10), Color.White);
			}
			base.Draw (gameTime);
				
			spriteBatch.End ();
		}
	#endregion

		bool CheckKey(Keys theKey)
		{
			return keyboardState.IsKeyUp(theKey) && oldKeyboardState.IsKeyDown(theKey);
		}
	}
}