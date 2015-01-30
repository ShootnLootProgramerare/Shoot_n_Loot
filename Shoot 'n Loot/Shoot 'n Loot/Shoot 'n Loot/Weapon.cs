﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shoot__n_Loot
{
    class Weapon
    {
        const float baseDamage = 1;
        const float baseBulletDamage = 1;
        const float baseBulletSpeed = 10;
        const int baseRange = 300;
        const byte baseShootTime =  15;
        const byte baseReloadTime = 60;
        const byte baseMagSize = 10;

        public enum AmmoType { Light, Medium, Heavy }

        AmmoType currentAmmoType;

        public Item item;

        
        private BulletProperties BulletProperties
        {
            get
            {
                return new BulletProperties(bulletDamage, bulletSpeed, );
            }
        }

        //private byte bulletPenetration;
        private byte shootTime;
        private byte reloadTime;
        private byte magSize;
        private float bulletSpeed;
        private float bulletDamage;
        private int range;

        public bool IsAuto { get; private set; }

        private byte reloadTimer;
        private byte shootTimer;

        private List<WeaponPart> parts;




        public Weapon()
        {
            parts = new List<WeaponPart>();
            reloadTimer = 0;
            shootTimer = 0;
        }



        public bool AddPart(WeaponPart part)
        {
            if (ContainsType(part.Type)) return false;

            parts.Add(part);

            return true;
        }

        public bool ContainsType(WeaponPart.PartType t)
        {
            foreach (WeaponPart p in parts) if (p.Type == t) return true;
            return false;
        }

        public void RemovePart(WeaponPart p)
        {
            parts.Remove(p);
        }


        /// <summary>
        /// tries to remove the part of the specified type. Returns null if no part was found, otherwise returns the removed part.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public WeaponPart RemovePart(WeaponPart.PartType type)
        {
            foreach (WeaponPart p in parts)
            {
                if (p.Type == type)
                {
                    parts.Remove(p);
                    return p;                
                }
            }
            return null;
        }

        private void CalculateValues()
        {
            float shootTimeMod = 1;
            float reloadTimeMod = 1;
            sbyte magSizeMod = 0;
            float bulletSpeedMod = 1;
            float bulletDamageMod = 1;
            bool auto = false;
            float rangeMod = 1;

            foreach(WeaponPart p in parts)
            {
                shootTimeMod += p.ShootSpeedMod;
                reloadTimeMod += p.ReloadSpeedMod;
                magSizeMod += p.MagSizeMod;
                bulletSpeedMod += p.BulletSpeedMod;
                bulletDamageMod += p.DamageMod;
                rangeMod += p.RangeMod;
                if (p.MakesAuto) auto = true;
            }

            shootTime = (byte)(baseShootTime * shootTimeMod);
            reloadTime = (byte)(baseReloadTime * reloadTimeMod);
            magSize = (byte)(baseMagSize + magSizeMod);
            bulletSpeed = baseBulletSpeed * bulletSpeedMod;
            bulletDamage = baseBulletDamage * bulletDamageMod;
            range = (int)(baseRange * rangeMod);
            IsAuto = auto;
        }

        private void Reload()
        {
            //ammo = maxAmmo etc
        }

        public void StartReload(Inventory bulletContainer)
        {
            //find and remove ammo
            reloadTimer = 1;
        }
        
        /// <summary>
        /// returns bulletproperties if you can shoot, otherwise null
        /// </summary>
        /// <returns></returns>
        public BulletProperties TryShoot()
        {
            //check if necessary parts are present
            if (shootTimer == 0) return BulletProperties;
            else return null;
        }

        public void Update()
        {
            if (reloadTimer > 0) reloadTimer++;
            if(reloadTimer >= ReloadTime)
            {
                reloadTimer = 0;
                Reload();
            }

            if (shootTimer > 0) shootTimer++;
            if (shootTimer >= ShootTime) shootTimer = 0;
        }
    }
}
