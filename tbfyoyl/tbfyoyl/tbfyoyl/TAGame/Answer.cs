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

        public Answer()
        {
            base.parent = EmptyObject.Instance;
            this.isMarkedCorrect = true;
            this.isMarkedCopied = false;
        }

        public Answer(Texture2D t, Vector2 pos, bool isCorrect, bool isCopied)
        {
            base.parent = new TextureObject(t, pos);
            this.isActuallyCorrect = isCorrect;
            this.isActuallyCopied = isCopied;
            this.isMarkedCorrect = true;
            this.isMarkedCopied = false;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (isMarkedCopied)
            {
                MediaManager.DrawArt(spriteBatch, "content/BLAH", (int)this.Position.X+50, (int)this.Position.Y);
            }
            else if(!isMarkedCorrect)
            {
                MediaManager.DrawArt(spriteBatch, "content/BLAH", (int)this.Position.X+100, (int)this.Position.Y);
            }
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
