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
    class PauseScene : Scene
    {
        Button b;
        Button bt;

        public PauseScene()
        {
            b = new Button("Continue", new Rectangle(-64, -32, 32, 128));
            bt = new Button("Main Menu", new Rectangle(-64, 32, 32, 128));
            base.Initialize();
        }

        public override void Update()
        {
            if (Input.KeyWasJustPressed(Keys.Escape)) SceneManager.currentScene = SceneManager.gameScene;
            Camera.Follow(Vector2.Zero);
            if (b.IsClicked) { SceneManager.currentScene = SceneManager.gameScene; }
            if (bt.IsClicked) { SceneManager.currentScene = SceneManager.mainMenuScene; }
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
