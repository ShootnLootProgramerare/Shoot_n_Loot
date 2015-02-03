using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shoot__n_Loot
{
    class GameScene : Scene
    {
        public  Player player;

        const float MINSPAWNDIST = 500, MAXSPAWNDIST = 1000;
        
        public GameScene()
        {
            base.Initialize();

            player = new Player();
            Enemy enemy = new Enemy(new Vector2(100, 100), Enemy.EnemyType.Fisherman);
            objects.Add(enemy);
            objects.Add(new Enemy(new Vector2(1000, 1000), Enemy.EnemyType.enemy2));
            objects.Add(player);
            objects.Add(new House());
            objects.Add(new Item(1, 1, 1, new Sprite(TextureManager.enemy1, new Vector2(1000), new Vector2(40))));
            Map.Initialize();
        }

        public override void Update()
        {
            Camera.Follow(player.Position);

            foreach (Chunk c in Map.chunks)
            {
                if (player.DistanceSquared(c.Center) < Math.Pow(MAXSPAWNDIST, 2) && player.DistanceSquared(c.Center) > Math.Pow(MINSPAWNDIST, 2)) c.SpawnZombie(objects);
            }
         
            base.Update();
        }

        public int NoOfZombies()
        {
            int i = 0;
            foreach (GameObject g in objects) if (g.Type == Enemy.TYPE) i++;
            return i;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Map.Draw(spriteBatch);
            base.Draw(spriteBatch);
        }
    }
}
