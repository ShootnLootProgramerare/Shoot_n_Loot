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
            Sprite = new Sprite(TextureManager.house, new Vector2(48 * 20) - new Vector2(0, 48 * 5), new Vector2(48 * 5));
            Sprite.Origin = Vector2.Zero;
        }

        public override void Update()
        {
            if (MapCollider.Intersects(Game1.gameScene.player.MapCollider))
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
