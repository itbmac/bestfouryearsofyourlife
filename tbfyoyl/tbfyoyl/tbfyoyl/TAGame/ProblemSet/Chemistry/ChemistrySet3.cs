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
    class ChemistrySet3 : ProblemSet
    {
        public ChemistrySet3()
        {
            positions = new Vector2[] {
                new Vector2(625,245),
                new Vector2(625,444),
                new Vector2(625,518),
            };


            possibleLocations = new Rectangle[5];
            for (int i = 0; i < 5; i++)
            {
                possibleLocations[i] = new Rectangle(0, i * MediaManager.chemAnswerHeight,
                        MediaManager.chemAnswerWidth, MediaManager.chemAnswerHeight);
            }

            //177x122
            //153x98
            textureLocations = new Rectangle[] {
                new Rectangle(0, 1*MediaManager.chemAnswerHeight, MediaManager.chemAnswerWidth, MediaManager.chemAnswerHeight),
                new Rectangle(4, 1*MediaManager.chemAnswerHeight, MediaManager.chemAnswerWidth, MediaManager.chemAnswerHeight),
                new Rectangle(3, 1*MediaManager.chemAnswerHeight, MediaManager.chemAnswerWidth, MediaManager.chemAnswerHeight),
            };

            baseTexture = MediaManager.content.Load<Texture2D>("content/Chemistry/hw_chemistry_hw3");
            answerTexture = MediaManager.textures["chem answers"];
            incorrectOverlay = MediaManager.textures["chem incorrect"];

        }
    }
}
