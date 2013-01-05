using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Pong
{
	public class Ball : Sprite
	{
		public Vector2 Speed;
		public Vector2 Direction;
		
		public Ball (Game game, SpriteBatch spriteBatch) : base (game, spriteBatch)
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
				Direction.Y = -Direction.Y;
			if (Position.Y + Size.Height >= GraphicsDevice.Viewport.Height)
				Direction.Y = -Direction.Y;
			if (Position.X <= 0 && Position.X != -20) {
				Direction = new Vector2 (0, 0);
				Position = new Vector2 (-20, 0);
				Speed = new Vector2 (100, 100);
			}
 			if (Position.X + Size.Width >= GraphicsDevice.Viewport.Width && 
			    Position.X != GraphicsDevice.Viewport.Width + 10) {
				Direction = new Vector2 (0, 0);
				Position = new Vector2 (GraphicsDevice.Viewport.Width + 10, 0);
				Speed = new Vector2 (100, 100);
			}
			base.Update (gameTime, Speed, Direction);
		}
	}
}

