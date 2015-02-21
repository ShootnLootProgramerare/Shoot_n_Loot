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

        Rectangle infoPos;

        Inventory parent;

        int x, y;

        public float Weight 
        {
            get
            {
                if (Item != null) return Item.Properties.Weight * StackSize;
                else return 0;
            }
        }

        List<Button> buttons;
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parent">the inventory that contains this slot.</param>
        /// <param name="x">where in the container this slot resides</param>
        /// <param name="y">where in the container this slot resides</param>
        public ItemSlot(Inventory parent, int x, int y)
        {
            this.parent = parent;
            this.x = x;
            this.y = y;
            StackSize = 0;
        }

        /// <summary>
        /// if this slot contains this item or null one will be added to the stack. if it contains something else it will be overwritten.
        /// </summary>
        /// <param name="i">the item to be added.</param>
        public void Add(Item item)
        {
            if (Item == null)
            {
                StackSize = 1;
            }
            else if (Item.Properties != item.Properties)
            {
                StackSize = 1;
            }
            else StackSize++;

            Item = item;
        }

        public bool CanContain(Item i)
        {
            //first check if this slot already contains that item
            if (Item != null)
            {
                if (Item.Properties != i.Properties) { Debug.WriteLine("slot " + x + ", " + y + " contains a different item"); return false; }
                else if (StackSize < Item.Properties.MaxStack) return true; //if its the same and the stack is not maxed, the item fits
                else return false;
            }
            //otherwise see if no other item is obstructing the slot
            foreach (ItemSlot s in parent.Slots) if (s.ExtendsTo(x, y)) { Debug.WriteLine("slot " + x + ", " + y + " is obstructed"); return false; }
            //then see if the item would obstruct another slot if placed here
            for (int x = 0; x < i.Properties.Width; x++ )
            {
                for (int y = 0; y < i.Properties.Height; y++)
                {
                    if (parent.Slots[x + this.x, y + this.y].Item != null) { Debug.WriteLine("adding to slot " + x + ", " + y + " would obstruct other slot"); return false; }
                }
            }
            //if none of the above is true, the item fits
            return true;
        }

        /// <summary>
        /// checks if the item in this slot also obstructs the slot at the specified position
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool ExtendsTo(int x, int y)
        {
            if (Item == null) return false;
            else return x < this.x + Item.Properties.Width && y < this.y + Item.Properties.Height;
        }

        /// <summary>
        /// removes the specified amount of items from the stack.
        /// </summary>
        /// <param name="num"></param>
        public void Remove(byte num)
        {
            for (int i = 0; i < num; i++)
            {
                StackSize--;

                if (StackSize == 0)
                {
                    Debug.WriteLine("no items in this slot");
                    Item = null;
                    break;
                }
                
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
            if (Item.Properties.IsWeaponPart)
            {
                if (ArrayOverlaps(SceneManager.gameScene.player.weapon.CompatitbleAmmoTypes(Item.Properties.WeaponPart.Type), Item.Properties.WeaponPart.AcceptableAmmo))
                    AddButton(buttons, new Button("use in weapon", baseRect, UseInWeapon));
                else AddButton(buttons, new Button("This part is not compatible with your weapon", baseRect, Color.Red));
            }
            if (Item.Properties.IsAmmo)
            {
                if (SceneManager.gameScene.player.weapon.CompatitbleAmmoTypes(null).Contains(Item.Properties.AmmoType)) AddButton(buttons, new Button("use this ammo", baseRect, UseAsAmmo));
                else AddButton(buttons, new Button("Your weapon can't use this ammo", baseRect, Color.Red));
            }
        }


        /// <summary>
        /// check if two arrays share any elements
        /// </summary>
        /// <param name="a1"></param>
        /// <param name="a2"></param>
        /// <returns></returns>
        bool ArrayOverlaps(Weapon.AmmoType[] a1, Weapon.AmmoType[] a2)
        {
            foreach (Weapon.AmmoType a in a1)
            {
                if (a2.Contains(a)) return true;
            }
            return false;
        }

        /// <summary>
        /// adds the specified button to the specified list, setting the position to align under the other buttons
        /// </summary>
        /// <param name="buttons"></param>
        /// <param name="b"></param>
        private void AddButton(List<Button> buttons, Button b)
        {
            if (buttons.Count > 0) b.Area = new Rectangle(b.Area.X, buttons[buttons.Count - 1].Area.Bottom, b.Area.Width, b.Area.Height);
            buttons.Add(b);
        }

        public void Update()
        {
            if (Item != null && ShowingOptions)
            {
                infoPos = parent.PositionForItem(x, y);
                infoPos.Width = (int)TextureManager.font.MeasureString(Item.Properties.InfoText).X + Button.PADDING_X * 2;
                infoPos.X -= infoPos.Width + 10;
                infoPos.Height = (int)TextureManager.font.MeasureString(Item.Properties.InfoText).Y + Button.PADDING_Y * 2;

                SetButtons(x, y, parent);
                foreach (Button b in buttons) b.Update();
            }
            else ShowingOptions = false;

            if (Input.AreaIsClicked(parent.PositionForItem(x, y)) && Input.LeftClickWasJustPressed())
            {
                parent.HideAllItemMenus();
                ShowingOptions = !ShowingOptions;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(TextureManager.inventorySlot, parent.PositionForItem(x, y), null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.0000004f);
            if (Item != null)
            {
                Item.DrawInInventory(parent.PositionForItem(x, y), spriteBatch);
                spriteBatch.DrawString(TextureManager.font, StackSize.ToString(), new Vector2(parent.PositionForItem(x, y).X, parent.PositionForItem(x, y).Y), Color.Black, 0, Vector2.Zero, 1, SpriteEffects.None, 0.0000002f);

                if (ShowingOptions)
                {
                    if (buttons != null) foreach (Button b in buttons) b.Draw(spriteBatch);
                    spriteBatch.Draw(TextureManager.inventorySlot, infoPos, null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.0000001f);
                    spriteBatch.DrawString(TextureManager.font, Item.Properties.InfoText, new Vector2(infoPos.X + Button.PADDING_X, infoPos.Y + Button.PADDING_Y), Color.Black);
                }
            }
        }

        //--------------- THESE ARE USED AS DELEGATES FOR BUTTON ONCLICKS -------------
        void DropItem() 
        {
            SceneManager.gameScene.AddObject(new Item(Item.Properties, SceneManager.gameScene.player.Position));
            Remove(1);
        }

        void DropAll()
        {
            while (StackSize > 0) DropItem();
        }

        void Consume()
        {
            Item.Properties.onConsume(SceneManager.gameScene.player);
            Remove(1);
        }

        void UseInWeapon()
        {
            Item i = SceneManager.gameScene.player.weapon.AddPart(Item);
            Remove(1);
            if (i != null) SceneManager.gameScene.player.Inventory.Add(i);
        }

        void UseAsAmmo()
        {
            SceneManager.gameScene.player.weapon.currentAmmoType = Item.Properties.AmmoType;
        }
    }
}
