using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shoot__n_Loot
{
    class Map
    {
        public const byte width = 8, height = 8;

        public static Chunk[,] chunks { get; set; }

        public static void Initialize()
        {
            chunks = new Chunk[width, height];
            //might want to load a single texture and split it later
            for(int w = 0; w < width; w++)
            {
                for (int h = 0; h < height; h++)
                {
                    if(TextureManager.chunks[w, h] != null) chunks[w, h] = new Chunk(TextureManager.chunks[w, h], Chunk.totalSize * new Vector2(w, h));
                    else chunks[w, h] = new Chunk(TextureManager.chunks[0, 0], Chunk.totalSize * new Vector2(w, h));
                }
            }
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            foreach(Chunk c in chunks)
            { c.Draw(spriteBatch); }
        }
    }
}
