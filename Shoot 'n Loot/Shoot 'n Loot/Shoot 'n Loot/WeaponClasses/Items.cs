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
            new ItemProperties(1, 1, 1, TextureManager.house, 1, "Auto: true\nAlso newLine", new WeaponPart(WeaponPart.PartType.Base, 1, 1, 30, true, 1, 1, new Weapon.AmmoType[] { Weapon.AmmoType.Medium } ))
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
                default: return null;
            }
        }

        static void healht1(Player p)
        {
            p.Health += 1;
        }
    }
}
