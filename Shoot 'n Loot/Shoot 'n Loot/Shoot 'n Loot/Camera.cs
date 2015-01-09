using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shoot__n_Loot
{
    class Camera
    {
        public static Matrix Transform { get { return Matrix.Identity* Matrix.CreateTranslation(new Vector3(-Position.X, -Position.Y, 0)) * Matrix.CreateTranslation(new Vector3(Origin.X, Origin.Y, 0)); } }
        public static float Scale { get; set; }
        public static Vector2 Position { get; set; }
        public static Vector2 Origin { get; set; }
        public static float FollowSpeed { get; set; }

        public static void Follow(Vector2 target)
        {
            Position += (target - Position) * FollowSpeed;
        }
    }
}
