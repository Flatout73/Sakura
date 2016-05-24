using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Sakura;
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
        Texture2D closedFlower, Leaf, selectTexture, exitTexture, lvlSelect, background, startTexture, backGroundOfLevel, nextTexture, lvlLocked, helpTexture;
        Texture2D startTexturePressed, nextTexturePressed, selectTexturePressed, exitTexturePressed;
        SpriteFont font, help;

        Vector2 flower0Position;
        Vector2 lvl0position;

        int lvli, lvlj;
        float totalTime = 0;

        int resX, resY;
        static float kx = 1f;

        public const int countFlowers = 28;

        Vertex[][] flowers;
        Level[][] levels;

        Button buttonSelect;
        Button buttonExit;
        Button buttonStart;
        Button buttonNext;
        Button buttonHelp;

       public GameProcess gameProcess = new GameProcess();

       LevelManager LevelManager = new LevelManager();

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

            base.Initialize();
            graphics.PreferredBackBufferWidth = 540;
            graphics.PreferredBackBufferHeight = 960;

            resX = graphics.GraphicsDevice.Viewport.Width;
            resY = graphics.GraphicsDevice.Viewport.Height;

            if (graphics.PreferredBackBufferWidth < resX)
            {
                kx = (float)resX / (float)graphics.PreferredBackBufferWidth;
            }


            buttonSelect = new Button((float)resX / 2f - (float)selectTexture.Width*(256f * kx / (float)selectTexture.Width) / 2f, (float)resY / 2f - (selectTexture.Height * (256f *kx / (float)selectTexture.Width) / 2f), kx, new Vector2(selectTexture.Width * (256f / (float)selectTexture.Width), selectTexture.Height * (256f / (float)selectTexture.Width)));
            buttonExit = new Button(resX/ 2f - exitTexture.Width * (256f * kx / (float)exitTexture.Width) / 2f, resY / 2f + (float)exitTexture.Height * (256f * kx / (float)exitTexture.Width), kx, new Vector2(exitTexture.Width * (256f / (float)exitTexture.Width), exitTexture.Height * (256f / (float)exitTexture.Width)));
            buttonStart = new Button(resX/2f - startTexture.Width * (256f * kx / (float)exitTexture.Width) / 2f, resY / 2f - 3f * (float)startTexture.Height * (256f * kx / (float)startTexture.Width), kx, new Vector2(startTexture.Width * (256f / (float)startTexture.Width), startTexture.Height * (256f / (float)startTexture.Width)));
            buttonNext = new Button((float)resX / 2f - (float)nextTexture.Width * (256f * kx / (float)nextTexture.Width) / 2f, (float)resY / 2f - (nextTexture.Height * (256f * kx / (float)nextTexture.Width) / 2f), kx, new Vector2(nextTexture.Width * (256f / (float)nextTexture.Width), nextTexture.Height * (256f / (float)nextTexture.Width)));
            buttonHelp = new Button(resX / 2f - (float)helpTexture.Width * (256f * kx / (float)helpTexture.Width), (float)resY - 2*(helpTexture.Height * (256f * kx / (float)helpTexture.Width) / 2f) - 5 * kx, kx, new Vector2(helpTexture.Width * (256f / (float)helpTexture.Width), helpTexture.Height * (256f / (float)helpTexture.Width)));

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
                flowers[0][0] = new Vertex(flower0Position + new Vector2((70 + 50) * kx * 0, 0), kx, true, false, false, true);
                flowers[0][1] = new Vertex(flower0Position + new Vector2((70 + 50) * kx * 1, 0), kx, true, false, true, false);
                flowers[0][2] = new Vertex(flower0Position + new Vector2((70 + 50) * kx * 2, 0), kx, true, false, false, true);
                flowers[0][3] = new Vertex(flower0Position + new Vector2((70 + 50) * kx * 3, 0), kx, true, false, false, false);
                flowers[1][0] = new Vertex(flower0Position + new Vector2((70 + 50) * kx * 0, (70 + 50) * kx * 1), kx, true, true, true, false);
                flowers[1][1] = new Vertex(flower0Position + new Vector2((70 + 50) * kx * 1, (70 + 50) * kx * 1), kx, false, true, false, true);
                flowers[1][2] = new Vertex(flower0Position + new Vector2((70 + 50) * kx * 2, (70 + 50) * kx * 1), kx, true, false, true, true);
                flowers[1][3] = new Vertex(flower0Position + new Vector2((70 + 50) * kx * 3, (70 + 50) * kx * 1), kx, true, true, false, false);

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

            Generator gen;

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 4; j++)                
                {
                    if (!(i == 0 && j == 0))
                    {
                        gen = new Generator((3*(i + 1) + 3*(j+1)) - 1, 50 * (i+1) + j);
                        levels[i][j].Initilize(gen._graph);
                        levels[i][j].Mix();
                        levels[i][j].isPrevEnd = false;
                        levels[i][j].button.isPressed = false;
                    }
                }
            }

            levels[0][0].isPrevEnd = true;

            LevelManager = LevelManager.ReadLevels();
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    levels[i][j].isPrevEnd = LevelManager.Levels.Value[i][j];
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
            helpTexture = Content.Load<Texture2D>("Help");
            help = Content.Load<SpriteFont>("helpf");
            startTexturePressed = Content.Load<Texture2D>("StartPressed");
            selectTexturePressed = Content.Load<Texture2D>("SelectLevelPressed");
            exitTexturePressed = Content.Load<Texture2D>("ExitPressed");
            nextTexturePressed = Content.Load<Texture2D>("NextLevelPressed");

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

            if(gameProcess.isWinning)
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
                else if (gameProcess.isHelp)
                {
                    gameProcess.StartGame();
                }
                else
                {
                    for (int i = 0; i < 5; i++)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            LevelManager.Levels.Value[i][j] = levels[i][j].isPrevEnd;
                        }
                    }
                    LevelManager.WriteLevels();
                    Exit();
                }
            }


            // TODO: Add your update logic here
          
            if (gameProcess.isMenuBegin)
            {
                buttonSelect.Process();
                buttonStart.Process();
                buttonExit.Process();
                if (buttonExit.isEnabled)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            LevelManager.Levels.Value[i][j] = levels[i][j].isPrevEnd;
                        }
                    }
                    LevelManager.WriteLevels();
                    Exit();
                    buttonExit.Reset();
                }
                if (buttonSelect.isEnabled)
                {
                    gameProcess.LvlSelect();
                    levels[0][0].Initilize(flowers);

                    Generator gen;

                    for (int i = 0; i < 5; i++)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            if (!(i == 0 && j == 0))
                            {
                                gen = new Generator((3 * (i + 1) + 3 * (j + 1)) - 1, 50 * (i + 1) + j);
                                levels[i][j].Initilize(gen._graph);
                                levels[i][j].Mix();
                            }
                        }
                    }
                    buttonSelect.Reset();
                }
                if (buttonStart.isEnabled)
                {
                    gameProcess.StartGame();
                    buttonStart.Reset();
                }
            }
            else
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
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (levels[i][j].button.isEnabled)
                        {
                            gameProcess.StartGame();
                            lvli = i;
                            lvlj = j;

                            levels[i][j].button.Reset();
                        }
                    }
                }
            }
            else
            if(gameProcess.isWin)
            {
                    totalTime = 0;
                    if (!((lvli == 4) && (lvlj == 3)))
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

                    buttonExit.Process();
                    buttonNext.Process();

                    if (buttonExit.isEnabled)
                    {
                    for (int i = 0; i < 5; i++)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            LevelManager.Levels.Value[i][j] = levels[i][j].isPrevEnd;
                        }
                    }
                    LevelManager.WriteLevels();
                    Exit();
                        buttonExit.Reset();
                    }
                    if (buttonNext.isEnabled)
                    {
                        if (!((lvli == 4) && (lvlj == 3)))
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
                            buttonNext.Reset();
                        }
                        else
                        {
                            gameProcess.End();
                        }
                    }
            }
            else
            if (gameProcess.isGame)
            {
                levels[lvli][lvlj].Update();
                buttonHelp.Process();
                if(buttonHelp.isEnabled)
                {
                    gameProcess.Help();
                    buttonHelp.Reset();
                }
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

            if (gameProcess.isGame | gameProcess.isHelp)
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

                if (buttonSelect.isPressed)
                {
                    spriteBatch.Draw(selectTexturePressed, new Vector2(resX / 2f, resY / 2f), null, Color.White, 0f, new Vector2(selectTexture.Width / 2f, selectTexture.Height / 2f), (256f * kx / (float)selectTexture.Width), SpriteEffects.None, 0f);
                }
                else
                {
                    spriteBatch.Draw(selectTexture, new Vector2(resX / 2f, resY / 2f), null, Color.White, 0f, new Vector2(selectTexture.Width / 2f, selectTexture.Height / 2f), (256f * kx / (float)selectTexture.Width), SpriteEffects.None, 0f);
                }
                if (buttonExit.isPressed)
                {
                    spriteBatch.Draw(exitTexturePressed, new Vector2(resX / 2f, buttonExit.y + exitTexture.Height * (256f * kx / (float)exitTexture.Width) / 2f), null, Color.White, 0f, new Vector2(exitTexture.Width / 2f, exitTexture.Height / 2f), (256f * kx / (float)exitTexture.Width), SpriteEffects.None, 0f);
                }
                else
                {
                    spriteBatch.Draw(exitTexture, new Vector2(resX / 2f, buttonExit.y + exitTexture.Height * (256f * kx / (float)exitTexture.Width) / 2f), null, Color.White, 0f, new Vector2(exitTexture.Width / 2f, exitTexture.Height / 2f), (256f * kx / (float)exitTexture.Width), SpriteEffects.None, 0f);
                }
                if (buttonStart.isPressed)
                {
                    spriteBatch.Draw(startTexturePressed, new Vector2(resX / 2f, buttonStart.y + startTexture.Height * (256f * kx / (float)startTexture.Width) / 2f), null, Color.White, 0f, new Vector2(startTexture.Width / 2f, startTexture.Height / 2f), (256f * kx / (float)startTexture.Width), SpriteEffects.None, 0f);
                }
                else
                {
                    spriteBatch.Draw(startTexture, new Vector2(resX / 2f, buttonStart.y + startTexture.Height * (256f * kx / (float)startTexture.Width) / 2f), null, Color.White, 0f, new Vector2(startTexture.Width / 2f, startTexture.Height / 2f), (256f * kx / (float)startTexture.Width), SpriteEffects.None, 0f);
                }

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
                spriteBatch.Draw(helpTexture, new Vector2(resX / 2f, buttonHelp.y + helpTexture.Height * (256f * kx / (float)helpTexture.Width) / 2f), null, Color.White, 0f, new Vector2(helpTexture.Width / 2f, helpTexture.Height / 2f), (256f * kx / (float)helpTexture.Width), SpriteEffects.None, 0f);
                if(gameProcess.isWinning)
                {
                    if (totalTime < 2)
                    {
                        Vector2 centre = font.MeasureString("You win!") / 2f;
                        spriteBatch.DrawString(font, "You win!", new Vector2(resX / 2f, resY / 2f), Color.Red, 0f, centre, kx, SpriteEffects.None, 1f);
                    }
                    else
                    {
                        gameProcess.WinGame();
                        totalTime = 0;
                    }
                }
            } else 
            if (gameProcess.isWin) {
                if (buttonNext.isPressed)
                {
                    spriteBatch.Draw(nextTexturePressed, new Vector2(buttonNext.x, buttonNext.y), null, Color.White, 0f, Vector2.Zero, (256f * kx / (float)nextTexture.Width), SpriteEffects.None, 0f);
                }
                else
                {
                    spriteBatch.Draw(nextTexture, new Vector2(buttonNext.x, buttonNext.y), null, Color.White, 0f, Vector2.Zero, (256f * kx / (float)nextTexture.Width), SpriteEffects.None, 0f);
                }
                if (buttonExit.isPressed)
                {
                    spriteBatch.Draw(exitTexturePressed, new Vector2(resX / 2f, buttonExit.y + exitTexture.Height * (256f * kx / (float)exitTexture.Width) / 2f), null, Color.White, 0f, new Vector2(exitTexture.Width / 2f, exitTexture.Height / 2f), (256f * kx / (float)exitTexture.Width), SpriteEffects.None, 0f);
                }
                else
                {
                    spriteBatch.Draw(exitTexture, new Vector2(resX / 2f, buttonExit.y + exitTexture.Height * (256f * kx / (float)exitTexture.Width) / 2f), null, Color.White, 0f, new Vector2(exitTexture.Width / 2f, exitTexture.Height / 2f), (256f * kx / (float)exitTexture.Width), SpriteEffects.None, 0f);
                }
            }
            else if (gameProcess.isEnd)
            {
                Vector2 centre = font.MeasureString("To be continued...") / 2f;
                spriteBatch.DrawString(font, "To be continued...", new Vector2(resX/2f, resY/2f), Color.Green, 0f, centre, kx, SpriteEffects.None, 1f);
            }
            else if (gameProcess.isHelp)
            {
                Vector2 centre = help.MeasureString("If you want to win, \nyou should connect all flowers \ntapping on them.") / 2f;
                spriteBatch.DrawString(help, "If you want to win, \nyou should connect all flowers \ntapping on them. Have fun.", new Vector2(resX/2, resY/2), Color.Red, 0f, centre, kx, SpriteEffects.None, 1f);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        public void newInitialize()
        {
            
        }
    }
}
