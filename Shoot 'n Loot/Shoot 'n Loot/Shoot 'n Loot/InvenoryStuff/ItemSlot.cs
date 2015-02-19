using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Shoot__n_Loot.Scenes;
using Shoot__n_Loot.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Shoot__n_Loot.InvenoryStuff
{
    class ItemSlot
    {
        const int BUTTON_W = 100, BUTTON_H = 25;

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

                //SetButtons();
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

        /// <summary>
        /// creates the buttons that appear when this item is selected, setting the position to the right spot
        /// </summary>
        private void SetButtons(int xOffset, int yOffset, Inventory container)
        {
            buttons = new List<Button>();
            Rectangle baseRect =  new Rectangle(container.PositionForItem(xOffset, yOffset).X, container.PositionForItem(xOffset, yOffset).Y, BUTTON_W, BUTTON_H);

            AddButton(buttons, new Button("drop", baseRect, DropItem));
            if (StackSize > 1) AddButton(buttons, new Button("drop all", baseRect, DropAll));
            if (Item.Properties.IsConsumable) AddButton(buttons, new Button("eat", baseRect, Consume));
        }

        /// <summary>
        /// adds the specified button to the specified list, setting the position to align under the other buttons
        /// </summary>
        /// <param name="buttons"></param>
        /// <param name="b"></param>
        private void AddButton(List<Button> buttons, Button b)
        {
            b.Area = new Rectangle(b.Area.X, b.Area.Bottom + buttons.Count * b.Area.Height, b.Area.Width, b.Area.Height);
            buttons.Add(b);
        }

        public void Update(int x, int y, Inventory container)
        {
            if (Input.AreaIsClicked(container.PositionForItem(x, y)) && Input.LeftClickWasJustPressed()) ShowingOptions = !ShowingOptions;
            if (Item != null && ShowingOptions)
            {
                SetButtons(x, y, container);
                foreach (Button b in buttons) b.Update();
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
                    if (buttons != null) foreach (Button b in buttons) b.Draw(spriteBatch);
                }
            }
        }

        /// <summary>
        /// to be used by buttons as a delegate
        /// </summary>
        void DropItem() 
        {
            SceneManager.gameScene.AddObject(new Item(Item.Properties, SceneManager.gameScene.player.Position));
            Remove(1);
        }

        void DropAll()
        {
            for (int i = 0; i < StackSize; i++) DropItem();
        }

        void Consume()
        {
            Item.Properties.onConsume(SceneManager.gameScene.player);
            Remove(1);
        }
    }
}
