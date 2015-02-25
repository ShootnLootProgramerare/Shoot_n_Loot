using Microsoft.Xna.Framework;
using Shoot__n_Loot.Scenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shoot__n_Loot.Enemies
{
    class Fisherman : Enemy
    {
        public Fisherman(Vector2 position)
        {
            Sprite = new Sprite(TextureManager.fishermanWalk[0], position, new Vector2(200, 100), 4, new Point(200, 100), 0);

            this.MaxHealth = 3;
            this.Damage = 8;
            this.Speed = 1.2f;
            this.range = 90;
        }

        public override void Update()
        {
            if (attacking)
            {
                Attacking();
                return;
            }
            else if (DistanceSquared(SceneManager.gameScene.player.Center) < Math.Pow(range, 2))
            {
                Velocity = Vector2.Zero;
                attacking = true;
            }
            else if (DistanceSquared(SceneManager.gameScene.player.Center) < 250000)
            {
                Vector2 d = SceneManager.gameScene.player.Position - Position;
                d.Normalize();
                Velocity = d * 3;

                Move(true);
            }

            base.Update();
        }
    }
}
