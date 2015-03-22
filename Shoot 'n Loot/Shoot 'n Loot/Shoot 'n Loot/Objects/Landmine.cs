using Microsoft.Xna.Framework;
using Shoot__n_Loot.Scenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shoot__n_Loot.Objects
{
    class Landmine : GameObject
    {
        const float range = 50;

        public Landmine(Vector2 position)
        {
            Sprite = new Sprite(TextureManager.landmine, position, new Vector2(38, 18), 2, new Point(38, 18), 1 / 60f);
        }

        public override void Update()
        {
            foreach (GameObject g in SceneManager.currentScene.objects.Where(item => item is Enemy))
            {
                if (DistanceSquared(g.Center) < range * range)
                {
                    //TODO: spawn new explosion
                    SceneManager.currentScene.AddObject(new Explosion(Center));
                    SceneManager.currentScene.RemoveObject(this);
                    g.Health -= 10;
                }
            }
        }
    }
}
