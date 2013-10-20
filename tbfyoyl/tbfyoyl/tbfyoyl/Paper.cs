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
    class Paper : DrawableObject
    {

        Answer[] answers;

        int currentPage;

        Answer selectedAnswer;

        int expectedScore;
        int actualScore;

        public Paper(Texture2D t, Vector2 p)
            : base(t, p)
        {
            selectedAnswer = answers[0];
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            answers[currentPage * 2].Draw(spriteBatch);
            answers[currentPage * 2 + 1].Draw(spriteBatch);
        }

        public void TryStamp(int x, int y, bool isCopyStamp)
        {
            if (selectedAnswer.BoundingBox().Contains(x, y))
            {
                selectedAnswer.Stamp(isCopyStamp);
            }
        }

        public override void Click(int x, int y)
        {
            if(base.BoundingBox().Contains(x, y))
            {
                if (answers[currentPage * 2].Click(x, y))
                {
                    selectedAnswer.Deselect();
                    selectedAnswer = answers[currentPage * 2];
                }
                else if (answers[currentPage * 2 + 1].Click(x, y))
                {
                    selectedAnswer.Deselect();
                    selectedAnswer = answers[currentPage * 2 + 1];
                }
            }
        }

    }
}
