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
        }
    }
}
