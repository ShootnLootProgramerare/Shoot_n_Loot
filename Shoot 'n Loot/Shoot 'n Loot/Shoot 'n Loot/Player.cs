using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shoot__n_Loot
{
    class Player : GameObject
    {
        private const float accelerationMult = .7f, friction = .87f;

        Vector2 velocity;

        Rectangle Hitbox { get { return new Rectangle(base.Hitbox.X + 8, base.Hitbox.Y + (int)(base.Hitbox.Height * .75f), base.Hitbox.Width - 16, (int)(base.Hitbox.Height * .25f)); } }

        public List<Bullet> Bullets { get; set; }

        public Player()
        {
            Sprite = new Sprite(TextureManager.playerHorizontal, new Vector2(500), new Vector2(50), 2, new Point(16, 16), 0);
            Bullets = new List<Bullet>();
        }

        new public void Update()
        {
            Move();
            Animate();
            Shoot();
            UpdateBullets();
        }

        void Move()
        {
            Vector2 acceleration = Vector2.Zero;
            if (Input.newKs.IsKeyDown(Keys.A)) acceleration.X -= 1;
            if (Input.newKs.IsKeyDown(Keys.D)) acceleration.X += 1;
            if (Input.newKs.IsKeyDown(Keys.W)) acceleration.Y -= 1;
            if (Input.newKs.IsKeyDown(Keys.S)) acceleration.Y += 1;
            velocity += acceleration * accelerationMult;
            velocity *= friction;
            if (Math.Abs(velocity.X) < .05) velocity.X = 0;
            if (Math.Abs(velocity.Y) < .05) velocity.Y = 0;

            List<Tile> solidTiles = CloseSolidTiles;
            Move(velocity.X, 0);
            int x = velocity.X.CompareTo(0);
            while (IsCollidingWithAny(solidTiles, Hitbox))
            {
                Move(-x, 0);
                velocity.X = 0;
            }
            Move(0, velocity.Y);
            int y = velocity.Y.CompareTo(0);
            while (IsCollidingWithAny(solidTiles, Hitbox))
            {
                Move(0, -y);
                velocity.Y = 0;
            }
        }

        void Animate()
        {
            if (velocity.LengthSquared() > .3f)
            {
                Sprite.AnimationSpeed = 9f / 60;
                if (Math.Abs(velocity.X) > Math.Abs(velocity.Y))
                {
                    //left and right movement
                    Sprite.SetTexture(TextureManager.playerHorizontal, 2, new Point(16, 16));
                    if (velocity.X > 0) Sprite.SpriteEffects = SpriteEffects.None; //maybe should be seperate sprites?
                    else if (velocity.X < 0) Sprite.SpriteEffects = SpriteEffects.FlipHorizontally;
                }
                else
                {
                    //Sprite.AnimationSpeed = 0;
                    if (velocity.Y > 0) Sprite.SetTexture(TextureManager.playerDown, 3, new Point(16, 16));
                    else if (velocity.Y < 0) Sprite.SetTexture(TextureManager.playerUp, 2, new Point(16, 16));
                }
            }
            else
            {
                Sprite.AnimationSpeed = 0;
                Sprite.Frame = 2;
            }
        }

        void Shoot()
        {
            if (Input.LeftClickWasJustPressed())
            {
                //check ammo etc
                if (true)
                {
                    Vector2 v = Input.MousePosition - Center;
                    Bullets.Add(new Bullet((float)Math.Atan2(v.Y, v.X), this.Center));
                }
            }
        }

        void UpdateBullets()
        {
            List<GameObject> objects = new List<GameObject>(); //get enemies, solid items etc

            for (int i = Bullets.Count - 1; i >= 0; i--)
            {
                if (Bullets[i].Dead) Bullets.RemoveAt(i);
                else Bullets[i].Update();
            }
        }

        new public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Bullet b in Bullets) b.Draw(spriteBatch);
            base.Draw(spriteBatch);
        }
    }
}
