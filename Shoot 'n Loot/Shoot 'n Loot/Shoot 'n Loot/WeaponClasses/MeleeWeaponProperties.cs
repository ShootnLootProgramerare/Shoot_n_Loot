using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shoot__n_Loot.WeaponClasses
{
    class MeleeWeaponProperties
    {
        public float Damage { get; private set; }
        public int AttackSpeed { get; private set; }
        public Texture2D[] walkSprites, hitSprites;
        public byte DamageFrame { get; private set; }

        public MeleeWeaponProperties(float damage, int attackSpeed)
            : this(damage, attackSpeed, null, null, 2) {
        }

        public MeleeWeaponProperties(float damage, int attackSpeed, Texture2D[] walkSprites, Texture2D[] hitSprites, byte damageFrame)
        {
            this.Damage = damage;
            this.AttackSpeed = attackSpeed;
            this.walkSprites = walkSprites;
            this.hitSprites = hitSprites;
            this.DamageFrame = damageFrame;
        }
    }
}
