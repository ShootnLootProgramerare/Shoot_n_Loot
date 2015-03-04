using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace Shoot__n_Loot
{
    class SoundManager
    {
        public static SoundEffect
            playerShoot,
            playerHurt,
            playerWalkOutside,
            playerWalkIndoor,
            playerDie,

            inventory,
            weaponEquip,
            itemUse,

            zombie1,
            zombie2,
            zombie3,
            zombieHurt,

            door,
            water;

        public static void Load(ContentManager content)
        {
            playerShoot = content.Load<SoundEffect>("");
            playerHurt = content.Load<SoundEffect>("");
            playerWalkOutside = content.Load<SoundEffect>("");
            playerWalkIndoor = content.Load<SoundEffect>("");

            inventory = content.Load<SoundEffect>("");
            weaponEquip = content.Load<SoundEffect>("");
            itemUse = content.Load<SoundEffect>("");

            zombie1 = content.Load<SoundEffect>("");
            zombie2 = content.Load<SoundEffect>("");
            zombie3 = content.Load<SoundEffect>("");
            zombieHurt = content.Load<SoundEffect>("");

            door = content.Load<SoundEffect>("");
            water = content.Load<SoundEffect>("");
        }


            

    }
}
