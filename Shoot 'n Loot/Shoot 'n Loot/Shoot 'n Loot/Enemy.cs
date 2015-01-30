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
    internal class Enemy : GameObject
    {
        public const string TYPE = "Enemy";
        public override string Type { get { return TYPE; } }

        public enum EnemyType { Fisherman, enemy2, enemy3 };

        public int Damage { get; set; }
        public float Speed { get; set; }
        public EnemyType enemyType { get; set; }

        public Enemy(Vector2 position, EnemyType enemytype)
        {
            this.enemyType = enemytype;

            if (enemyType == EnemyType.Fisherman) { Sprite = new Sprite(TextureManager.enemy1, position, new Vector2(50)); }
            if (enemyType == EnemyType.enemy2) { Sprite = new Sprite(TextureManager.enemy2, position, new Vector2(50)); }
            if (enemyType == EnemyType.enemy3) { Sprite = new Sprite(TextureManager.enemy3, position, new Vector2(50)); }

            if (enemyType == EnemyType.Fisherman) { this.Health = 32; this.Damage = 8; this.Speed = 1.2f; }
            if (enemyType == EnemyType.enemy2) { this.Health = 48; this.Damage = 2; this.Speed = 0.8f; }
            if (enemyType == EnemyType.enemy3) { this.Health = 12; this.Damage = 12; this.Speed = 2.4f; }

            CanDie = true; //testing purposes, remove and use as example if you want
            ObstructsBullets = true;
            Health = 2;
        }

        public override void Update()
        {
            Animate();

            if (enemyType == EnemyType.Fisherman)
            {

                if (DistanceSquared(Game1.gameScene.player.Center) < 250000)
                {
                    Move(true);

                    Vector2 d = Game1.gameScene.player.Position - Position;
                    d.Normalize();
                    Velocity = d * 3;
                }
            }

            if (enemyType == EnemyType.enemy2)
            {
                if (DistanceSquared(Game1.gameScene.player.Center) < 80000)
                {
                    Move(true);

                    Vector2 d = Game1.gameScene.player.Position - Position;
                    d.Normalize();
                    Velocity = d * 4;
                }
                else if (DistanceSquared(Game1.gameScene.player.Center) < 1000000)
                {
                    Move(true);

                    Vector2 d = Game1.gameScene.player.Position - Position;
                    d.Normalize();
                    Velocity = d;
                }
            }

            if (enemyType == EnemyType.enemy3)
            {

            }
        }

        private void Animate()
        {
            if (Velocity.LengthSquared() > .3f)
            {
                Sprite.AnimationSpeed = 9f / 60;
                if (Math.Abs(Velocity.X) > Math.Abs(Velocity.Y))
                {
                    //left and right movement
                    if (Velocity.X > 0) Sprite.SetTexture(TextureManager.fishermanRight, 4, new Point(100, 50));
                    else if (Velocity.X < 0) Sprite.SetTexture(TextureManager.fishermanLeft, 4, new Point(100, 50));
                }
                else
                {
                    if (Velocity.Y > 0) Sprite.SetTexture(TextureManager.fishermanDown, 4, new Point(100, 50));
                    else if (Velocity.Y < 0) Sprite.SetTexture(TextureManager.fishermanUp, 4, new Point(100, 50));
                }
            }
            else
            {
                Sprite.AnimationSpeed = 0;
                Sprite.Frame = 0;
            }
        }

        protected override void OnDestroy()
        {
            //create particles, spawn dropped items etc
            Game1.gameScene.AddObject(new Enemy(new Vector2(400), EnemyType.Fisherman));
            Game1.gameScene.AddObject(new Item(1, 1, .1f, new Sprite(TextureManager.medicineItem, Position, new Vector2(16))));
        }
    }
}
