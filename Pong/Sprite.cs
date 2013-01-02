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
        // The current position of the sprite
        public Vector2 Position;

        // The texture object used when drawing the sprite
        Texture2D spriteTexture;

        //The size of the Sprite
        public Rectangle Size;
 
        //Used to size the Sprite up or down from the original image
		private float scale = 1.0f;
		public float Scale {
			get { return scale; }
			set { 
				scale = value;
				//Recalculate the Size of the Sprite with the new scale
				Size = new Rectangle(0, 0, (int)(Source.Width * Scale), (int)(Source.Height * Scale));
			}
		}

		//The asset name for the Sprite's Texture
		public string AssetName;

		Rectangle source;
		public Rectangle Source {
			get { return source; }
			set {
				source = value;
				Size = new Rectangle(0, 0, (int)(source.Width * Scale), (int)(source.Height * Scale));
			}
		}

		public Sprite (Game game) : base (game)
		{
		}

        //Load the texture for the sprite using the Content Pipeline
        public virtual void LoadContent(ContentManager contentManager, string assetName)
        {
			AssetName = assetName;
            spriteTexture = contentManager.Load<Texture2D>(assetName);
            Size = new Rectangle(0, 0, (int)(spriteTexture.Width * Scale), (int)(spriteTexture.Height * Scale));
			Source = new Rectangle(0, 0, spriteTexture.Width, spriteTexture.Height);
        }

        //Draw the sprite to the screen
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(spriteTexture, Position,
               Size, Color.White,
               0.0f, Vector2.Zero, Scale, SpriteEffects.None, 0);
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
