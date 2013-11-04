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
    public class ClickableObject : DecoratorObject
    {
        private Action onClick;

        public ClickableObject(GameObject p, Action behavior)
            : base(p)
        {
            onClick = behavior;
        }

        public override void ClickUp(Vector2 pos)
        {
            if (parent.Contains(pos))
            {
                onClick();
            }
            parent.ClickDown(pos);
        }
    }
}
