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
        public static Texture2D
            playerLeft,
            playerRight,
            playerUp,
            playerDown,

            map,
            spawnData,
            tiles,

            enemy1,
            enemy2,
            enemy3,

            bullet, 
            house,
            inventorySlot,

            medicineItem,
            
            lightAmmo,
            mediumAmmo,
            heavyAmmo;

        public static Texture2D[]
            fishermanWalk,
            fishermanAttack;

        public static SpriteFont font;

        public static void Load(ContentManager content)
        {
            playerLeft = content.Load<Texture2D>("player/left");
            playerRight = content.Load<Texture2D>("player/right");
            playerUp = content.Load<Texture2D>("player/up");
            playerDown = content.Load<Texture2D>("player/down");

            font = content.Load<SpriteFont>("font");
            tiles = content.Load<Texture2D>("tiles");
            map = content.Load<Texture2D>("map/map");
            spawnData = content.Load<Texture2D>("map/spawnData");

            enemy1 = content.Load<Texture2D>("enemies/enemy1");
            enemy2 = content.Load<Texture2D>("enemies/enemy2");
            enemy3 = content.Load<Texture2D>("enemies/enemy3");

            fishermanWalk = new Texture2D[4];
            fishermanAttack = new Texture2D[4];
            fishermanWalk[0] = content.Load<Texture2D>("enemies/fisherman/walk/Up");
            fishermanWalk[1] = content.Load<Texture2D>("enemies/fisherman/walk/Down");
            fishermanWalk[2]= content.Load<Texture2D>("enemies/fisherman/walk/Left");
            fishermanWalk[3] = content.Load<Texture2D>("enemies/fisherman/walk/Right");
            fishermanAttack[0] = content.Load<Texture2D>("enemies/fisherman/attack/up");
            fishermanAttack[1] = content.Load<Texture2D>("enemies/fisherman/attack/down");
            fishermanAttack[2] = content.Load<Texture2D>("enemies/fisherman/attack/left");
            fishermanAttack[3] = content.Load<Texture2D>("enemies/fisherman/attack/right");

            bullet = content.Load<Texture2D>("bullet");
            house = content.Load<Texture2D>("house");
            inventorySlot = content.Load<Texture2D>("inventorySlot");

            medicineItem = content.Load<Texture2D>("items/pill");

            lightAmmo = mediumAmmo = heavyAmmo = content.Load<Texture2D>("items/ammo"); //should be different when we get sprites
        }
    }
}
