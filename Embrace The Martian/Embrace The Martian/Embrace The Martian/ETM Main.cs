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

namespace Embrace_The_Martian
{
   public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        float audioTimer = 0, totalTimer = 0, waveTotalTimer = 0, finalTimer = 0, lowestWaveTimer = 0;
        float[] waveTimer = new float[21];
        int lowestWave = 0, score = 1000000, highScore = 0, breakTimeLength = 5;
        bool newHighScore = false, newMusic = true, levelBreak = false;
        
        public static bool setUpNewWave = true, newGameSetUp;

        public static int livesTaken;

        public static int wave = -2;
        public static int newWave = 1;
        public static int pastWave = 0;
        public static float newWaveDelay = 0;

        public static int asteroidWidth, bulletWidth, playerWidth, martianWidth, martianHeight, playerHeight, backgroundHeight, backgroundWidth, backgroundPosition, backgroundPosition2;
        public static float backgroundSpeed;
        public static float asteroidYVelocity, asteroidXVelocity, asteroidX, asteroidY, asteroidSpeed;

        public static KeyboardState oldks, oldks2;


        public static SpaceObject sObject;
        /*
        public static SpaceObject.asteroidList; = new List<SpaceObject>();
        public static SpaceObject.playerList   = new List<SpaceObject>();
        public static SpaceObject.bulletList = new List<SpaceObject>();
         */

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
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
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            SpaceObject.helper = new Helper121();
            SpaceObject.helper.Setup(Services, graphics);

            SpaceObject.asteroidList = new List<SpaceObject>();
            SpaceObject.playerList   = new List<SpaceObject>();
            SpaceObject.bulletList = new List<SpaceObject>();

            //SpaceObject sObject;

            //------------------------------------------------------
            sObject = new SpaceObject(SpaceObject.SOType.PLAYER);
            sObject.position.X = 400;
            sObject.position.Y = 300;

            SpaceObject.playerList.Add(sObject);
            //------------------------------------------------------

            asteroidWidth = 40;
            bulletWidth = 5;
            playerHeight = 15;
            playerWidth = 40;
            martianWidth = 235;
            martianHeight = 135;
            backgroundWidth = 1024;
            backgroundHeight = 300;
            backgroundPosition = 0;
            backgroundPosition2 = (backgroundWidth * 2);
            backgroundSpeed = -6.0f;

            for (int i = SpaceObject.asteroidList.Count - 1; i >= 0; i--)
            {
                SpaceObject.asteroidList.Remove(SpaceObject.asteroidList[i]);
            }
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
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            //----------------------Timers------------------------------

            if (wave >= 1 && wave <= 20)
            {
                newWaveDelay = newWaveDelay - gameTime.ElapsedGameTime.Milliseconds / 1000f;
                if (newWaveDelay >= 0)
                {
                    levelBreak = true;
                }
                else
                {
                    waveTimer[wave] = waveTimer[wave] + gameTime.ElapsedGameTime.Milliseconds / 1000f;
                    totalTimer = totalTimer + gameTime.ElapsedGameTime.Milliseconds / 1000f;
                    levelBreak = false;
                }
            }

            audioTimer = audioTimer + gameTime.ElapsedGameTime.Milliseconds / 1000f;

            //-----------------------Music---------------------
            

            if (audioTimer < 1 && newMusic == true)
            {
                SpaceObject.helper.PlaySound("Content/Embrace The Martian");
                newMusic = false;
            }
            else if (audioTimer > 184 && audioTimer < 188 && newMusic == false)
            {
                SpaceObject.helper.PlaySound("Content/The Arrival");
                newMusic = true;
            }
            else if (audioTimer > 320)
            {
                audioTimer = 0;
                newMusic = true;
            }

            // ------------------- Start Menus -----------------

            KeyboardState ks = Keyboard.GetState();

            if (wave <= 0 && ks.IsKeyDown(Keys.Space) && oldks.IsKeyUp(Keys.Space))
            {
                wave = wave + 1;
                newGameSetUp = true;
            }

            oldks = ks;

            // ------------------ First Wave Set-Up --------------------------

            if (wave == 1 && newGameSetUp == true)
            {
                livesTaken = 0;
                SpaceObject.health = 100;
                waveTotalTimer = 0;
                totalTimer = 0;
                finalTimer = 0;
                lowestWave = 0;
                lowestWaveTimer = 0;
                newHighScore = false;
                score = 1000000;
                newGameSetUp = false;
                for (int w = 1; w < 20; w++)
                {
                    waveTimer[w] = 0;
                }

                setupWave(wave, gameTime);
            }

