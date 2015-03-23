using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Shoot__n_Loot.Scenes;
using Shoot__n_Loot.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shoot__n_Loot
{
    class MainMenuScene : Scene
    {
        List<Button> b;

        public MainMenuScene()
        {
            b = new List<Button>();
            b.Add(new Button("Play", new Rectangle(-16, -32, 100, 32)));
            b.Add(new Button("Info", new Rectangle(-16, 32, 100, 32)));
            b.Add(new Button("Quit", new Rectangle(-16, 96, 100, 32)));

            base.Initialize();
        }

        public override void Update()
        {
            if (Input.newKs.IsKeyDown(Keys.Enter)) SceneManager.currentScene = SceneManager.gameScene;
            else if (Input.KeyWasJustPressed(Keys.Escape)) SceneManager.currentScene = SceneManager.aboutScene;
            
            if (b[0].IsClicked) 
            { 
                SceneManager.gameScene = new GameScene();
                Map.Initialize();
                SceneManager.currentScene = SceneManager.gameScene; 
            }
            if (b[1].IsClicked) { SceneManager.currentScene = SceneManager.aboutScene; }
            if (b[2].IsClicked) { /* Exit the Game */ Game1.exit = true; }

            foreach (Button bu in b) bu.Update();

            base.Update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            string s = "Press Enter for game or Escape for info";
            for (int i = 0; i < b.Count; i++)
            {
                b[i].Draw(spriteBatch);
            }
            spriteBatch.DrawString(TextureManager.font, s, Vector2.Zero - TextureManager.font.MeasureString(s) / 2, Color.Black);
            base.Draw(spriteBatch);
        }
    }
}
