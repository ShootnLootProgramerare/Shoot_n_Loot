using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shoot__n_Loot.InvenoryStuff
{
    class ItemSlot
    {
        public Item Item { get; private set; }
        public byte StackSize { get; private set; }

        public ItemSlot()
        {
            StackSize = 0;
        }

        /// <summary>
        /// if this slot contains this item or null one will be added to the stack. if it contains something else it will be overwritten.
        /// </summary>
        /// <param name="i">the item to be added.</param>
        public void Add(Item i)
        {
            if (CanContain(i))
            {
                Item = i;
                StackSize++;
            }
        }

        public bool CanContain(Item i)
        {
            return ((Item == i && StackSize < i.Properties.MaxStack) || Item == null);
        }

        /// <summary>
        /// removes the specified amount of items from the stack.
        /// </summary>
        /// <param name="num"></param>
        public void Remove(byte num)
        {
            if (StackSize > num) StackSize -= num;
            else
            {
                Item = null;
                StackSize = 0;
            }
        }

        public void Draw(SpriteBatch spriteBatch, Rectangle position)
        {
            if (Item != null) 
            {
                Item.DrawInInventory(position, spriteBatch);
                spriteBatch.DrawString(TextureManager.font, StackSize.ToString(), new Vector2(position.X, position.Y), Color.Black, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
            }
        }
    }
}
