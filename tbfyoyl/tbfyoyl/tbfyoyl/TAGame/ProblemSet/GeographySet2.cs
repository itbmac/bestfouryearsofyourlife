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
    class GeographySet2 : ProblemSet
    {
        public GeographySet2()
        {
            positions = new Vector2[] {
                new Vector2 (470,109),
                new Vector2 (530,254),
                new Vector2 (547,342),
                new Vector2 (367,525),
                new Vector2 (157,443),
                new Vector2 (475,442),
                new Vector2 (281,363),
                new Vector2 (224,285),
                new Vector2 (248,207),
                new Vector2 (273,119),
            };

            //177x122
            //153x98
            textureLocations = new Rectangle[] {
                
                new Rectangle (1*MediaManager.spacingwidth,6*MediaManager.spacingheight, MediaManager.flagwidth, MediaManager.flagheight), 
                new Rectangle (3*MediaManager.spacingwidth,5*MediaManager.spacingheight, MediaManager.flagwidth, MediaManager.flagheight), 
                new Rectangle (3*MediaManager.spacingwidth,4*MediaManager.spacingheight, MediaManager.flagwidth, MediaManager.flagheight), 
                new Rectangle (2*MediaManager.spacingwidth,5*MediaManager.spacingheight, MediaManager.flagwidth, MediaManager.flagheight), 
                new Rectangle (1*MediaManager.spacingwidth,9*MediaManager.spacingheight, MediaManager.flagwidth, MediaManager.flagheight), 
                new Rectangle (0*MediaManager.spacingwidth,9*MediaManager.spacingheight, MediaManager.flagwidth, MediaManager.flagheight), 
                new Rectangle (3*MediaManager.spacingwidth,6*MediaManager.spacingheight, MediaManager.flagwidth, MediaManager.flagheight), 
                new Rectangle (1*MediaManager.spacingwidth,7*MediaManager.spacingheight, MediaManager.flagwidth, MediaManager.flagheight), 
                new Rectangle (3*MediaManager.spacingwidth,7*MediaManager.spacingheight, MediaManager.flagwidth, MediaManager.flagheight), 
                new Rectangle (4*MediaManager.spacingwidth,7*MediaManager.spacingheight, MediaManager.flagwidth, MediaManager.flagheight), 
            };

            baseTexture = MediaManager.content.Load<Texture2D>("content/hw_us_geo_hw2");
            answerTexture = MediaManager.textures["state flags"];
            incorrectOverlay = MediaManager.textures["flag incorrect"];

        }
    }
}
