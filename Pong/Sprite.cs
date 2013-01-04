using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Pong
{
    public class Sprite : DrawableGameComponent
    {
        public Vector2 Position;
		public Rectangle Size;
		
		SpriteBatch spriteBatch;
		Texture2D spriteTexture;

		//The asset name for the Sprite's Texture
		public string AssetName;

		public Sprite (Game game, SpriteBatch spriteBatch) : base (game)
		{
		}

        //Load the texture for the sprite using the Content Pipeline
        public virtual void LoadContent(ContentManager contentManager, string assetName)
        {
			AssetName = assetName;
            spriteTexture = contentManager.Load<Texture2D>(assetName);
        }

        //Draw the sprite to the screen
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(spriteTexture, Position,
               Size, Color.White);
        }

		//Update the Sprite and change it's position based on the passed in speed, direction and elapsed time.
		public virtual void Update(GameTime gameTime, Vector2 speed, Vector2 direction)
		{
			Position += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
		}

		public bool intersects (Sprite sprite)
		{
			if (Position.X > sprite.Position.X &&
			    Position.X < sprite.Position.X + sprite.Size.Width &&
			    Position.Y > sprite.Position.Y &&
			    Position.Y < sprite.Position.Y + sprite.Size.Height)
				return true;

			if (Position.X + Size.Width > sprite.Position.X &&
			    Position.X + Size.Width < sprite.Position.X + sprite.Size.Width &&
			    Position.Y > sprite.Position.Y &&
			    Position.Y < sprite.Position.Y + sprite.Size.Height)
				return true;

			if (Position.X > sprite.Position.X &&
			    Position.X < sprite.Position.X + sprite.Size.Width &&
			    Position.Y + Size.Height > sprite.Position.Y &&
			    Position.Y + Size.Height < sprite.Position.Y + sprite.Size.Height)
				return true;

			if (Position.X + Size.Width > sprite.Position.X &&
			    Position.X + Size.Width < sprite.Position.X + sprite.Size.Width &&
			    Position.Y + Size.Height > sprite.Position.Y &&
			    Position.Y + Size.Height < sprite.Position.Y + sprite.Size.Height)
				return true;

			return false;
		}
    }
}
