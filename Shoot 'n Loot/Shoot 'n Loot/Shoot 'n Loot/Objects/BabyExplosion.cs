using Microsoft.Xna.Framework;
using Shoot__n_Loot.Enemies;
using Shoot__n_Loot.Scenes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Shoot__n_Loot.Objects
{
    class BabyExplosion : GameObject
    {
        const float explosionRadius = 200;
        const float damage = 20;
        bool doesDamage;

        public BabyExplosion(Baby b, bool doesDamage)
        {
            Sprite = new Sprite(TextureManager.babyNuke[(int)b.VelDirection], b.Position, new Vector2(75), 6, new Point(75, 75), 1f / 6);
            this.doesDamage = doesDamage;
        }

        public override void Update()
        {
            if (Sprite.EndOfAnim)
            {
                if (DistanceSquared(SceneManager.gameScene.player.Center) <= explosionRadius * explosionRadius)
                {
                    SoundManager.playerHurt.Play();
                    SceneManager.gameScene.player.Health -= damage * (doesDamage ? 1 : .3f);
                    SceneManager.gameScene.player.bleeding += .001f; // maybe this should be different for different zombies
                }
                SceneManager.CurrentScene.RemoveObject(this);
            }
            base.Update();
        }
    }
}
