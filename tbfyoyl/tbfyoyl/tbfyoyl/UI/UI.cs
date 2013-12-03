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
        private int UI_display_state = 0;
        private int UI_button_state = 0;
        private int UI_MAX_Y = 0;
        private int UI_MIN_X = 1050;
        private int UI_BUTTON_X = 0;
        private int UI_BUTTON_Y = 0;
        ClickableObject[] menuItems;

        public UI(MainGame game)
            : base(game)
        {
            menuItems = new ClickableObject[1];
            menuItems[0] = new ClickableObject(new TextureObject(MediaManager.textures["MENU_MIDDLE"], new Vector2(700, 400)),
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

        public override bool ClickUp(Vector2 pos)
        {
            pos = Vector2.Transform(pos, MediaManager.cam.get_transformation(game.GraphicsDevice));
            bool isInUI = true;
            return isInUI;
        }

        public override bool ClickDown(Vector2 pos)
        {
            pos = Vector2.Transform(pos, MediaManager.cam.get_transformation(game.GraphicsDevice));
            System.Diagnostics.Debug.WriteLine("POS Y: " + pos.Y.ToString());
            
            bool isInUI = true;

            if (pos.X >= UI_MIN_X && pos.Y <= UI_MAX_Y)
            {
                if (UI_display_state == 0)
                    UI_display_state = 1;
                else
                    UI_display_state = 0;

                isInUI = false;
            }
            
            return isInUI;
        }

        public override void MouseOver(Vector2 pos)
        {
            pos = Vector2.Transform(pos, MediaManager.cam.get_transformation(game.GraphicsDevice));
            if (pos.X >= UI_MIN_X && pos.Y <= UI_MAX_Y)
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

            spriteBatch.End();
            spriteBatch.Begin();

            Vector2 mousePos = Vector2.Transform(MediaManager.GetCurMousePos(), MediaManager.cam.get_transformation(game.GraphicsDevice));
   
            /*
            if (game.ActiveGame == "TAGAME" || game.ActiveGame == "BOOKSTOREGAME")
            {

                Color UI_Color = Color.White;

                int UI_Top_Height = 253;
                int UI_Bot_Height = 253;
                int UI_Pad_Height = 10;
                int UI_Mid_Height = -11;

                int UI_width = 150;
                int line_height = 25;
                int window_width = 1200;

                int UI_DIST_BW_AS_AND_HID = 20;
                int UI_X = window_width - UI_width;
                int UI_Y_AS = 5;
                int UI_Y_HID = 0;

                int UI_Font_Padding = 10;
                int UI_Font_X = UI_Font_Padding + UI_X;

                int DEFAULT_NUM_ITEMS_AS = 2;
                int BS_GAME_NUM_ITEMS_AS = 2 + DEFAULT_NUM_ITEMS_AS;
                int TA_GAME_NUM_ITEMS_AS = 2 + DEFAULT_NUM_ITEMS_AS;

                int DEFAULT_NUM_ITEMS_NS = 3;
                int BS_GAME_NUM_ITEMS_NS = 2 + DEFAULT_NUM_ITEMS_NS;
                int TA_GAME_NUM_ITEMS_NS = 2 + DEFAULT_NUM_ITEMS_NS;

                int TA_GAME_HEIGHT_AS = TA_GAME_NUM_ITEMS_AS * line_height + UI_Pad_Height;
                int TA_GAME_HEIGHT_NS = TA_GAME_NUM_ITEMS_NS * line_height + 2 * UI_Pad_Height;
                int BS_GAME_HEIGHT_AS = BS_GAME_NUM_ITEMS_AS * line_height + UI_Pad_Height;
                int BS_GAME_HEIGHT_NS = BS_GAME_NUM_ITEMS_NS * line_height + 2 * UI_Pad_Height;

                if (UI_button_state == 1)
                {
                    foreach (ClickableObject o in menuItems)
                    {
                        o.Draw(spriteBatch);
                    }
                }

                
                switch (game.ActiveGame)
                {
                    case "TAGAME":
                        UI_MAX_Y = TA_GAME_HEIGHT_AS;
                        if (UI_display_state == 1)
                        {
                            MediaManager.DrawArt(spriteBatch, "Content/UI_Bottom_150p", UI_X - 50, TA_GAME_HEIGHT_AS + TA_GAME_HEIGHT_NS - UI_Bot_Height);
                            UI_MAX_Y = TA_GAME_HEIGHT_AS + TA_GAME_HEIGHT_NS;
                        }
                        MediaManager.DrawArt(spriteBatch, "Content/UI_Top_150p", UI_X - 50, TA_GAME_HEIGHT_AS - UI_Top_Height);
                        break;
                    case "BOOKSTOREGAME":
                        UI_MAX_Y = BS_GAME_HEIGHT_AS;
                        if (UI_display_state == 1)
                        {
                            MediaManager.DrawArt(spriteBatch, "Content/UI_Bottom_150p", UI_X - 50, BS_GAME_HEIGHT_AS + BS_GAME_HEIGHT_NS - UI_Bot_Height);
                            UI_MAX_Y = BS_GAME_HEIGHT_AS + BS_GAME_HEIGHT_NS;
                        }
                        MediaManager.DrawArt(spriteBatch, "Content/UI_Top_150p", UI_X - 50, BS_GAME_HEIGHT_AS - UI_Top_Height);
                        break;
                    default:
                        break;
                }

                MediaManager.DrawText(spriteBatch, "TIME: " + game.time, UI_Font_X, UI_Y_AS, UI_Color);
                UI_Y_AS += line_height;
                MediaManager.DrawText(spriteBatch, "BSL: " + game.bloodSugarLevel.ToString(), UI_Font_X, UI_Y_AS, UI_Color);
                UI_Y_AS += line_height;

                switch (game.ActiveGame)
                {
                    case "TAGAME":
                        MediaManager.DrawText(spriteBatch, "PAPERS LEFT: ", UI_Font_X, UI_Y_AS, UI_Color);
                        UI_Y_AS += line_height;
                        MediaManager.DrawText(spriteBatch, "CLASS PERCENTAGE: ", UI_Font_X, UI_Y_AS, UI_Color);
                        UI_Y_AS += line_height;
                        break;
                    case "BOOKSTOREGAME":
                        MediaManager.DrawText(spriteBatch, "CUSTOMERS: ", UI_Font_X, UI_Y_AS, UI_Color);
                        UI_Y_AS += line_height;
                        MediaManager.DrawText(spriteBatch, "TIME TIL CLOSING: ", UI_Font_X, UI_Y_AS, UI_Color);
                        UI_Y_AS += line_height;
                        break;
                    default:
                        break;
                }

                UI_Y_HID = UI_Y_AS + UI_DIST_BW_AS_AND_HID;

                if (UI_display_state == 1)
                {
                    MediaManager.DrawText(spriteBatch, "SCORE: " + game.score, UI_Font_X, UI_Y_HID, UI_Color);
                    UI_Y_HID += line_height;
                    MediaManager.DrawText(spriteBatch, "TIME: " + game.time, UI_Font_X, UI_Y_HID, UI_Color);
                    UI_Y_HID += line_height;
                    MediaManager.DrawText(spriteBatch, "GRADE: ", UI_Font_X, UI_Y_HID, UI_Color);
                    UI_Y_HID += line_height;

                    switch (game.ActiveGame)
                    {
                        case "TAGAME":
                            MediaManager.DrawText(spriteBatch, "PAPERS LEFT: ", UI_Font_X, UI_Y_HID, UI_Color);
                            UI_Y_HID += line_height;
                            MediaManager.DrawText(spriteBatch, "CLASS PERCENTAGE: ", UI_Font_X, UI_Y_HID, UI_Color);
                            UI_Y_HID += line_height;
                            break;
                        case "BOOKSTOREGAME":
                            MediaManager.DrawText(spriteBatch, "CUSTOMERS: ", UI_Font_X, UI_Y_HID, UI_Color);
                            UI_Y_HID += line_height;
                            MediaManager.DrawText(spriteBatch, "TIME TIL CLOSING: ", UI_Font_X, UI_Y_HID, UI_Color);
                            UI_Y_HID += line_height;
                            break;
                        default:
                            break;
                    }
                }

                switch (game.ActiveGame)
                {
                    case "TAGAME":
                        if (UI_display_state == 0)
                        {
                            Vector2 newButtonPos = new Vector2(UI_X, TA_GAME_HEIGHT_AS + UI_Mid_Height);
                            menuItems[0].Position = newButtonPos;
                            if (UI_button_state == 1)
                            {
                                MediaManager.DrawText(spriteBatch, "Click to Expand", UI_X + 22, TA_GAME_HEIGHT_AS + UI_Mid_Height + 14, UI_Color);
                            }
                            UI_BUTTON_Y = TA_GAME_HEIGHT_AS + UI_Mid_Height;
                        }
                        else if (UI_display_state == 1)
                        {
                            Vector2 newButtonPos = new Vector2(UI_X, TA_GAME_HEIGHT_AS + TA_GAME_HEIGHT_NS + UI_Mid_Height);
                            menuItems[0].Position = newButtonPos;
                            if (UI_button_state == 1)
                            {
                                MediaManager.DrawText(spriteBatch, "Click to Hide", UI_X + 37, TA_GAME_HEIGHT_AS + TA_GAME_HEIGHT_NS + UI_Mid_Height + 14, UI_Color);
                            }
                            UI_BUTTON_Y = TA_GAME_HEIGHT_AS + TA_GAME_HEIGHT_NS + UI_Mid_Height;
                        }
                        break;
                    case "BOOKSTOREGAME":
                        if (UI_display_state == 0)
                        {
                            Vector2 newButtonPos = new Vector2(UI_X, BS_GAME_HEIGHT_AS + UI_Mid_Height);
                            menuItems[0].Position = newButtonPos;
                            if (UI_button_state == 1)
                            {
                                MediaManager.DrawText(spriteBatch, "Click to Expand", UI_X + 22, BS_GAME_HEIGHT_AS + UI_Mid_Height + 14, UI_Color);
                            }
                            UI_BUTTON_Y = BS_GAME_HEIGHT_AS + UI_Mid_Height;
                        }
                        else if (UI_display_state == 1)
                        {
                            Vector2 newButtonPos = new Vector2(UI_X, BS_GAME_HEIGHT_AS + BS_GAME_HEIGHT_NS + UI_Mid_Height);
                            menuItems[0].Position = newButtonPos;
                            if (UI_button_state == 1)
                            {
                                MediaManager.DrawText(spriteBatch, "Click to Hide", UI_X + 37, BS_GAME_HEIGHT_AS + BS_GAME_HEIGHT_NS + UI_Mid_Height + 14, UI_Color);
                            }
                            UI_BUTTON_Y = BS_GAME_HEIGHT_AS + BS_GAME_HEIGHT_NS + UI_Mid_Height;
                        }
                        break;
                    default:
                        break;
                }
                UI_BUTTON_X = UI_X;
                 
            }
            */
            MediaManager.DrawArt(spriteBatch, "Content/cursorSmall30", (int)mousePos.X - 30 / 2, (int)mousePos.Y - 30 / 2);
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred,
                        null,
                        null,
                        null,
                        null,
                        null,
                        MediaManager.cam.get_transformation(spriteBatch.GraphicsDevice));
        }

    }

}