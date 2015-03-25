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
        const float nukeRadius = 100;
        const float jumpDist = 100;
        const int nukeDamage = 30;

        byte attacks;
        bool nuking;
        bool jumping;


        public Baby(Vector2 position)
            : base(position, TextureManager.babyWalk, TextureManager.babyAttack)
        {
            SetGameplayVars(1, 1, 3, 50);
            SetAnimVars(new Point(75, 75), 4, 4f / 60, 4, 10f / 60);
        }

        public override void Update()
        {
            if (attacking)
            {
                Attacking();
            }
            else if (DistanceSquared(SceneManager.gameScene.player.Center) < range * range)
            {
                nuking = true;
                Sprite.SetTexture(TextureManager.babyNuke[(int)VelDirection], 6, new Point(75, 75));
                Sprite.AnimationSpeed = 10f / 60;
            }
            else if (DistanceSquared(SceneManager.gameScene.player.Center) < sightRadius * sightRadius)
            {
                MoveTowardsPlayer(Speed);
                if (Math.Abs(DistanceSquared(SceneManager.gameScene.player.Center) - jumpDist * jumpDist) < 10 && !jumping)
                {
                    jumping = true;
                    walkingAnims = TextureManager.babyAttack;
                    Speed *= 2;
                    Debug.WriteLine("baby jumping");
                }
            }

            if (jumping && Sprite.EndOfAnim)
            {
                Sprite.AnimationSpeed /= 2;
                walkingAnims = TextureManager.babyWalk;
                jumping = false;
                Debug.WriteLine("baby stopped jumping");
            }

            if (nuking && Sprite.EndOfAnim)
            {
                foreach (Player p in SceneManager.gameScene.objects.Where(item => item is Player && item.DistanceSquared(Center) < nukeRadius * nukeRadius)) p.Health -= nukeDamage;
                SceneManager.CurrentScene.RemoveObject(this);
            }

            if (!nuking) base.Update(); //shouldnt animate more, the rest doesnt matter either
        }
    }
}
