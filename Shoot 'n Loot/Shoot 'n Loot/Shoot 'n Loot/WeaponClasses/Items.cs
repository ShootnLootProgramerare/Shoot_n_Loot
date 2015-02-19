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
        public static Item RandomItem(Vector2 position)
        {
            Item i = new Item(properties[Game1.random.Next(properties.Length)], position);
            i.Position = position;
            return i;
        }

        public static ItemProperties[] properties = new ItemProperties[]
        {
            new ItemProperties(1, 1, 1, TextureManager.enemy1, 1),
            new ItemProperties(2, 1, 1, TextureManager.medicineItem, 10, healht1)
        } ;

        static void healht1(Player p)
        {
            p.Health += 1;
        }
    }
}
