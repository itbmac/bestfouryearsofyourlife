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
    class HomeworkSet
    {
        ProblemSet[] problems;

        //shallow copy, be careful
        public HomeworkSet(ProblemSet[] set)
        {
            problems = set;
        }

        public Homework GenerateHomework(float threshold)
        {
            GameObject nextButton = new TextureObject(MediaManager.textures["next button"], new Vector2(0, 650));
            GameObject prevButton = new TextureObject(MediaManager.textures["prev button"], new Vector2(600, 650));

            Paper[] papers = new Paper[problems.Length];
            for (int i = 0; i < problems.Length; i++)
            {
                papers[i] = problems[i].generatePaper(threshold);
            }

            Homework ret = new Homework(nextButton, prevButton, papers);

            return ret;
        }
    }
}
