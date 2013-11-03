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
    public abstract class DecoratorObject : GameObject
    {
        /*
         * Basic decorator subclass to allow free extension of GameObject's
         * As it is, each method just calls the associated method of the parent
         * class.
         */

        protected GameObject parent;

        public DecoratorObject(GameObject p)
        {
            parent = p;
        }

        public virtual void Draw(SpriteBatch b)
        {
            parent.Draw(b);
        }

        public virtual void Click(Vector2 pos)
        {
            parent.Click(pos);
        }

        public virtual void Drag(Vector2 start, Vector2 end)
        {
            parent.Drag(start, end);
        }

        public virtual bool Contains(Vector2 pos)
        {
            return parent.Contains(pos);
        }

        public Vector2 Position
        {
            get
            {
                return parent.Position;
            }
            set
            {
                parent.Position = value;
            }
        }

    }
}