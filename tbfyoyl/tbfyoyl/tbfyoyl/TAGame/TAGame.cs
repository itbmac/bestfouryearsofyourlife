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
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class TAGame : Minigame
    {
        private Paper currentPaper;
        private Paper answerKey;
        private PaperStack graded;
        private PaperStack ungraded;
        private GradingStamp pen;
        private GradingStamp cheater;

/*
        private double runningTotal;
        private int numPapersGraded;
        private int numPapersLeft;
*/

        public TAGame(MainGame game)
            : base(game)
        {
            // TODO: Construct any child components here
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here
            currentPaper = new Paper(MediaManager.textures["paper"], new Vector2(100, 100), MediaManager.allAnswers[0]);
            answerKey = new Paper(MediaManager.textures["paper"], new Vector2(100, 200), MediaManager.allAnswers[1]);
            graded = new PaperStack(MediaManager.textures["paper stack"], new Vector2(0, 0));
            ungraded = new PaperStack(MediaManager.textures["paper stack"], new Vector2(0, 400));
            pen = new GradingStamp(MediaManager.textures["pen_incorrect"], new Vector2(400, 500));
            cheater = new GradingStamp(MediaManager.textures["pen_cheater"], new Vector2(500, 500));

            drawableObjects.Add(ungraded);
            drawableObjects.Add(graded);
            drawableObjects.Add(answerKey);
            drawableObjects.Add(currentPaper);
            drawableObjects.Add(cheater);
            drawableObjects.Add(pen);
            clickableObjects.Add(ungraded);
            clickableObjects.Add(graded);
            clickableObjects.Add(answerKey);
            clickableObjects.Add(currentPaper);
            clickableObjects.Add(cheater);
            clickableObjects.Add(pen);
            
            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here

            base.Update(gameTime);
        }

        public tbfyoyl.Answer[] getAnswerKey()
        {
            return null;
        }

        public override bool ClickDown(Vector2 pos)
        {
            bool ret = base.ClickDown(pos);
            if (activeObject == ungraded)
            {
                currentPaper.Position = pos;
                drawableObjects.Add(currentPaper);
                clickableObjects.Add(currentPaper);
                activeObject = currentPaper;
            }
            return ret;
        }

        public override bool ClickUp(Vector2 pos)
        {
            if (activeObject == currentPaper && ungraded.Contains(pos))
            {
                drawableObjects.Remove(currentPaper);
                clickableObjects.Remove(currentPaper);
                activeObject = null;
            }
            bool ret = base.ClickUp(pos);
            return ret;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            MediaManager.DrawArt(spriteBatch, "Content/background", 0, 0);
            base.Draw(spriteBatch);
        }
    }
}
