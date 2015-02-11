using Microsoft.Xna.Framework;
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

        byte width, height;
        float maxWeight;

        public float Weight
        {
            get
            {
                List<Item> items = new List<Item>();
                foreach (Item i in Items) if (i != null) items.Add(i);
                items = items.Distinct().ToList();
                float w = 0;
                foreach (Item i in items) w += i.Properties.Weight;
                return w;
            }
        }

        ItemSlot[,] Slots { get; set; }

        public List<Item> Items { get { List<Item> i = new List<Item>(); foreach (ItemSlot s in Slots) if (s.Item != null) for (int j = 0; j < s.StackSize; j++) i.Add(s.Item); return i; } }

        public Inventory(byte width, byte height, float maxWeight)
        {
            this.width = width;
            this.height = height;
            this.maxWeight = maxWeight;
            Slots = new ItemSlot[width, height];
            for(int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Slots[x, y] = new ItemSlot();
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

            //Slots[p.X, p.Y].Add(item);

            for (int x = 0; x < item.Properties.Width; x++)
            {
                for (int y = 0; y < item.Properties.Height; y++)
                {
                    Slots[x + p.X, y + p.Y].Add(item);
                }
            }
        }

        public void Remove(Item item, byte num)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (Slots[x, y].Item == item) Slots[x, y].Remove(num);
                }
            }
        }

        Point SlotThatFits(Item i)
        {
            Point p = new Point(-1, -1);

            bool fits = true;
            for (int x = 0; x < this.width; x++)
            {
                for (int y = 0; y < this.height; y++)
                {
                    if (Slots[x, y].CanContain(i))
                    {
                        fits = true;
                        p = new Point(x, y);
                        for (int xi = 0; xi < width && xi + x < this.width; xi++)
                        {
                            for (int yi = 0; yi < height && yi + y < this.height; yi++) //checking if out of bounds here might cause issues
                            {
                                if (!Slots[xi + x, yi + y].CanContain(i))
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


        /// <summary>
        /// 
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="center">center of the inventory relative to the center of the screen. Note that it will automatically adjust for camera position!</param>
        public void Draw(SpriteBatch spriteBatch, Point center)
        {
            Point offset = new Point(center.X - (width * DRAWNSIZE) / 2, center.Y - (height * DRAWNSIZE) / 2);
            List<Item> drawnItems = new List<Item>(); //keep track of duplicates and dont draw them
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Rectangle t = new Rectangle(x * DRAWNSIZE + (int)Camera.Center.X + offset.X, y * DRAWNSIZE + (int)Camera.Center.Y + offset.Y, DRAWNSIZE, DRAWNSIZE);

                    spriteBatch.Draw(TextureManager.inventorySlot, t, null, Color.White, 0, Vector2.Zero, SpriteEffects.None, .001f);

                    if (Slots[x, y].Item != null && !drawnItems.Contains(Slots[x, y].Item))
                    {
                        Slots[x, y].Draw(spriteBatch, t);
                        drawnItems.Add(Slots[x, y].Item);
                    }
                    //else if(!drawnItems.Contains(slots[x, y])) spriteBatch.Draw(TextureManager.enemy2, new Rectangle(x * DRAWNSIZE + (int)Camera.TotalOffset.X, y * DRAWNSIZE + (int)Camera.TotalOffset.Y, DRAWNSIZE, DRAWNSIZE), Color.White);
                }
            }
            string s =  Weight + "/" + maxWeight + " kg";
            spriteBatch.DrawString(TextureManager.font, s, Camera.Center + new Vector2(0, 100) - TextureManager.font.MeasureString(s) / 2, Color.Black, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
        }
    }
}
