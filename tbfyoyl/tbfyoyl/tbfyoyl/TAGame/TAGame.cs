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

namespace tbfyoyl.TAGame
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class TAGame : Minigame
    {
        //the current paper being graded
        private Paper currentPaper;
        //the answer key
        private Paper answerKey;

        //the stack of graded papers
        private PaperStack graded;
        //the stack of ungraded papers
        private PaperStack ungraded;
        //the "stack" of papers we want to zoom in on. 
        private PaperStack working;

        //the pens marking whether a particular question is wrong,
        //or whether it wa copied
        private GradingStamp pen;
        private GradingStamp cheater;

        private int numPapersGraded;
        private int numPapersLeft;
        private Score currentScore;

        public TAGame(MainGame game)
            : base(game)
        { }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {

            MediaManager.cam = new Camera2d();
            MediaManager.cam.Move(new Vector2(600, 400));
            //set cam's perspective

            currentScore = new Score();

            //currentPaper = new Paper(MediaManager.textures["paper"], new Vector2(100, 100), MediaManager.allAnswers[0]);
            answerKey = new Paper(MediaManager.textures["paper"], new Vector2(100, 200), MediaManager.allAnswers[1]);
            graded = new PaperStack(MediaManager.textures["paper stack"], new Vector2(0, 0));
            ungraded = new PaperStack(MediaManager.textures["paper stack"], new Vector2(0, 400));
            working = new PaperStack(MediaManager.textures["paper stack"], new Vector2(400, 450));

            pen = new GradingStamp(MediaManager.textures["pen_incorrect"], new Vector2(800, 500));
            cheater = new GradingStamp(MediaManager.textures["pen_cheater"], new Vector2(800, 550));

            //set camera perspective to a slant here

            drawableObjects.Add(ungraded);
            drawableObjects.Add(graded);
            drawableObjects.Add(working);
            drawableObjects.Add(answerKey);
            //drawableObjects.Add(currentPaper);
            drawableObjects.Add(cheater);
            drawableObjects.Add(pen);
            clickableObjects.Add(ungraded);
            clickableObjects.Add(graded);
            //clickableObjects.Add(currentPaper);
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

        public Answer[] getAnswerKey()
        {
            return null;
        }

        public override bool ClickDown(Vector2 pos)
        {
            bool ret = base.ClickDown(pos);
            if (activeObject == ungraded && !drawableObjects.Contains(currentPaper))
            {
                //create new paper
                currentPaper = ungraded.getPaper();
                drawableObjects.Add(currentPaper);
                clickableObjects.Add(currentPaper);
                activeObject = currentPaper;
            }
            if (activeObject == currentPaper || activeObject  == cheater || activeObject == pen)
            {
                drawableObjects.Remove(activeObject);
                clickableObjects.Remove(activeObject);
                drawableObjects.Add(activeObject);
                clickableObjects.Add(activeObject);
            }
            return ret;
        }

        public override bool ClickUp(Vector2 pos)
        {
            if (activeObject == currentPaper)
            {
                if (graded.Contains(pos))
                {
                    drawableObjects.Remove(currentPaper);
                    clickableObjects.Remove(currentPaper);
                    //destroy old paper
                    currentScore += currentPaper.GetScore();
                    graded.addPaper(currentPaper);
                    currentPaper = null;
                    activeObject = null;
                }
                else if (working.Contains(pos))
                {
                    activeObject.Position = working.Position;
                    MediaManager.cam = new Camera2d();
                    MediaManager.cam.Move(working.Position + new Vector2(200, 125));
                    MediaManager.cam.Zoom = 2.0f;
                    //set cam's perspective
                    activeObject = null;
                }
                else
                {
                    MediaManager.cam = new Camera2d();
                    MediaManager.cam.Move(new Vector2(600, 400));
                }
            }
            else if (activeObject == pen)
            {
                currentPaper.TryStamp(pos, false);
                pen.SnapBack();
            }
            else if (activeObject == cheater)
            {
                currentPaper.TryStamp(pos, true);
                cheater.SnapBack();
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