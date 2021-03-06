﻿using System;
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
        Answer selectedAnswer;
        Texture2D questions;

        public Paper(Texture2D t, Vector2 p, Answer[] ans)
            : base(MediaManager.textures["paper"], p)
        {
            answers = new Answer[ans.Length];
            ans.CopyTo(answers, 0);

            selectedAnswer = answers[0];
            questions = t;
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
                foreach (Answer a in answers)
                {
                    a.Position += diff;
                }
                base.Position = value;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            spriteBatch.Draw(questions, Position, Color.White);
            foreach (Answer a in answers)
            {
                a.Draw(spriteBatch);
            }
        }
   
        public void TryStamp(Vector2 pos, bool isCopyStamp)
        {
            foreach (Answer a in answers)
            {
                if (a.Contains(pos))
                {
                    a.Stamp(isCopyStamp);
                }
            }
        }

        public override void ClickDown(Vector2 pos)
        {
            if(base.Contains(pos))
            {
                foreach (Answer a in answers)
                {
                    if (a.ClickDown(pos))
                    {
                        selectedAnswer.Deselect();
                        selectedAnswer = a;
                    }
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
