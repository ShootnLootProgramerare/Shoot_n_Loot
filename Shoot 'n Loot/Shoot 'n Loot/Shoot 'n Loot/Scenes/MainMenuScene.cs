using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
            if (Input.newKs.IsKeyDown(Keys.Enter)) Game1.currentScene = Game1.gameScene;

            base.Update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            string s = "Press Enter";
            spriteBatch.DrawString(TextureManager.font, s, Vector2.Zero - TextureManager.font.MeasureString(s) / 2, Color.Black);
            base.Draw(spriteBatch);
        }
    }
}
