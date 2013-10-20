using System;
using System.Collections;
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
    public class UI : Minigame
    {
        //tool to handle display of the game
        public MediaManager Helper;


        public UI(MainGame game)
            : base(game)
        {
            Helper = game.Helper;
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here

            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here

            base.Update(gameTime);
        }

        public override bool Click(int x, int y)
        {
            bool isInUI = true;
            return isInUI;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {

            int UI_width = 100;
            int line_height = 25;
            int window_width = 800;
            int UI_X = window_width - UI_width;

            MouseState ms = Mouse.GetState();
            Helper.DrawArt(spriteBatch, "Content/cursorSmall30", (int)ms.X - 30 / 2, (int)ms.Y - 30 / 2);

            Helper.DrawArt(spriteBatch, "Content/HUDSmall25", UI_X, 0);
            Helper.DrawText(spriteBatch, "MONEY: " + game.money.ToString(), UI_X, line_height, Color.Black);
            Helper.DrawText(spriteBatch, "BSL: " + game.bloodSugarLevel.ToString(), UI_X, line_height*2, Color.Black);
            Helper.DrawText(spriteBatch, "SCORE: " + game.score, UI_X, line_height * 3, Color.Black);
            Helper.DrawText(spriteBatch, "TIME: " + game.time, UI_X, line_height*4, Color.Black);
            Helper.DrawText(spriteBatch, "BOOKS: ", UI_X, line_height*5, Color.Black);
            Helper.DrawText(spriteBatch, "GRADE: ", UI_X, line_height*6, Color.Black);

            switch (game.ActiveGame)
            {
                case "TAGAME":
                    Helper.DrawText(spriteBatch, "PAPERS LEFT: ", UI_X, line_height * 7, Color.Black);
                    Helper.DrawText(spriteBatch, "CLASS PERCENTAGE: ", UI_X, line_height * 8, Color.Black);
                    break;
                case "BOOKSTOREGAME":
                    Helper.DrawText(spriteBatch, "CUSTOMERS: ", UI_X, line_height * 7, Color.Black);
                    Helper.DrawText(spriteBatch, "TIME TIL CLOSING: ", UI_X, line_height * 8, Color.Black);
                    break;
                default:
                    break;
            }

        }

    }
}
