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

namespace tbfyoyl.TAGame
{
    class Paper : TextureObject
    {

        Answer[] answers;

        int currentPage;

        Answer selectedAnswer;

        public Paper(Texture2D t, Vector2 p, Answer[] ans)
            : base(t, p)
        {
            answers = new Answer[ans.Length];
            ans.CopyTo(answers, 0);
            
            selectedAnswer = answers[0];
            SetCurrentPage(0);
        }

        public override Vector2 Position
        {
            get
            {
                return base.Position;
            }
            set
            {
                Vector2 diff = value - base.Position;
                answers[currentPage * 2].Position += diff;
                answers[currentPage * 2 + 1].Position += diff;
                base.Position = value;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            answers[currentPage * 2].Draw(spriteBatch);
            answers[currentPage * 2 + 1].Draw(spriteBatch);
        }

        public void SetCurrentPage(int newPageIndex)
        {
            currentPage = newPageIndex;
            answers[currentPage * 2].Position = this.Position;
            answers[currentPage * 2 + 1].Position = this.Position + new Vector2(0, 60);
        }
   
        public void TryStamp(Vector2 pos, bool isCopyStamp)
        {
            if (answers[currentPage * 2].Contains(pos))
            {
                answers[currentPage * 2].Stamp(isCopyStamp);
            }
            else if (answers[currentPage * 2 + 1].Contains(pos))
            {
                answers[currentPage * 2 + 1].Stamp(isCopyStamp);
            }
            System.Diagnostics.Debug.Write("TryStamp Paper\n");
        }

        public override void ClickDown(Vector2 pos)
        {
            if(base.Contains(pos))
            {
                if (answers[currentPage * 2].ClickDown(pos))
                {
                    selectedAnswer.Deselect();
                    selectedAnswer = answers[currentPage * 2];
                }
                else if (answers[currentPage * 2 + 1].ClickDown(pos))
                {
                    selectedAnswer.Deselect();
                    selectedAnswer = answers[currentPage * 2 + 1];
                }
            }
        }

        public override void Drag(Vector2 start, Vector2 end)
        {
            Position += end - start;
        }

        public Score GetScore()
        {
            Score ret = new Score();
            ret.NumQuestions = answers.Length;
            foreach (Answer a in answers)
            {
                if (!a.IsMarkedCorrectly())
                {
                    ret.NumGradingMistakes++;
                }
                if (!a.WasCheaterIdentified())
                {
                    ret.NumCheaterMistakes++;
                }
            }
            return ret;
        }

    }
}
