using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace MainGame
{
    class Menu
    {
        static public int menuOptions; //1=Play, 2=Edit, 3=Test, 4=Load, 5=Help, 6=Exit
        static public Color selectedColor = Color.Red;
        static public Color unselectedColor = Color.White;

        static KeyboardState lastFrameKeyState;

        static Color[] optionColors = new Color[6] { selectedColor, unselectedColor, unselectedColor, unselectedColor, unselectedColor, unselectedColor };

        /*************************************************************************
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Main.helper = new Helper121();
            Main.helper.Setup(Services, graphics);
        }
        */
        //*************************************************************************
        static public void Update(GameTime gameTime)
        {
            KeyboardState curKeyState = Keyboard.GetState();

            if (menuOptions < 3 && curKeyState.IsKeyUp(Keys.Down) && lastFrameKeyState.IsKeyDown(Keys.Down))
            {
                optionColors[menuOptions] = unselectedColor;
                menuOptions = menuOptions + 1;
                optionColors[menuOptions] = selectedColor;
            }

            if (menuOptions > 0 && curKeyState.IsKeyUp(Keys.Up) && lastFrameKeyState.IsKeyDown(Keys.Up))
            {
                optionColors[menuOptions] = unselectedColor;
                menuOptions = menuOptions - 1;
                optionColors[menuOptions] = selectedColor;
            }

            if (curKeyState.IsKeyUp(Keys.Enter) && lastFrameKeyState.IsKeyDown(Keys.Enter))
            {
                if (menuOptions == 0)
                {
                    Main.type = Main.ModeType.STORY;
                    Map.newPlay = true;
                    Map.resetPlay = true;
                }
                if (menuOptions == 1)
                {
                    Main.type = Main.ModeType.ARCADE;
                    Map.Load();
                }
                if (menuOptions == 2)
                    Main.type = Main.ModeType.HELP;
                if (menuOptions == 3)
                    Main.type = Main.ModeType.EXIT;
            }

            lastFrameKeyState = curKeyState;
        }

        //*************************************************************************
        static public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.GraphicsDevice.Clear(Color.Black);

            //Main.helper.DrawFilledRectangle(spriteBatch, new Rectangle(0,0,640,720), Color.Blue);

            //Main.helper.DrawText(spriteBatch, "Main Menu", 352, 90, unselectedColor);
            spriteBatch.DrawString(Main.helper.MainFont2, "Time To Pretend", new Vector2(130, 50), Color.White);

            int height = 300;
            int heightStep = 70;

            Main.helper.DrawText(spriteBatch, "Story Mode", 510, height, optionColors[0]);
            height = height + heightStep;
            Main.helper.DrawText(spriteBatch, "Arcade Mode", 485, height, optionColors[1]);
            height = height + heightStep;
            Main.helper.DrawText(spriteBatch, "Help", 575, height, optionColors[2]);
            height = height + heightStep;
            Main.helper.DrawText(spriteBatch, "Exit Game", 520, height, optionColors[3]);
        }
    }
}
