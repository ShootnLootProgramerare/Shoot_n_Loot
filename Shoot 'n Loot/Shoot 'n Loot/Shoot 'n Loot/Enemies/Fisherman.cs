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
            : base(position, TextureManager.fishermanWalk, TextureManager.fishermanAttack)
        {
            SetGameplayVars(3, 8, 1.2f, 90);
            SetAnimVars(new Point(200, 100), 4, 9f / 60, 5, 6f / 60);
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
                Velocity = d * Speed;

                Move(true);
            }

            base.Update();
        }
    }
}
