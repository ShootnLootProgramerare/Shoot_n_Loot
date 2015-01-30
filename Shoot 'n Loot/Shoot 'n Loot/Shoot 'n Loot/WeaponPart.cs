using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shoot__n_Loot
{
    class WeaponPart
    {
        public enum PartType { Base, Barrel, Mag }

        public PartType Type { get; private set; }

        public float DamageMod { get; private set; }
        public float BulletSpeedMod { get; private set; }
        public float ShootSpeedMod { get; private set; }
        public float ReloadSpeedMod { get; private set; }
        public float RangeMod { get; private set; }
        public sbyte MagSizeMod { get; private set; }
        public bool MakesAuto { get; private set; }
        public Weapon.AmmoType[] AcceptableAmmo { get; private set; }
        //maybe armor penetration?

        public WeaponPart(PartType type, float damageMod, float speedMod, sbyte magSizeMod, bool makesAuto, float reloadSpeedMod, float rangeMod, Weapon.AmmoType[] acceptableAmmo)
        {
            this.Type = type;
            this.DamageMod = damageMod;
            this.BulletSpeedMod = speedMod;
            this.MagSizeMod = magSizeMod;
            this.MakesAuto = makesAuto;
            this.ReloadSpeedMod = reloadSpeedMod;
            this.AcceptableAmmo = acceptableAmmo;
            this.BulletSpeedMod = BulletSpeedMod;
        }
    }
}
