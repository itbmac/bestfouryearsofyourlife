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

        //the pens marking whether a particular question is wrong,
        //or whether it was copied
        private GradingStamp pen;
        private GradingStamp cheater;

        //how accurately we've been grading
        private Score currentScore;

        private Camera2d zoomedOut;
        private Camera2d zoomedIn;

        public TAGame(MainGame game)
            : base(game) { }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            /*
            zoomedOut = new Camera2d();
            zoomedOut.Zoom = 0.5f;
            zoomedOut.Pos = new Vector2(750, 500);

            zoomedIn = new Camera2d();
            zoomedIn.Zoom = 0.8f;
            zoomedIn.Pos = new Vector2(750, 500);
            MediaManager.cam = zoomedOut;
            */

            currentScore = new Score();

            answerKey = new Paper(MediaManager.textures["paper"], new Vector2(1000, 450), MediaManager.allAnswers[0]);
            graded = new PaperStack(MediaManager.textures["paper"], new Vector2(500, 450));
            ungraded = new PaperStack(MediaManager.textures["ungraded stack"], new Vector2(0, 450));

            pen = new GradingStamp(MediaManager.textures["pen_incorrect"], new Vector2(800, 500));
            cheater = new GradingStamp(MediaManager.textures["pen_cheater"], new Vector2(800, 550));
            
            for (int i = 1; i < MediaManager.allAnswers.Length; i++)
            {
                Paper p1 = new Paper(MediaManager.textures["paper"], new Vector2(0, 0), MediaManager.allAnswers[i]);
                ungraded.addPaper(p1);
            }

            drawableObjects.Add(ungraded);
            drawableObjects.Add(graded);
            drawableObjects.Add(answerKey);
            drawableObjects.Add(cheater);
            drawableObjects.Add(pen);

            clickableObjects.Add(ungraded);
            clickableObjects.Add(graded);
            clickableObjects.Add(cheater);
            clickableObjects.Add(pen);
            
            base.Initialize();
        }

        public override bool ClickDown(Vector2 pos)
        {

            bool ret = base.ClickDown(pos);
            if (activeObject == ungraded && !drawableObjects.Contains(currentPaper))
            {
                //create new paper
                currentPaper = ungraded.getPaper();
                if (currentPaper != null)
                {
                    drawableObjects.Add(currentPaper);
                    clickableObjects.Add(currentPaper);
                }
                activeObject = currentPaper;
            }
            if (activeObject == graded && !drawableObjects.Contains(currentPaper))
            {
                //create new paper
                currentPaper = graded.getPaper();
                if (currentPaper != null)
                {
                    drawableObjects.Add(currentPaper);
                    clickableObjects.Add(currentPaper);
                }
                activeObject = currentPaper;
            }

            //puts the selected object at the front
            if (activeObject != null && (activeObject == currentPaper || activeObject  == cheater || activeObject == pen))
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
            if (activeObject != null)
            {
                if (activeObject == currentPaper)
                {
                    if (graded.Contains(pos))
                    {
                        //destroy old paper
                        drawableObjects.Remove(currentPaper);
                        clickableObjects.Remove(currentPaper);
                        currentScore += currentPaper.GetScore();
                        graded.addPaper(currentPaper);
                        currentPaper = null;
                        activeObject = null;

                        //zoom in
                        MediaManager.cam = zoomedIn;
                    }
                    else
                    {
                        MediaManager.cam = zoomedOut;
                        if (ungraded.Contains(pos))
                        {
                            //destroy old paper
                            drawableObjects.Remove(currentPaper);
                            clickableObjects.Remove(currentPaper);
                            currentScore -= currentPaper.GetScore();
                            ungraded.addPaper(currentPaper);
                            currentPaper = null;
                            activeObject = null;
                        }
                    }
                }
                else if (activeObject == pen)
                {
                    if (graded.peekPaper() != null)
                    {
                        currentScore -= graded.peekPaper().GetScore();
                        graded.peekPaper().TryStamp(pos, false);
                        currentScore += graded.peekPaper().GetScore();
                    }
                    pen.SnapBack();
                }
                else if (activeObject == cheater)
                {
                    if (graded.peekPaper() != null)
                    {
                        currentScore -= graded.peekPaper().GetScore();
                        graded.peekPaper().TryStamp(pos, true);
                        currentScore += graded.peekPaper().GetScore();
                    }
                    cheater.SnapBack();
                }
                else
                {
                    MediaManager.cam = zoomedOut;
                }
            }
            else
            {
                MediaManager.cam = zoomedOut;
            }
            bool ret = base.ClickUp(pos);
            return ret;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            MediaManager.DrawArt(spriteBatch, "Content/table_clear", 0, 0);
            MediaManager.DrawArt(spriteBatch, "Content/books_pen", 0, 0);
            MediaManager.DrawArt(spriteBatch, "Content/lamp", 400, -200);
            MediaManager.DrawArt(spriteBatch, "Content/implied_partay", 0, 0);
            MediaManager.DrawArt(spriteBatch, "Content/math", 0, 0);
            base.Draw(spriteBatch);
            MediaManager.DrawArt(spriteBatch, "Content/lamp_light", 0, 0);
        }
    }
}