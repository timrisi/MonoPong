using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace Pong
{
	public class Menu : DrawableGameComponent
	{
		string[] menuItems;
		int selectedIndex;

		Color normal = Color.White;
		Color selected = Color.Yellow;

		KeyboardState keyboardState;
		KeyboardState oldKeyboardState;

		SpriteBatch spriteBatch;
		SpriteFont spriteFont;

		Vector2 position;
		float width = 0f;
		float height = 0f;

		public int SelectedIndex {
			get { return selectedIndex; }
			set {
				selectedIndex = value;
				if (selectedIndex < 0)
					selectedIndex = 1;
				if (selectedIndex > 1)
					selectedIndex = 0;
			}
		}

		public Menu (Game game, SpriteBatch spriteBatch, SpriteFont spriteFont, string[] menuItems) : base (game)
		{
			this.spriteBatch = spriteBatch;
			this.spriteFont = spriteFont;
			this.menuItems = menuItems;
			measureMenu ();
		}

		void measureMenu ()
		{
			width = 0f;
			height = 0f;

			for (var i = 0; i < menuItems.Length; i += 2) {
				Vector2 size1 = spriteFont.MeasureString (menuItems [i]);
				Vector2 size2 = spriteFont.MeasureString (menuItems [i + 1]);

				if (size1.X + 100 + size2.X > width)
					width = size1.X + 100 + size2.X;

				height += spriteFont.LineSpacing;
				if (i > 0)
					height += 5;
			}

			position = new Vector2 ((Game.GraphicsDevice.Viewport.Width - width) / 2,
			                        (Game.GraphicsDevice.Viewport.Height - height) / 2);
		}

		bool CheckKey(Keys theKey)
		{
			return keyboardState.IsKeyUp(theKey) && oldKeyboardState.IsKeyDown(theKey);
		}


		public override void Initialize ()
		{
			base.Initialize ();
		}

		public override void Update (GameTime gameTime)
		{
			keyboardState = Keyboard.GetState ();

			if (CheckKey (Keys.Right))
				SelectedIndex++;
			if (CheckKey (Keys.Left))
				SelectedIndex--;

			base.Update (gameTime);

			oldKeyboardState = keyboardState;
		}

		public override void Draw (GameTime gameTime)
		{
			base.Draw (gameTime);
			Vector2 location = position;
			Color color;

			for (var i = 0; i < menuItems.Length; i += 2) {
				if (i == selectedIndex)
					color = selected;
				else
					color = normal;

				spriteBatch.DrawString (spriteFont, menuItems [i], location, color);

				if (i + 1 == selectedIndex)
					color = selected;
				else
					color = normal;

				location.X += width - spriteFont.MeasureString (menuItems [i + 1]).X;

				spriteBatch.DrawString (spriteFont, menuItems [i + 1], location, color);

				location.X = position.X;

				location.Y += spriteFont.LineSpacing + 5;
			}
		}
	}
}

