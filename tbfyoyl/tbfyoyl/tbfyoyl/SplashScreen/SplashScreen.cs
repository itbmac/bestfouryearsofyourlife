
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
    public class SplashScreen : Minigame
    {
        /*
         * The initial splash screen for the game
         */
        private ClickableObject[] menuItems;

        public SplashScreen(MainGame game)
            : base(game)
        {
            menuItems = new ClickableObject[2];
            menuItems[0] = new ClickableObject(new TextObject("                          ", new Vector2(850, 800)),
                delegate()
                {
                    game.ActiveGame = "TAGAME";
                });
            menuItems[1] = new ClickableObject(new TextObject("                          ", new Vector2(850, 1110)),
                delegate()
                {
                    game.Exit();
                });
        }

        Camera2d current;
        Camera2d final;
        double startSeconds;

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {

            game.Restart();
            //MediaManager.dark_beat.Stop();
            MediaManager.emilia.Play();
            // TODO: Add your initialization code here
            final = new Camera2d();
            final.Zoom = 1.0f; // = 1200/2250
            final.Pos = new Vector2(1125, 975);

            // TODO: Add your initialization code here
            current = new Camera2d();
            current.Zoom = 0.533333333f; // = 1200/2250
            current.Pos = new Vector2(1125, 750);

            MediaManager.cam = current;

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

            if (startSeconds == 0)
            {
                startSeconds = gameTime.TotalGameTime.Seconds;
            }

            if (current.Zoom >= final.Zoom || current.Pos.Y >= final.Pos.Y)
            {
                MediaManager.cam = final;
            }
            else if((gameTime.TotalGameTime.TotalSeconds - startSeconds) > 1.0)
            {
                float scale = gameTime.ElapsedGameTime.Milliseconds / 1000f / 2f;
                current.Pos += new Vector2(0.0f, 225 * scale);
                current.Zoom += (scale * 0.466666667f);
                MediaManager.cam = current;
            }

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            MediaManager.DrawArt(spriteBatch, "Content/table_clear", 0, 0);
            MediaManager.DrawArt(spriteBatch, "Content/books_pen", 0, 0);
            MediaManager.DrawArt(spriteBatch, "Content/implied_partay", 0, 0);
            MediaManager.DrawArt(spriteBatch, "Content/math", 0, 0);
            MediaManager.DrawArt(spriteBatch, "Content/lamp", 525, -150);
            MediaManager.DrawArt(spriteBatch, "Content/splash_screen_paper", 750, 600);

            foreach (ClickableObject o in menuItems)
            {
                o.Draw(spriteBatch);
            }
            base.Draw(spriteBatch);
            MediaManager.DrawArt(spriteBatch, "Content/lamp_light", 0, 0);
        }

    }
}
