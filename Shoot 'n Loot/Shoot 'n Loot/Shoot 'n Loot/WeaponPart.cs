using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shoot__n_Loot
{
    class WeaponPart
    {
        public enum PartType { Base, Barrel, Mag }

        public  PartType Type { get; private set; }

        public float DamageMod { get; private set; }
        public float SpeedMod { get; private set; }
        public byte MagSizeMod { get; private set; }

        public WeaponPart(PartType type, float damageMod, float speedMod, byte magSizeMod)
        {
            this.Type = type;
            this.DamageMod = damageMod;
            this.SpeedMod = speedMod;
            this.MagSizeMod = magSizeMod;
        }
    }
}
