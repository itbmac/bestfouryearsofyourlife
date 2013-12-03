
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


namespace tbfyoyl.TAGame
{
    public class TAResults : Minigame
    {
        /*
         * The initial splash screen for the game
         */
        private ClickableObject[] menuItems;
        private TAGame tagame;

        public TAResults(MainGame game, TAGame ta)
            : base(game)
        {

            tagame = ta;
            menuItems = new ClickableObject[2];
            menuItems[0] = new ClickableObject(new TextObject("CONTINUE", new Vector2(800, 850)),
                delegate()
                {
                    game.ActiveGame = "TAGAME";
                });
            menuItems[1] = new ClickableObject(new TextObject("QUIT", new Vector2(800, 950)),
                delegate()
                {
                    game.Exit();
                });
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here
            MediaManager.cam = new Camera2d();
            MediaManager.cam.Zoom = 0.533333333f; // = 1200/2250
            MediaManager.cam.Pos = new Vector2(1125, 750);
            base.Initialize();

        }

        public override bool ClickDown(Vector2 pos)
        {
            foreach (GameObject o in menuItems)
            {
                if (o.Contains(pos))
                {
                    activeObject = o;
                    break;
                }
            }
            return base.ClickDown(pos);
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            MediaManager.DrawArt(spriteBatch, "Content/table_clear", 0, 0);
            MediaManager.DrawArt(spriteBatch, "Content/books_pen", 0, 0);
            MediaManager.DrawArt(spriteBatch, "Content/implied_partay", 0, 0);
            MediaManager.DrawArt(spriteBatch, "Content/math", 0, 0);
            MediaManager.DrawArt(spriteBatch, "Content/lamp", 525, -150);
            MediaManager.DrawArt(spriteBatch, "Content/main_paper", 750, 600);


            MediaManager.DrawText(spriteBatch, "test", 750, 600, Color.Black);
            MediaManager.DrawText(spriteBatch, tagame.CurrentScore.ToString(),
                800, 650, Color.Black);
            MediaManager.DrawText(spriteBatch, tagame.TotalScore.ToString(),
                800, 700, Color.Black);

            foreach (ClickableObject o in menuItems)
            {
                o.Draw(spriteBatch);
            }
            base.Draw(spriteBatch);
            MediaManager.DrawArt(spriteBatch, "Content/lamp_light", 0, 0);
        }

    }
}
