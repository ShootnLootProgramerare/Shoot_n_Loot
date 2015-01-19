using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Shoot__n_Loot
{
    internal class Enemy: GameObject
    {
        public enum EnemyType { enemy1, enemy2, enemy3 };

        public int Damage { get; set; }
        public float Speed { get; set; }
        public EnemyType enemyType { get; set; }

        public Enemy(Vector2 position, EnemyType enemytype)
        {
            this.enemyType = enemytype;

            if (enemyType == EnemyType.enemy1) { Sprite = new Sprite(TextureManager.enemy1, position, new Vector2(50)); }
            if (enemyType == EnemyType.enemy2) { Sprite = new Sprite(TextureManager.enemy2, position, new Vector2(50)); }
            if (enemyType == EnemyType.enemy3) { Sprite = new Sprite(TextureManager.enemy3, position, new Vector2(50)); }

            if (enemyType == EnemyType.enemy1) { this.Health = 32; this.Damage = 8; this.Speed = 1.2f; }
            if (enemyType == EnemyType.enemy2) { this.Health = 48; this.Damage = 2; this.Speed = 0.8f; }
            if (enemyType == EnemyType.enemy3) { this.Health = 12; this.Damage = 12; this.Speed = 2.4f; }

            CanDie = true; //testing purposes, remove and use as example if you want
            ObstructsBullets = true;
            Health = 2;
        }

        public override void Update()
        {

            if (enemyType == EnemyType.enemy1)
            {

                if (DistanceSquared(Game1.player.Center) < 250000)
                {
                    Move(true);

                    Vector2 d = Game1.player.Position - Position;
                    d.Normalize();
                    Velocity = d * 3;
                }
            }

            if (enemyType == EnemyType.enemy2)
            {
                for (int i = 0; i < Game1.objects.Count; i++)
                {
                    if (DistanceSquared(Game1.objects[i].Position) < 1000)
                    {
                        Move(true);

                        Vector2 d = Game1.player.Position - Position;
                        d.Normalize();
                        Velocity = d;
                    }
                }
            }

            if (enemyType == EnemyType.enemy3)
            {

            }
        }

        protected override void OnDestroy()
        {
            //create particles, spawn dropped items etc
            Game1.objectsToAdd.Add(new Enemy(new Vector2(400), EnemyType.enemy1));
        }
    }
}
