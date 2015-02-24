using Microsoft.Xna.Framework;
using Shoot__n_Loot.InvenoryStuff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shoot__n_Loot.WeaponClasses
{
    static class Items
    {
        static ItemProperties
            ligthAmmo = new ItemProperties(1, 1, 1, TextureManager.lightAmmo, 20, "Light Ammo", Weapon.AmmoType.Light),
            mediumAmmo = new ItemProperties(1, 1, 1, TextureManager.lightAmmo, 20, "Medium Ammo", Weapon.AmmoType.Medium),
            heavyAmmo = new ItemProperties(1, 1, 1, TextureManager.lightAmmo, 20, "Heavy Ammo", Weapon.AmmoType.Heavy);

        public static Item RandomItem(Vector2 position)
        {
            Item i = new Item(properties[Game1.random.Next(properties.Length)], position);
            i.Position = position;
            return i;
        }

        public static ItemProperties[] properties = new ItemProperties[]
        {
            ligthAmmo,
            mediumAmmo,
            heavyAmmo,
            new ItemProperties(1, 1, 1, TextureManager.enemy1, 1, "Pink blob\nDoes nothing"),
            new ItemProperties(2, 1, 1, TextureManager.medicineItem, 10, "Heal 1 point", healht1),
            new ItemProperties(1, 1, 1, TextureManager.house, 1, "Auto: true\nAlso newLine", new WeaponPart(WeaponPart.PartType.Base, 1, 1, 30, true, 1, 1, new Weapon.AmmoType[] { Weapon.AmmoType.Medium } )),
            new ItemProperties(1, 1, 1, TextureManager.enemy2, 1, "Weaponpart 2", new WeaponPart(WeaponPart.PartType.Barrel, 1, 1, 0, false, 10, 10, new Weapon.AmmoType[] { Weapon.AmmoType.Light } )),
            new ItemProperties(1, 1, 1, TextureManager.house, 1, "Other house like part", new WeaponPart(WeaponPart.PartType.Base, 1, 3, 10, false, 2, 10, new Weapon.AmmoType[] { Weapon.AmmoType.Heavy } )),
            new ItemProperties(1, 1, 1, TextureManager.beans, 5, "Can of Beans\nRestore 10 health points", Hunger)
        } ;

        public static Item GetAmmo(Weapon.AmmoType type, Vector2 position)
        {
            switch (type)
            {
                case Weapon.AmmoType.Light :
                    return new Item(ligthAmmo, position);
                case Weapon.AmmoType.Medium:
                    return new Item(mediumAmmo, position);
                case Weapon.AmmoType.Heavy:
                    return new Item(heavyAmmo, position);
                default: goto case Weapon.AmmoType.Light; // keep all the bases covered ( ͡° ͜ʖ ͡°)
            }
        }

        //======================== DELEGATES FOR ITEM USAGE =======================

        static void healht1(Player p)
        {
            p.Health += 1;
        }

        static void Hunger(Player p)
        {
            p.Hunger -= 10;
            if (p.Hunger < 0) p.Hunger = 0;
        }
    }
}
