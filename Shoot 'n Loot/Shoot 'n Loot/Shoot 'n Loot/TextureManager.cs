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

            explosion,

            map,
            spawnData,
            tiles,
            propData,

            enemy1,
            enemy2,
            enemy3,

            bullet,
            house,
            lightHouse,
            inventorySlot,
            landmine,

            medicineItem,
            beans,
            bandage,
            fuelCan,

            twoByFour,
            
            boat,

            lightAmmo,
            mediumAmmo,
            heavyAmmo,
            nails,

            gunBarrel,
            gunMechs,

            hpBar,
            hpRed;

        public static Texture2D[]
            fishermanWalk,
            fishermanAttack,

            oneleggedWalk,
            oneleggedAttack,

            fatLadyWalk,
            fatLadyAttack,

            babyWalk,
            babyAttack,
            babyNuke,

            playerWalkNoWeapon,
            playerWalkGun,
            playerWalkMelee,
            playerAttack;

        public static SpriteFont font;

        public static void Load(ContentManager content)
        {
            playerWalkNoWeapon = LoadWalkSprites("player/walk/noWeapon", content);
            playerWalkGun = LoadWalkSprites("player/walk/gun", content);
            playerWalkMelee = LoadWalkSprites("player/walk/melee", content);
            playerAttack = LoadWalkSprites("player/attack/melee", content);
            /*playerWalkWeapon[0] = content.Load<Texture2D>("player/walk/Weapon/up");
            playerWalkWeapon[1] = content.Load<Texture2D>("player/walk/Weapon/down");
            playerWalkWeapon[2] = content.Load<Texture2D>("player/walk/Weapon/left");
            playerWalkWeapon[3] = content.Load<Texture2D>("player/walk/Weapon/right");*/
            
            font = content.Load<SpriteFont>("font");
            tiles = content.Load<Texture2D>("tiles");
            map = content.Load<Texture2D>("map/map_wotrees");
            spawnData = content.Load<Texture2D>("map/spawnData");
            propData = content.Load<Texture2D>("map/propData");

            explosion = content.Load<Texture2D>("explosion");

            enemy1 = content.Load<Texture2D>("enemies/enemy1");
            enemy2 = content.Load<Texture2D>("enemies/enemy2");
            enemy3 = content.Load<Texture2D>("enemies/enemy3");

            fishermanWalk = LoadWalkSprites("enemies/fisherman/walk", content);
            fishermanAttack = LoadWalkSprites("enemies/fisherman/attack", content);

            oneleggedWalk = LoadWalkSprites("enemies/onelegged/walk", content);
            oneleggedAttack = LoadWalkSprites("enemies/onelegged/attack", content);

            fatLadyWalk = LoadWalkSprites("enemies/fatLady/walk", content);
            fatLadyAttack = LoadWalkSprites("enemies/fatLady/attack", content);

            babyWalk = LoadWalkSprites("enemies/baby/walk", content);
            babyAttack = LoadWalkSprites("enemies/baby/attack", content);
            babyNuke = LoadWalkSprites("enemies/baby/nuke", content);

            bullet = content.Load<Texture2D>("bullet");
            house = content.Load<Texture2D>("houses/house");
            lightHouse = content.Load<Texture2D>("houses/lightHouse");
            inventorySlot = content.Load<Texture2D>("inventorySlot");
            landmine = content.Load<Texture2D>("items/landmine");

            medicineItem = content.Load<Texture2D>("items/pill");
            beans = content.Load<Texture2D>("items/beans");
            bandage = content.Load<Texture2D>("items/bandage");
            fuelCan = content.Load<Texture2D>("items/fuelCan");

            twoByFour = content.Load<Texture2D>("items/twoByFour");

            boat = content.Load<Texture2D>("boat");

            lightAmmo = mediumAmmo = heavyAmmo = content.Load<Texture2D>("items/ammo"); //should be different when we get sprites
            nails = content.Load<Texture2D>("items/nails");

            gunBarrel = content.Load<Texture2D>("items/gunBarrel");
            gunMechs = content.Load<Texture2D>("items/gunMechs");

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
