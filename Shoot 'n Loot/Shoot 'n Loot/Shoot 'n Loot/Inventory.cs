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

        float Weight
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
            if (Weight + item.Weight > maxWeight) return false;

            if (SlotThatFits(item.Width, item.Height) == new Point(-1, -1)) return false;

            return true;
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
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (slots[x, y] == null)
                    {
                        fits = true;
                        p = new Point(x, y);
                        for (int xi = x; xi < width && xi < width; xi++)
                        {
                            for (int yi = y; yi < height && yi < height; yi++)
                            {
                                if (slots[xi, yi] != null)
                                {
                                    fits = false;
                                }
                            }
                        }
                    }
                }
            }
            if (fits) return p;
            else return new Point(-1, -1);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (slots[x, y] != null) slots[x, y].DrawInInventory(new Rectangle(x * DRAWNSIZE, y * DRAWNSIZE, DRAWNSIZE, DRAWNSIZE), spriteBatch);
                }
            }
        }
    }
}
