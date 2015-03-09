using Microsoft.Xna.Framework;
using Shoot__n_Loot.Scenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shoot__n_Loot.Enemies
{
    class Onelegged : Enemy
    {
        const int mw = 30, mh = 15;
        public override Rectangle MapCollider { get { return new Rectangle((int)(Position.X - mw / 2), (int)(Position.Y + Size.Y / 2 - mh), mw, mh); } }
        public override Rectangle BulletCollider { get { return new Rectangle(base.BulletCollider.X + (int)(base.MapCollider.Width / 3f), base.MapCollider.Y, base.MapCollider.Width / 3, base.MapCollider.Height); } }


        public Onelegged(Vector2 position)
            : base(position, TextureManager.oneleggedWalk, TextureManager.oneleggedAttack)
        {
            SetGameplayVars(10, 1, .7f, 100);
            SetAnimVars(new Point(100, 150), 4, 9f / 60, 4, 6f / 60);
        }

        public override void Update()
        {
            if (attacking)
            {
                Attacking();
            }
            else if (DistanceSquared(SceneManager.gameScene.player.Center) < range * range)
            {
                attacking = true;
            }
            else if (DistanceSquared(SceneManager.gameScene.player.Center) < 300000)
            {
                MoveTowardsPlayer(4 * Speed);
            }
            else
            {
                MoveTowardsPlayer(Speed);
            }

            base.Update();
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            //spriteBatch.DrawString(TextureManager.font, (DistanceSquared(SceneManager.gameScene.player.Center) - (range * range)).ToString(), Position, Color.Black);
            base.Draw(spriteBatch);
        }
    }
}
