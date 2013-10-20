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
    class Answer : DrawableObject
    {

        bool isSelected;
        bool isMarkedCorrect;
        bool isActuallyCorrect;
        bool isCopied;
        bool isActuallyCopied;
        
        public Answer(Texture2D t, Vector2 p)
            : base(t, p)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (isSelected)
            {
                base.Draw(spriteBatch);
            }
            else
            {
            }
        }

        public int getStatus()
        {
            return 0;
        }

        public void Stamp(bool isCopyStamp)
        {
            isCopied = isCopyStamp;
            isMarkedCorrect = false;
            isSelected = false;
        }

        public void Deselect()
        {
            isSelected = false;
        }

        public override bool Click(int x, int y)
        {
            if (base.BoundingBox().Contains(x, y))
            {
                isSelected = !isSelected;
                return true;
            }
            return false;
        }
    }
}
