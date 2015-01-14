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
        public Vector2 Position { get { return Sprite.Position; } protected set { Sprite.Position = value; } }
        public Vector2 Size { get { return Sprite.Size; } protected set { Sprite.Size = value; } }
        public Vector2 Center { get { return new Vector2(Hitbox.Center.X, Hitbox.Center.Y); } }
        public Rectangle Hitbox { get { return Sprite.Area; } }
        public bool Dead { get; set; }
        
        protected Sprite Sprite { get; set; }

        /// <summary>
        /// should be overridden by child object
        /// </summary>
        public void Update() { }

        /// <summary>
        /// draws the sprite. override and draw sprite manually or call base.Draw
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            Sprite.Draw(spriteBatch);
        }

        public float DistanceSquared(Vector2 target)
        {
            return (target - Position).LengthSquared();
        }

        public void Move(float x, float y)
        {
            Position += new Vector2(x, y);
        }

        protected List<Tile> CloseSolidTiles
        {
            get
            {
                List<Tile> solidTiles = new List<Tile>();

                foreach (Chunk c in Map.VisibleChunks)
                {
                    foreach (Tile t in c.NonWalkableTiles())
                    {
                        solidTiles.Add(t);
                    }
                }

                for (int i = solidTiles.Count - 1; i >= 0; i--)
                {
                    if (DistanceSquared(solidTiles[i].Position) > 10000) solidTiles.RemoveAt(i);
                }

                return solidTiles;
            }
        }

        protected bool IsCollidingWithAny(List<Tile> tiles, Rectangle rectangle)
        {
            foreach (Tile t in tiles)
            {
                if (t.Hitbox.Intersects(rectangle)) return true;
            }
            return false;
        } 
    }
}
