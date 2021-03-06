﻿using System;
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

        ClickableObject[] menuItems;

        public WorldMap(MainGame game)
            : base(game)
        {
            menuItems = new ClickableObject[2];
            menuItems[0] = new ClickableObject(new TextureObject(MediaManager.textures["BLAH"], new Vector2(100, 100)),
                delegate()
                {
                    game.ActiveGame = "TAGAME";
                    MediaManager.StopSound();
                    MediaManager.PlaySound("Content/dark_beat");
                });
            menuItems[1] = new ClickableObject(new TextureObject(MediaManager.textures["BLAH"], new Vector2(700, 100)),
                delegate()
                {
                    game.ActiveGame = "BOOKSTOREGAME";
                    MediaManager.StopSound();
                    MediaManager.PlaySound("Content/Emilia");
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
            MediaManager.cam.Move(new Vector2(600, 400));
            base.Initialize();
        }

        public override bool ClickDown(Vector2 pos)
        {
            foreach (GameObject o in menuItems)
            {
                if (o.Contains(pos))
                {
                    activeObject = o;
                 
            MediaManager.cam = new Camera2d();
            MediaManager.cam.Move(new Vector2(600, 400));   break;
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
            foreach (ClickableObject o in menuItems)
            {
                o.Draw(spriteBatch);
            }
        }

    }
}
