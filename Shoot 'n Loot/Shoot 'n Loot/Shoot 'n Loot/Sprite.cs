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
        public byte Frames { get; set; }
        public byte Frame { get; set; }
        public Point FrameSize { get; set; }

        Vector2 size;
        Vector2 scale;
        Texture2D texture;
        Rectangle sourceRectangle { get { return new Rectangle(FrameSize.X * Frame, 0, FrameSize.X, FrameSize.Y); } }
        float frameCounter;

        /// <summary>
        /// creates a sprite with the size of the texture that will not animate.
        /// </summary>
        /// <param name="texture"></param>
        /// <param name="position"></param>
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

        /// <summary>
        /// creates a sprite with the specified size that animates with the specified parameters.
        /// </summary>
        /// <param name="texture"></param>
        /// <param name="position"></param>
        /// <param name="size"></param>
        /// <param name="frameSize">The size of a frame on the spritesheet</param>
        /// <param name="frames">the number of frames on the spritesheet</param>
        /// <param name="animSpeed">n / 60 will switch frame n times per second.</param>
        public Sprite(Texture2D texture, Vector2 position, Vector2 size, Point frameSize, byte frames, float animSpeed)
        {
            this.texture = texture;
            this.Position = position;
            this.Size = size;
            this.Frames = frames;
            this.FrameSize = frameSize;
            this.AnimationSpeed = animSpeed;
            Color = Color.White;
            Rotation = 0;
            SpriteEffects = Microsoft.Xna.Framework.Graphics.SpriteEffects.None;
            LayerDepth = .5f;
            Origin = Size / 2;
            Frame = 0;
        }

        public Sprite(Texture2D texture, Vector2 position, Vector2 size) : this(texture, position, new Vector2(texture.Width, texture.Height), new Vector2(texture.Width, texture.Height) / 2, 0, Color.White, 0, SpriteEffects.None, .5f, 1, new Point(texture.Width, texture.Height))
        {
        }

        public Sprite(Texture2D texture, Vector2 position, Vector2 size, Vector2 origin, float animSpeed, Color color, float rotation, SpriteEffects effects, float layerDepth, byte frames, Point frameSize)
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
            this.Frame = 0;
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
