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
        
        public GameScene()
        {
            base.Initialize();

            player = new Player();
            Enemy enemy = new Enemy(new Vector2(100, 100), Enemy.EnemyType.enemy1);
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
         
            base.Update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Map.Draw(spriteBatch);
            base.Draw(spriteBatch);
        }
    }
}
