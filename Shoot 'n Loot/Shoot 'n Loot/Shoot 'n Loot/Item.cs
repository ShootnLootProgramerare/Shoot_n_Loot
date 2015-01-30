﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shoot__n_Loot
{
    class Item : GameObject
    {
        public byte Width { get; private set; }
        public byte Height { get; private set; }
        public float Weight { get; private set; }

        public Item(byte width, byte height, float weight, Sprite sprite)
        {
            this.Width = width;
            this.Height = height;
            this.Weight = weight;
            this.Sprite = sprite;
        }

        public override void Update()
        {
            if (Game1.gameScene.player.Hitbox.Intersects(Hitbox) && Input.KeyWasJustPressed(Microsoft.Xna.Framework.Input.Keys.E))
            {
                if (Game1.gameScene.player.Inventory.Fits(this))
                {
                    Game1.gameScene.player.Inventory.Add(this);
                    Game1.gameScene.RemoveObject(this);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="position">width and height will be multiplied by the size automatically</param>
        /// <param name="spriteBatch"></param>
        public void DrawInInventory(Rectangle position, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Sprite.Texture, new Rectangle(position.X, position.Y, position.Width * Width, position.Height * Height), Color.White);
        }
    }
}