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


namespace tbfyoyl
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class MainGame : Microsoft.Xna.Framework.Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        //tool to handle display of the game
        public MediaManager Helper;

        //a list of the contexts in this game
        private Dictionary<String, Minigame> games;
        //the currently active context
        private Minigame activeGame;

        private MouseState prevMouseState;
        private MouseState startClickState;

        //in cents
        public int money;
        //mg/dL
        public int bloodSugarLevel;
        public int score;
        public int time;

        //a public accessor for the activeGame context, allowing safe access
        //to the activeGame
        public string ActiveGame
        {
            get
            {
                foreach (KeyValuePair<String, Minigame> pair in games)
                {
                    if (pair.Value == activeGame)
                        return pair.Key;
                }
                return null;
            }
            set
            {
                if(games.ContainsKey(value))
                    activeGame = games[value];
            }
        }

        public MainGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
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


            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Helper = new MediaManager();
            Helper.Setup(Services, graphics);

            prevMouseState = Mouse.GetState();

            games = new Dictionary<String, Minigame>()
            {
                {"UI", new UI(this)},
                {"WORLDMAP", new WorldMap(this)},
                {"BOOKSTOREGAME", new BookstoreGame(this)},
                {"TAGAME", new TAGame(this)},
                {"SPLASHSCREEN", new SplashScreen(this)},
            };
            ActiveGame = "SPLASHSCREEN";


            foreach (Minigame game in games.Values)
                game.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // TODO: use this.Content to load your game content here
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

            MouseState curMouseState = Mouse.GetState();

            games["UI"].MousePosition(curMouseState.X, curMouseState.Y);

            if ((prevMouseState.LeftButton == ButtonState.Released) && (curMouseState.LeftButton == ButtonState.Pressed))
            {
                startClickState = curMouseState;
            }

            double distance = Math.Sqrt(Math.Pow(startClickState.X - curMouseState.X, 2)
                + Math.Pow(startClickState.Y - curMouseState.Y, 2));

            if ((prevMouseState.LeftButton == ButtonState.Pressed) && (curMouseState.LeftButton == ButtonState.Pressed)
                && (distance > 10))
            {
                activeGame.Drag(prevMouseState.X, prevMouseState.Y, curMouseState.X, curMouseState.Y);
            }

            if ((prevMouseState.LeftButton == ButtonState.Pressed) && (curMouseState.LeftButton == ButtonState.Released))
            {
                if (distance < 10)
                {
                    activeGame.EndDrag(curMouseState.X, curMouseState.Y);
                }
                else if (games["UI"].Click(curMouseState.X, curMouseState.Y))
                {
                    activeGame.Click(curMouseState.X, curMouseState.Y);
                }
            }

            prevMouseState = curMouseState;

            activeGame.Update(gameTime);

            if (gameTime.TotalGameTime.Seconds % 13 == 0)
            {
                money++;
                bloodSugarLevel = Helper.GetRandomInt(40, 250);
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            activeGame.Draw(spriteBatch);

            games["UI"].Draw(spriteBatch);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
