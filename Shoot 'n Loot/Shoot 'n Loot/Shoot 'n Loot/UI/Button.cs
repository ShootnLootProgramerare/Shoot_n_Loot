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
        public const int PADDING_X = 10, PADDING_Y = 2;

        public enum ButtonState { None, Hovering, Clicked }
        public ButtonState State { get; private set; }
        public string Text { get; set; }
        public Rectangle Area { get; set; }
        public bool IsClicked { get { return Input.AreaIsClicked(Area); } }
        public Color color;

        Action onClick;
        Vector2 textSize;

        Color AdjustedColor
        {
            get
            {
                switch (State)
                {
                    case ButtonState.None: return color;
                    case ButtonState.Hovering: return new Color(color.ToVector4() - new Vector4(.2f, .2f, .2f, 0));
                    case ButtonState.Clicked: return new Color(color.ToVector4() - new Vector4(.2f, .2f, .2f, 0));
                    default: return color;
                }
            }
        }

        /// <summary>
        /// use if you want to manually check if the button is clicked and do stuff
        /// </summary>
        /// <param name="area"></param>
        public Button(string text, Rectangle area)
            : this(text, area, null, Color.White)
        { }

        public Button(string text, Rectangle area, Color color)
            : this(text, area, null, color)
        { }

        public Button(string text, Rectangle area, Action onClick)
            : this(text, area, onClick, Color.White)
        { }

        /// <summary>
        /// use if you want to call update() each frame and the button to call onClick automatically when the button is clicked.
        /// </summary>
        /// <param name="area"></param>
        /// <param name="onClick"></param>
        public Button(string text, Rectangle area, Action onClick, Color color)
        {
            this.Text = text;
            this.Area = area;
            this.onClick = onClick;
            this.color = color;

            textSize = TextureManager.font.MeasureString(text);
            Vector2 minSize = textSize + new Vector2(PADDING_X, PADDING_Y) * 2;

            if (area.Width < minSize.X) Area = new Rectangle(Area.X, Area.Y, (int)minSize.X, Area.Height);
            if (area.Height < minSize.Y) Area = new Rectangle(Area.X, Area.Y, Area.Width, (int)minSize.Y);
        }

        /// <summary>
        /// will check if the button is clicked and if so call onClick() if its not null
        /// </summary>
        public void Update()
        {
            if (Input.AreaIsHoveredOver(Area))
            {
                if (Input.newMs.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
                {
                    State = ButtonState.Clicked;
                    if (Input.oldMs.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Released) if (onClick != null) onClick();
                }
                else State = ButtonState.Hovering;
            }
            else State = ButtonState.None;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(TextureManager.inventorySlot, Area, null, AdjustedColor, 0, Vector2.Zero, SpriteEffects.None, 0.0000001f);
            spriteBatch.DrawString(TextureManager.font, Text, new Vector2(Area.X + Area.Width / 2, Area.Y + Area.Height / 2), Color.Black, 0, textSize / 2, 1, SpriteEffects.None, 0);
        }
    }
}
