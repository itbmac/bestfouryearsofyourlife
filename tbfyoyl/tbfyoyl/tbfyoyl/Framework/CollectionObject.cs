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

namespace tbfyoyl.Framework
{
    class CollectionObject : List<GameObject>, GameObject
    {
        /*
         * A GameObject made of a collection of other GameObjects
         * The objects in this collection are such that the last object is on
         * top, ie. is drawn last, and clicked on first.
         */

        private Vector2 position;

        public CollectionObject() 
            : base()
        {
            position = new Vector2(0, 0);
        }

        public Vector2 Position
        {
            get
            {
                return new Vector2(position.X, position.Y);
            }
            set
            {
                Vector2 diff = value - position;
                foreach(GameObject o in this)
                {
                    o.Position += diff;
                }
                position = new Vector2(value.X, value.Y);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for(int i = 0; i < this.Count; i++)
            {
                this[i].Draw(spriteBatch);
            }
        }

        public void ClickUp(Vector2 pos)
        {
            for (int i = this.Count - 1; i >= 0; i--)
            {
            }
        }

        public void ClickDown(Vector2 pos)
        {
            for (int i = this.Count - 1; i >= 0; i--)
            {
            }
        }

        public void Drag(Vector2 start, Vector2 end)
        {
        }
        public bool Contains(Vector2 pos)
        {
            return true;
        }
    }
}
