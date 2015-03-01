using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Shoot__n_Loot.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shoot__n_Loot.WeaponClasses
{
    class CustomizationSlot
    {
        Rectangle position; // should be relative to camera
        public WeaponPart.PartType Type { get; private set; }
        List<Button> buttons;
        string partInfo;

        private Rectangle WorldPosition { get { return new Rectangle(position.X + (int)Camera.Position.X, position.Y + (int)Camera.Position.Y, position.Width, position.Height); } }

        public CustomizationSlot(WeaponPart.PartType type, Rectangle position)
        {
            this.position = position;
            this.Type = type;
            buttons = new List<Button>();
            partInfo = "no info :(";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="part">should be raw from weapon.partOfType, ie can be null</param>
        public void Update(Item part)
        {
            if (part != null)
            {
                //on click create buttons, remove on another click or part removal

                partInfo = part.Properties.WeaponPart.GetInfoText();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(TextureManager.inventorySlot, WorldPosition, null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.0000004f);
            spriteBatch.DrawString(TextureManager.font, Type.ToString(), new Vector2(WorldPosition.X, WorldPosition.Y - 25), Color.Black);
            //if mouseover and no buttons draw part text
            if (Input.AreaIsHoveredOver(WorldPosition) && buttons.Count == 0) spriteBatch.DrawString(TextureManager.font, partInfo, new Vector2(Input.newMs.X, Input.newMs.Y) + Camera.TotalOffset, Color.Black);
        }
    }
}
