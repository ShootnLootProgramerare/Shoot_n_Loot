using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shoot__n_Loot
{
    class Bullet : GameObject
    {
        const float speed = 15;
        const float w = 6, h = 3;

        Vector2 velocity;

        public BulletProperties Properties { get; private set; }
        
        public Bullet(float angle, Vector2 position, BulletProperties properties)
        {
            velocity = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * speed;
            Sprite = new Sprite(TextureManager.bullet, position, new Vector2(w, h), angle, null);
            Sprite.LayerDepth = 0;
            this.Properties = properties;
        }

        public override void Update()
        {
            Position += velocity;

            //List<GameObject> objects = new List<GameObject>();
            //foreach (Enemy e in Game1.enemies) objects.Add((GameObject)e);

            foreach(GameObject g in Game1.objects)
            {
                if (g.ObstructsBullets)
                {
                    if (Hitbox.Intersects(g.Hitbox))
                    {
                        g.Health -= 1; //this should have a damage property
                        this.Dead = true;
                    }
                }
            }
            foreach(Tile t in CloseSolidTiles)
            {
                if (t.Properties.ObstructsBullets)
                {
                    if (Hitbox.Intersects(t.Hitbox))
                    {
                        this.Dead = true;
                    }
                }
            }
            if (!Camera.AreaIsVisible(Hitbox)) Dead = true;
        }
    }
}