            //------------------Martian Action-------------------------

            if (wave <= 0 || wave > 20)
            {
                for (int i = SpaceObject.playerList.Count - 1; i >= 0; i--)
                {
                    SpaceObject.playerList[i].Update(gameTime);
                }
            }

            //------------------End of Game Operations -------------------

            if (wave >= 21)
            {
                if (setUpNewWave == true)
                {
                    finalTimer = totalTimer;
                    lowestWaveTimer = finalTimer;
                    for (int z = 1; z < 10; z++)
                    {
                        waveTotalTimer = waveTotalTimer + waveTimer[z];
                    }
                    score = 1000000 - (((int)waveTotalTimer * 1000) + (livesTaken * 10000) + ((((int)finalTimer - (int)waveTotalTimer) / (int)finalTimer)) * ((int)waveTotalTimer * 100));
                    if (score > highScore)
                    {
                        highScore = score;
                        newHighScore = true;
                    }
                    setUpNewWave = false;
                }
                else
                {
                    ks = Keyboard.GetState();

                    if (ks.IsKeyDown(Keys.Space))
                    {
                        wave = -2;
                    }

                    oldks = ks;
                }
            }

            //----------------------Play Waves------------------------------
            if (wave > 0 && wave < 21 && levelBreak == false && setUpNewWave == false)
            {
                /*
                foreach (SpaceObject sObject in SpaceObject.asteroidList)
                {
                    sObject.Update(gameTime);
                }

                foreach (SpaceObject sObject in SpaceObject.playerList)
                {
                    sObject.Update(gameTime);
                }

                foreach (SpaceObject sObject in SpaceObject.bulletList)
                {
                    sObject.Update(gameTime);
                }
                */

                if (SpaceObject.asteroidList.Count == 0)//(waveTimer[wave] > waveTimeLength)
                {
                    newWaveDelay = breakTimeLength;
                    wave = wave + 1;
                    setUpNewWave = true;
                    levelBreak = true;

                    SpaceObject.helper.PlaySound("Content/Wave Break");
                }

                if (SpaceObject.health <= 0)
                {
                    livesTaken = livesTaken + 1;
                    newWaveDelay = breakTimeLength;
                    setUpNewWave = true;
                    levelBreak = true;

                    SpaceObject.helper.PlaySound("Content/Death Noise");
                }

                for (int i = SpaceObject.asteroidList.Count - 1; i >= 0; i--)
                {
                    SpaceObject.asteroidList[i].Update(gameTime);
                    if (SpaceObject.asteroidList[i].isDead)
                        SpaceObject.asteroidList.Remove(SpaceObject.asteroidList[i]);
                }

                for (int i = SpaceObject.playerList.Count - 1; i >= 0; i--)
                {
                    SpaceObject.playerList[i].Update(gameTime);
                    if (SpaceObject.playerList[i].isDead)
                        SpaceObject.playerList.Remove(SpaceObject.playerList[i]);
                }

                for (int i = SpaceObject.bulletList.Count - 1; i >= 0; i--)
                {
                    SpaceObject.bulletList[i].Update(gameTime);
                    if (SpaceObject.bulletList[i].isDead)
                    {
                        SpaceObject.bulletList.Remove(SpaceObject.bulletList[i]);
                        SpaceObject.bulletLimit = SpaceObject.bulletLimit + 1;
                    }
                }
            }
            else if (wave > 0 && levelBreak == true && setUpNewWave == true)
            {
                    cleanupWave(gameTime);
                    //levelBreak = false;
            }
            else if (wave > 0 && levelBreak == false && setUpNewWave == true)
            {
                cleanupWave(gameTime);
                setupWave(wave, gameTime);
                setUpNewWave = false;
            }
            //------------------------------------------------------

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            if (newWaveDelay > 0)
                backgroundSpeed = -16.0f;

            if (wave > 20 || wave < 0)
            {
                backgroundSpeed = -30.0f;
            }

