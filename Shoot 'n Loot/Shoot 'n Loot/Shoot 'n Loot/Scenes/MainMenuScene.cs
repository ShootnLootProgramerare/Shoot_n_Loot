using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Shoot__n_Loot.Scenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shoot__n_Loot
{
    class MainMenuScene : Scene
    {
        public MainMenuScene()
        {
            base.Initialize();
        }

        public override void Update()
        {
            if (Input.newKs.IsKeyDown(Keys.Enter)) SceneManager.currentScene = SceneManager.gameScene;
            else if (Input.KeyWasJustPressed(Keys.Escape)) SceneManager.currentScene = SceneManager.aboutScene;

            base.Update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            string s = "Press Enter for game or Escape for info";
            spriteBatch.DrawString(TextureManager.font, s, Vector2.Zero - TextureManager.font.MeasureString(s) / 2, Color.Black);
            base.Draw(spriteBatch);
        }
    }
}
