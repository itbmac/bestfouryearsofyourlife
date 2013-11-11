using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Spine;

namespace tbfyoyl
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class BookstoreGame : Minigame
    {
        public Player player;

        private float screenWidth = 1200;
        private float bgWidth = 4000;
        private float bgXOffset = 0;
        int direction = 1;
        float speed = 1;
        float distStartToTarget = 10;
        float targetX = 0;
        Boolean grr = false;
        
        protected override void LoadContent () {
			
		}
        
        public BookstoreGame(MainGame game)
            : base(game)
        {
            // TODO: Construct any child components here
            player = new Player(MediaManager.textures["BLAH"], new Vector2(600, 600), game.GraphicsDevice);
            bgXOffset = 0;
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

        public override bool ClickDown(Vector2 pos)
        {
            //player.setX(pos.X);
            if (pos.X > (screenWidth / 2))
            {
                direction = -1;
                player.setDirection(false);
            }
            else
            {
                direction = 1;
                player.setDirection(true);
            }
            
            bool ret = base.ClickDown(pos);

            System.Diagnostics.Debug.WriteLine("BGO: " + bgXOffset.ToString());
            System.Diagnostics.Debug.WriteLine("POS: " + pos.ToString());

            if ((bgXOffset <= 0) && ((bgXOffset + Math.Abs((screenWidth / 2) - pos.X)) >= 0) && (pos.X < (screenWidth / 2)))
            {
                targetX = Math.Abs(bgXOffset);
                distStartToTarget = Math.Abs(bgXOffset);
               // grr = true;
            }
            else if ((bgXOffset > 0) && ((bgXOffset + Math.Abs((screenWidth / 2) - pos.X)) >= 0) && (pos.X < (screenWidth / 2)))
            {
                targetX = 0;
                distStartToTarget = 0;
                bgXOffset = 0;
            }

            else if ((bgXOffset >= (screenWidth - bgWidth)) && ((bgXOffset - Math.Abs((screenWidth / 2) - pos.X)) <= ((screenWidth) - bgWidth)) && (pos.X > (screenWidth / 2)))
            {
                targetX = Math.Abs((bgWidth - screenWidth) + bgXOffset);
                distStartToTarget = Math.Abs((bgWidth - screenWidth) + bgXOffset);
            }

            else if ((bgXOffset <= (screenWidth - bgWidth)) && ((bgXOffset - Math.Abs((screenWidth / 2) - pos.X)) <= ((screenWidth) - bgWidth)) && (pos.X > (screenWidth / 2)))
            {
                targetX = 0;
                distStartToTarget = 0;
                bgXOffset = (screenWidth - bgWidth);
            }

            else
            {
                targetX = Math.Abs((screenWidth / 2) - pos.X);
                distStartToTarget = Math.Abs((screenWidth / 2) - pos.X);
            }
            
            return ret;
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here
            player.Update(gameTime);

            if (targetX > 0)
            {
                float one = 1.0f;
                float dist = targetX / (distStartToTarget / 10);
                speed = Math.Max(one, dist);
                bgXOffset += direction * speed;
                targetX -= speed;
                //state.SetAnimation(0, "walk", true);
                
                
            }
            else
            {
                speed = 0;
                targetX = 0;
                distStartToTarget = 0;
                //state.SetAnimation(0, "stand", false);
              
            }

            System.Diagnostics.Debug.WriteLine("BGO: " + bgXOffset.ToString());

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            MediaManager.DrawArt(spriteBatch, "Content/bookstore_background", (int)bgXOffset, 0);
            MediaManager.DrawArt(spriteBatch, "Content/bookstore_background", (int)bgXOffset + 2000, 0);
            player.Draw(spriteBatch);
            
            if (grr)
                MediaManager.DrawArt(spriteBatch, "Content/BLAH", 200, 40);
            base.Draw(spriteBatch);
        }

    }
}
