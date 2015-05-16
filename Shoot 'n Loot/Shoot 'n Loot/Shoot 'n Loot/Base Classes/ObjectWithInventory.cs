using Microsoft.Xna.Framework;
using Shoot__n_Loot.InvenoryStuff;
using Shoot__n_Loot.Scenes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Shoot__n_Loot.Base_Classes
{
    class ObjectWithInventory : GameObject
    {
        public Inventory inventory;
        public bool inventoryVisible;
        protected bool isPlayerInventory;

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

        public override void Update()
        {
            if (inventoryVisible || EnemyInvetoryRenderer.closest == this) inventory.Update(new Point(0, 0), isPlayerInventory);
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch, bool drawInventory)
        {
            if (inventoryVisible && SceneManager.gameScene.player.inventoryVisible && drawInventory) inventory.Draw(spriteBatch);
            base.Draw(spriteBatch);
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            this.Draw(spriteBatch, true);
        }
    }
}
