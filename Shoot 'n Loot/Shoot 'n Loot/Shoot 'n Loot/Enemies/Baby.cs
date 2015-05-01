using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Shoot__n_Loot.Objects;
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
        const float sightRadius = 600;
        const float nukeRadius = 100;
        const float jumpDist = 250;
        const int nukeDamage = 30;
        const int pauseTime = 60;

        byte attacks;
        bool nuking;
        bool jumping;
        bool wasKilled;

        int pauseCounter;

        public Baby(Vector2 position)
            : base(position, TextureManager.babyWalk, TextureManager.babyAttack, null)
        {
            SetGameplayVars(1, 1, 3, 50);
            SetAnimVars(new Point(75, 75), 4, 4f / 60, 4, 15f / 60, 3);
            Sprite.Frame = 0;
        }

        protected override void OnTakeDamage(float amount)
        {
            if (Health <= 0) wasKilled = true;
            base.OnTakeDamage(amount);
        }

        protected override void OnDestroy()
        {
            SceneManager.CurrentScene.AddObject(new BabyExplosion(this, !wasKilled));
            Debug.WriteLine("ondestroy on baby");
            base.OnDestroy();
        }

        public override void Update()
        {
            if (pauseCounter < pauseTime)
            {
                pauseCounter++;
                return;
            }

            CanDie = pauseCounter >= pauseTime;

            if (DistanceSquared(SceneManager.gameScene.player.Center) < range * range && !nuking)
            {
                Health = 0;
                Sprite.Frame = 0;
                Sprite.AnimationSpeed = 15f / 60;
            }
            else if (DistanceSquared(SceneManager.gameScene.player.Center) < sightRadius * sightRadius && !nuking)
            {
                MoveTowardsPlayer(Speed);
                if (Math.Abs(DistanceSquared(SceneManager.gameScene.player.Center) - jumpDist * jumpDist) < 30 && !jumping)
                {
                    jumping = true;
                    walkingAnims = TextureManager.babyAttack;
                    Sprite.Frame = 0;
                    Speed *= 2;
                }
            }

            if (jumping && Sprite.EndOfAnim)
            {
                Sprite.AnimationSpeed /= 2;
                walkingAnims = TextureManager.babyWalk;
                Sprite.Frame = 0;
                jumping = false;
            }

            base.Update(); //shouldnt animate more, the rest doesnt matter either
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (pauseCounter >= pauseTime) base.Draw(spriteBatch);
            //spriteBatch.DrawString(TextureManager.font, jumping.ToString() + "\n" + Math.Sqrt(DistanceSquared(SceneManager.gameScene.player.Center)), Center, Color.Black);
        }
    }
}