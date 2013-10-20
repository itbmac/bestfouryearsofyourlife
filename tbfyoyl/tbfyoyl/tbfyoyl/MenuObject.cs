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
    public class MenuObject : DecoratorObject
    {
        public delegate void OnClick();
        private OnClick onClick;

        public MenuObject(GameObject p, OnClick behavior)
            : base(p)
        {
            onClick = behavior;
        }

        //function pointer -> on click
        public bool InsideBoundingBox(int x, int y)
        {
            return parent.BoundingBox().Contains(x, y);
        }

        //function pointer -> on click
        public override void Click(int x, int y)
        {
            parent.Click(x, y);
            if(parent.BoundingBox().Contains(x, y))
            {
                onClick();
            }
        }
    }
}
