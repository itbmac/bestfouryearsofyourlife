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
    class DraggableObject : DecoratorObject
    {
        public DraggableObject(GameObject p)
            : base(p) { }

        public override void Drag(Vector2 start, Vector2 end)
        {
            Position += end - start;
        }
    }
}
