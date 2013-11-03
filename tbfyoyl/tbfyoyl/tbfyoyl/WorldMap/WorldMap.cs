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
    public class WorldMap : Minigame
    {

        MenuObject[] menuItems;

        public WorldMap(MainGame game)
            : base(game)
        {
            menuItems = new MenuObject[2];
            menuItems[0] = new MenuObject(new DrawableObject(MediaManager.textures["BLAH"], new Vector2(100, 100)),
                delegate()
                {
                    game.ActiveGame = "TAGAME";
                });
            menuItems[1] = new MenuObject(new DrawableObject(MediaManager.textures["BLAH"], new Vector2(700, 100)),
                delegate()
                {
                    game.ActiveGame = "BOOKSTOREGAME";
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

        public override bool Click(int x, int y)
        {
            foreach (MenuObject o in menuItems)
                o.Click(x, y);
            return base.Click(x, y);
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
            foreach (MenuObject o in menuItems)
            {
                o.Draw(spriteBatch);
            }
            //prettify
        }

    }
}
