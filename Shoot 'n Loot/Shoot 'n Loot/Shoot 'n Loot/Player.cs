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
            Position = new Vector2(100);
            Sprite = new Sprite(Textures.player, Position);
        }
    }
}