            if (wave > -20)
            {
                GraphicsDevice.Clear(Color.Black);

                backgroundPosition = (int)(backgroundPosition + backgroundSpeed);
                backgroundPosition2 = (int)(backgroundPosition2 + backgroundSpeed);

                /*
                while (backgroundPosition > SpaceObject.helper.screenW)
                    backgroundPosition -= SpaceObject.helper.screenW;
                while (backgroundPosition < 0)
                    backgroundPosition += SpaceObject.helper.screenW;
                */
                
                Rectangle r = new Rectangle((backgroundPosition),
                                            (0),
                                            (2 * backgroundWidth),
                                            (2 * backgroundHeight));

                Rectangle r2 = new Rectangle((backgroundPosition2),
                                            (0),
                                            (2 * backgroundWidth),
                                            (2 * backgroundHeight));
                
                Texture2D retVal = SpaceObject.helper.GetArt("Content/BackgroundFinal2");
                if (retVal != null)
                {
                    spriteBatch.Draw(retVal, r, Color.White);
                    spriteBatch.Draw(retVal, r2, Color.White);

                    if (backgroundPosition < - (backgroundWidth * 2))//((backgroundWidth * 2) - SpaceObject.helper.screenW))
                    {
                        //r.X = 0;
                        backgroundPosition = backgroundPosition2 + (backgroundWidth * 2);
                    }

                    if (backgroundPosition2 < - (backgroundWidth * 2))//((backgroundWidth * 2) - SpaceObject.helper.screenW))
                    {
                        //r.X = 0;
                        backgroundPosition2 = backgroundPosition + (backgroundWidth * 2);
                    }

                    /*draw side echos

                    r.X += SpaceObject.helper.screenW;
                    spriteBatch.Draw(retVal, r, Color.White);

                    r.X -= SpaceObject.helper.screenW * 2;
                    spriteBatch.Draw(retVal, r, Color.White);
                    */
                }
            }

            if (wave <= 0)
            {
                foreach (SpaceObject sObject in SpaceObject.playerList)
                {
                    sObject.Draw(spriteBatch);
                }

                SpaceObject.helper.DrawArt(spriteBatch, "Content/HUDSmall25", 0, 575);
                SpaceObject.helper.DrawText(spriteBatch, "Press SPACE to Continue", 350, 575, Color.Black);
            }

            if (wave == -2)
            {
                SpaceObject.helper.DrawArt(spriteBatch, "Content/StartScreen", 0, 0);
            }
            else if (wave == -1)
            {
                SpaceObject.helper.DrawArt(spriteBatch, "Content/PremiseScreen", 0, 0);
            }
            else if (wave == 0)
            {
                SpaceObject.helper.DrawArt(spriteBatch, "Content/InfoScreen", 0, 0);
            }
            else if (wave > 0 && wave < 21)
            {
                foreach (SpaceObject sObject in SpaceObject.asteroidList)
                {
                    sObject.Draw(spriteBatch);
                }

                foreach (SpaceObject sObject in SpaceObject.playerList)
                {
                    sObject.Draw(spriteBatch);
                }

                foreach (SpaceObject sObject in SpaceObject.bulletList)
                {
                    sObject.Draw(spriteBatch);
                }

                if (newWaveDelay <= 0)
                {
                    MouseState ms = Mouse.GetState();
                    SpaceObject.helper.DrawArt(spriteBatch, "Content/cursorSmall30", (int)ms.X - 64 / 2, (int)ms.Y - 64 / 2);
                }

                SpaceObject.helper.DrawArt(spriteBatch, "Content/HUDSmall25", 0, 575);
                SpaceObject.helper.DrawText(spriteBatch, "On Wave: " + wave, 35, 575, Color.Black);
                SpaceObject.helper.DrawText(spriteBatch, "Wave Time: " + waveTimer[wave].ToString(".#"), 175, 575, Color.Black);
                SpaceObject.helper.DrawText(spriteBatch, "Total Time: " + totalTimer.ToString("."), 340, 575, Color.Black);
                SpaceObject.helper.DrawText(spriteBatch, "Deaths: " + livesTaken, 500, 575, Color.Black);
                SpaceObject.helper.DrawText(spriteBatch, "Asteroids: " + SpaceObject.asteroidList.Count, 625, 575, Color.Black);
                SpaceObject.helper.DrawText(spriteBatch, "Health: " + SpaceObject.health, 760, 575, Color.Black);
                //SpaceObject.helper.DrawText(spriteBatch, "Bullets: " + SpaceObject.bulletLimit, 780, 575, Color.Black);
            }

