using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Shoot__n_Loot.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shoot__n_Loot.Scenes
{
    class GameOverScene : Scene
    {
        Button b;
        Button bt;

        public GameOverScene()
        {
            b = new Button("Main Menu", new Rectangle(-64, -32, 128, 32));
            bt = new Button("Quit", new Rectangle(-64, 32, 128, 32));
            base.Initialize();
        }

        public override void Update()
        {
            if (b.IsClicked) { SceneManager.currentScene = SceneManager.mainMenuScene; }
            if (bt.IsClicked) { /* Exit the Game */ }
            b.Update();
            bt.Update();
            base.Update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            b.Draw(spriteBatch);
            bt.Draw(spriteBatch);
            spriteBatch.DrawString(TextureManager.font, "Paused", - TextureManager.font.MeasureString("Paused") / 2, Color.Black);
            base.Draw(spriteBatch);
        }
    }
}
