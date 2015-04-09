﻿using Shoot__n_Loot.InvenoryStuff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shoot__n_Loot.Base_Classes
{
    class ObjectWithInventory : GameObject
    {
        public Inventory inventory;
        public bool inventoryVisible;

        protected void FillStacks()
        {
            foreach (ItemSlot s in inventory.Slots)
            {
                if (s.Item == null) continue;
                byte b = (byte)Game1.random.Next(s.Item.Properties.MaxStack);
                for (byte i = 0; i < b; i++)
                {
                    s.Add(s.Item);
                }
            }
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            if (inventoryVisible) inventory.Draw(spriteBatch);
            base.Draw(spriteBatch);
        }
    }
}