            else if (wave > 20)
            {
                foreach (SpaceObject sObject in SpaceObject.playerList)
                {
                    sObject.Draw(spriteBatch);
                }

                SpaceObject.helper.DrawArt(spriteBatch, "Content/EndScreen", 0, 0);
                SpaceObject.helper.DrawArt(spriteBatch, "Content/HUD", 0, 545);
                spriteBatch.DrawString(SpaceObject.helper.mediumFont2, "Time (sec): " + finalTimer.ToString("."), new Vector2(50, 550), Color.Black);
                spriteBatch.DrawString(SpaceObject.helper.mediumFont2, "Deaths: " + livesTaken, new Vector2(275, 550), Color.Black);
                spriteBatch.DrawString(SpaceObject.helper.mediumFont2, "Score: " + score, new Vector2(450, 550), Color.Black);
                if (!newHighScore)
                    spriteBatch.DrawString(SpaceObject.helper.mediumFont2, "High Score: " + highScore, new Vector2(630, 550), Color.Black);
                else
                    spriteBatch.DrawString(SpaceObject.helper.mediumFont2, "NEW HIGH SCORE!!", new Vector2(630, 550), Color.Black);
                SpaceObject.helper.DrawText(spriteBatch, "Press SPACE to Continue", 350, 575, Color.Black);
            }

            //spriteBatch.DrawString(SpaceObject.helper.mediumFont2, "Audio Timer: " + audioTimer.ToString(".##"), new Vector2(600, 30), Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }


