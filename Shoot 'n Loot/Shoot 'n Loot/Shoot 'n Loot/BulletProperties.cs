using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shoot__n_Loot
{
    class BulletProperties
    {
        public float Damage { get; private set; }
        public float Speed { get; private set; }
        public byte Penetration { get; private set; }

        public BulletProperties(float damage, float speed, byte penetration)
        {
            this.Damage = damage;
            this.Speed = speed;
            this.Penetration = penetration;
        }

        public Bullet GetBullet(Vector2 position, float angle)
        {
            return new Bullet(angle, position, this);
        }
    }
}
