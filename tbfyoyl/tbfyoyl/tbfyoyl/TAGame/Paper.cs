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
    class Paper : TextureObject
    {

        Answer[] answers;

        int currentPage;

        Answer selectedAnswer;

        int expectedScore;
        int actualScore;

        public Paper(Texture2D t, Vector2 p, Answer[] ans)
            : base(t, p)
        {
            answers = new Answer[ans.Length];
            ans.CopyTo(answers, 0);
            
            selectedAnswer = answers[0];
            SetCurrentPage(0);
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

        
        public void TryStamp(int x, int y, bool isCopyStamp)
        {
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
            answers[currentPage * 2].Position += end - start;
            answers[currentPage * 2 + 1].Position += end - start;
        }

    }
}
