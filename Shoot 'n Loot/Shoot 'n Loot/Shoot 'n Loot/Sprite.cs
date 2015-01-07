using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shoot__n_Loot
{
    class Sprite
    {
        public Vector2 Position { get; set; }
        public Vector2 Size { get { return size; } set { size = value; scale = new Vector2(size.X / FrameSize.X, size.Y / FrameSize.Y); } }
        public Vector2 Origin { get; set; }
        public float AnimationSpeed { get; set; }
        public Color Color { get; set; }
        public float Rotation { get; set; }
        public SpriteEffects SpriteEffects { get; set; }
        public float LayerDepth { get; set; }
        public int Frames { get; set; }
        public int Frame { get; set; }
        public Point FrameSize { get; set; }

        Vector2 size;
        Vector2 scale;
        Texture2D texture;
        Rectangle sourceRectangle { get { return new Rectangle(FrameSize.X * Frame, 0, FrameSize.X, FrameSize.Y); } }
        float frameCounter;

        public Sprite(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.Position = position;
            Size = new Vector2(texture.Width, texture.Height);
            Origin = Size / 2;
            AnimationSpeed = 0;
            Color = Color.White;
            Rotation = 0;
            SpriteEffects = Microsoft.Xna.Framework.Graphics.SpriteEffects.None;
            LayerDepth = .5f;
            Frames = 1;
            Frame = 0;
            FrameSize = new Point((int)Size.X, (int)Size.Y);
        }

        public Sprite(Texture2D texture, Vector2 position, Vector2 size, Point frameSize, int frames, float animSpeed)
        {

        }

        public Sprite(Texture2D texture, Vector2 position, Vector2 size, Vector2 origin, float animSpeed, Color color, float rotation, SpriteEffects effects, float layerDepth, int frames, Point frameSize, int frame)
        {
            this.texture = texture;
            this.Position = position;
            this.Size = size;
            this.Origin = origin;
            this.AnimationSpeed = animSpeed;
            this.Color = color;
            this.Rotation = rotation;
            this.SpriteEffects = effects;
            this.LayerDepth = layerDepth;
            this.Frames = frames;
            this.FrameSize = frameSize;
            this.Frame = frame;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if(Frames > 1)
            {
                frameCounter += AnimationSpeed;
                if(frameCounter >= 1)
                {
                    frameCounter = 0;
                    Frame++;
                }
            }
            spriteBatch.Draw(texture, Position, sourceRectangle, Color, Rotation, Origin, scale, SpriteEffects, LayerDepth);
        }
    }
}
