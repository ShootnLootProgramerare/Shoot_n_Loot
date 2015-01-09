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
        public const byte width = 32, height = 32;

        public static Chunk[,] chunks { get; set; }

        public static void Initialize(Texture2D map)
        {
            chunks = new Chunk[width, height];

            for(int w = 0; w < width; w++)
            {
                for (int h = 0; h < height; h++)
                {
                    //chunks[w, h] = new Chunk()
                }
            }
        }
    }
}
