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
    class GeographySet5 : ProblemSet
    {
        public GeographySet5()
        {
            positions = new Vector2[] {
                new Vector2 (206,197),
                new Vector2 (415,210),
                new Vector2 (568,219),
                new Vector2 (568,312),
                new Vector2 (568,405),
                new Vector2 (568,498),
                new Vector2 (568,603),
                new Vector2 (408,566),
                new Vector2 (45,622),
                new Vector2 (177,537),
                new Vector2 (241,409),
            };


            //177x122
            //153x98
            textureLocations = new Rectangle[] {

                new Rectangle (3*MediaManager.spacingwidth, 2*MediaManager.spacingheight, MediaManager.flagwidth, MediaManager.flagheight), 
                new Rectangle (2*MediaManager.spacingwidth, 4*MediaManager.spacingheight, MediaManager.flagwidth, MediaManager.flagheight), 
                new Rectangle (3*MediaManager.spacingwidth, 1*MediaManager.spacingheight, MediaManager.flagwidth, MediaManager.flagheight), 
                new Rectangle (0*MediaManager.spacingwidth, 1*MediaManager.spacingheight, MediaManager.flagwidth, MediaManager.flagheight), 
                new Rectangle (2*MediaManager.spacingwidth, 2*MediaManager.spacingheight, MediaManager.flagwidth, MediaManager.flagheight), 
                new Rectangle (4*MediaManager.spacingwidth, 0*MediaManager.spacingheight, MediaManager.flagwidth, MediaManager.flagheight), 
                new Rectangle (0*MediaManager.spacingwidth, 0*MediaManager.spacingheight, MediaManager.flagwidth, MediaManager.flagheight), 
                new Rectangle (2*MediaManager.spacingwidth, 0*MediaManager.spacingheight, MediaManager.flagwidth, MediaManager.flagheight), 
                new Rectangle (1*MediaManager.spacingwidth, 1*MediaManager.spacingheight, MediaManager.flagwidth, MediaManager.flagheight), 
                new Rectangle (1*MediaManager.spacingwidth, 0*MediaManager.spacingheight, MediaManager.flagwidth, MediaManager.flagheight), 
                new Rectangle (0*MediaManager.spacingwidth, 2*MediaManager.spacingheight, MediaManager.flagwidth, MediaManager.flagheight),

            };

            baseTexture = MediaManager.content.Load<Texture2D>("content/hw_us_geo_hw5");
            answerTexture = MediaManager.textures["state flags"];
            incorrectOverlay = MediaManager.textures["flag incorrect"];

        }
    }
}
