using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace tbfyoyl.TAGame
{
    class GradingStamp : DecoratorObject
    {

        private Vector2 initalPosition;

        public GradingStamp(Texture2D t, Vector2 p)
        {
            base.parent = new TextureObject(t, p);
            base.parent = new DraggableObject(base.parent);

            initalPosition = p;
        }

        public void SnapBack()
        {
            Position = initalPosition;
        }

    }
}

