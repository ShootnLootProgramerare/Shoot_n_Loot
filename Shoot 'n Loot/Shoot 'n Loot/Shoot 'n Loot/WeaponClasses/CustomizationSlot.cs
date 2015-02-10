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
        public WeaponPart Part { get { return part; } }
        public Rectangle Position { get {  return new Rectangle((int)Camera.TotalOffset.X + position.X, (int)Camera.TotalOffset.Y + position.Y, position.Width, position.Height); } }

        public bool HasPart { get { return part != null; } }

        private WeaponPart part;


        private Rectangle position;

        public CustomizationSlot(Rectangle position, WeaponPart.PartType type)
        {
            this.position = position;
            this.type = type;
        }

        /// <summary>
        /// tries to add the specified part. Returns the old part on succes (Might be null!) or returns null if the part doesnt fit.
        /// </summary>
        /// <param name="part"></param>
        /// <returns></returns>
        public WeaponPart AddPart(WeaponPart part)
        {
            WeaponPart p = null;
            if (part.Type == type)
            {
                p = this.part;
                this.part = part;
            }
            return p;
        }

        /// <summary>
        /// sets the this.part to null and returns the old part. (might be null)
        /// </summary>
        /// <returns></returns>
        public WeaponPart RemovePart()
        {
            WeaponPart p = part;
            part = null;
            return p;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(TextureManager.inventorySlot, Position, Color.White);
        }
    }
}
