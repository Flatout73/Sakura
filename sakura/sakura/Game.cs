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
        Texture2D closedFlower, Leaf, LeafHor, selectTexture, exitTexture, lvlSelect, background, startTexture, backGroundOfLevel, nextTexture, lvlLocked;
        SpriteFont font;

        Vector2 flower0Position;
        Vector2 lvl0position;

        int lvli, lvlj;
        float totalTime;

        int resX, resY;
        static float kx = 1f, ky = 1f;

        public const int countFlowers = 28;

        Vertex[][] flowers;
        Level[][] levels;

        private TouchCollection gamePadState;
        private TouchCollection lastGamePadState;

        SpriteFont Font;

        Button ButtonSelect;
        Button ButtonExit;
        Button ButtonStart;
        Button ButtonNext;

       public GameProcess gameProcess = new GameProcess();

        

        public Game()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.IsFullScreen = true;
            graphics.PreferredBackBufferWidth = 540;
            graphics.PreferredBackBufferHeight = 960;
            graphics.SupportedOrientations = DisplayOrientation.Portrait;
        
            gameProcess.init += newInitialize;

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


            ButtonSelect = new Button((float)resX / 2f - (float)selectTexture.Width*(256f * kx / (float)selectTexture.Width) / 2f, (float)resY / 2f - (selectTexture.Height * (256f *kx / (float)selectTexture.Width) / 2f), kx, new Vector2(selectTexture.Width * (256f / (float)selectTexture.Width), selectTexture.Height * (256f / (float)selectTexture.Width)), selectTexture);
            ButtonExit = new Button(resX/ 2f - exitTexture.Width * (256f * kx / (float)exitTexture.Width) / 2f, resY / 2f + (float)exitTexture.Height * (256f * kx / (float)exitTexture.Width), kx, new Vector2(exitTexture.Width * (256f / (float)exitTexture.Width), exitTexture.Height * (256f / (float)exitTexture.Width)), exitTexture);
            ButtonStart = new Button(resX/2f - startTexture.Width * (256f * kx / (float)exitTexture.Width) / 2f, resY / 2f - 3f * (float)startTexture.Height * (256f * kx / (float)startTexture.Width), kx, new Vector2(startTexture.Width * (256f / (float)startTexture.Width), startTexture.Height * (256f / (float)startTexture.Width)), startTexture);
            ButtonNext = new Button((float)resX / 2f - (float)nextTexture.Width * (256f * kx / (float)nextTexture.Width) / 2f, (float)resY / 2f - (nextTexture.Height * (256f * kx / (float)nextTexture.Width) / 2f), kx, new Vector2(nextTexture.Width * (256f / (float)nextTexture.Width), nextTexture.Height * (256f / (float)nextTexture.Width)), nextTexture);

            flower0Position = new Vector2((int)(35 * kx + 30*kx), (int)(35 * kx + 30*kx));
            flowers = new Vertex[7][];

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
                        levels[i][j].isPrevEnd = false;
                    }
                }
            }

            levels[0][0].isPrevEnd = true;      
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            closedFlower = Content.Load<Texture2D>("FlowerOfSakura");
            Leaf = Content.Load<Texture2D>("Branchs");
            startTexture = Content.Load<Texture2D>("Start");  //  нопка Start
            selectTexture = Content.Load<Texture2D>("SelectLevel2"); //  нопка Select Level
            exitTexture = Content.Load<Texture2D>("ExitFl");
            lvlSelect = Content.Load<Texture2D>("sakuraLevels");
            lvlLocked = Content.Load<Texture2D>("sakuraLevelsLocked");
            background = Content.Load<Texture2D>("sakuraw");
            backGroundOfLevel = Content.Load<Texture2D>("sakura and bird");
            nextTexture = Content.Load<Texture2D>("NextLevel");
            font = Content.Load<SpriteFont>("NumberOfLevel");

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

            totalTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            {
                if(gameProcess.isMenuLvlSelect)
                {
                    gameProcess.NewGame();
                }
                else if (gameProcess.isGame)
                {
                    if (gameProcess.isMenuLvlSelectOld)
                    {
                        gameProcess.LvlSelect();
                    }
                    else
                    {
                        gameProcess.NewGame();
                    }

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
          
            if (gameProcess.isMenuBegin)
            {
                ButtonSelect.Process();
                ButtonStart.Process();
                ButtonExit.Process();
                if (ButtonExit.IsEnabled)
                {
                    Exit();
                    ButtonExit.Reset();
                }
                if (ButtonSelect.IsEnabled)
                {
                    gameProcess.LvlSelect();
                    gameProcess.Initialize();
                    ButtonSelect.Reset();
                }
                if (ButtonStart.IsEnabled)
                {
                    gameProcess.StartGame();
                    ButtonStart.Reset();
                }
            }

            if (gameProcess.isMenuLvlSelect)
            {
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (levels[i][j].isPrevEnd == true)
                        {
                            levels[i][j].button.Process();
                        }
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

            if(gameProcess.isWin)
            {
            //    if (totalTime > 1)
              //  {
                    totalTime = 0;
                    if ((lvli != 4) && (lvlj != 3))
                    {
                        if (lvlj % 4 != 3)
                        {
                            levels[lvli][lvlj + 1].isPrevEnd = true;
                        }
                        else
                        {
                            levels[lvli + 1][0].isPrevEnd = true;
                        }
                    }

                    ButtonExit.Process();
                    ButtonNext.Process();

                    if (ButtonExit.IsEnabled)
                    {
                        Exit();
                        ButtonExit.Reset();
                    }
                    if (ButtonNext.IsEnabled)
                    {
                        if ((lvli != 4) && (lvlj != 3))
                        {
                            if (lvlj % 4 != 3)
                            {
                                lvlj++;
                            }
                            else
                            {
                                lvlj = 0;
                                lvli++;
                            }
                            gameProcess.StartGame();
                            ButtonNext.Reset();
                        }
                        else
                        {
                            gameProcess.End();
                        }
                    }
               // }
            }

            if (gameProcess.isGame)
            {
                levels[lvli][lvlj].Update();
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {

            GraphicsDevice.Clear(Color.AliceBlue); // заполнить фон
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied); // установить последовательный пор€док отрисовки объектов

            Rectangle bg = new Rectangle(0, 0, resX, resY);

            if (gameProcess.isGame)
            {
                spriteBatch.Draw(backGroundOfLevel, bg, Color.White);
            }
            else
            {
                spriteBatch.Draw(background, bg, Color.White);
            }

            /*  if (touch)
              {
                  spriteBatch.Draw(closedFlower, flower1 + new Vector2(closedFlower.Width / 2, closedFlower.Height / 2), null, Color.White, (float)(Math.PI / 2.0f), new Vector2(closedFlower.Width / 2, closedFlower.Height / 2), 1, SpriteEffects.None, 0f);
              }
              else
              {
                  spriteBatch.Draw(closedFlower, flower1, Color.White);
              }*/

            if (gameProcess.isMenuBegin == true) {

                spriteBatch.Draw(selectTexture, new Vector2(resX / 2f, resY / 2f), null, Color.White, 0f, new Vector2(selectTexture.Width / 2f, selectTexture.Height / 2f), (256f * kx / (float)selectTexture.Width), SpriteEffects.None, 0f);
                spriteBatch.Draw(exitTexture, new Vector2(resX / 2f, ButtonExit.y + exitTexture.Height * (256f * kx / (float)exitTexture.Width) / 2f), null, Color.White, 0f, new Vector2(exitTexture.Width / 2f, exitTexture.Height / 2f), (256f * kx / (float)exitTexture.Width), SpriteEffects.None, 0f);
                spriteBatch.Draw(startTexture, new Vector2(resX / 2f, ButtonStart.y + startTexture.Height * (256f * kx / (float)startTexture.Width) / 2f), null, Color.White, 0f, new Vector2(startTexture.Width / 2f, startTexture.Height / 2f), (256f * kx / (float)startTexture.Width), SpriteEffects.None, 0f);

            } else if (gameProcess.isMenuLvlSelect == true) {
                Rectangle recrLvl;
                Vector2 centre;
                for (int i = 0; i < 5; i++) {
                    for (int j = 0; j < 4; j++) {                  
                        recrLvl = new Rectangle((int)(lvl0position.X + j * 28 * kx + j * 100 * kx), (int)(lvl0position.Y + 28 * kx * i + 100 * i * kx), (int)(100 * kx), (int)(100 * kx));
                        
                        
                        if (levels[i][j].isPrevEnd == true)
                        {
                            centre = font.MeasureString(String.Format("{0}", i * 4 + j + 1)) / 2f;
                            spriteBatch.Draw(lvlSelect, recrLvl, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 1f);
                            spriteBatch.DrawString(font, String.Format("{0}", i * 4 + j + 1), new Vector2(recrLvl.X + 50f * kx, recrLvl.Y + 50f * kx), Color.Coral, 0f, centre, kx, SpriteEffects.None, 0f);
                        }
                        else
                        {
                            centre = font.MeasureString("?") / 2f;
                            spriteBatch.Draw(lvlLocked, recrLvl, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 1f);
                            spriteBatch.DrawString(font, "?", new Vector2(recrLvl.X + 50f * kx, recrLvl.Y + 50f * kx), Color.Coral, 0f, centre, kx, SpriteEffects.None, 0f);
                        }
                    }
                }

            } else if (gameProcess.isGame) {
                levels[lvli][lvlj].Draw(closedFlower, Leaf, spriteBatch);

            } else 
        //    if(totalTime > 1)
            if (gameProcess.isWin) {
                spriteBatch.Draw(nextTexture, new Vector2(ButtonNext.x, ButtonNext.y), null, Color.White, 0f, Vector2.Zero, (256f * kx / (float)nextTexture.Width), SpriteEffects.None, 0f);
                spriteBatch.Draw(exitTexture, new Vector2(resX / 2f, ButtonExit.y + exitTexture.Height * (256f * kx / (float)exitTexture.Width) / 2f), null, Color.White, 0f, new Vector2(exitTexture.Width / 2f, exitTexture.Height / 2f), (256f * kx / (float)exitTexture.Width), SpriteEffects.None, 0f);
            }
            else if (gameProcess.isEnd)
            {
                Vector2 centre = font.MeasureString("To be continued...") / 2f;
                spriteBatch.DrawString(font, "To be continued...", new Vector2(resX/2f, resY/2f), Color.Green, 0f, centre, kx, SpriteEffects.None, 1f);
            }

            //spriteBatch.DrawString(Font, "You win!", new Vector2(200*kx, 200*kx), Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void newInitialize()
        {

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

            lvl0position = new Vector2(28 * kx, 28 * kx);

            levels = new Level[5][];
            for (int i = 0; i < 5; i++)
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
            levels[0][1].Initilize(gen._graph);
            levels[0][1].Mix();

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (!(i == 0 && j == 0))
                    {
                        gen = new Generator(2 * (i + 1) + 2 * (j + 1));
                        levels[i][j].Initilize(gen._graph);
                        levels[i][j].Mix();
                    }
                }
            }
            levels[0][0].isPrevEnd = true;

        }
    }
}
