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
            else
            {
                Vector2 d = -1 * (Position - SceneManager.gameScene.player.Center);
                d.Normalize();
                Velocity = d * Speed;
                Move(true);
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
