using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace Pong
{
	public abstract class GameScreen : DrawableGameComponent
	{
		List<DrawableGameComponent> components = new List<DrawableGameComponent>();
		protected SpriteBatch spriteBatch;

		public List<DrawableGameComponent> Components {
			get { return components; }
		}

		public GameScreen (Game game, SpriteBatch spriteBatch) : base (game)
		{
			this.spriteBatch = spriteBatch;
		}

		public override void Initialize ()
		{
			base.Initialize ();
		}

		protected override void LoadContent ()
		{
			base.LoadContent ();
		}

		public override void Update (GameTime gameTime)
		{
			base.Update (gameTime);

			foreach (var component in components) {
				if (component.Enabled)
					component.Update (gameTime);
			}
		}

		public override void Draw (GameTime gameTime)
		{
			base.Draw (gameTime);

			foreach (var component in components) {
				if (component.Visible)
					component.Draw (gameTime);
			}
		}

		public virtual void Show ()
		{
			Visible = true;
			Enabled = true;

			foreach (var component in components) {
				component.Enabled = true;
				component.Visible = true;
			}
		}

		public virtual void Hide ()
		{
			Visible = false;
			Enabled = false;

			foreach (var component in components) {
				component.Enabled = false;
				component.Visible = false;
			}
		}
	}
}

