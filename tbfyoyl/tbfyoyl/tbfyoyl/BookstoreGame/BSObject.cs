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


namespace tbfyoyl
{
    class BSObject : DecoratorObject
    {

        private Vector2 initalPosition;
        private bool shouldUpdatePos = true;

        public BSObject(Texture2D t, Vector2 p)
        {
            base.parent = new TextureObject(t, p);
            base.parent = new DraggableObject(base.parent);

            initalPosition = p;
        }

        public void SnapBack(float newX)
        {
            Position = new Vector2(newX, initalPosition.Y);
        }

        public void updateX(float diff)
        {
            if (shouldUpdatePos)
                Position = new Vector2(Position.X + diff, Position.Y);
        }

        public void updateState(bool state)
        {
            shouldUpdatePos = state;
        }

        public bool getState()
        {
            return shouldUpdatePos;
        }

    }
}

