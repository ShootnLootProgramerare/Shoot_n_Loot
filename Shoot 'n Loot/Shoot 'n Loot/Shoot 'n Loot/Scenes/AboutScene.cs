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
            "Controls:\n\nWASD to move\nLeft click to shoot\nQ to pick up items\nE to view inventory\nR to reload\n\nRight click-and-drag to move items between containers\nLeft click on items to bring up options\n\nFind a way of the island before the zombies kill you!\nGood luck.";

        public AboutScene()
        {
            b = new Button("", new Rectangle(-60, 200, 120, 72), TextureManager.resumeButton, TextureManager.resumeButtonL, null);
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
