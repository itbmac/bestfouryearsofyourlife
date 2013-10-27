﻿using System;
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
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here
            //currentPaper = new Paper(MediaManager.textures["paper"], new Vector2(100, 100));
            answerKey = new Paper(MediaManager.textures["paper"], new Vector2(100, 200), MediaManager.allAnswers[0]);
            graded = new PaperStack(MediaManager.textures["paper stack"], new Vector2(0, 0));
            ungraded = new PaperStack(MediaManager.textures["paper stack"], new Vector2(0, 400));
            pen = new GradingStamp(MediaManager.textures["pen"], new Vector2(400, 500));
            cheater = new GradingStamp(MediaManager.textures["pen"], new Vector2(500, 500));
            
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

        public override void Draw(SpriteBatch spriteBatch)
        {
            currentPaper.Draw(spriteBatch);
            answerKey.Draw(spriteBatch);
            graded.Draw(spriteBatch);
            ungraded.Draw(spriteBatch);
            pen.Draw(spriteBatch);
            cheater.Draw(spriteBatch);
        }

        public override bool Click(int x, int y)
        {
            return base.Click(x, y);
        }

        public override void Drag(int x1, int y1, int x2, int y2)
        {
            if (pen.BoundingBox().Contains(x1, y1))
            {
                pen.SetPosition(new Vector2(x2, y2));
            }
            if (cheater.BoundingBox().Contains(x1, y1))
            {
                cheater.SetPosition(new Vector2(x2, y2));
            }
            base.Drag(x1, y1, x2, y2);
        }

        public override void EndDrag(int x, int y)
        {

            if (pen.BoundingBox().Contains(x, y))
            {
                currentPaper.TryStamp(x, y, false);
            }
            if (cheater.BoundingBox().Contains(x, y))
            {
                currentPaper.TryStamp(x, y, true);
            }
            base.EndDrag(x, y);

        }

    }
}
