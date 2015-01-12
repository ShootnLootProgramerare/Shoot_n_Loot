﻿using Microsoft.Xna.Framework;
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
        public static Texture2D
            playerHorizontal,
            playerUp,
            playerDown,
            map,
            tiles,
            enemy1,
            enemy2,
            enemy3;

        public static void Load(ContentManager content)
        {
            playerHorizontal = content.Load<Texture2D>("player/horizontal");
            playerUp = content.Load<Texture2D>("player/up");
            playerDown = content.Load<Texture2D>("player/down");
            font = content.Load<SpriteFont>("font");
            tiles = content.Load<Texture2D>("tiles");
            map = content.Load<Texture2D>("map");
<<<<<<< HEAD
            enemy1 = content.Load<Texture2D>("enemies/enemy1");
            //enemy2 = content.Load<Texture2D>("enemy2");
            //enemy3 = content.Load<Texture2D>("enemy3");
=======
            enemy1 = content.Load<Texture2D>("enemy1");
            enemy2 = content.Load<Texture2D>("enemy2");
            enemy3 = content.Load<Texture2D>("enemy3");
>>>>>>> origin/master
        }

        public static SpriteFont font;
    }
}
