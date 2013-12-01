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
        //private Paper answerKey;

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

        private int lastAnswerSet;

        public TAGame(MainGame game)
            : base(game)
        {

            lastAnswerSet = -1;

            //create camera views
            zoomedOut = new Camera2d();
            zoomedOut.Zoom = 0.533333333f; // = 1200/2250
            zoomedOut.Pos = new Vector2(1125, 750);

            zoomedIn = new Camera2d();
            zoomedIn.Zoom = 1.0f; // = 1200/1200
            zoomedIn.Pos = new Vector2(1350, 975);

            //create paperstacks
            graded = new PaperStack(MediaManager.textures["paper"], new Vector2(750, 600));
            ungraded = new PaperStack(MediaManager.textures["ungraded stack"], new Vector2(0, 570));

            pen = new GradingStamp(MediaManager.textures["pen_incorrect"], new Vector2(1500, 750));
            cheater = new GradingStamp(MediaManager.textures["pen_cheater"], new Vector2(1500, 1050));
            currentScore = new Score();

            ClickableObject back = new ClickableObject(new TextureObject(MediaManager.textures["BLAH"], new Vector2(0, 0)),
                delegate()
                {
                    game.ActiveGame = "WORLDMAP";
                });

            drawableObjects.Add(back);
            drawableObjects.Add(ungraded);
            drawableObjects.Add(graded);
            //drawableObjects.Add(cheater);
            drawableObjects.Add(pen);

            clickableObjects.Add(back);
            clickableObjects.Add(ungraded);
            clickableObjects.Add(graded);
            //clickableObjects.Add(cheater);
            clickableObjects.Add(pen);

        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            
            MediaManager.cam = zoomedOut;

            //only resets the game if we finished all the previous papers
            if(lastAnswerSet == -1 || (lastAnswerSet < MediaManager.allAnswers.Length && graded.numPapers() == MediaManager.allAnswers[lastAnswerSet].Length - 1))
            {
                //clear the papers
                lastAnswerSet++;
                ungraded.clear();
                graded.clear();
                currentScore.zero();

                //TODO: algorithmically generate answer sets
                //TODO: reincorporate UI

                //if we can, construct a set of papers and answers
                if (MediaManager.allAnswers.Length > lastAnswerSet)
                {
                    //answerKey = new Paper(MediaManager.textures["paper"], new Vector2(1000, 400), MediaManager.allAnswers[lastAnswerSet][0]);
                    for (int i = 0; i < MediaManager.allAnswers[lastAnswerSet].Length; i++)
                    {
                        Paper p1 = new Paper(MediaManager.textures["paper"], new Vector2(0, 0), MediaManager.allAnswers[lastAnswerSet][i]);
                        ungraded.addPaper(p1);
                    }
                    //drawableObjects.Add(answerKey);
                }
            }
            
            base.Initialize();
            System.Diagnostics.Debug.WriteLine("Creating new set");
        }

        public override void Deinitialize()
        {
            //cut off accuracy at 70%, below 70% means no scores
            double accuracy = 0.3 - (1.0 * currentScore.NumGradingMistakes) / currentScore.NumQuestions;
            if (accuracy < 0)
                accuracy = 0;
            accuracy *= 1 / 0.3;
            //accuracy now ranges from 0 to 0.3, with a higher score being better

            double wagePerPaper = 0.7; //an arbitrary number
            game.money += (int) (wagePerPaper * graded.numPapers() * accuracy);

            base.Deinitialize();
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
                        graded.peekPaper().TryStamp(pen.Position, false);
                        currentScore += graded.peekPaper().GetScore();
                    }
                    pen.SnapBack();
                }
                else if (activeObject == cheater)
                {
                    if (graded.peekPaper() != null)
                    {
                        currentScore -= graded.peekPaper().GetScore();
                        graded.peekPaper().TryStamp(cheater.Position, true);
                        currentScore += graded.peekPaper().GetScore();
                    }
                    cheater.SnapBack();
                }
                else
                {
                    MediaManager.cam = zoomedOut;
                }
            }
            else //if(!answerKey.Contains(pos))
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
            MediaManager.DrawArt(spriteBatch, "Content/implied_partay", 0, 0);
            MediaManager.DrawArt(spriteBatch, "Content/math", 0, 0);
            MediaManager.DrawArt(spriteBatch, "Content/lamp", 525, -150);
            base.Draw(spriteBatch);
            MediaManager.DrawArt(spriteBatch, "Content/lamp_light", 0, 0);
        }
    }
}