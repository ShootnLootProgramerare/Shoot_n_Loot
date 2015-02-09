using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shoot__n_Loot.WeaponClasses
{
    class CustomizationSlot
    {
        public WeaponPart.PartType type;
        public Rectangle Position { get {  return new Rectangle((int)Camera.TotalOffset.X + position.X, (int)Camera.TotalOffset.Y + position.Y, position.Width, position.Height); } }

        private Rectangle position;

        public CustomizationSlot(Rectangle position, WeaponPart.PartType type)
        {
            this.position = position;
            this.type = type;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(TextureManager.inventorySlot, Position, Color.White);
        }
    }
}
