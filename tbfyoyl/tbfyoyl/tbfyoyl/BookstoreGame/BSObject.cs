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
        public float textureWidth = 0;
        private Texture2D texture;

        public BSObject(Texture2D t, Vector2 p)
        {
            textureWidth = t.Width;
            texture = t;
            
            p = new Vector2(p.X + textureWidth / 2, p.Y);
            base.parent = new TextureObject(t, p,false);
            base.parent = new DraggableObject(base.parent);
            
            initalPosition = p;
        }

        public void SnapBack(float newX)
        {
            Position = new Vector2(newX, initalPosition.Y);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //assumes spriteBatch.begin() has already been called
            spriteBatch.Draw(texture, new Vector2(Position.X, Position.Y), Color.White);
        }

        public void updateX(float diff, bool forceUpdate = false)
        {
            if (shouldUpdatePos)
                Position = new Vector2(Position.X + diff, Position.Y);
            else if (forceUpdate)
                Position = new Vector2(diff, Position.Y);
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

