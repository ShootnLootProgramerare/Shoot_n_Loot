using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shoot__n_Loot
{
    class Player : GameObject
    {
        public Player()
        {
            Sprite = new Sprite(Textures.player, new Vector2(100), new Vector2(100));
        }
    }
}
