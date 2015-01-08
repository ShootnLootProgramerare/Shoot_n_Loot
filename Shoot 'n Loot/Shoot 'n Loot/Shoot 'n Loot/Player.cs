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
        private const float accelerationMult = 1, friction = .87f;

        Vector2 velocity;

        public Player()
        {
            Sprite = new Sprite(Textures.player, new Vector2(100), new Vector2(100), 2, new Point(16, 16), 0);
        }

        new public void Update()
        {
            Move();
            Animate();
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
            Position += velocity;
        }

        void Animate()
        {
            if (velocity.LengthSquared() > .3f)
            {
                Sprite.AnimationSpeed = 9f / 60;
                if (Math.Abs(velocity.X) > Math.Abs(velocity.Y))
                {
                    //left and right movement
                    if (velocity.X > 0) Sprite.SpriteEffects = SpriteEffects.None; //maybe should be seperate sprites?
                    else if (velocity.X < 0) Sprite.SpriteEffects = SpriteEffects.FlipHorizontally;
                }
                else
                {
                    Sprite.AnimationSpeed = 0;
                    /*
                    if (velocity.Y > 0) set sprite to moving down
                    else if (velocity.Y < 0) set sprite to moving up
                     */
                }
            }
            else
            {
                Sprite.AnimationSpeed = 0;
                Sprite.Frame = 0;
            }
        }
    }
}
