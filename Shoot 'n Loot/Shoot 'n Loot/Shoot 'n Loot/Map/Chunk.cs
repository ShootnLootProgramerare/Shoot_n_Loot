using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Shoot__n_Loot.Scenes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Shoot__n_Loot
{
    class Chunk
    {
        static Color[] tileTypes = new Color[] { Color.Green, Color.Blue, new Color(1f, .5f, 0, 1f), new Color(255, 128, 128) };
        public const byte size = 16;
        public static short sizePx { get { return size * Tile.size; } }

        Color spawnData; // decides which tiles spawn which zombies and how likely it is. Alpha decides overall likeliness and each color is the chance of each type of zombie.

        public Tile[,] Tiles;
        public Vector2 Position { get; private set; }
        public Vector2 Center { get { return Position + new Vector2(size / 2); } }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="map">the texture that contains the map of this chunk</param>
        /// <param name="relativePosition">posiiton in the game of the top left corner</param>
        public Chunk(Vector2 relativePosition, Color[,] mapData, Color spawnData)
        {
            Tiles = new Tile[size, size];
            Position = relativePosition;
            this.spawnData = spawnData;

            for(int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    //if(!tileTypes.Contains(mapData[x, y])) throw new Exception("Color not valid, pixel " + x + ", " + y);
                    byte type = 0;
                    for (byte i = 0; i < tileTypes.Length; i++)
			        {
			            if(tileTypes[i] == mapData[x, y]) type = i;
			        }
                    Tiles[x ,y] = new Tile(Position + new Vector2(x, y) * Tile.size, type);
                }
            }
        }

        public List<Tile> NonWalkableTiles()
        {
            List<Tile> tiles = new List<Tile>();
            foreach (Tile t in Tiles)
	        {
		        if(!t.Properties.IsWalkable) tiles.Add(t);
	        }
            return tiles;
        }

        /// <summary>
        /// checks with the spawnData to spawn a zombie.
        /// </summary>
        /// <param name="list">where the zombie will be added.</param>
        public void SpawnZombie(List<GameObject> list)
        {
            const float SPAWNRATE = .00001f;

            if (Game1.random.Next(255) < spawnData.A * SPAWNRATE && SceneManager.gameScene.NoOfZombies() < Map.maxZombies)
            {
                int r = Game1.random.Next(spawnData.R + spawnData.G + spawnData.B);

                Enemy.EnemyType e;
                if (r > spawnData.R + spawnData.G) e = (Enemy.EnemyType)3; //decide which type
                else if (r > spawnData.R) e = (Enemy.EnemyType)2;
                else e = (Enemy.EnemyType)1;
                Debug.WriteLine("adding " + e + " at " + Center);


                list.Add(new Enemy(Center, e)); //add it
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach(Tile t in Tiles)
            {
                t.Draw(spriteBatch);
            }
        }
    }
}
