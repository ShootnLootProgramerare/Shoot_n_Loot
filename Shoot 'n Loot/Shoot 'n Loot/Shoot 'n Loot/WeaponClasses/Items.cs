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
            heavyAmmo = new ItemProperties(1, 1, 1, TextureManager.lightAmmo, 20, "Heavy Ammo", Weapon.AmmoType.Heavy),
            nailAmmo = new ItemProperties(1, 1, .1f, TextureManager.nails, 20, "Nails\nVery deadly", Weapon.AmmoType.Nails);

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
            nailAmmo,
            new ItemProperties(1, 1, 3, TextureManager.enemy1, 1, "Pink blob\nDoes nothing"),
            new ItemProperties(2, 1, .01f, TextureManager.medicineItem, 10, "Heal 1 point", healht1),
            new ItemProperties(1, 1, .3f, TextureManager.house, 1, "Auto: true\nAlso newLine", new WeaponPart(WeaponPart.PartType.Base, 1, 1, 30, true, 1, 1, new Weapon.AmmoType[] { Weapon.AmmoType.Medium } )),
            new ItemProperties(1, 1, .4f, TextureManager.enemy2, 1, "Weaponpart 2", new WeaponPart(WeaponPart.PartType.Barrel, 1, 1, 0, false, 10, 10, new Weapon.AmmoType[] { Weapon.AmmoType.Light } )),
            new ItemProperties(1, 1, .35f, TextureManager.house, 1, "Other house like part", new WeaponPart(WeaponPart.PartType.Base, 1, 3, 10, false, 2, 10, new Weapon.AmmoType[] { Weapon.AmmoType.Heavy } )),
            new ItemProperties(1, 1, .5f, TextureManager.beans, 5, "Can of Beans\nRestore 10 hunger points", Hunger),
            new ItemProperties(1, 1, .2f, TextureManager.bandage, 10, "Bandage\nReduce bleeding", Bandage),
            new ItemProperties(1, 1, .5f, TextureManager.twoByFour, 1, "2 by 4 wood\nCrushes sculls", new MeleeWeaponProperties(2, 100))
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
                case Weapon.AmmoType.Nails:
                    return new Item(nailAmmo, position);
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

        static void Bandage(Player p)
        {
            p.bleeding -= .5f;
            if (p.bleeding < 0) p.bleeding = 0;
        }
    }
}
