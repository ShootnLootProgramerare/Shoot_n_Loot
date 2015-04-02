using Microsoft.Xna.Framework;
using Shoot__n_Loot.Base_Classes;
using Shoot__n_Loot.Scenes;
using Shoot__n_Loot.WeaponClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shoot__n_Loot.Objects
{
    class ItemContainer : ObjectWithInventory
    {
        const int randomItems = 4;
        public ItemContainer(Vector2 position)
        {
            Sprite = new Sprite(TextureManager.chest, position, new Vector2(100));
            inventory = new Inventory(this, new Point(-200, 0), 3, 3, 10);
            for (int i = 0; i < randomItems; i++)
                inventory.Add(Items.RandomItem(Position));
            FillStacks();
        }

        public override void Update()
        {
            if (DistanceSquared(SceneManager.gameScene.player.Center) < 2500)
            {
                inventoryVisible = true;
            }
            else inventoryVisible = false;

            base.Update();
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            if (inventoryVisible) inventory.Draw(spriteBatch);
            base.Draw(spriteBatch);
        }
    }
}
