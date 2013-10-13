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

        //static public Color selectedColor = Color.Red;
        //static public Color unselectedColor = Color.White;
        //static KeyboardState prevKeyState;

        MenuObject[] menuItems;

        public SplashScreen(MainGame game)
            : base(game)
        {
            menuItems = new MenuObject[2];
            menuItems[0] = new MenuObject(new DrawableObject(game.Helper.textures["MENU1"], new Vector2(0, 0)),
                delegate()
                {
                    game.ActiveGame = "WORLDMAP";
                });
            menuItems[1] = new MenuObject(new DrawableObject(game.Helper.textures["MENU2"], new Vector2(100, 0)),
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
            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {

            MouseState curMouseState = Mouse.GetState();
            if (curMouseState.LeftButton == ButtonState.Pressed)
            {
                foreach (MenuObject o in menuItems)
                    o.Click(curMouseState.X, curMouseState.Y);
            }

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (MenuObject o in menuItems)
            {
                o.Draw(spriteBatch);
            }
            //prettify
        }

    }
}
