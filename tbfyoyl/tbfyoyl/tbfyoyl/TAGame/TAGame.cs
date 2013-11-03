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
        public MediaManager Helper;
        Paper currentPaper;
        Paper answerKey;
        PaperStack graded;
        PaperStack ungraded;
        GradingStamp pen;
        GradingStamp cheater;

        double runningTotal;
        int numPapersGraded;
        int numPapersLeft;


        public TAGame(MainGame game)
            : base(game)
        {
            // TODO: Construct any child components here
            Helper = game.Helper;
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here
            currentPaper = new Paper(MediaManager.textures["paper"], new Vector2(100, 100), MediaManager.allAnswers[0]);
            answerKey = new Paper(MediaManager.textures["paper"], new Vector2(100, 200), MediaManager.allAnswers[0]);
            graded = new PaperStack(MediaManager.textures["paper stack"], new Vector2(0, 0));
            ungraded = new PaperStack(MediaManager.textures["paper stack"], new Vector2(0, 400));
            pen = new GradingStamp(MediaManager.textures["pen_incorrect"], new Vector2(400, 500));
            cheater = new GradingStamp(MediaManager.textures["pen_cheater"], new Vector2(500, 500));
            
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

        public override void Draw(SpriteBatch spriteBatch)
        {
            Helper.DrawArt(spriteBatch, "Content/background", 0,0);
            currentPaper.Draw(spriteBatch);
            //answerKey.Draw(spriteBatch);
            //graded.Draw(spriteBatch);
            //ungraded.Draw(spriteBatch);
            pen.Draw(spriteBatch);
            cheater.Draw(spriteBatch);
        }

        public override bool ClickDown(Vector2 pos)
        {
            if (pen.Contains(pos))
            {
                activeObject = pen;
            }
            else if (cheater.Contains(pos))
            {
                activeObject = cheater;
            }/*
            else if (currentPaper.Contains(pos))
            {
                activeObject = currentPaper;
            }*/
            else if (answerKey.Contains(pos))
            {
                activeObject = answerKey;
            }
            else if (graded.Contains(pos))
            {
                activeObject = graded;
            }
            else if (ungraded.Contains(pos))
            {
                activeObject = ungraded;
            }
            return base.ClickDown(pos);
        }
        /*
        public override bool Drag(Vector2 start, Vector2 end)
        {
            if (pen.Contains(start))
            {
                pen.Drag(start, end);
            }
            if (cheater.Contains(start))
            {
                cheater.Drag(start, end);
            }
            return base.Drag(start, end);
        }
        */
    }
}
