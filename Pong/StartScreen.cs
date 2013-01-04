using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Pong
{
	public class StartScreen : GameScreen
	{
		Menu menu;

		public int SelectedIndex {
			get { return menu.SelectedIndex; }
			set { menu.SelectedIndex = value; }
		}

		public StartScreen (Game game, SpriteBatch spriteBatch, SpriteFont spriteFont) : 
			base (game, spriteBatch)
		{
			string [] menuItems = { "One Player", 
			                        "Two Players",
			                        "Up: W",
			                        "Up: Up",
			                        "Down: S",
			                        "Down: Down" };
			menu = new Menu (game, spriteBatch, spriteFont, menuItems);
			Components.Add (menu);
		}

		public override void Update (GameTime gameTime)
		{
			base.Update (gameTime);
		}

		public override void Draw (GameTime gameTime)
		{
			base.Draw (gameTime);
		}
	}
}

