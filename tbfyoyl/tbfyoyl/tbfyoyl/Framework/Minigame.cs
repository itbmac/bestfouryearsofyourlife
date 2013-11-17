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
        protected GameObject activeObject; //the object that was first selected when clicking
        protected List<GameObject> clickableObjects; //all objects in the game that are clickable
        protected List<GameObject> drawableObjects; //all objects in the game that should be drawn

        public Minigame(MainGame m_game)
            : base(m_game)
        {
            this.game = m_game;
            activeObject = null;
            clickableObjects = new List<GameObject>();
            drawableObjects = new List<GameObject>();
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here
            base.Initialize();
            activeObject = null;
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

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            foreach(GameObject o in drawableObjects)
            {
                o.Draw(spriteBatch);
            }
        }
        
        public virtual void MouseOver(Vector2 pos)
        {
        }

        public virtual bool ClickDown(Vector2 pos)
        {
            for (int i = clickableObjects.Count - 1; i >= 0; i--)
            {
                if (clickableObjects[i].Contains(pos))
                {
                    activeObject = clickableObjects[i];
                    activeObject.ClickDown(pos);
                    break;
                }
            }
            return true;
        }

        public virtual bool Drag(Vector2 start, Vector2 end)
        {
            if (activeObject != null)
            {
                activeObject.Drag(start, end);
            }
            return true;
        }

        public virtual bool ClickUp(Vector2 pos)
        {
            if (activeObject != null)
            {
                activeObject.ClickUp(pos);
            }
            activeObject = null;
            return true;
        }

    }
}
