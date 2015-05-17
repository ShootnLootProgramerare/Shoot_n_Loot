using Microsoft.Xna.Framework;
using Shoot__n_Loot.Scenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shoot__n_Loot.Objects
{
    class LightHouse : GameObject
    {
        public LightHouse(Vector2 position)
        {
            Sprite = new Sprite(TextureManager.lightHouse, position, new Vector2(200, 528));
        }

        public override void Update()
        {
            if (MapCollider.Intersects(SceneManager.gameScene.player.MapCollider))
            {
                if (Sprite.Alpha == 1) SoundManager.door.Play();
                if (Sprite.Alpha > .4f) { Sprite.Alpha -= .05f; }
            }
            else
            {
                if (Sprite.Alpha <= .4f) SoundManager.door.Play(); 
                if (Sprite.Alpha < 1) { Sprite.Alpha += .05f; }
            }
        }
    }
}
