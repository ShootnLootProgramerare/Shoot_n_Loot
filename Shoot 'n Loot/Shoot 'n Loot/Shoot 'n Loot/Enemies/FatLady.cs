﻿using Microsoft.Xna.Framework;
using Shoot__n_Loot.Scenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shoot__n_Loot.Enemies
{
    class FatLady : Enemy
    {
        public FatLady(Vector2 position) 
            : base(position, TextureManager.fatLadyWalk, TextureManager.fatLadyAttack)
        {
            SetGameplayVars(3, 1, 1, 100);
            SetAnimVars(new Point(100, 100), 4, .1f, 4, .1f);
        }

        public override void Update()
        {
            if (attacking)
            {
                Attacking();
            }
            else if (DistanceSquared(SceneManager.gameScene.player.Center) > range * range)
            {
                MoveTowardsPlayer(Speed);
            }
            else
            {
                attacking = true;
                Velocity = Vector2.Zero;
            }
            base.Update();
        }
    }
}
