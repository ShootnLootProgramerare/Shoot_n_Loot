using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shoot__n_Loot.InvenoryStuff
{
    class ItemProperties
    {
        public string InfoText { get; private set; }
        public byte Width { get; private set; }
        public byte Height { get; private set; }
        public byte MaxStack { get; private set; }
        public float Weight { get; private set; }
        public Texture2D Texture { get; private set; }

        public bool IsConsumable { get { return onConsume != null; } }
        public bool IsWeaponPart { get { return WeaponPart != null; } }

        public WeaponPart WeaponPart { get; private set; }

        public Action<Player> onConsume;


        public ItemProperties(byte width, byte height, byte weight, Texture2D texture, byte maxStack, string infoText) : this(width, height, weight, texture, maxStack, infoText, null, null) { }

        public ItemProperties(byte width, byte height, float weight, Texture2D texture, byte maxStack, string infoText, Action<Player> onConsume) : this(width, height, weight, texture, maxStack, infoText, onConsume, null) { }

        public ItemProperties(byte width, byte height, float weight, Texture2D texture, byte maxStack, string infoText, WeaponPart part) : this(width, height, weight, texture, maxStack, infoText, null, part) { }

        public ItemProperties(byte width, byte height, float weight, Texture2D texture, byte maxStack, string infoText, Action<Player> onConsume, WeaponPart weaponPart)
        {
            this.Width = width;
            this.Height = height;
            this.Weight = weight;
            this.onConsume = onConsume;
            this.WeaponPart = weaponPart;
            this.MaxStack = maxStack;
            this.Texture = texture;
            this.InfoText = infoText;
        }
    }
}
