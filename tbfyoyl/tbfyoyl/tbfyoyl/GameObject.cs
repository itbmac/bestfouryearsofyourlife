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
        public virtual void Draw(SpriteBatch spriteBatch) { }
        public virtual void Click(int x, int y) { }
        public virtual Rectangle BoundingBox() { return new Rectangle(0, 0, 0, 0); }
        public virtual void SetPosition(Vector2 newP) { }
    }
}
