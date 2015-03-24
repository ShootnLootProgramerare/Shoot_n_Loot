using Microsoft.Xna.Framework;
using Shoot__n_Loot.Scenes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Shoot__n_Loot.Objects
{
    class Boat : GameObject
    {
        const byte REQUIRED_FUEL = 1;
        byte fuel;

        public Boat(Vector2 position)
        {
            Sprite = new Sprite(TextureManager.boat, position, new Vector2(200, 100), 2, new Point(200, 100), 0);
        }

        public override void Update()
        {
            foreach (Item item in SceneManager.gameScene.objects.Where(item => item is Item)) //yo dawg, i heard you like items..
            {
                if (item.Properties.InfoText.Contains("Fuel"))
                {
                    SceneManager.gameScene.RemoveObject(item);
                    fuel++;
                    Debug.WriteLine("boat got fuel, level: " + fuel);
                }
            }

            if (fuel >= REQUIRED_FUEL && MapCollider.Intersects(SceneManager.gameScene.player.MapCollider))
            {
                //end the game
                Velocity = new Vector2(10, 0);
                SceneManager.gameScene.RemoveObject(SceneManager.gameScene.player);
                Sprite.Frame = 1;
            }

            Position += Velocity;

            if (Velocity.Length() > 0)
            {
                Camera.Follow(Center);
            }

            base.Update();
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
