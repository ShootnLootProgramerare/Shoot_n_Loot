using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Shoot__n_Loot.Scenes;
using Shoot__n_Loot.WeaponClasses;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
                return new BulletProperties(bulletDamage, bulletSpeed, range);
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
        
        public byte Ammo { get; private set; }

        private List<Item> parts;




        public Weapon()
        {
            parts = new List<Item>();
            reloadTimer = 0;
            shootTimer = 0;
            CalculateValues();
        }


        /// <summary>
        /// returns the replaced part if one existed, otherwise null
        /// </summary>
        /// <param name="part"></param>
        /// <returns></returns>
        public Item AddPart(Item part)
        {
            Item oldPart = RemovePart(part.Properties.WeaponPart.Type);
            parts.Add(part);

            CalculateValues();
            return oldPart;
        }

        public bool ContainsType(WeaponPart.PartType t)
        {
            foreach (Item p in parts) if (p.Properties.WeaponPart.Type == t) return true;
            return false;
        }

        public void RemovePart(Item p)
        {
            parts.Remove(p);
            CalculateValues();
        }


        /// <summary>
        /// tries to remove the part of the specified type. Returns null if no part was found, otherwise returns the removed part.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public Item RemovePart(WeaponPart.PartType type)
        {
            foreach (Item p in parts)
            {
                if (p.Properties.WeaponPart.Type == type)
                {
                    parts.Remove(p);
                    CalculateValues();
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

            foreach(Item p in parts)
            {
                shootTimeMod += p.Properties.WeaponPart.ShootSpeedMod;
                reloadTimeMod += p.Properties.WeaponPart.ReloadSpeedMod;
                magSizeMod += p.Properties.WeaponPart.MagSizeMod;
                bulletSpeedMod += p.Properties.WeaponPart.BulletSpeedMod;
                bulletDamageMod += p.Properties.WeaponPart.DamageMod;
                rangeMod += p.Properties.WeaponPart.RangeMod;
                if (p.Properties.WeaponPart.MakesAuto) auto = true;
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
            Ammo = magSize;
        }

        public void StartReload(Inventory bulletContainer)
        {
            //find and remove ammo
            reloadTimer = 1;
            Ammo = 0; //TODO: drop old ammo on ground or return to inventory?
        }
        
        /// <summary>
        /// checks if the weapon can shoot and if so adds a bullet
        /// </summary>
        /// <returns></returns>
        public void TryShoot(Vector2 position, float angle, Scene scene)
        {
            //TODO: check if necessary parts are present
            if (shootTimer == 0 && Ammo > 0)
            {
                scene.AddObject(new Bullet(angle, position, BulletProperties));
                Ammo--;
                shootTimer = 1;
            }
        }

        public void ShootingUpdate()
        {
            if (reloadTimer > 0) reloadTimer++;
            if (reloadTimer >= reloadTime)
            {
                reloadTimer = 0;
                Reload();
            }

            if (shootTimer > 0) shootTimer++;
            if (shootTimer >= shootTime) shootTimer = 0;
        }

        CustomizationSlot 
            barrelSlot = new CustomizationSlot(new Rectangle(800, 100, 100, 100), WeaponPart.PartType.Barrel),
            baseSlot = new CustomizationSlot(new Rectangle(200, 100, 100, 100), WeaponPart.PartType.Base),
            magSlot = new CustomizationSlot(new Rectangle(200, 230, 100, 100), WeaponPart.PartType.Mag);

        Item draggedItem;
        /*Inventory unusedParts 
        { 
            get
            {
                //Inventory i = new Inventory(10, 10, 100000);
                
                //waponparts need to be a seperate inventory
                //go through all slots and get all items that arent null

                //return i;
            }
        }*/

        public void CustomizingUpdate(Inventory i, Point inventoryDrawOffset)
        {
            //dra items från alla weaponparts
            //om p[ en legit slot n'r man sl'pper s't det d'r och returnera gammalt till ;verblivna
            //annars l'gg tillbaka till 'verblivna
            if (draggedItem == null)
            {
                for (int y = 0; y < i.Height; y++)
                {
                    for (int x = 0; x < i.Width; x++)
                    {
                        Rectangle r = i.PositionForItem(x, y);
                        Item t = i.Slots[x, y].Item;
                        if (t == null) continue;
                        if (Input.AreaIsClicked(new Rectangle(r.X, r.Y, r.Width * t.Properties.Width, r.Height * t.Properties.Height)))
                        {
                            draggedItem = i.Slots[x, y].Item;
                            i.Slots[x, y].Remove(1);
                            draggedItem.Position = new Vector2(r.X + r.Width / 2, r.Y + r.Height / 2);
                            Debug.WriteLine("item was clicked");
                        }
                    }
                }
            }
            else
            {
                if (Input.newMs.LeftButton == ButtonState.Released)
                {
                    i.Add(draggedItem);
                    draggedItem = null;
                }
                else
                {
                    draggedItem.Position -= Input.DeltaPos;
                }
            }
        }

        public void DrawCustomization(SpriteBatch spriteBatch)
        {
            barrelSlot.Draw(spriteBatch);
            baseSlot.Draw(spriteBatch);
            magSlot.Draw(spriteBatch);

            if (draggedItem != null) draggedItem.Draw(spriteBatch);
        }
    }
}
