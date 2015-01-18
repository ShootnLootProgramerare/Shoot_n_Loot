﻿using System;
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
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        public static Point ScreenSize { get { return new Point(1200, 750); } }
        internal static Player player;

        internal static List<GameObject> objects, 
            objectsToAdd; //all objects in this list are moved to the main list at the beginning of each frame, to avoid breaking the foreach loops

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        //Player player;

        Enemy enemy;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = ScreenSize.X;
            graphics.PreferredBackBufferHeight = ScreenSize.Y;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            Input.Initialize();
            Camera.FollowSpeed = .3f;
            objects = new List<GameObject>();
            objectsToAdd = new List<GameObject>();
            Camera.Scale = 1;
            Camera.Origin = new Vector2(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height) / (2 * Camera.Scale);


            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            //Textures.player = Content.Load<Texture2D>("player");
            TextureManager.Load(Content);
            player = new Player();
            enemy = new Enemy(new Vector2(100, 100), Enemy.EnemyType.enemy1);
            objects.Add(enemy);
            objects.Add(player);
            objects.Add(new DroppedItem(new Sprite(TextureManager.enemy1, new Vector2(700), new Vector2(20)), new InventoryItem()));
            Map.Initialize();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            Input.Update();

            foreach (GameObject g in objectsToAdd) objects.Add(g);
            objectsToAdd.Clear();

            //player.Update();
            for (int i = objects.Count - 1; i >= 0; i--)
            {
                if (objects[i].Dead) objects.RemoveAt(i);
                else objects[i].Update();
            }
            Camera.Follow(player.Position);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            spriteBatch.Begin(SpriteSortMode.BackToFront, null, null, null, null, null, Camera.Transform);

            Map.Draw(spriteBatch);
            //player.Draw(spriteBatch);
            foreach (GameObject o in objects) o.Draw(spriteBatch);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