        void setupWave(int newWaveIndex, GameTime gameTime)
        {
            SpaceObject.health = 100;
            waveTimer[wave] = 0;

            cleanupWave(gameTime);

            if (wave == 1)
            {
                backgroundSpeed = -4.0f;
                //SpaceObject.asteroidList = new List<SpaceObject>();
                for (int g = 0; g < 5; ++g)
                {

                    asteroidX = SpaceObject.helper.GetRandomFloat(SpaceObject.helper.screenW - 250, SpaceObject.helper.screenW - 10);
                    asteroidY = SpaceObject.helper.GetRandomFloat(10, SpaceObject.helper.screenH - 10);
                    asteroidXVelocity = SpaceObject.helper.GetRandomFloat(-1, 2);
                    asteroidYVelocity = SpaceObject.helper.GetRandomFloat(-2, 3);
                    asteroidSpeed = -2.0f;

                    createAsteroid(asteroidX, asteroidY, asteroidXVelocity, asteroidYVelocity, asteroidSpeed);
                }
            }

            else if (wave == 2)
            {
                backgroundSpeed = -4.0f;
                //SpaceObject.asteroidList = new List<SpaceObject>();
                for (int g = 0; g < 7; ++g)
                {
                    asteroidX = SpaceObject.helper.GetRandomFloat(SpaceObject.helper.screenW - 110, SpaceObject.helper.screenW - 10);
                    asteroidY = SpaceObject.helper.GetRandomFloat(10, SpaceObject.helper.screenH - 10);
                    asteroidXVelocity = SpaceObject.helper.GetRandomFloat(-1, 2);
                    asteroidYVelocity = SpaceObject.helper.GetRandomFloat(-2, 2);
                    asteroidSpeed = -2.0f;

                    createAsteroid(asteroidX, asteroidY, asteroidXVelocity, asteroidYVelocity, asteroidSpeed);
                }
            }

            else if (wave == 3)
            {
                backgroundSpeed = -6.0f;
                //SpaceObject.asteroidList = new List<SpaceObject>();
                for (int g = 0; g < 7; ++g)
                {
                    asteroidX = SpaceObject.helper.GetRandomFloat(SpaceObject.helper.screenW - 110, SpaceObject.helper.screenW - 10);
                    asteroidY = SpaceObject.helper.GetRandomFloat(10, SpaceObject.helper.screenH - 10);
                    asteroidXVelocity = SpaceObject.helper.GetRandomFloat(-1, 2);
                    asteroidYVelocity = SpaceObject.helper.GetRandomFloat(-2, 2);
                    asteroidSpeed = -2.0f;

                    createAsteroid(asteroidX, asteroidY, asteroidXVelocity, asteroidYVelocity, asteroidSpeed);
                }
            }

            else if (wave == 4)
            {
                backgroundSpeed = -6.0f;
                //SpaceObject.asteroidList = new List<SpaceObject>();
                for (int g = 0; g < 7; ++g)
                {
                    asteroidX = SpaceObject.helper.GetRandomFloat(SpaceObject.helper.screenW - 110, SpaceObject.helper.screenW - 10);
                    asteroidY = SpaceObject.helper.GetRandomFloat(10, SpaceObject.helper.screenH - 10);
                    asteroidXVelocity = SpaceObject.helper.GetRandomFloat(-1, 2);
                    asteroidYVelocity = SpaceObject.helper.GetRandomFloat(-2, 2);
                    asteroidSpeed = -3.0f;

                    createAsteroid(asteroidX, asteroidY, asteroidXVelocity, asteroidYVelocity, asteroidSpeed);
                }
            }

            else if (wave == 5)
            {
                backgroundSpeed = -7.0f;
                //SpaceObject.asteroidList = new List<SpaceObject>();
                for (int g = 0; g < 10; ++g)
                {
                    asteroidX = SpaceObject.helper.GetRandomFloat(SpaceObject.helper.screenW - 110, SpaceObject.helper.screenW - 10);
                    asteroidY = SpaceObject.helper.GetRandomFloat(10, SpaceObject.helper.screenH - 10);
                    asteroidXVelocity = SpaceObject.helper.GetRandomFloat(-1, 2);
                    asteroidYVelocity = SpaceObject.helper.GetRandomFloat(-2, 2);
                    asteroidSpeed = -2.0f;

                    createAsteroid(asteroidX, asteroidY, asteroidXVelocity, asteroidYVelocity, asteroidSpeed);
                }
            }

            else if (wave == 6)
            {
                backgroundSpeed = -3.0f;
                //SpaceObject.asteroidList = new List<SpaceObject>();
                for (int g = 0; g < 15; ++g)
                {
                    asteroidX = SpaceObject.helper.GetRandomFloat(SpaceObject.helper.screenW - 110, SpaceObject.helper.screenW - 10);
                    asteroidY = SpaceObject.helper.GetRandomFloat(10, SpaceObject.helper.screenH - 10);
                    asteroidXVelocity = SpaceObject.helper.GetRandomFloat(-1, 2);
                    asteroidYVelocity = SpaceObject.helper.GetRandomFloat(-2, 2);
                    asteroidSpeed = -2.0f;

                    createAsteroid(asteroidX, asteroidY, asteroidXVelocity, asteroidYVelocity, asteroidSpeed);
                }
            }

            else if (wave == 7)
            {
                backgroundSpeed = -7.0f;
                //SpaceObject.asteroidList = new List<SpaceObject>();
                for (int g = 0; g < 7; ++g)
                {
                    asteroidX = SpaceObject.helper.GetRandomFloat(SpaceObject.helper.screenW - 110, SpaceObject.helper.screenW - 10);
                    asteroidY = SpaceObject.helper.GetRandomFloat(10, SpaceObject.helper.screenH - 10);
                    asteroidXVelocity = SpaceObject.helper.GetRandomFloat(-1, 2);
                    asteroidYVelocity = SpaceObject.helper.GetRandomFloat(-2, 2);
                    asteroidSpeed = -4.0f;

                    createAsteroid(asteroidX, asteroidY, asteroidXVelocity, asteroidYVelocity, asteroidSpeed);
                }
            }

            else if (wave == 8)
            {
                backgroundSpeed = -7.0f;
                //SpaceObject.asteroidList = new List<SpaceObject>();
                for (int g = 0; g < 6; ++g)
                {
                    asteroidX = SpaceObject.helper.GetRandomFloat(SpaceObject.helper.screenW - 110, SpaceObject.helper.screenW - 10);
                    asteroidY = SpaceObject.helper.GetRandomFloat(10, SpaceObject.helper.screenH - 10);
                    asteroidXVelocity = SpaceObject.helper.GetRandomFloat(-1, 2);
                    asteroidYVelocity = SpaceObject.helper.GetRandomFloat(-3, 3);
                    asteroidSpeed = -4.0f;

                    createAsteroid(asteroidX, asteroidY, asteroidXVelocity, asteroidYVelocity, asteroidSpeed);
                }
            }

            else if (wave == 9)
            {
                backgroundSpeed = -7.0f;
                //SpaceObject.asteroidList = new List<SpaceObject>();
                for (int g = 0; g < 8; ++g)
                {
                    asteroidX = SpaceObject.helper.GetRandomFloat(SpaceObject.helper.screenW - 110, SpaceObject.helper.screenW - 10);
                    asteroidY = SpaceObject.helper.GetRandomFloat(10, SpaceObject.helper.screenH - 10);
                    asteroidXVelocity = SpaceObject.helper.GetRandomFloat(-1, 4);
                    asteroidYVelocity = SpaceObject.helper.GetRandomFloat(-4, 4);
                    asteroidSpeed = -4.0f;

                    createAsteroid(asteroidX, asteroidY, asteroidXVelocity, asteroidYVelocity, asteroidSpeed);
                }
            }

            else if (wave == 10)
            {
                backgroundSpeed = -10.0f;
                //SpaceObject.asteroidList = new List<SpaceObject>();
                for (int g = 0; g < 5; ++g)
                {
                    asteroidX = SpaceObject.helper.GetRandomFloat(SpaceObject.helper.screenW - 110, SpaceObject.helper.screenW - 10);
                    asteroidY = SpaceObject.helper.GetRandomFloat(10, SpaceObject.helper.screenH - 10);
                    asteroidXVelocity = SpaceObject.helper.GetRandomFloat(-1, 2);
                    asteroidYVelocity = SpaceObject.helper.GetRandomFloat(-2, 2);
                    asteroidSpeed = -6.0f;

                    createAsteroid(asteroidX, asteroidY, asteroidXVelocity, asteroidYVelocity, asteroidSpeed);
                }
            }

            else if (wave == 11)
            {
                backgroundSpeed = -10.0f;
                //SpaceObject.asteroidList = new List<SpaceObject>();
                for (int g = 0; g < 8; ++g)
                {
                    asteroidX = SpaceObject.helper.GetRandomFloat(SpaceObject.helper.screenW - 110, SpaceObject.helper.screenW - 10);
                    asteroidY = SpaceObject.helper.GetRandomFloat(10, SpaceObject.helper.screenH - 10);
                    asteroidXVelocity = SpaceObject.helper.GetRandomFloat(-1, 2);
                    asteroidYVelocity = SpaceObject.helper.GetRandomFloat(-2, 2);
                    asteroidSpeed = -6.0f;

                    createAsteroid(asteroidX, asteroidY, asteroidXVelocity, asteroidYVelocity, asteroidSpeed);
                }
            }
            else if (wave == 12)
            {
                backgroundSpeed = -10.0f;
                //SpaceObject.asteroidList = new List<SpaceObject>();
                for (int g = 0; g < 12; ++g)
                {
                    asteroidX = SpaceObject.helper.GetRandomFloat(SpaceObject.helper.screenW - 110, SpaceObject.helper.screenW - 10);
                    asteroidY = SpaceObject.helper.GetRandomFloat(10, SpaceObject.helper.screenH - 10);
                    asteroidXVelocity = SpaceObject.helper.GetRandomFloat(-1, 2);
                    asteroidYVelocity = SpaceObject.helper.GetRandomFloat(-2, 2);
                    asteroidSpeed = -6.0f;

                    createAsteroid(asteroidX, asteroidY, asteroidXVelocity, asteroidYVelocity, asteroidSpeed);
                }
            }

            else if (wave == 13)
            {
                backgroundSpeed = -10.0f;
                //SpaceObject.asteroidList = new List<SpaceObject>();
                for (int g = 0; g < 5; ++g)
                {
                    asteroidX = SpaceObject.helper.GetRandomFloat(SpaceObject.helper.screenW - 210, SpaceObject.helper.screenW - 10);
                    asteroidY = SpaceObject.helper.GetRandomFloat(10, SpaceObject.helper.screenH - 10);
                    asteroidXVelocity = SpaceObject.helper.GetRandomFloat(-1, 2);
                    asteroidYVelocity = SpaceObject.helper.GetRandomFloat(-2, 2);
                    asteroidSpeed = -8.0f;

                    createAsteroid(asteroidX, asteroidY, asteroidXVelocity, asteroidYVelocity, asteroidSpeed);
                }
            }

            else if (wave == 14)
            {
                backgroundSpeed = -12.0f;
                //SpaceObject.asteroidList = new List<SpaceObject>();
                for (int g = 0; g < 5; ++g)
                {
                    asteroidX = SpaceObject.helper.GetRandomFloat(SpaceObject.helper.screenW - 210, SpaceObject.helper.screenW - 10);
                    asteroidY = SpaceObject.helper.GetRandomFloat(10, SpaceObject.helper.screenH - 10);
                    asteroidXVelocity = SpaceObject.helper.GetRandomFloat(-1, 2);
                    asteroidYVelocity = SpaceObject.helper.GetRandomFloat(-2, 2);
                    asteroidSpeed = -4.0f;

                    createAsteroid(asteroidX, asteroidY, asteroidXVelocity, asteroidYVelocity, asteroidSpeed);
                }
            }

            else if (wave == 15)
            {
                backgroundSpeed = -12.0f;
                //SpaceObject.asteroidList = new List<SpaceObject>();
                for (int g = 0; g < 7; ++g)
                {
                    asteroidX = SpaceObject.helper.GetRandomFloat(SpaceObject.helper.screenW - 210, SpaceObject.helper.screenW - 10);
                    asteroidY = SpaceObject.helper.GetRandomFloat(10, SpaceObject.helper.screenH - 10);
                    asteroidXVelocity = SpaceObject.helper.GetRandomFloat(-1, 2);
                    asteroidYVelocity = SpaceObject.helper.GetRandomFloat(-3, 3);
                    asteroidSpeed = -4.0f;

                    createAsteroid(asteroidX, asteroidY, asteroidXVelocity, asteroidYVelocity, asteroidSpeed);
                }
            }

            else if (wave == 16)
            {
                backgroundSpeed = -10.0f;
                //SpaceObject.asteroidList = new List<SpaceObject>();
                for (int g = 0; g < 5; ++g)
                {
                    asteroidX = SpaceObject.helper.GetRandomFloat(SpaceObject.helper.screenW - 110, SpaceObject.helper.screenW - 10);
                    asteroidY = SpaceObject.helper.GetRandomFloat(10, SpaceObject.helper.screenH - 10);
                    asteroidXVelocity = SpaceObject.helper.GetRandomFloat(-1, 2);
                    asteroidYVelocity = SpaceObject.helper.GetRandomFloat(-5, 5);
                    asteroidSpeed = -6.0f;

                    createAsteroid(asteroidX, asteroidY, asteroidXVelocity, asteroidYVelocity, asteroidSpeed);
                }
            }

            else if (wave == 17)
            {
                backgroundSpeed = -10.0f;
                //SpaceObject.asteroidList = new List<SpaceObject>();
                for (int g = 0; g < 5; ++g)
                {
                    asteroidX = SpaceObject.helper.GetRandomFloat(SpaceObject.helper.screenW - 110, SpaceObject.helper.screenW - 10);
                    asteroidY = SpaceObject.helper.GetRandomFloat(10, SpaceObject.helper.screenH - 10);
                    asteroidXVelocity = SpaceObject.helper.GetRandomFloat(-1, 2);
                    asteroidYVelocity = SpaceObject.helper.GetRandomFloat(-20, 20);
                    asteroidSpeed = -6.0f;

                    createAsteroid(asteroidX, asteroidY, asteroidXVelocity, asteroidYVelocity, asteroidSpeed);
                }
            }

            else if (wave == 18)
            {
                backgroundSpeed = -10.0f;
                //SpaceObject.asteroidList = new List<SpaceObject>();
                for (int g = 0; g < 7; ++g)
                {
                    asteroidX = SpaceObject.helper.GetRandomFloat(SpaceObject.helper.screenW - 210, SpaceObject.helper.screenW - 10);
                    asteroidY = SpaceObject.helper.GetRandomFloat(10, SpaceObject.helper.screenH - 10);
                    asteroidXVelocity = SpaceObject.helper.GetRandomFloat(-1, 2);
                    asteroidYVelocity = SpaceObject.helper.GetRandomFloat(-20, 20);
                    asteroidSpeed = -6.0f;

                    createAsteroid(asteroidX, asteroidY, asteroidXVelocity, asteroidYVelocity, asteroidSpeed);
                }
            }

            else if (wave == 19)
            {
                backgroundSpeed = -15.0f;
                //SpaceObject.asteroidList = new List<SpaceObject>();
                for (int g = 0; g < 4; ++g)
                {
                    asteroidX = SpaceObject.helper.GetRandomFloat(SpaceObject.helper.screenW - 110, SpaceObject.helper.screenW - 10);
                    asteroidY = SpaceObject.helper.GetRandomFloat(10, SpaceObject.helper.screenH - 10);
                    asteroidXVelocity = SpaceObject.helper.GetRandomFloat(-1, 2);
                    asteroidYVelocity = SpaceObject.helper.GetRandomFloat(-4, 4);
                    asteroidSpeed = -10.0f;

                    createAsteroid(asteroidX, asteroidY, asteroidXVelocity, asteroidYVelocity, asteroidSpeed);
                }
            }

            else if (wave == 20)
            {
                backgroundSpeed = -25.0f;
                //SpaceObject.asteroidList = new List<SpaceObject>();
                for (int g = 0; g < 4; ++g)
                {
                    asteroidX = SpaceObject.helper.GetRandomFloat(SpaceObject.helper.screenW - 110, SpaceObject.helper.screenW - 10);
                    asteroidY = SpaceObject.helper.GetRandomFloat(10, SpaceObject.helper.screenH - 10);
                    asteroidXVelocity = SpaceObject.helper.GetRandomFloat(-1, 2);
                    asteroidYVelocity = SpaceObject.helper.GetRandomFloat(-4, 4);
                    asteroidSpeed = -18.0f;

                    createAsteroid(asteroidX, asteroidY, asteroidXVelocity, asteroidYVelocity, asteroidSpeed);
                }
            }
        }

