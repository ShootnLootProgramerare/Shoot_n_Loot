using Microsoft.Xna.Framework;
using Shoot__n_Loot.Scenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shoot__n_Loot.Enemies
{
    class FatLady : Enemy
    {
        public override Rectangle MapCollider
        {
            get
            {
                const float w = .2f, h = .2f;
                return new Rectangle(
                    (int)(base.MapCollider.X + base.MapCollider.Width * (1 - w) * .125f),
                    (int)(base.MapCollider.Y + base.MapCollider.Height * (1 - h)),
                    (int)(base.MapCollider.Width * w),
                    (int)(base.MapCollider.Height * h));
            }
        }

        public FatLady(Vector2 position) 
            : base(position, TextureManager.fatLadyWalk, TextureManager.fatLadyAttack, TextureManager.deadLady)
        {
            SetGameplayVars(3, 1, 1, 100);
            SetAnimVars(new Point(100, 100), 4, .1f, 4, 4f / 60, 3);
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
                StartAttack();
                Velocity = Vector2.Zero;
            }
            base.Update();
        }

        protected override void OnDestroy()
        {
            SceneManager.CurrentScene.AddObject(new Baby(Position));
            SceneManager.CurrentScene.AddObject(new Baby(Position));
            base.OnDestroy();
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(TextureManager.gameOverBackground, MapCollider, Color.White * .5f);
            base.Draw(spriteBatch);
        }
    }
}
