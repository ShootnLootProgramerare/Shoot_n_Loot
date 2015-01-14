using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shoot__n_Loot
{
    class Bullet : GameObject
    {
        const float speed = 3;
        const float w = 3, h = 6;

        Vector2 velocity;
        
        public Bullet(float angle, Vector2 position)
        {
            velocity = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * speed;
            Sprite = new Sprite(TextureManager.bullet, position, new Vector2(w, h), angle, null);
        }

        public void Update()
        {
            Position += velocity;
            if (!Camera.AreaIsVisible(Hitbox)) Dead = true;
        }
    }
}
