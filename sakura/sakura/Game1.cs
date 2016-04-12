using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System;

namespace sakura
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D closedFlower, Leaf, LeafHor;
        Vector2 flower0Position;
        int resX, resY;
        int touches = 0;
        static float kx = 1f, ky = 1f;
        bool touch;
        Vertex[] flowers;
        private TouchCollection gamePadState;
        private TouchCollection lastGamePadState;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.IsFullScreen = true;
            graphics.PreferredBackBufferWidth = 540;
            graphics.PreferredBackBufferHeight = 960;
            graphics.SupportedOrientations = DisplayOrientation.Portrait;
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

            base.Initialize();
            graphics.PreferredBackBufferWidth = 540;
            graphics.PreferredBackBufferHeight = 960;

            resX = graphics.GraphicsDevice.Viewport.Width;
            resY = graphics.GraphicsDevice.Viewport.Height;

            if (graphics.PreferredBackBufferWidth < resX)
            {
                kx = (float)resX / (float)graphics.PreferredBackBufferWidth;
            }
            if (graphics.PreferredBackBufferHeight < resY)
            {
                ky = (float)resY / (float)graphics.PreferredBackBufferWidth;
            }
            flower0Position = new Vector2((int)(32 * kx), (int)(32 * kx));
            flowers = new Vertex[5];
            for (int i = 0; i < 5; i++)
            {
                flowers[i] = new Vertex(flower0Position + new Vector2((64 + 44) * kx * i, 0), kx, ky, true, true, false, true);
            }

            touch = false;


        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            closedFlower = Content.Load<Texture2D>("fl");
            Leaf = Content.Load<Texture2D>("Leaf");
            LeafHor = Content.Load<Texture2D>("LeafHor");
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
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
            gamePadState = TouchPanel.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                Exit();

            var tc = TouchPanel.GetState();
            if (tc.Count == 1)
            {
                touch = true;
                touches++;
            } 

            lastGamePadState = gamePadState;

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            /*  if (touch)
              {
                  spriteBatch.Draw(closedFlower, flower1 + new Vector2(closedFlower.Width / 2, closedFlower.Height / 2), null, Color.White, (float)(Math.PI / 2.0f), new Vector2(closedFlower.Width / 2, closedFlower.Height / 2), 1, SpriteEffects.None, 0f);
              }
              else
              {
                  spriteBatch.Draw(closedFlower, flower1, Color.White);
              }*/

            for (int i = 0; i < 5; i++)
            {
                flowers[i].Draw(closedFlower, Leaf, LeafHor, spriteBatch, touches);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
