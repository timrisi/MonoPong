using System;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pong
{
	public class Paddle : Sprite
	{
		Vector2 speed;
		public Vector2 Direction;

		public Paddle (Game game, SpriteBatch spriteBatch) : base (game, spriteBatch)
		{
			Position = new Vector2 (0, 0);
			speed = new Vector2 (0, 150);
		}

		public void LoadContent (ContentManager contentManager)
		{
			base.LoadContent (contentManager, "WhiteSquare");
			Size = new Rectangle ((int) Position.X, (int) Position.Y, 20, 80);
		}

		public override void Update (GameTime gameTime)
		{
			if (Position.Y <= 0)
				Direction = new Vector2 (0, 1);
			if (Position.Y + Size.Height >= GraphicsDevice.Viewport.Height)
				Direction = new Vector2 (0, -1);
			base.Update (gameTime, speed, Direction);
		}
	}
}

