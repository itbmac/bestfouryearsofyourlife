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
        private int UI_display_state = 0;
        private int UI_button_state = 0;
        MenuObject[] menuItems;

        public UI(MainGame game)
            : base(game)
        {
            Helper = game.Helper;
            
            menuItems = new MenuObject[1];
            menuItems[0] = new MenuObject(new DrawableObject(MediaManager.textures["MENU1"], new Vector2(700, 400)),
                delegate()
                {
                    if (UI_display_state == 0)
                        UI_display_state = 1;
                    else
                        UI_display_state = 0;
                });
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
            foreach (MenuObject o in menuItems)
                o.Click(x, y);
            
            bool isInUI = true;
            return isInUI;
        }

        public override void MousePosition(int x, int y)
        {
            if (menuItems[0].InsideBoundingBox(x, y))
            {
                UI_button_state = 1;
            }
            else
            {
                UI_button_state = 0;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {

            int UI_width = 100;
            int line_height = 25;
            int window_width = 800;
            int UI_DIST_BW_AS_AND_HID = 20;
            int UI_X = window_width - UI_width;
            int UI_Y_AS = 0;
            int UI_Y_HID = 0;

            MouseState ms = Mouse.GetState();
            Helper.DrawArt(spriteBatch, "Content/cursorSmall30", (int)ms.X - 30 / 2, (int)ms.Y - 30 / 2);

            Helper.DrawArt(spriteBatch, "Content/HUDSmall25", UI_X, 0);

            Helper.DrawText(spriteBatch, "TIME: " + game.time, UI_X, UI_Y_AS, Color.Black); 
            UI_Y_AS += line_height;
            Helper.DrawText(spriteBatch, "BSL: " + game.bloodSugarLevel.ToString(), UI_X, UI_Y_AS, Color.Black);
            UI_Y_AS += line_height;

            switch (game.ActiveGame)
            {
                case "TAGAME":
                    Helper.DrawText(spriteBatch, "PAPERS LEFT: ", UI_X, UI_Y_AS, Color.Black);
                    UI_Y_AS += line_height;
                    Helper.DrawText(spriteBatch, "CLASS PERCENTAGE: ", UI_X, UI_Y_AS, Color.Black);
                    UI_Y_AS += line_height;
                    break;
                case "BOOKSTOREGAME":
                    Helper.DrawText(spriteBatch, "CUSTOMERS: ", UI_X, UI_Y_AS, Color.Black);
                    UI_Y_AS += line_height;
                    Helper.DrawText(spriteBatch, "TIME TIL CLOSING: ", UI_X, UI_Y_AS, Color.Black);
                    UI_Y_AS += line_height;
                    break;
                default:
                    break;
            }

            UI_Y_HID = UI_Y_AS + UI_DIST_BW_AS_AND_HID;

            if (UI_display_state == 1)
            {
                Helper.DrawText(spriteBatch, "SCORE: " + game.score, UI_X, UI_Y_HID, Color.Black);
                UI_Y_HID += line_height;
                Helper.DrawText(spriteBatch, "TIME: " + game.time, UI_X, UI_Y_HID, Color.Black);
                UI_Y_HID += line_height;
                Helper.DrawText(spriteBatch, "GRADE: ", UI_X, UI_Y_HID, Color.Black);
                UI_Y_HID += line_height;

                switch (game.ActiveGame)
                {
                    case "TAGAME":
                        Helper.DrawText(spriteBatch, "PAPERS LEFT: ", UI_X, UI_Y_HID, Color.Black);
                        UI_Y_HID += line_height;
                        Helper.DrawText(spriteBatch, "CLASS PERCENTAGE: ", UI_X, UI_Y_HID, Color.Black);
                        UI_Y_HID += line_height;
                        break;
                    case "BOOKSTOREGAME":
                        Helper.DrawText(spriteBatch, "CUSTOMERS: ", UI_X, UI_Y_HID, Color.Black);
                        UI_Y_HID += line_height;
                        Helper.DrawText(spriteBatch, "TIME TIL CLOSING: ", UI_X, UI_Y_HID, Color.Black);
                        UI_Y_HID += line_height;
                        break;
                    default:
                        break;
                }
            }

            Vector2 newButtonPos = new Vector2(UI_X, UI_Y_HID + UI_DIST_BW_AS_AND_HID);
            menuItems[0].SetPosition(newButtonPos);

            if (UI_button_state == 1)
            {
                foreach (MenuObject o in menuItems)
                {
                    o.Draw(spriteBatch);
                }
            }
        }

    }
}
