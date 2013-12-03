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
    public class TextObject : GameObject
    {
        /*
         * A GameObject that is drawn as a block of text
         */

        private String text;
        // The absolute position of this object
        private Vector2 position;
        private SpriteFont font;
        private Color color;
        private Rectangle rect;

        public TextObject(String t, Vector2 pos)
            : this(t, pos, MediaManager.mediumFont2, Color.Black) {}

        public TextObject(String t, Vector2 pos, SpriteFont f, Color c)
        {
            text = t;
            position = new Vector2(pos.X, pos.Y);
            font = f;
            color = c;
           
            Vector2 bbox = font.MeasureString(text);
            rect = new Rectangle(0, 0, (int)bbox.X, (int)bbox.Y);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            //assumes spriteBatch.begin() has already been called
            spriteBatch.DrawString(MediaManager.mediumFont2, text, position, Color.Black);
        }

        public virtual void ClickDown(Vector2 pos) { }

        public virtual void ClickUp(Vector2 pos) { }

        public virtual void Drag(Vector2 start, Vector2 end) { }

        public virtual bool Contains(Vector2 pos)
        {
            Vector2 relative = pos - position;
            return rect.Contains((int)relative.X, (int)relative.Y);
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

    }
}
