using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Shoot__n_Loot.Scenes;

namespace Shoot__n_Loot
{
    class HPBar : GameObject
    {
        public Vector2 Position { get; set; }
        public int Health { get; set; }

        public HPBar(Vector2 position, int health)
        {
            this.Position = position;
            this.Health = health;
        }

        //percentage = currentHP / maxHp
        public void Draw(SpriteBatch spriteBatch, float percentage)
        {

        }
    }
}
