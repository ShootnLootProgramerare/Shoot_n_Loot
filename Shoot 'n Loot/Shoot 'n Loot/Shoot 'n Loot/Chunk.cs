using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shoot__n_Loot
{
    class Chunk
    {
        static Color[] tileTypes = new Color[] { Color.Green, Color.Blue };
        const byte size = 32;
        public static short totalSize { get { return size * Tile.size; } }

        public Tile[,] tiles;
        public Vector2 position { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="map">the texture that contains the map of this chunk</param>
        /// <param name="relativePosition">posiiton in the game of the top left corner</param>
        public Chunk(Texture2D map, Vector2 relativePosition)
        {
            position = relativePosition;
            tiles = new Tile[size, size];

            Color[] colors1D = new Color[map.Width * map.Height];
            map.GetData(colors1D);
            Color[,] mapData = new Color[map.Width, map.Height];
            for (int x = 0; x < map.Width; x++)
                 for (int y = 0; y < map.Height; y++)
                    mapData[x, y] = colors1D[x + y * map.Width];

            for(int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    if(!tileTypes.Contains(mapData[x, y])) throw new Exception("Color not valid, pixel " + x + ", " + y);
                    byte type = 0;
                    for (byte i = 0; i < tileTypes.Length; i++)
			        {
			            if(tileTypes[i] == mapData[x, y]) type = i;
			        }
                    tiles[x ,y] = new Tile(position + new Vector2(x, y) * Tile.size, type);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach(Tile t in tiles)
            {
                t.Draw(spriteBatch);
            }
        }
    }
}
