using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shoot__n_Loot
{
    class DroppedItem : GameObject
    {
        public InventoryItem inventoryItem;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sprite"></param>
        /// <param name="item">the InventoryItem counterpart.</param>
        public DroppedItem(Sprite sprite, InventoryItem item)
        {
            this.Sprite = sprite;
            this.inventoryItem = item;
        }

        public override void Update()
        {
            if(Game1.player.Hitbox.Intersects(Hitbox) && Input.KeyWasJustPressed(Microsoft.Xna.Framework.Input.Keys.E))
            {
                //if(Game1.player.Inventory.Fits(this)) Game1.player.Inventory.Add(this);
            }
        }
    }
}
