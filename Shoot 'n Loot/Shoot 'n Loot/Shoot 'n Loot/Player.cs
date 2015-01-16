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

        public override Rectangle Hitbox { get { return new Rectangle(base.Hitbox.X + 8, base.Hitbox.Y + (int)(base.Hitbox.Height * .75f), base.Hitbox.Width - 16, (int)(base.Hitbox.Height * .25f)); } }

        public List<Bullet> Bullets { get; set; }

        public Player()
        {
            Sprite = new Sprite(TextureManager.playerRight, new Vector2(500), new Vector2(50), 4, new Point(50, 50), 0);
            Bullets = new List<Bullet>();
        }

        new public void Update()
        {
            Move();
            Animate();
            Shoot();
        }

        void Move()
        {
            Vector2 acceleration = Vector2.Zero;
            if (Input.newKs.IsKeyDown(Keys.A)) acceleration.X -= 1;
            if (Input.newKs.IsKeyDown(Keys.D)) acceleration.X += 1;
            if (Input.newKs.IsKeyDown(Keys.W)) acceleration.Y -= 1;
            if (Input.newKs.IsKeyDown(Keys.S)) acceleration.Y += 1;
            Velocity += acceleration * accelerationMult;
            Velocity *= friction;
            if (Math.Abs(Velocity.X) < .05) Velocity = new Vector2(0, Velocity.Y);
            if (Math.Abs(Velocity.Y) < .05) Velocity = new Vector2(Velocity.X, 0);

            MoveWithTileCollision();
        }

        void Animate()
        {
            if (Velocity.LengthSquared() > .3f)
            {
                Sprite.AnimationSpeed = 9f / 60;
                if (Math.Abs(Velocity.X) > Math.Abs(Velocity.Y))
                {
                    //left and right movement
                    if (Velocity.X > 0) Sprite.SetTexture(TextureManager.playerRight, 4, new Point(50, 50));
                    else if (Velocity.X < 0) Sprite.SetTexture(TextureManager.playerLeft, 4, new Point(50, 50));
                }
                else
                {
                    //Sprite.AnimationSpeed = 0;
                    if (Velocity.Y > 0) Sprite.SetTexture(TextureManager.playerDown, 4, new Point(50, 50));
                    else if (Velocity.Y < 0) Sprite.SetTexture(TextureManager.playerUp, 4, new Point(50, 50));
                }
            }
            else
            {
                Sprite.AnimationSpeed = 0;
                Sprite.Frame = 0;
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
                    Game1.objects.Add(new Bullet((float)Math.Atan2(v.Y, v.X), this.Center));
                }
            }
        }

        new public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Bullet b in Bullets) b.Draw(spriteBatch);
            base.Draw(spriteBatch);
        }
    }
}
