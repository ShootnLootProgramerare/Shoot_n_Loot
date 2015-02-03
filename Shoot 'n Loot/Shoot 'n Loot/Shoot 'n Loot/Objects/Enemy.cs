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
        enum Direction { Up = 0, Down = 1, Left = 2, Right = 3} //number corresponds to index in array in texturemanager

        new public const string TYPE = "Enemy";
        public override string Type { get { return TYPE; } }

        public override Rectangle MapCollider { get { return new Rectangle(base.MapCollider.X + (int)(base.MapCollider.Width * .4f), base.MapCollider.Y + (int)(base.MapCollider.Height * .75f), (int)(base.MapCollider.Width / 5), (int)(base.MapCollider.Height * .25f)); } }

        public enum EnemyType { Fisherman = 1, enemy2 = 2, enemy3 = 3 };

        public int Damage { get; set; }
        public float Speed { get; set; }
        public EnemyType enemyType { get; set; }

        float range;

        bool attacking;

        Direction direction;

        public Enemy(Vector2 position, EnemyType enemytype)
        {
            this.enemyType = enemytype;
            
            Sprite = new Sprite(TextureManager.fishermanWalk[0], position, new Vector2(200, 100), 4, new Point(200, 100), 0); //TODO: should be type specific when we get sprites

            switch (enemytype)
            {
                case EnemyType.Fisherman:
                    this.Health = 32; 
                    this.Damage = 8; 
                    this.Speed = 1.2f;
                    this.range = 100;
                    break;
                case EnemyType.enemy2:
                    this.Health = 48; 
                    this.Damage = 2; 
                    this.Speed = 0.8f;
                    this.range = 150;
                    break;
                case EnemyType.enemy3:
                    this.Health = 12; 
                    this.Damage = 12; 
                    this.Speed = 2.4f;
                    break;
            }

            CanDie = true; //testing purposes, remove and use as example if you want
            ObstructsBullets = true;
        }

        public override void Update()
        {
            Animate();

            if (enemyType == EnemyType.Fisherman)
            {
                if (attacking)
                {
                    Attacking();
                    return;
                }
                else if (DistanceSquared(Game1.gameScene.player.Center) < Math.Pow(range, 2))
                {
                    Velocity = Vector2.Zero;
                    attacking = true;
                }
                else if (DistanceSquared(Game1.gameScene.player.Center) < 250000)
                {
                    Vector2 d = Game1.gameScene.player.Position - Position;
                    d.Normalize();
                    Velocity = d * 3;

                    Move(true);
                }
            }

            if (enemyType == EnemyType.enemy2)
            {
                if(attacking)
                {
                    Attacking();
                    return;
                }
                else if (DistanceSquared(Game1.gameScene.player.Center) < Math.Pow(range, 2))
                {
                    Velocity = Vector2.Zero;
                    attacking = true;
                }
                else if (DistanceSquared(Game1.gameScene.player.Center) < 80000)
                {
                    Move(true);
                    Vector2 d = Game1.gameScene.player.Position - Position;
                    d.Normalize();
                    Velocity = d * 4;
                }
                else if (DistanceSquared(Game1.gameScene.player.Center) < 1000000)
                {
                    Move(true);
                    attacking = false;
                    Vector2 d = Game1.gameScene.player.Position - Position;
                    d.Normalize();
                    Velocity = d;
                }
            }

            if (enemyType == EnemyType.enemy3)
            {

            }
        }

        private void Attacking()
        {
            if (Sprite.EndOfAnim)
            {
                attacking = false;

                if (DistanceSquared(Game1.gameScene.player.Center) < 40000) Game1.gameScene.player.Health -= Damage;
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
                    if (Velocity.X > 0) direction = Direction.Right;
                    else if (Velocity.X < 0) direction = Direction.Left;
                }
                else
                {
                    if (Velocity.Y > 0) direction = Direction.Down;
                    else if (Velocity.Y < 0) direction = Direction.Up;
                }
            }
            else if (!attacking)
            {
                Sprite.AnimationSpeed = 0;
                Sprite.Frame = 0;
            }

            if (attacking)
            {
                Sprite.SetTexture(TextureManager.fishermanAttack[(int)direction], 7, new Point(200, 100));
                Sprite.AnimationSpeed = 6f / 60;
            }
            else Sprite.SetTexture(TextureManager.fishermanWalk[(int)direction], 4, new Point(200, 100));
        }

        protected override void OnDestroy()
        {
            //create particles, spawn dropped items etc
            Game1.gameScene.AddObject(new Enemy(new Vector2(400), EnemyType.Fisherman));
            Game1.gameScene.AddObject(new Item(1, 1, .1f, new Sprite(TextureManager.medicineItem, Position, new Vector2(16))));
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(TextureManager.house, MapCollider, Color.White * .5f);
            base.Draw(spriteBatch);
        }
    }
}
