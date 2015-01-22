using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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

        byte width, height;
        float maxWeight;

        public float Weight
        {
            get
            {
                List<Item> items = new List<Item>();
                foreach (Item i in slots) if (i != null) items.Add(i);
                items = items.Distinct().ToList();
                float w = 0;
                foreach (Item i in items) w += i.Weight;
                return w;
            }
        }

        Item[,] slots;

        public Inventory(byte width, byte height, float maxWeight)
        {
            this.width = width;
            this.height = height;
            this.maxWeight = maxWeight;
            slots = new Item[width, height];
        }

        public bool Fits(Item item)
        {
            return Weight + item.Weight < maxWeight && SlotThatFits(item.Width, item.Height) != new Point(-1, -1);
        }

        public void Add(Item item)
        {
            Point p = SlotThatFits(item.Width, item.Height);
            if (p == new Point(-1, -1)) return;

            slots[p.X, p.Y] = item;

            for (int x = 0; x < item.Width; x++)
            {
                for (int y = 0; y < item.Height; y++)
                {
                    slots[x + p.X, y + p.Y] = item;
                }
            }
        }

        Point SlotThatFits(byte width, byte height)
        {
            Point p = new Point(-1, -1);

            bool fits = true;
            for (int x = 0; x < this.width; x++)
            {
                for (int y = 0; y < this.height; y++)
                {
                    if (slots[x, y] == null)
                    {
                        fits = true;
                        p = new Point(x, y);
                        for (int xi = 0; xi < width; xi++)
                        {
                            for (int yi = 0; yi < height; yi++)
                            {
                                if (slots[xi + x, yi + y] != null)
                                {
                                    fits = false;
                                }
                            }
                        }
                    }
                    else fits = false;
                    if (fits) return p;
                }
            }
            return new Point(-1, -1);
        }

        public void Draw(SpriteBatch spriteBatch, Point center)
        {
            Point offset = new Point(center.X - (width * DRAWNSIZE) / 2, center.Y - (height * DRAWNSIZE) / 2);
            List<Item> drawnItems = new List<Item>(); //keep track of duplicates and dont draw them
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Rectangle t = new Rectangle(x * DRAWNSIZE + (int)Camera.TotalOffset.X + offset.X, y * DRAWNSIZE + (int)Camera.TotalOffset.Y + offset.Y, DRAWNSIZE, DRAWNSIZE);

                    spriteBatch.Draw(TextureManager.inventorySlot, t, null, Color.White, 0, Vector2.Zero, SpriteEffects.None, .6f);

                    if (slots[x, y] != null && !drawnItems.Contains(slots[x, y]))
                    {
                        slots[x, y].DrawInInventory(t, spriteBatch);
                        drawnItems.Add(slots[x, y]);
                    }
                    //else if(!drawnItems.Contains(slots[x, y])) spriteBatch.Draw(TextureManager.enemy2, new Rectangle(x * DRAWNSIZE + (int)Camera.TotalOffset.X, y * DRAWNSIZE + (int)Camera.TotalOffset.Y, DRAWNSIZE, DRAWNSIZE), Color.White);
                }
            }
        }
    }
}
