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
    class GeographySet4 : ProblemSet
    {
        public GeographySet4()
        {
            positions = new Vector2[] {
                new Vector2 (104,277),
                new Vector2 (287,210),
                new Vector2 (501,165),
                new Vector2 (546,316),
                new Vector2 (427,356),
                new Vector2 (518,478),
                new Vector2 (294,298),
                new Vector2 (214,376),
                new Vector2 (91,414),
            };

            //177x122
            //153x98
            textureLocations = new Rectangle[] {

                new Rectangle (4*MediaManager.spacingwidth,4*MediaManager.spacingheight, MediaManager.flagwidth, MediaManager.flagheight), 
                new Rectangle (0*MediaManager.spacingwidth,3*MediaManager.spacingheight, MediaManager.flagwidth, MediaManager.flagheight), 
                new Rectangle (1*MediaManager.spacingwidth,2*MediaManager.spacingheight, MediaManager.flagwidth, MediaManager.flagheight), 
                new Rectangle (2*MediaManager.spacingwidth,1*MediaManager.spacingheight, MediaManager.flagwidth, MediaManager.flagheight), 
                new Rectangle (3*MediaManager.spacingwidth,0*MediaManager.spacingheight, MediaManager.flagwidth, MediaManager.flagheight), 
                new Rectangle (1*MediaManager.spacingwidth,5*MediaManager.spacingheight, MediaManager.flagwidth, MediaManager.flagheight), 
                new Rectangle (1*MediaManager.spacingwidth,4*MediaManager.spacingheight, MediaManager.flagwidth, MediaManager.flagheight), 
                new Rectangle (4*MediaManager.spacingwidth,3*MediaManager.spacingheight, MediaManager.flagwidth, MediaManager.flagheight), 
                new Rectangle (2*MediaManager.spacingwidth,3*MediaManager.spacingheight, MediaManager.flagwidth, MediaManager.flagheight), 

            };

            baseTexture = MediaManager.content.Load<Texture2D>("content/hw_us_geo_hw4");
            answerTexture = MediaManager.textures["state flags"];
            incorrectOverlay = MediaManager.textures["flag incorrect"];

        }
    }
}
