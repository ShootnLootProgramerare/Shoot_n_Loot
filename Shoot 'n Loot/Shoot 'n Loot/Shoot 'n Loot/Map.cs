﻿using Microsoft.Xna.Framework;
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
        public const byte width = 8, height = 8; //number of chunks. width * Tile.size should equal the width of the map texture, same for height.

        public static Chunk[,] chunks { get; set; }

        public static void Initialize()
        {
            chunks = new Chunk[width, height];

            Color[] colors1D = new Color[TextureManager.map.Width * TextureManager.map.Height];
            TextureManager.map.GetData(colors1D);
            Color[,] mapData = new Color[TextureManager.map.Width, TextureManager.map.Height];
            for (int x = 0; x < TextureManager.map.Width; x++)
            {
                for (int y = 0; y < TextureManager.map.Height; y++)
                {
                    mapData[x, y] = colors1D[x + y * TextureManager.map.Width];
                }
            }

            for(int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    chunks[x, y] = new Chunk(Chunk.sizePx * new Vector2(x, y), subChunk(mapData, x * Chunk.size, y * Chunk.size, Chunk.size, Chunk.size));
                }
            }
        }

        static Color[,] subChunk(Color[,] source, int x, int y, int w, int h)
        {
            Color[,] c = new Color[w, h];
            for (int ix = 0; ix < w; ix++)
            {
                for (int iy = 0; iy < h; iy++)
                {
                    c[ix, iy] = source[ix + x, iy + y];
                }
            }
            return c;
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            int drawn = 0;
            foreach(Chunk c in chunks)
            {
                if (Camera.AreaIsVisible(c.position, new Vector2(Chunk.sizePx)))
                {
                    c.Draw(spriteBatch); //check which chunks are visible
                    drawn++;
                }
            }
            spriteBatch.DrawString(TextureManager.font, drawn.ToString(), new Vector2(10) + Camera.Position, Color.Black);
        }
    }
}
