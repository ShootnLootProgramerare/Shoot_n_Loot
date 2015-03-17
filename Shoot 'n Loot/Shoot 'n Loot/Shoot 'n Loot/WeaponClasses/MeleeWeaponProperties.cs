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

        public MeleeWeaponProperties(float damage, int attackSpeed)
        {
            this.Damage = damage;
            this.AttackSpeed = attackSpeed;
        }
    }
}
