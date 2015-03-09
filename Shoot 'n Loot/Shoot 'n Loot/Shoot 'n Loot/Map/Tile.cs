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
        public static TileProperties[] TilePrefabs = new TileProperties[] 
        { 
            new TileProperties(0, true), //grass
            new TileProperties(1, false, false, 2, 30), //water
            new TileProperties(2, false), //floor
            new TileProperties(3, true), //wall
            new TileProperties(4, true), //beach
            new TileProperties(5, true) //path
        };

        public enum TileType { Grass = 0, Sea = 1, Wood = 2 }

        public const byte size = 48;
        
        public Vector2 Position { get; private set; }
        public TileType Type { get; private set; }

        public TileProperties Properties { get { return TilePrefabs[(int)Type]; } }
        public Rectangle Hitbox { get { return new Rectangle((int)Position.X, (int)Position.Y, size, size); } }

        byte frame, animCounter;

        public Tile(Vector2 position, byte type)
        {
            frame = 0;
            animCounter = 0;
            this.Position = position;
            this.Type = (TileType)type;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Properties.IsAnimated) Properties.Animate(ref frame, ref animCounter);
            spriteBatch.Draw(TextureManager.tiles, Hitbox, new Rectangle(frame * 48, Properties.TextureIndex * 48, 48, 48), Color.White, 0, Vector2.Zero, SpriteEffects.None, 1); 
        }
    }
}
