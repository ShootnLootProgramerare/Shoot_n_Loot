using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shoot__n_Loot.UI
{
    class Bar
    {
        Texture2D foreground, background;
        Rectangle screenSpace;

        Rectangle RealSpace { get { return new Rectangle(screenSpace.X + (int)Camera.TotalOffset.X, screenSpace.Y + (int)Camera.TotalOffset.Y, screenSpace.Width, screenSpace.Height); } }

        public Bar(Texture2D foreground, Texture2D background, Rectangle screenSpace)
        {
            this.foreground = foreground;
            this.background = background;
            this.screenSpace = screenSpace;
        }

        private Color BackgroundColor(float percent)
        {
            return Color.White;
        }

        private Rectangle BackgroundRectangle(float percent)
        {
            return new Rectangle(RealSpace.X, RealSpace.Y, (int)(RealSpace.Width * percent), RealSpace.Height);
        }

        public void Draw(SpriteBatch batch, float percent)
        {
            batch.Draw(background, BackgroundRectangle(percent), null, BackgroundColor(percent), 0, Vector2.Zero, SpriteEffects.None, .001f);
            batch.Draw(foreground, RealSpace, Color.White);
        }
    }
}
