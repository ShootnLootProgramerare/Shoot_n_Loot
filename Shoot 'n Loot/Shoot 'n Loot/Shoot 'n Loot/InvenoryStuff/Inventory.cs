﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Shoot__n_Loot.InvenoryStuff;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Shoot__n_Loot
{
    class Inventory 
    {
        const byte DRAWNSIZE = 32;

        public byte Width { get; private set; }
        public byte Height { get; private set; }
        
        float maxWeight;

        public float Weight
        {
            get
            {
                float w = 0;
                foreach (ItemSlot s in Slots) w += s.Weight;
                return w;
            }
        }

        public ItemSlot[,] Slots { get; set; }

        public List<Item> Items { get { List<Item> i = new List<Item>(); foreach (ItemSlot s in Slots) if (s.Item != null) for (int j = 0; j < s.StackSize; j++) i.Add(s.Item); return i; } }

        public GameObject parent { get; private set; }
        
        Point drawOffset;

        public Inventory(GameObject parent, Point drawOffset, byte width, byte height, float maxWeight)
        {
            this.parent = parent;
            this.drawOffset = drawOffset;
            this.Width = width;
            this.Height = height;
            this.maxWeight = maxWeight;
            Slots = new ItemSlot[width, height];
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Slots[x, y] = new ItemSlot(this, x, y);
                }
            }
        }

        public bool Fits(Item item)
        {
            return Weight + item.Properties.Weight <= maxWeight && SlotThatFits(item) != new Point(-1, -1);
        }

        public void Add(Item item)
        {
            Point p = SlotThatFits(item);
            if (p == new Point(-1, -1)) return;

            Slots[p.X, p.Y].Add(item);

            
        }

        public void Remove(Item item, byte num)
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    if (Slots[x, y].Item == item) Slots[x, y].Remove(num);
                }
            }
        }

        public void Update(Point drawOffset)
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    Slots[x, y].Update();
                }
            }
        }

        public void HideAllItemMenus()
        {
            foreach (ItemSlot s in Slots) s.ShowingOptions = false;
        }

        Point SlotThatFits(Item i)
        {
            for (int x = 0; x < this.Width; x++)
            {
                for (int y = 0; y < this.Height; y++)
                {
                    if (Slots[x, y].CanContain(i)) return new Point(x, y);
                }
            }
            return new Point(-1, -1);
        }

        public Rectangle PositionForItem(int x, int y)
        {
            return new Rectangle((x - Width / 2) * DRAWNSIZE + (int)Camera.Center.X + drawOffset.X, (y - Height / 2) * DRAWNSIZE + (int)Camera.Center.Y + drawOffset.Y, DRAWNSIZE, DRAWNSIZE);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="center">center of the inventory relative to the center of the screen. Note that it will automatically adjust for camera position!</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            //Point offset = new Point(drawOffset.X - (Width * DRAWNSIZE) / 2, drawOffset.Y - (Height * DRAWNSIZE) / 2);
            //List<Item> drawnItems = new List<Item>(); //keep track of duplicates and dont draw them
            foreach (ItemSlot slot in Slots) slot.Draw(spriteBatch);
            string s = Weight + "/" + maxWeight + " kg";

            spriteBatch.DrawString(TextureManager.font, s, Camera.Center + new Vector2(drawOffset.X, drawOffset.Y) + new Vector2(0, 100) - TextureManager.font.MeasureString(s) / 2, Color.Black, 0, Vector2.Zero, 1, SpriteEffects.None, 0.0000004f);
        }
    }
}
