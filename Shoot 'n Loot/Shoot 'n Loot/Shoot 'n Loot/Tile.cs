using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shoot__n_Loot
{
    class Tile
    {
        public enum TileType { Grass = 0, Sea = 1, Wood = 2 } //numbers must correspond to the right texture in Textures.tiles

        public const byte size = 32;
        
        public Vector2 Position { get; private set; }
        public TileType Type { get; private set; }

        public Rectangle Hitbox { get { return new Rectangle((int)Position.X, (int)Position.Y, size, size); } }

        public Tile(Vector2 position, TileType type)
        {
            this.Position = position;
            this.Type = type;
        }

        public Tile(Vector2 position, byte type)
        {
            this.Position = position;
            this.Type = (TileType) type;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(TextureManager.tiles, Hitbox, new Rectangle(1, 1 + (int)Type * 34, 32, 32), Color.White, 0, Vector2.Zero, SpriteEffects.None, 1); //TODO: use a spritesheet instead
        }
    }
}
