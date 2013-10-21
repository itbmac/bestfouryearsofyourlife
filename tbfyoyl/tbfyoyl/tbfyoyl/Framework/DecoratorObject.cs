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

        public override void Draw(SpriteBatch b)
        {
            parent.Draw(b);
        }

        public override void Click(int x, int y)
        {
            parent.Click(x, y);
        }

        public override void Drag(int x1, int y1, int x2, int y2)
        {
            parent.Drag(x1, y1, x2, y2);
        }

        public override Rectangle BoundingBox()
        {
            return parent.BoundingBox();
        }

        public override void SetPosition(Vector2 newP)
        {
            parent.SetPosition(newP);
        }

    }
}