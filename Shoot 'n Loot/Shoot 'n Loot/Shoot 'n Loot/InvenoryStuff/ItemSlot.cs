using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Shoot__n_Loot.Scenes;
using Shoot__n_Loot.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shoot__n_Loot.InvenoryStuff
{
    class ItemSlot
    {
        const int BUTTON_W = 50, BUTTON_H = 15;

        public Item Item { get; private set; }
        public byte StackSize { get; private set; }

        public bool ShowingOptions { get; set; }

        public float Weight 
        {
            get
            {
                if (Item != null) return Item.Properties.Weight * StackSize;
                else return 0;
            }
        }

        List<Button> buttons;

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

                buttons = new List<Button>();
                buttons.Add(new Button("drop", new Rectangle(0, 0, BUTTON_W, BUTTON_H), DropItem));
            }
        }

        public bool CanContain(Item i)
        {
            if (Item == null) return true;
            else return (Item.Properties == i.Properties && StackSize < i.Properties.MaxStack);
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

                if (ShowingOptions)
                {
                    foreach (Button b in buttons) b.Draw(spriteBatch);
                }
            }
        }

        void DropItem() 
        {
            Remove(1);
            Item.Position = SceneManager.gameScene.player.Position;
            SceneManager.gameScene.AddObject(Item);
        }
    }
}
