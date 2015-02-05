using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shoot__n_Loot.Scenes
{
    class PauseScene : Scene
    {
        public PauseScene()
        {
            base.Initialize();
        }

        public override void Update()
        {
            if (Input.KeyWasJustPressed(Keys.Escape)) SceneManager.currentScene = SceneManager.gameScene;
            Camera.Follow(Vector2.Zero);
            base.Update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(TextureManager.font, "paused", - TextureManager.font.MeasureString("paused") / 2, Color.Black);
            base.Draw(spriteBatch);
        }
    }
}
