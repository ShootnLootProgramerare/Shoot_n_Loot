using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Shoot__n_Loot.Objects
{
    class Boat : GameObject
    {
        public Boat(Vector2 position)
        {
            Sprite = new Sprite(TextureManager.boat, position, new Vector2(200, 100));
            Debug.WriteLine("boat constructed");
            CanDie = false;
            Health = 100;
        }

        public override void Update()
        {
            Debug.WriteLine("updating boat");
        }

        protected override void OnDestroy()
        {
            Debug.WriteLine("boat was destroyed");
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            Debug.WriteLine("drawing boat");
            base.Draw(spriteBatch);
        }
    }
}
