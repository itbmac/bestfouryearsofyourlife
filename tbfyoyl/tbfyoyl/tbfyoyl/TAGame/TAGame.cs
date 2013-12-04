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
        private Homework currentHomework;
        //the answer key
        //private Paper answerKey;

        //the stack of graded papers
        private HomeworkStack graded;
        //the stack of ungraded papers
        private HomeworkStack ungraded;

        //the pens marking whether a particular question is wrong,
        //or whether it was copied
        private GradingStamp pen;
        private GradingStamp cheater;

        //how accurately we've been grading
        private Score currentScore;
        private Score totalScore;

        public Score CurrentScore
        {
            get
            {
                return new Score() + currentScore;
            }
        }

        public Score TotalScore
        {
            get
            {
                return new Score() + totalScore;
            }
        }
    
    
        private Camera2d zoomedOut;
        private Camera2d zoomedIn;

        //a list of problem sets
        private Queue<HomeworkSet> homeworks;
        private int numPapers;

        public TAGame(MainGame game)
            : base(game)
        {

            homeworks = new Queue<HomeworkSet>();
            homeworks.Enqueue(new HomeworkSet(new ProblemSet[] { new GeographySet1(), new GeographySet2() }));
            //homeworks.Enqueue(new HomeworkSet(new ProblemSet[] { new GeographySet2() }));
            homeworks.Enqueue(new HomeworkSet(new ProblemSet[] { new GeographySet3() }));
            homeworks.Enqueue(new HomeworkSet(new ProblemSet[] { new GeographySet4() }));
            homeworks.Enqueue(new HomeworkSet(new ProblemSet[] { new GeographySet5() }));
            numPapers = -1;

            //create camera views
            zoomedOut = new Camera2d();
            zoomedOut.Zoom = 0.533333333f; // = 1200/2250
            zoomedOut.Pos = new Vector2(1125, 750);

            zoomedIn = new Camera2d();
            zoomedIn.Zoom = 1.0f; // = 1200/1200
            zoomedIn.Pos = new Vector2(1350, 975);

            //create paperstacks
            graded = new HomeworkStack(MediaManager.textures["paper"], new Vector2(750, 600));
            ungraded = new HomeworkStack(MediaManager.textures["ungraded stack"], new Vector2(0, 570));

            pen = new GradingStamp(MediaManager.textures["pen_incorrect"], new Vector2(1500, 750));
            cheater = new GradingStamp(MediaManager.textures["pen_cheater"], new Vector2(1500, 1050));
            currentScore = new Score();
            totalScore = new Score();

            drawableObjects.Add(ungraded);
            drawableObjects.Add(graded);
            drawableObjects.Add(pen);

            clickableObjects.Add(ungraded);
            clickableObjects.Add(graded);
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
            if(graded.numHomeworks() == (numPapers+1))
            {

                if (numPapers < 3)
                {
                    numPapers = 3;
                }
                //clear the papers
                ungraded.clear();
                graded.clear();
                currentScore.zero();

                ungraded.addHomework(new Homework(EmptyObject.Instance, EmptyObject.Instance, new Paper[] { new DummyPaper() }));

                //TODO: reincorporate UI

                //if we can, construct a set of papers and answers
                if (homeworks.Count != 0)
                {
                    HomeworkSet problemSet = homeworks.Dequeue();
                    //numPapers += 2;
                    if (numPapers > 20)
                    {
                        numPapers = 20;
                    }
                    for (int i = 0; i < numPapers; i++)
                    {
                        Homework h = problemSet.GenerateHomework(MediaManager.GetRandomFloat(0, 0.4));
                        ungraded.addHomework(h);
                    }
                }
            }
            
            base.Initialize();
        }

        public override void Deinitialize()
        {
            //cut off accuracy at 70%, below 70% means no scores
            double accuracy = 0.3 - (1.0 * currentScore.NumGradingMistakes) / currentScore.NumQuestions;
            if (accuracy < 0)
                accuracy = 0;
            accuracy *= 1 / 0.3;
            //accuracy now ranges from 0 to 0.3, with a higher score being better

            totalScore += currentScore;
            System.Diagnostics.Debug.WriteLine("total score: " + totalScore);

            double wagePerPaper = 0.7; //an arbitrary number
            game.money += (int) (wagePerPaper * graded.numHomeworks() * accuracy);

            base.Deinitialize();
        }

        public override bool ClickDown(Vector2 pos)
        {

            bool ret = base.ClickDown(pos);
            if (activeObject == ungraded && !drawableObjects.Contains(currentHomework))
            {
                //create new paper
                currentHomework = ungraded.getPaper();
                if (currentHomework != null)
                {
                    drawableObjects.Add(currentHomework);
                    clickableObjects.Add(currentHomework);
                }
                activeObject = currentHomework;
            }
            if (activeObject == graded && !drawableObjects.Contains(currentHomework))
            {
                //create new paper
                currentHomework = graded.getPaper();
                if (currentHomework != null)
                {
                    drawableObjects.Add(currentHomework);
                    clickableObjects.Add(currentHomework);
                }
                activeObject = currentHomework;
            }

            //puts the selected object at the front
            if (activeObject != null && (activeObject == currentHomework || activeObject  == cheater || activeObject == pen))
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
                if (activeObject == currentHomework)
                {
                    if (graded.Contains(pos))
                    {

                        //destroy old paper
                        drawableObjects.Remove(currentHomework);
                        clickableObjects.Remove(currentHomework);
                        graded.addHomework(currentHomework);

                        if (ungraded.numHomeworks() == 0)
                        {
                            game.ActiveGame = "TARESULTS";
                            return base.ClickUp(pos);
                        }
                        currentScore += currentHomework.GetScore();

                        currentHomework.ClickUp(pos);

                        currentHomework = null;
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
                            currentScore -= currentHomework.GetScore();
                            drawableObjects.Remove(currentHomework);
                            clickableObjects.Remove(currentHomework);
                            ungraded.addHomework(currentHomework);
                            currentHomework = null;
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