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
    class ChemistrySet8 : ProblemSet
    {
        public ChemistrySet8()
        {
            positions = new Vector2[] {
                new Vector2(625,253),
                new Vector2(625,419),
                new Vector2(625,600),
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
                new Rectangle(0, 2*MediaManager.chemAnswerHeight, MediaManager.chemAnswerWidth, MediaManager.chemAnswerHeight),
                new Rectangle(0, 1*MediaManager.chemAnswerHeight, MediaManager.chemAnswerWidth, MediaManager.chemAnswerHeight),
                new Rectangle(0, 4*MediaManager.chemAnswerHeight, MediaManager.chemAnswerWidth, MediaManager.chemAnswerHeight),
            };

            baseTexture = MediaManager.content.Load<Texture2D>("content/Chemistry/hw_chemistry_hw8");
            answerTexture = MediaManager.textures["chem answers"];
            incorrectOverlay = MediaManager.textures["chem incorrect"];

        }
    }
}
