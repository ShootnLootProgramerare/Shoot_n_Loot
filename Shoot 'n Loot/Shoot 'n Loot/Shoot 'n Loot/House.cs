using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shoot__n_Loot
{
    class House : GameObject
    {
        public House()
        {
            Sprite = new Sprite(TextureManager.house, new Vector2(1000, 1000), new Vector2(200));
        }

        public override void Update()
        {
            if (Hitbox.Intersects(Game1.player.Hitbox))
            {
                if (Sprite.Alpha > 0) Sprite.Alpha -= .05f;
            }
            else
            {
                if(Sprite.Alpha < 1) Sprite.Alpha += .05f;
            }
        }
    }
}
