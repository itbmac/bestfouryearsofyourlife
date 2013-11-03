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
    /// Each "minigame" represents one possible state of the screen. A game
    /// can have several states, like a loading state, a state where you're
    /// playing a specific minigame, one for the world map, etc.
    /// </summary>
    public abstract class Minigame : Microsoft.Xna.Framework.DrawableGameComponent
    {

        protected MainGame game; //the game that this minigame displays onto

        public Minigame(MainGame m_game)
            : base(m_game)
        {
            this.game = m_game;
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


        public abstract void Draw(SpriteBatch spriteBatch);
        
        public virtual void MousePosition(int x, int y)
        {
        }
        public virtual bool ClickDown(int x, int y)
        {
            return true;
        }
        public virtual bool Drag(int x1, int y1, int x2, int y2)
        {
            return true;
        }
        public virtual bool ClickUp(int x, int y)
        {
            return true;
        }

    }
}
