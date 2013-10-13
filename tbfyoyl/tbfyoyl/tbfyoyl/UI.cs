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

        public override void Draw(SpriteBatch spriteBatch)
        {
            MouseState ms = Mouse.GetState();
            Helper.DrawArt(spriteBatch, "Content/cursorSmall30", (int)ms.X - 64 / 2, (int)ms.Y - 64 / 2);
                Helper.DrawArt(spriteBatch, "Content/HUDSmall25", 0, 575);
                Helper.DrawText(spriteBatch, "MONEY: " + game.money.ToString(), 35, 575, Color.Black);
                Helper.DrawText(spriteBatch, "BSL: " + game.bloodSugarLevel.ToString() , 195, 575, Color.Black);
                Helper.DrawText(spriteBatch, "SCORE: " + game.score, 340, 575, Color.Black);
                Helper.DrawText(spriteBatch, "TIME: " + game.time, 500, 575, Color.Black);
                Helper.DrawText(spriteBatch, "BOOKS: " , 625, 575, Color.Black);
                Helper.DrawText(spriteBatch, "GRADE: " , 760, 575, Color.Black);
            
        }

    }
}
