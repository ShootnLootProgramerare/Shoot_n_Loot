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

            Area = new Rectangle(Area.X, area.Y, (int)TextureManager.font.MeasureString(text).X + PADDING_X * 2, (int)TextureManager.font.MeasureString(text).Y + PADDING_Y * 2);
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
            spriteBatch.DrawString(TextureManager.font, Text, new Vector2(Area.X + PADDING_X, Area.Y + PADDING_Y), Color.Black, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
        }
    }
}
