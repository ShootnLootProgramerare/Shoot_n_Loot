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
            if (Input.newMs.LeftButton == ButtonState.Pressed) Game1.currentScene = Game1.gameScene;

            base.Update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(TextureManager.font, "Leftclick to go to game", Vector2.Zero, Color.White);
            base.Draw(spriteBatch);
        }
    }
}
