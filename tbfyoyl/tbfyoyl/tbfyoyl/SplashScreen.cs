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
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class SplashScreen : Minigame
    {

        public int currentOption; //0=Play, 1=Exit
        public int numOptions;
        static public Color selectedColor = Color.Red;
        static public Color unselectedColor = Color.White;
        static KeyboardState prevKeyState;

        public SplashScreen(MainGame game)
            : base(game)
        {
            currentOption = 0;
            numOptions = 2;
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {

            KeyboardState curKeyState = Keyboard.GetState();

            if (currentOption < numOptions && curKeyState.IsKeyUp(Keys.Down) && prevKeyState.IsKeyDown(Keys.Down))
                currentOption++;
            if (currentOption > 0  && curKeyState.IsKeyUp(Keys.Up) && prevKeyState.IsKeyDown(Keys.Up))
                currentOption--;
            if (curKeyState.IsKeyUp(Keys.Enter) && prevKeyState.IsKeyDown(Keys.Enter))
            {
                switch (currentOption)
                {
                    case 0:
                        game.activeGame = 1;
                        break;
                    case 1:
                        game.Exit();
                        break;
                    default:
                        break;
                }
            }

            prevKeyState = curKeyState;

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            System.Diagnostics.Debug.WriteLine(" calling draw ");

            spriteBatch.GraphicsDevice.Clear(Color.Black);

            //Main.helper.DrawFilledRectangle(spriteBatch, new Rectangle(0,0,640,720), Color.Blue);

            //Main.helper.DrawText(spriteBatch, "Main Menu", 352, 90, unselectedColor);
            spriteBatch.DrawString(game.helper.mediumFont2, "Time To Pretend", new Vector2(130, 50), Color.White);

            int height = 300;
            int heightStep = 70;

            game.helper.DrawText(spriteBatch, "Play", 510, height, currentOption == 0 ? selectedColor : unselectedColor);
            height = height + heightStep;
            game.helper.DrawText(spriteBatch, "Exit", 485, height, currentOption == 1 ? selectedColor : unselectedColor);
            height = height + heightStep;

        }

    }
}
