using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shoot__n_Loot.WeaponClasses
{
    static class Items
    {
        public static Item[] items = new Item[]
        {
            new Item(1, 1, 1, new Sprite(TextureManager.medicineItem, Vector2.Zero, new Vector2(50)))
        } ;

        Item.OnConsume healht1(Player p)
        {
            p.Health += 1;
            return null;
        }
    }
}
