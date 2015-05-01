using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Shoot__n_Loot.Base_Classes;
using Shoot__n_Loot.Scenes;
using Shoot__n_Loot.WeaponClasses;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Shoot__n_Loot.Objects
{
    class DeadEnemy : ObjectWithInventory
    {
        const int MAX_LIFE = 3600;
        int lifeTime;

        public DeadEnemy(Texture2D texture, Vector2 position) 
        {
            Sprite = new Sprite(texture, position, new Vector2(texture.Width, texture.Height));
            inventory = new Inventory(this, new Point(200, 0), 2, 2, 10);
            for (int i = 0; i < 3; i++) inventory.Add(Items.RandomItem(Position));
            FillStacks();
        }

        public void UpdateInventory()
        {
            Debug.WriteLine("deadenemy inventory was updated");
            base.Update(); 
        }

        public override void Update()
        {
            lifeTime++;
            if (lifeTime > MAX_LIFE) SceneManager.CurrentScene.RemoveObject(this);
            if (Sprite.Area.Intersects(SceneManager.gameScene.player.Sprite.Area)) EnemyInvetoryRenderer.Submit(this);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Sprite.Draw(spriteBatch);
        }
    }
}
