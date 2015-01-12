﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Shoot__n_Loot
{
    class Enemy: GameObject
    {
        enum Type { enemy1, enemy2, enemy3 };
        public Enemy(Vector2 position)
        {
            
            Sprite = new Sprite(TextureManager.enemy1, position, new Vector2(50));
        }
        public void Update()
        {
            
        }
    }
}
