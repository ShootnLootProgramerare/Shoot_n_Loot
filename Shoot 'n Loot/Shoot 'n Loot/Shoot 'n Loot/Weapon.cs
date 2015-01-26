using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shoot__n_Loot
{
    class Weapon
    {
        const byte baseAmmo = 10;
        const float baseDamage = 1;
        const float baseBulletSpeed = 10;
        const byte baseReloadTime = 60;

        public Item item;

        private float bulletDamage
        {
            get
            {
                float m = 1;
                foreach (WeaponPart p in parts) m += p.DamageMod;
                return m * baseDamage;
            }
        }
        private float bulletSpeed
        {
            get
            {
                float m = 1;
                foreach (WeaponPart p in parts) m += p.SpeedMod;
                return m * baseBulletSpeed;
            }
        }
        private byte MagSize
        {
            get
            {
                byte m = 0;
                foreach (WeaponPart p in parts) m += p.MagSizeMod;
                return (byte)(m + baseAmmo);
            }
        }
        private byte ReloadTime
        {
            get 
            {
                return baseReloadTime;
            }
        }
        private byte bulletPenetration;

        private byte reloadTimer;

        private List<WeaponPart> parts;

        BulletProperties BulletProperties
        {
            get
            {
                return new BulletProperties(bulletDamage, bulletSpeed, bulletPenetration);
            }
        }

        public Weapon()
        {
            parts = new List<WeaponPart>();
            reloadTimer = 0;
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

        public Bullet GetBullet(Vector2 position, float angle)
        {
            return new Bullet(angle, position, BulletProperties);
        }

        public void StartReload()
        {
            reloadTimer = 1;
        }


        public void Reload()
        {

        }

        public void Update()
        {
            if (reloadTimer > 0) reloadTimer++;
            if(reloadTimer >= ReloadTime)
            {
                reloadTimer = 0;
                Reload();
            }
        }
    }
}
