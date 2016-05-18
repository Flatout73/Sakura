using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Collections.Generic;

namespace sakura
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D closedFlower, Leaf, LeafHor, beginTexture, exitTexture, lvlSelect;

        Vector2 flower0Position;
        Vector2 lvl0position;

        int lvli, lvlj;

        int resX, resY;
        static float kx = 1f, ky = 1f;

        public const int countFlowers = 28;
        
        Level[][] levels;

        private TouchCollection gamePadState;
        private TouchCollection lastGamePadState;

        SpriteFont Font;

        Button ButtonBegin;
        Button ButtonExit;

       public GameProcess gameProcess = new GameProcess();

        

        public Game()
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
            flower0Position = new Vector2((int)(35 * kx + 30*kx), (int)(35 * kx + 30*kx));
            Vertex[][] flowers = new Vertex[7][];

            for (int i = 0; i < 7; i++)
            {
                flowers[i] = new Vertex[4];
            }

            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    flowers[i][j] = null;
                }
            }
                flowers[0][0] = new Vertex(flower0Position + new Vector2((70 + 50) * kx * 0, 0), kx, ky, true, false, false, true);
                flowers[0][1] = new Vertex(flower0Position + new Vector2((70 + 50) * kx * 1, 0), kx, ky, true, false, true, false);
                flowers[0][2] = new Vertex(flower0Position + new Vector2((70 + 50) * kx * 2, 0), kx, ky, true, false, false, true);
                flowers[0][3] = new Vertex(flower0Position + new Vector2((70 + 50) * kx * 3, 0), kx, ky, true, false, false, false);
                flowers[1][0] = new Vertex(flower0Position + new Vector2((70 + 50) * kx * 0, (70 + 50) * kx * 1), kx, ky, true, true, true, false);
                flowers[1][1] = new Vertex(flower0Position + new Vector2((70 + 50) * kx * 1, (70 + 50) * kx * 1), kx, ky, false, true, false, true);
                flowers[1][2] = new Vertex(flower0Position + new Vector2((70 + 50) * kx * 2, (70 + 50) * kx * 1), kx, ky, true, false, true, true);
                flowers[1][3] = new Vertex(flower0Position + new Vector2((70 + 50) * kx * 3, (70 + 50) * kx * 1), kx, ky, true, true, false, false);
      

            gameProcess.NewGame();

            lvl0position = new Vector2(28 * kx, 28 * kx);

            levels = new Level[5][];
            for(int i = 0; i < 5; i++)
            {
                levels[i] = new Level[4];
            }
            
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    levels[i][j] = new Level(lvlSelect, new Vector2(lvl0position.X + j * 28 * kx + j * 100 * kx, lvl0position.Y + 28 * kx * i + 100 * i * kx), kx, gameProcess);
                }
            }

            levels[0][0].Initilize(flowers);  

			Generator gen = new Generator(4);
			levels [0] [1].Initilize (gen._graph);
			levels [0] [1].Mix ();

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 4; j++)                
                {
                    if (!(i == 0 && j == 0))
                    {
                        gen = new Generator(2*(i + 1)+ 2*(j + 1));
                        levels[i][j].Initilize(gen._graph);
                        levels[i][j].Mix();
                    }
                }
            }
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
            beginTexture = Content.Load<Texture2D>("Begin");
            exitTexture = Content.Load<Texture2D>("Exit");
            lvlSelect = Content.Load<Texture2D>("lvl");


            ButtonBegin = new Button(graphics.PreferredBackBufferWidth / 2f - beginTexture.Width / 2f, graphics.PreferredBackBufferHeight/2f - beginTexture.Height/2f, kx, new Vector2(100f, 60f), beginTexture);
            ButtonExit = new Button(graphics.PreferredBackBufferWidth / 2f - beginTexture.Width / 2f, graphics.PreferredBackBufferHeight / 2f + 4f * beginTexture.Height, kx, new Vector2(100f, 60f), exitTexture);
            //   LeafHor = Content.Load<Texture2D>("LeafHor");
            //   Font = Content.Load<SpriteFont>("font");
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

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            {
                if(gameProcess.isMenuLvlSelect)
                {
                    gameProcess.NewGame();
                }
                else if (gameProcess.isGame)
                {
                    gameProcess.LvlSelect();
                }
                else if (gameProcess.isWin)
                {
                    gameProcess.LvlSelect();
                }
                else
                {
                    Exit();
                }
            }


            // TODO: Add your update logic here

            ButtonBegin.Process();
            ButtonExit.Process();

            if(ButtonExit.IsEnabled)
            {
                Exit();
                ButtonExit.Reset();
            }

            if(gameProcess.isMenuLvlSelect)
            {
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        levels[i][j].button.Process();
                    }
                }
            }

            if (gameProcess.isMenuLvlSelect)
            {
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (levels[i][j].button.IsEnabled)
                        {
                            gameProcess.StartGame();
                            lvli = i;
                            lvlj = j;

                            levels[i][j].button.Reset();
                        }
                    }
                }
            }

            if(gameProcess.isGame)
            levels[lvli][lvlj].Update();

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

			if (gameProcess.isMenuBegin == true) {
              
				spriteBatch.Draw (beginTexture, new Vector2 (ButtonBegin.x + beginTexture.Width / 2f, ButtonBegin.y + beginTexture.Height / 2f), null, Color.White, 0f, new Vector2 (beginTexture.Width / 2f, beginTexture.Height / 2f), 100f / beginTexture.Width * kx, SpriteEffects.None, 0f);
				spriteBatch.Draw (exitTexture, new Vector2 (ButtonExit.x + exitTexture.Width / 2f, ButtonExit.y + exitTexture.Height / 2f), null, Color.White, 0f, new Vector2 (exitTexture.Width / 2f, exitTexture.Height / 2f), 100f / exitTexture.Width * kx, SpriteEffects.None, 0f);
				if (ButtonBegin.IsEnabled) {
					gameProcess.LvlSelect ();
					ButtonBegin.Reset ();
				}
			} else if (gameProcess.isMenuLvlSelect == true) {
				for (int i = 0; i < 5; i++) {
					for (int j = 0; j < 4; j++) {
						spriteBatch.Draw (lvlSelect, levels [i] [j]._position + new Vector2 (levels [i] [j]._texture.Width / 2 * kx, levels [i] [j]._texture.Height / 2 * kx), null, Color.White, 0f, new Vector2 (levels [i] [j]._texture.Width / 2, levels [i] [j]._texture.Height / 2), 100f / levels [i] [j]._texture.Width * kx, SpriteEffects.None, 0f);
					}
				}
                
			} else if (gameProcess.isGame) {
				levels [lvli] [lvlj].Draw (closedFlower, Leaf, spriteBatch);

			} else if (gameProcess.isWin) {
				spriteBatch.Draw (exitTexture, new Vector2 (resX / 2, resY / 2), Color.White);
			}

            //spriteBatch.DrawString(Font, "You win!", new Vector2(200*kx, 200*kx), Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
