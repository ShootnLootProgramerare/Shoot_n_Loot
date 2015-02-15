using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shoot__n_Loot.UI
{
    class Button
    {
        public string Text { get; set; }
        public Rectangle Area { get; private set; }
        public bool IsClicked { get { return Input.AreaIsClicked(Area); } }

        Action onClick;

        /// <summary>
        /// use if you want to manually check if the button is clicked and do stuff
        /// </summary>
        /// <param name="area"></param>
        public Button(string text, Rectangle area)
            : this(text, area, null)
        { }

        /// <summary>
        /// use if you want to call update() each frame and the button to call onClick automatically when the button is clicked.
        /// </summary>
        /// <param name="area"></param>
        /// <param name="onClick"></param>
        public Button(string text, Rectangle area, Action onClick)
        {
            this.Text = text;
            this.Area = area;
            this.onClick = onClick;
        }

        /// <summary>
        /// will check if the button is clicked and if so call onClick() if its not null
        /// </summary>
        public void Update()
        {
            if (onClick == null) return;
            if (IsClicked) onClick();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(TextureManager.inventorySlot, Area, Color.White);
            spriteBatch.DrawString(TextureManager.font, Text, new Vector2(Area.X, Area.Y), Color.White);
        }
    }
}
