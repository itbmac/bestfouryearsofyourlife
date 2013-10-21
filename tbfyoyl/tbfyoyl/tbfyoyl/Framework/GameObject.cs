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
    public abstract class GameObject
    {
        /*
         * Abstract class representing any object that could theoretically exist in
         * the game. In theory, all non-trivial objects that we end of drawing on
         * the screen should end up subclassing off of this class eventually. 
         */
        public abstract void Draw(SpriteBatch spriteBatch);
        public abstract void Click(int x, int y);
        public abstract void Drag(int x1, int y1, int x2, int y2);
        public abstract Rectangle BoundingBox();
        public abstract void SetPosition(Vector2 newP);
    }
}
