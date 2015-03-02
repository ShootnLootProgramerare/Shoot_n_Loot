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
using Shoot__n_Loot.Scenes;
using Shoot__n_Loot.WeaponClasses;
using System.Diagnostics;

namespace Shoot__n_Loot
{
    internal class Enemy : GameObject
    {
        enum Direction { Up = 0, Down = 1, Left = 2, Right = 3} //number corresponds to index in array in texturemanager

        new public const string TYPE = "Enemy";
        public override string Type { get { return TYPE; } }

        public override Rectangle MapCollider { get { return new Rectangle(base.MapCollider.X + (int)(base.MapCollider.Width * .4f), base.MapCollider.Y + (int)(base.MapCollider.Height * .75f), (int)(base.MapCollider.Width / 5), (int)(base.MapCollider.Height * .25f)); } }
        public override Rectangle BulletCollider { get { return new Rectangle(base.BulletCollider.X + (int)(base.MapCollider.Width / 3f), base.MapCollider.Y, base.MapCollider.Width / 3, base.MapCollider.Height); } }

        public enum EnemyType { Fisherman = 1, enemy2 = 2, enemy3 = 3 };

        public int Damage { get; set; }
        public float Speed { get; set; }
        public EnemyType enemyType { get; set; }

        protected float range;
        protected bool attacking;

        byte walkFrames, attackFrames;
        float walkAnimSpeed, attackAnimSpeed;
        Point frameSize;

        Texture2D[] walkingAnims, attackAnims;

        Direction direction;

        public Enemy(Vector2 position, Texture2D[] walkingAnims, Texture2D[] attackAnims)
        {
            this.walkingAnims = walkingAnims;
            this.attackAnims = attackAnims;
            Sprite = new Sprite(walkingAnims[0], position, new Vector2(200, 100), 4, new Point(200, 100), 0); //maybe an overload for different sizes etc
        }


        /// <summary>
        /// set all the vars one might need for an enemy
        /// </summary>
        /// <param name="maxHealth"></param>
        /// <param name="damage"></param>
        /// <param name="speed"></param>
        /// <param name="range"></param>
        protected void SetGameplayVars(float maxHealth, int damage, float speed, float range)
        {
            this.MaxHealth = maxHealth;
            this.Damage = damage;
            this.Speed = speed;
            this.range = range;
        }

        protected void SetAnimVars(Point frameSize, byte walkFrames, float walkAnimSpeed, byte attackFrames, float attackAnimSpeed)
        {
            this.frameSize = frameSize;
            this.walkFrames = walkFrames;
            this.walkAnimSpeed = walkAnimSpeed;
            this.attackFrames = attackFrames;
            this.attackAnimSpeed = attackAnimSpeed;
        }

        /*public Enemy(Vector2 position, EnemyType enemytype)
        {
            this.enemyType = enemytype;
            
            Sprite = new Sprite(TextureManager.fishermanWalk[0], position, new Vector2(200, 100), 4, new Point(200, 100), 0); //TODO: should be type specific when we get sprites

            switch (enemytype)
            {
                case EnemyType.Fisherman:
                    this.MaxHealth = 3; 
                    this.Damage = 8; 
                    this.Speed = 1.2f;
                    this.range = 90;
                    break;
                case EnemyType.enemy2:
                    this.MaxHealth = 4; 
                    this.Damage = 2; 
                    this.Speed = 0.8f;
                    this.range = 90;
                    break;
                case EnemyType.enemy3:
                    this.MaxHealth = 6; 
                    this.Damage = 12; 
                    this.Speed = 2.4f;
                    this.range = 90;
                    break;
            }

            CanDie = true; //testing purposes, remove and use as example if you want
            ObstructsBullets = true;
        }*/

        public override void Update()
        {
            Animate();

            /*if (enemyType == EnemyType.Fisherman)
            {
                if (attacking)
                {
                    Attacking();
                    return;
                }
                else if (DistanceSquared(SceneManager.gameScene.player.Center) < Math.Pow(range, 2))
                {
                    Velocity = Vector2.Zero;
                    attacking = true;
                }
                else if (DistanceSquared(SceneManager.gameScene.player.Center) < 250000)
                {
                    Vector2 d = SceneManager.gameScene.player.Position - Position;
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
                else if (DistanceSquared(SceneManager.gameScene.player.Center) < Math.Pow(range, 2))
                {
                    Velocity = Vector2.Zero;
                    attacking = true;
                }
                else if (DistanceSquared(SceneManager.gameScene.player.Center) < 80000)
                {
                    Move(true);
                    Vector2 d = SceneManager.gameScene.player.Position - Position;
                    d.Normalize();
                    Velocity = d * 4;
                }
                else if (DistanceSquared(SceneManager.gameScene.player.Center) < 1000000)
                {
                    Move(true);
                    attacking = false;
                    Vector2 d = SceneManager.gameScene.player.Position - Position;
                    d.Normalize();
                    Velocity = d;
                }
            }

            if (enemyType == EnemyType.enemy3)
            {

            }*/

            foreach (GameObject g in SceneManager.currentScene.objects)
            {
                if (g.Type == Enemy.TYPE && g != this)
                {
                    if (g.DistanceSquared(Position) < 3000)
                    {
                        Vector2 v = g.Position - Position;
                        if (v == Vector2.Zero) v = new Vector2((float)Game1.random.NextDouble() - .5f, (float)Game1.random.NextDouble()- .5f);
                        v.Normalize();
                        Position += v * -1;
                        //Debug.WriteLine("moving zombie, distance = " + (g.Position - Position).Length());
                    }
                }
            }
        }

        /// <summary>
        /// checks if the animation is over and if so removes damage hp from the player if they are in range
        /// </summary>
        protected void Attacking()
        {
            if (Sprite.EndOfAnim)
            {
                attacking = false;

                if (DistanceSquared(SceneManager.gameScene.player.Center) <= range * range)
                {
                    SceneManager.gameScene.player.Health -= Damage;
                    SceneManager.gameScene.player.bleeding += .001f; // maybe this should be different for different zombies
                }
            }
        }

        protected void Animate()
        {
            if (Velocity.LengthSquared() > .3f)
            {
                Sprite.AnimationSpeed = walkAnimSpeed;
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
                Sprite.SetTexture(attackAnims[(int)direction], attackFrames, frameSize); //maybe global var for how many frames
                Sprite.AnimationSpeed = attackAnimSpeed;
            }
            else Sprite.SetTexture(walkingAnims[(int)direction], walkFrames, frameSize);
        }

        protected override void OnDestroy()
        {
            //create particles, spawn dropped items etc
            //SceneManager.gameScene.AddObject(new Enemy(new Vector2(400), EnemyType.Fisherman));
            Item r = Items.RandomItem(Position);
            int num = Game1.random.Next(r.Properties.MaxStack);
            for (int i = 0; i < num; i++)
            {
                SceneManager.gameScene.AddObject(r);
            } 
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(TextureManager.house, MapCollider, Color.White * .5f);
            base.Draw(spriteBatch);
        }
    }
}
