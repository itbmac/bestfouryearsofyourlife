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
    class Homework : DecoratorObject
    {

        private LinkedList<Paper> papers;
        private ClickableObject nextButton;
        private ClickableObject prevButton;

        public Homework(GameObject next, GameObject prev, Paper[] paperList)
        {
            papers = new LinkedList<Paper>(paperList);
            parent = papers.First();

            nextButton = new ClickableObject(next,
                delegate() { papers.AddLast(papers.First()); papers.RemoveFirst(); parent = papers.First(); });
            prevButton = new ClickableObject(prev,
                delegate() { papers.AddFirst(papers.Last()); papers.RemoveLast(); parent = papers.First(); });

        }

        public override void Draw(SpriteBatch b)
        {
            base.Draw(b);
            nextButton.Draw(b);
            prevButton.Draw(b);
        }

        public void TryStamp(Vector2 pos, bool isCopyStamp)
        {
            papers.First().TryStamp(pos, isCopyStamp);
        }

        public Score GetScore()
        {

            Score ret = new Score();
            foreach (Paper p in papers)
            {
                ret += p.GetScore();
            }
            return ret;
        }

        public override Vector2 Position
        {
            get
            {
                return base.Position;
            }
            set
            {
                nextButton.Position += value - base.Position;
                prevButton.Position += value - base.Position;
                foreach (Paper p in papers)
                    p.Position = value;
            }
        }

        public override void Drag(Vector2 start, Vector2 end)
        {
            Position += end - start;
        }

        public override void ClickUp(Vector2 pos)
        {
            if (nextButton.Contains(pos))
                nextButton.ClickUp(pos);
            else if (prevButton.Contains(pos))
                prevButton.ClickUp(pos);
            base.ClickUp(pos);
        }

    }
}
