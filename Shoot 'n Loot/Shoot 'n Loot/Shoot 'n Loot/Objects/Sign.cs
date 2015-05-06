using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Shoot__n_Loot.Scenes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Shoot__n_Loot.Objects
{
    class Sign : GameObject
    {
        Texture2D big; //the actual sign face
        enum State { Far, Close, Viewing };

        State state;

        public Sign(Vector2 position, Texture2D big)
        {
            Sprite = new Sprite(TextureManager.sign, position, new Vector2(48));
            this.big = big;
        }

        public override void Update()
        {
            if (DistanceSquared(SceneManager.gameScene.player.Center) < 2500)
            {
                state = State.Close;
                if (Input.newKs.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.C)) state = State.Viewing;
            }
            else state = State.Far;

            base.Update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (state == State.Viewing) spriteBatch.Draw(big, Camera.Center, null, Color.White, 0, new Vector2(big.Width, big.Height) / 2, 1, SpriteEffects.None, 0);
            else if (state == State.Close) spriteBatch.DrawString(TextureManager.font, "[Press C to view]", Center, Color.Black);
            base.Draw(spriteBatch);
        }
    }
}
