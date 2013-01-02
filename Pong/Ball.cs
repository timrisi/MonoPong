using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Pong
{
	public class Ball : Sprite
	{
		public Vector2 Speed;
		public Vector2 Direction;
		
		public Ball (Game game) : base (game)
		{
			Speed = new Vector2 (100, 100);
		}
		
		public void LoadContent (ContentManager contentManager)
		{
			base.LoadContent (contentManager, "WhiteSquare");
			Size = new Rectangle (0, 0, 10, 10);
		}
		
		public override void Update (GameTime gameTime)
		{
			if (Position.Y <= 0)
				Direction.Y = 1;
			if (Position.Y + Size.Height >= GraphicsDevice.Viewport.Height)
				Direction.Y = -1;
			if (Position.X <= 0) {
				Direction.X = 2;
				Speed = new Vector2 (100, 100);
			}
			if (Position.X + Size.Width >= GraphicsDevice.Viewport.Width) {
				Direction.X = -2;
				Speed = new Vector2 (100, 100);
			}
			base.Update (gameTime, Speed, Direction);
		}
	}
}
