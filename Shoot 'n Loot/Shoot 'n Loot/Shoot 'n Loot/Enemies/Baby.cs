using Microsoft.Xna.Framework;
using Shoot__n_Loot.Scenes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Shoot__n_Loot.Enemies
{
    class Baby : Enemy
    {
        const float sightRadius = 300;
        const float nukeRadius = 50;
        const int nukeDamage = 30;

        byte attacks;
        bool nuking;


        public Baby(Vector2 position)
            : base(position, TextureManager.babyWalk, TextureManager.babyAttack)
        {
            SetGameplayVars(3, 1, 1, 50);
            SetAnimVars(new Point(75, 75), 4, 4f / 60, 4, 10f / 60);
        }

        public override void Update()
        {
            if (attacking)
            {
                Attacking();
            }
            else if (attacks > 3 && !nuking)
            {
                nuking = true;
                Debug.WriteLine("nuke incoming");
                Sprite.SetTexture(TextureManager.babyNuke[(int)VelDirection], 6, new Point(75, 75));
                Sprite.AnimationSpeed = 10f / 60;
            }
            else if (DistanceSquared(SceneManager.gameScene.player.Center) < range * range)
            {
                attacking = true;
                attacks++;
            }
            else if (DistanceSquared(SceneManager.gameScene.player.Center) < sightRadius * sightRadius)
            {
                MoveTowardsPlayer(Speed);
            }

            if (nuking && Sprite.EndOfAnim)
            {
                foreach (Player p in SceneManager.gameScene.objects.Where(item => item is Player && item.DistanceSquared(Center) < nukeRadius * nukeRadius)) p.Health -= nukeDamage;
                SceneManager.CurrentScene.RemoveObject(this);
            }

            if (!nuking) base.Update();
        }
    }
}
