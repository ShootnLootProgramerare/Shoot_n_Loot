using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shoot__n_Loot
{
    class GameObject
    {
        public Vector2 Position { get; protected set; }
        public Vector2 Size { get; protected set; }
        public Vector2 Center { get { return Position + Size / 2; } }
        public Rectangle Hitbox { get { return new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y); } }
        
        protected Sprite Sprite { get; protected set; }

        public void Update();

        public void Draw(SpriteBatch spriteBatch)
        {
            Sprite.Draw(spriteBatch);
        }
    }
}
