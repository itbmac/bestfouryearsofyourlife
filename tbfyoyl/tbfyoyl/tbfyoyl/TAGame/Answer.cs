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

namespace tbfyoyl.TAGame
{
    public class Answer : DecoratorObject
    {

        bool isSelected;
        bool isMarkedCorrect;
        bool isActuallyCorrect;
        bool isMarkedCopied;
        bool isActuallyCopied;

        Texture2D incorrectOverlay;

        public Answer()
        {
            base.parent = EmptyObject.Instance;
            this.isMarkedCorrect = true;
            this.isMarkedCopied = false;
        }

        public Answer(Texture2D t, Texture2D i, Vector2 pos, Rectangle source, bool isCorrect, bool isCopied)
        {
            incorrectOverlay = i;
            base.parent = new TextureSheetObject(t, pos, source);
            this.isActuallyCorrect = isCorrect;
            this.isActuallyCopied = isCopied;
            this.isMarkedCorrect = true;
            this.isMarkedCopied = false;
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
            if (isMarkedCopied)
            {
            }
            else if (!isMarkedCorrect)
            {
                spriteBatch.Draw(incorrectOverlay, Position, Color.White);
            }
        }

        public int getStatus()
        {
            return 0;
        }

        public void Stamp(bool isCopyStamp)
        {
            isMarkedCorrect = false;
            isMarkedCopied |= isCopyStamp;
            isSelected = false;
        }

        public void Deselect()
        {
            isSelected = false;
        }

        public new bool ClickDown(Vector2 pos)
        {
            if (base.Contains(pos))
            {
                isSelected = !isSelected;
                return true;
            }
            return false;
        }

        public bool IsMarkedCorrectly()
        {
            return !(isActuallyCorrect ^ isMarkedCorrect);
        }

        public bool WasCheaterIdentified()
        {
            return !(isActuallyCopied ^ isMarkedCopied);
        }

    }
}