        void createAsteroid(float astX, float astY, float astXVel, float astYVel, float astSpeed)
        {
            sObject = new SpaceObject(SpaceObject.SOType.ASTEROID);
            sObject.position.X = astX;
            sObject.position.Y = astY;
            sObject.velocity.X = astXVel;
            sObject.velocity.Y = astYVel;
            sObject.asteroidDriftVelocity.X = astSpeed;
            sObject.asteroidSize = 1;

            SpaceObject.asteroidList.Add(sObject);
        }

        void oldsetupWave(int newWaveIndex, GameTime gameTime)
        {
            SpaceObject.health = 100;
            waveTimer[wave] = 0;

            //cleanupWave(gameTime);

            if (wave > 1)
            {
                //SpaceObject.asteroidList = new List<SpaceObject>();
                for (int g = 0; g < 5; ++g)
                {
                    
                    sObject = new SpaceObject(SpaceObject.SOType.ASTEROID);
                    sObject.position.X = 0;//SpaceObject.helper.GetRandomFloat(810, 820);
                    sObject.position.Y = 0;//SpaceObject.helper.GetRandomFloat(10, 11);
                    sObject.velocity.X = 0;//SpaceObject.helper.GetRandomFloat(-1, 1);
                    sObject.velocity.Y = 0;//SpaceObject.helper.GetRandomFloat(-2, 2);
                    sObject.asteroidDriftVelocity.X = 0f;
                    sObject.asteroidSize = 1;

                    SpaceObject.asteroidList.Add(sObject);
                }
            }

            else if (wave == 2)
            {
                //SpaceObject.asteroidList = new List<SpaceObject>();
                for (int g = 0; g < 7; ++g)
                {
                    sObject = new SpaceObject(SpaceObject.SOType.ASTEROID);
                    sObject.position.X = SpaceObject.helper.GetRandomFloat(10, 800 - 10);
                    sObject.position.Y = SpaceObject.helper.GetRandomFloat(10, 500 - 10);
                    sObject.velocity.X = SpaceObject.helper.GetRandomFloat(-1, 2);
                    sObject.velocity.Y = SpaceObject.helper.GetRandomFloat(-2, 2);
                    sObject.asteroidDriftVelocity.X = -2.0f;
                    sObject.asteroidSize = 1;

                    SpaceObject.asteroidList.Add(sObject);
                }
            }
        }
        
        void cleanupWave(GameTime gameTime)
        {
            for (int i = SpaceObject.asteroidList.Count - 1; i >= 0; i--)
            {
                SpaceObject.asteroidList.Remove(SpaceObject.asteroidList[i]);
            }

            for (int i = SpaceObject.playerList.Count - 1; i >= 0; i--)
            {
                SpaceObject.playerList[i].Update(gameTime);
            }

            for (int i = SpaceObject.bulletList.Count - 1; i >= 0; i--)
            {
                SpaceObject.bulletList.Remove(SpaceObject.bulletList[i]);
            }  
        }

    }
}

