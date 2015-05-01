using Microsoft.Xna.Framework.Graphics;
using Shoot__n_Loot.Objects;
using Shoot__n_Loot.Scenes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Shoot__n_Loot
{
    static class EnemyInvetoryRenderer
    {
        public static DeadEnemy closest;
        static float dist;

        public static void Init() 
        {
            closest = null;
            dist = float.MaxValue;
        }

        public static void Submit(DeadEnemy e)
        {
            Debug.WriteLine("enemy submitted");
            float d = SceneManager.gameScene.player.DistanceSquared(e.Center);
            if (d < dist)
            {
                Debug.WriteLine("it was best");
                dist = d;
                closest = e;
            }
        }

        public static void Update()
        {
            if (closest != null)
            {
                Debug.WriteLine("updating eir");
                closest.UpdateInventory();
            }
        }

        public static void Draw(SpriteBatch batch)
        {
            if (closest != null && SceneManager.gameScene.player.inventoryVisible) closest.inventory.Draw(batch);
        }
    }
}
