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
using tbfyoyl;

namespace tbfyoyl
{
    public class Answer : TextureObject
    {

        bool isSelected;
        bool isCorrect;
        bool isActuallyCorrect;
        bool isCopied;
        bool isActuallyCopied;
        Vector2 pageOffset;
        
        public Answer(Texture2D t, Vector2 pos, bool isCorrect, bool isCopied)
            : base(t, pos)
        {
            this.isCopied = isCopied;
            this.isCorrect = isCorrect;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (isSelected)
            {
                base.Draw(spriteBatch);
            }
            else
            {
                base.Draw(spriteBatch);
            }
        }

        public int getStatus()
        {
            return 0;
        }

        public void Stamp(bool isCopyStamp)
        {
            isCopied = isCopyStamp;
            isCorrect = false;
            isSelected = false;
        }

        public void Deselect()
        {
            isSelected = false;
        }

        public new bool Click(Vector2 pos)
        {
            if (base.Contains(pos))
            {
                isSelected = !isSelected;
                return true;
            }
            return false;
        }
    }
}
