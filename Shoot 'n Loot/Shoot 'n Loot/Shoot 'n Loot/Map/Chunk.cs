﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Shoot__n_Loot.Enemies;
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
        public const byte size = 12;
        public static short sizePx { get { return size * Tile.size; } }

        Color spawnData; // decides which tiles spawn which zombies and how likely it is. Alpha decides overall likeliness and each color is the chance of each type of zombie.

        public Tile[,] Tiles;
        public Vector2 Position { get; private set; }
        public Vector2 Center { get { return Position + new Vector2(size / 2); } }

        List<Vector2> spawnPositions;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="relativePosition">posiiton in the game of the top left corner</param>
        /// <param name="mapData">the color[] that contains the tiles</param>
        /// <param name="spawnData">the color that describes spawning in this chunk</param>
        public Chunk(Vector2 relativePosition, Color[,] mapData, Color[,] propData, Color spawnData)
        {
            Tiles = new Tile[size, size];
            Position = relativePosition;
            this.spawnData = spawnData;

            spawnPositions = new List<Vector2>();

            for(int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    //if(!tileTypes.Contains(mapData[x, y])) throw new Exception("Color not valid, pixel " + x + ", " + y);
                    byte type = 0;
                    for (byte i = 0; i < Tile.tileTypes.Length; i++)
			        {
			            if(Tile.tileTypes[i] == mapData[x, y]) type = i;
			        }
                    Tiles[x ,y] = new Tile(Position + new Vector2(x, y) * Tile.size, type);

                    Color prop = propData[x, y];
                    if (prop == Color.Red)
                    {
                        SceneManager.gameScene.player.Position = TilePosition(x, y);
                    }
                    else if (prop == Color.Black)
                    {
                        SceneManager.gameScene.objects.Add(new House(TilePosition(x, y)));
                    }
                    else if (prop == Color.Yellow)
                    {
                        spawnPositions.Add(TilePosition(x, y));
                    }
                }
            }
        }

        private Vector2 TilePosition(int x, int y)
        {
            return new Vector2(Tile.size) * new Vector2(x, y) + Position;
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

            if (Game1.random.Next(255) < spawnData.A * SPAWNRATE && SceneManager.gameScene.NoOfZombies() < Map.maxZombies && spawnPositions.Count > 0)
            {
                Vector2 position = spawnPositions[(Game1.random.Next(spawnPositions.Count))];

                int r = Game1.random.Next(spawnData.R + spawnData.G + spawnData.B);

                //TODO: rewrite this so every type of enemy has a seperate map of spawn frequency
                if (r > spawnData.R + spawnData.G) SceneManager.gameScene.AddObject(new Fisherman(position));//e = (Enemy.EnemyType)3; //decide which type
                else if (r > spawnData.R) SceneManager.gameScene.AddObject(new Onelegged(position));
                else SceneManager.gameScene.AddObject(new FatLady(position));
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
