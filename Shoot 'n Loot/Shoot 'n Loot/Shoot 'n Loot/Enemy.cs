using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Shoot__n_Loot
{
    class Enemy: GameObject
    {
        public enum EnemyType { enemy1, enemy2, enemy3 };

        public int HP { get; set; }
        public int Damage { get; set; }
        public float Speed { get; set; }
        public EnemyType enemyType { get; set; }

        public Enemy(Vector2 position, EnemyType enemytype)
        {
            this.enemyType = enemytype;

            Sprite = new Sprite(TextureManager.enemy1, position, new Vector2(50));

            if (enemyType == EnemyType.enemy1) { this.HP = 32; this.Damage = 8; this.Speed = 1.2f; }
            if (enemyType == EnemyType.enemy2) { this.HP = 48; this.Damage = 2; this.Speed = 0.8f; }
            if (enemyType == EnemyType.enemy3) { this.HP = 12; this.Damage = 12; this.Speed = 2.4f; }
        }

        public void Update()
        {
            if (enemyType == EnemyType.enemy1)
            {
                this.Position += new Vector2((float)Math.Cos(this.Speed), (float)  Math.Sin(this.Speed));
            }

            if (enemyType == EnemyType.enemy2)
            {

            }

            if (enemyType == EnemyType.enemy3)
            {

            }












































































































































































































        }
    }
}
