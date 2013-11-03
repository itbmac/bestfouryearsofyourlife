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
         * 
         * All method calls are made by calling the corresponding
         * method on each of the 
         */
        public CollectionObject() 
            : base()
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for(int i = 0; i < this.Count[]; i++)
            {
                this[i].Draw(spriteBatch);
            }
        }

        public void Click(int x, int y)
        {
        }

        public abstract void Drag(int x1, int y1, int x2, int y2);
        public abstract Rectangle BoundingBox();
        public abstract void SetPosition(Vector2 newP);
    }
}
