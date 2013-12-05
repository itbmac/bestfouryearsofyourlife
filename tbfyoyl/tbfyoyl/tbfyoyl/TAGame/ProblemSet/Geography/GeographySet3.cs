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
    class GeographySet3 : ProblemSet
    {
        public GeographySet3()
        {
            positions = new Vector2[] {

                new Vector2 (323,180),
                new Vector2 (381,370),
                new Vector2 (514,348),
                new Vector2 (539,488),
                new Vector2 (318,526),
                new Vector2 (259,363),
                new Vector2 (168,454),
                new Vector2 (136,256),  

            };


            possibleLocations = new Rectangle[50];
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    possibleLocations[i * 10 + j] = new Rectangle(i * MediaManager.spacingwidth, j * MediaManager.spacingheight,
                        MediaManager.flagwidth, MediaManager.flagheight);
                }
            }
            //177x122
            //153x98
            textureLocations = new Rectangle[] {
                new Rectangle (0*MediaManager.spacingwidth,5*MediaManager.spacingheight, MediaManager.flagwidth, MediaManager.flagheight), 
                new Rectangle (1*MediaManager.spacingwidth,3*MediaManager.spacingheight, MediaManager.flagwidth, MediaManager.flagheight), 
                new Rectangle (4*MediaManager.spacingwidth,6*MediaManager.spacingheight, MediaManager.flagwidth, MediaManager.flagheight), 
                new Rectangle (4*MediaManager.spacingwidth,1*MediaManager.spacingheight, MediaManager.flagwidth, MediaManager.flagheight), 
                new Rectangle (4*MediaManager.spacingwidth,2*MediaManager.spacingheight, MediaManager.flagwidth, MediaManager.flagheight), 
                new Rectangle (3*MediaManager.spacingwidth,3*MediaManager.spacingheight, MediaManager.flagwidth, MediaManager.flagheight), 
                new Rectangle (0*MediaManager.spacingwidth,4*MediaManager.spacingheight, MediaManager.flagwidth, MediaManager.flagheight), 
                new Rectangle (4*MediaManager.spacingwidth,5*MediaManager.spacingheight, MediaManager.flagwidth, MediaManager.flagheight), 
            };
            baseTexture = MediaManager.content.Load<Texture2D>("content/hw_us_geo_hw3");
            answerTexture = MediaManager.textures["state flags"];
            incorrectOverlay = MediaManager.textures["flag incorrect"];

        }
    }
}
