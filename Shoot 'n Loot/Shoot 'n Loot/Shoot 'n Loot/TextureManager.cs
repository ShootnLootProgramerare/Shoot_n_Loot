using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Shoot__n_Loot
{
    class TextureManager
    {
        public static void Load(ContentManager content)
        {
            player = content.Load<Texture2D>("player");
            font = content.Load<SpriteFont>("font");

            #region tiles
            tiles = new Texture2D[tileTextureNames.Length];
            for(int i = 0; i < tileTextureNames.Length; i++)
            {
                tiles[i] = content.Load<Texture2D>("tiles/" + tileTextureNames[i]);
            }
            #endregion

            #region map
            chunks = new Texture2D[Map.width, Map.height];
            DirectoryInfo d = new DirectoryInfo(content.RootDirectory + "/map");
            FileInfo[] f = d.GetFiles();
            foreach(FileInfo fi in f)
            {
                int chunkX = int.Parse(fi.Name.Substring(0, 2)),
                    chunkY = int.Parse(fi.Name.Substring(2, 2));
                chunks[chunkX, chunkY] = content.Load<Texture2D>("map/" + fi.Name.Substring(0, 4));
            }
            #endregion
        }

        public static Texture2D
            player;

        static string[] tileTextureNames = { "grass", "sea" };

        public static Texture2D[] tiles;

        public static Texture2D[,] chunks;

        public static SpriteFont font;
    }
}
