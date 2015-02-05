using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shoot__n_Loot.Scenes
{
    class AboutScene : Scene
    {
        const string text =
            "WASD to move\nLeft click to shoot\nE to pick up items\nI to view inventory\nR to reload\n\nEscape to return to main menu";

        public AboutScene()
        {
            base.Initialize();  
        }

        public override void Update()
        {
            if (Input.KeyWasJustPressed(Keys.Escape)) Game1.currentScene = Game1.mainMenuScene;

            base.Update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(TextureManager.font, text, -TextureManager.font.MeasureString(text) / 2, Microsoft.Xna.Framework.Color.Black);
            base.Draw(spriteBatch);
        }
    }
}
