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
        BSObject books;
        BSObject books1;
        BSObject books2;
        BSObject books3;
        BSObject books4;
        BSObject books5;
        Cart cart;

        List<BSObject> bookstoreObjects;

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
            bgXOffset = 0;

            player = new Player(MediaManager.textures["BLAH"], new Vector2(600, 600), game.GraphicsDevice);
            books = new BSObject(MediaManager.textures["books"], new Vector2(300, 500));
            books1 = new BSObject(MediaManager.textures["books"], new Vector2(800, 500));
            books2 = new BSObject(MediaManager.textures["books"], new Vector2(1000, 500));
            books3 = new BSObject(MediaManager.textures["books"], new Vector2(1100, 500));
            books4 = new BSObject(MediaManager.textures["books"], new Vector2(1300, 500));
            books5 = new BSObject(MediaManager.textures["books"], new Vector2(1500, 500));
            cart = new Cart(MediaManager.textures["cart"], new Vector2(600, 425));

            drawableObjects.Add(books);
            drawableObjects.Add(books1);
            drawableObjects.Add(books2);
            drawableObjects.Add(books3);
            drawableObjects.Add(books4);
            drawableObjects.Add(books5);

            drawableObjects.Add(player);
            drawableObjects.Add(cart);

            clickableObjects.Add(books);
            clickableObjects.Add(books1);
            clickableObjects.Add(books2);
            clickableObjects.Add(books3);
            clickableObjects.Add(books4);
            clickableObjects.Add(books5);

            bookstoreObjects = new List<BSObject>();
            bookstoreObjects.Add(books);
            bookstoreObjects.Add(books1);
            bookstoreObjects.Add(books2);
            bookstoreObjects.Add(books3);
            bookstoreObjects.Add(books4);
            bookstoreObjects.Add(books5);
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
            bool ret = base.ClickDown(pos);
            return ret;
        }


        public override bool ClickUp(Vector2 pos)
        {
            bool touchedObj = false;
            
            foreach (BSObject o in bookstoreObjects)
            {
                if (activeObject == o)
                {
                    if (cart.TryInsert(pos))
                    {
                        o.updateState(false);
                    }
                    else
                    {
                        o.SnapBack(pos.X);
                        o.updateState(true);
                    }

                    touchedObj = true;
                    activeObject = null;
                }
            }
            
            if (!touchedObj)
            {
                moveTo(pos);
            }

            bool ret = base.ClickUp(pos);
            return ret;
        }


        public void moveTo(Vector2 pos)
        {
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

            //System.Diagnostics.Debug.WriteLine("BGO: " + bgXOffset.ToString());
            //System.Diagnostics.Debug.WriteLine("POS: " + pos.ToString());

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

                foreach (BSObject o in bookstoreObjects)
                {
                    o.updateX(direction * speed);
                }
                //state.SetAnimation(0, "walk", true);
                
                
            }
            else
            {
                speed = 0;
                targetX = 0;
                distStartToTarget = 0;
                //state.SetAnimation(0, "stand", false);
              
            }

            base.Update(gameTime);
        }


        public override void Draw(SpriteBatch spriteBatch)
        {
            MediaManager.DrawArt(spriteBatch, "Content/bookstore_background", (int)bgXOffset, 0);
            MediaManager.DrawArt(spriteBatch, "Content/bookstore_background", (int)bgXOffset + 2000, 0);

            // player & objects drawn by this!
            base.Draw(spriteBatch);

           /* if (direction < 0)
                MediaManager.DrawArt(spriteBatch, "Content/shopping_cart_200", (int)600, 425);
            else
                MediaManager.DrawArt(spriteBatch, "Content/shopping_cart_200_h", (int)400, 425);
            */
            if (grr)
                MediaManager.DrawArt(spriteBatch, "Content/BLAH", 200, 40);
            
        }

    }
}
