using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Shoot__n_Loot.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shoot__n_Loot.Scenes
{
    class AboutScene : Scene
    {
        Button b;

        const string text =
            "WASD to move\nLeft click to shoot\nE to pick up items\nI to view inventory\nR to reload\n\nEscape to return to main menu";

        public AboutScene()
        {
            b = new Button("", new Rectangle(-64, 96, 120, 72), TextureManager.resumeButton, TextureManager.resumeButtonL, null);
            base.Initialize();  
        }

        public override void Update()
        {
            if (Input.KeyWasJustPressed(Keys.Escape) || b.IsClicked) SceneManager.CurrentScene = SceneManager.mainMenuScene;
            b.Update();

            base.Update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            b.Draw(spriteBatch);
            spriteBatch.DrawString(TextureManager.font, text, -TextureManager.font.MeasureString(text) / 2, Microsoft.Xna.Framework.Color.Black);
            base.Draw(spriteBatch);
        }
    }
}
