using System;
using System.Collections.Generic;
using System.Collections;
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
    public class TextureObject : GameObject
    {
        /*
         * A GameObject that is drawn with a Texture2D
         */

        // Should this actually be drawn
        private Boolean shouldDraw;

        // The texture this object should display as
        protected Texture2D texture;

        // The absolute position of this object
        private Vector2 position;

        public TextureObject(Texture2D t, Vector2 pos, Boolean shouldBeDrawn = true)
        {
            shouldDraw = shouldBeDrawn;
            texture = t;
            position = new Vector2(pos.X, pos.Y);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            //assumes spriteBatch.begin() has already been called
            if (shouldDraw)
                spriteBatch.Draw(texture, position, Color.White);
        }

        public virtual void ClickDown(Vector2 pos) { }

        public virtual void ClickUp(Vector2 pos) { }

        public virtual void Drag(Vector2 start, Vector2 end) { }

        public virtual bool Contains(Vector2 pos)
        {
            Vector2 relative = pos - position;
            return texture.Bounds.Contains((int)relative.X, (int)relative.Y);
        }

        public virtual Vector2 Position
        {
            get
            {
                return position;
            }
            set
            {
                position = new Vector2(value.X, value.Y);
            }
        }

        public virtual void Update(GameTime gameTime)
        {
        }

    }
}
