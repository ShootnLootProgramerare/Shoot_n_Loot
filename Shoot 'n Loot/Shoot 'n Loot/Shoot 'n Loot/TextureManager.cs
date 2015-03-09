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
            beans,

            lightAmmo,
            mediumAmmo,
            heavyAmmo,

            hpBar,
            hpRed;

        public static Texture2D[]
            fishermanWalk,
            fishermanAttack,

            oneleggedWalk,
            oneleggedAttack,
            
            playerWalkNoWeapon,
            playerWalkWeapon,
            playerAttack;

        public static SpriteFont font;

        public static void Load(ContentManager content)
        {
            playerWalkNoWeapon = new Texture2D[4];
            playerWalkNoWeapon[0] = content.Load<Texture2D>("player/walk/noWeapon/up");
            playerWalkNoWeapon[1] = content.Load<Texture2D>("player/walk/noWeapon/down");
            playerWalkNoWeapon[2] = content.Load<Texture2D>("player/walk/noWeapon/left");
            playerWalkNoWeapon[3] = content.Load<Texture2D>("player/walk/noWeapon/right");
            playerWalkNoWeapon = LoadWalkSprites("player/walk/noWeapon", content);
            playerWalkWeapon = LoadWalkSprites("player/walk/weapon", content);
            /*playerWalkWeapon[0] = content.Load<Texture2D>("player/walk/Weapon/up");
            playerWalkWeapon[1] = content.Load<Texture2D>("player/walk/Weapon/down");
            playerWalkWeapon[2] = content.Load<Texture2D>("player/walk/Weapon/left");
            playerWalkWeapon[3] = content.Load<Texture2D>("player/walk/Weapon/right");*/
            
            font = content.Load<SpriteFont>("font");
            tiles = content.Load<Texture2D>("tiles");
            map = content.Load<Texture2D>("map/map_wotrees");
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

            oneleggedWalk = new Texture2D[4];
            oneleggedAttack = new Texture2D[4];
            oneleggedWalk[0] = content.Load<Texture2D>("enemies/onelegged/walk/Up");
            oneleggedWalk[1] = content.Load<Texture2D>("enemies/onelegged/walk/Down");
            oneleggedWalk[2] = content.Load<Texture2D>("enemies/onelegged/walk/Left");
            oneleggedWalk[3] = content.Load<Texture2D>("enemies/onelegged/walk/Right");
            oneleggedAttack[0] = content.Load<Texture2D>("enemies/onelegged/attack/up");
            oneleggedAttack[1] = content.Load<Texture2D>("enemies/onelegged/attack/down");
            oneleggedAttack[2] = content.Load<Texture2D>("enemies/onelegged/attack/left");
            oneleggedAttack[3] = content.Load<Texture2D>("enemies/onelegged/attack/right");

            bullet = content.Load<Texture2D>("bullet");
            house = content.Load<Texture2D>("house");
            inventorySlot = content.Load<Texture2D>("inventorySlot");

            medicineItem = content.Load<Texture2D>("items/pill");
            beans = content.Load<Texture2D>("items/beans");

            lightAmmo = mediumAmmo = heavyAmmo = content.Load<Texture2D>("items/ammo"); //should be different when we get sprites

            hpBar = content.Load<Texture2D>("hpBar");
            hpRed = content.Load<Texture2D>("RedHP");
        }

        private static Texture2D[] LoadWalkSprites(string path, ContentManager content)
        {
            Texture2D[] t = new Texture2D[4];
            t[0] = content.Load<Texture2D>(path + "/up");
            t[1] = content.Load<Texture2D>(path + "/down");
            t[2] = content.Load<Texture2D>(path + "/left");
            t[3] = content.Load<Texture2D>(path + "/right");
            return t;
        }
    }
}
