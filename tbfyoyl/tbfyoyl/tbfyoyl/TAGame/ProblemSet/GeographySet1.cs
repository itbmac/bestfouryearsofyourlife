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
    class GeographySet1 : ProblemSet
    {
        public GeographySet1()
        {
            positions = new Vector2[] {
                new Vector2(503,199),
                new Vector2(528,323),
                new Vector2(561,437),
                new Vector2(420,424),
                new Vector2(406,562),
                new Vector2(301,365),
                new Vector2(207,443),
                new Vector2(255,249),
                new Vector2(385,285),
                new Vector2(283,147),
                new Vector2(68,344),
                new Vector2(104,590),
            };

            //177x122
            //153x98
            textureLocations = new Rectangle[] {
                new Rectangle(0*MediaManager.spacingwidth, 8*MediaManager.spacingheight, MediaManager.flagwidth, MediaManager.flagheight),
                new Rectangle(3*MediaManager.spacingwidth, 8*MediaManager.spacingheight, MediaManager.flagwidth, MediaManager.flagheight),
                new Rectangle(2*MediaManager.spacingwidth, 7*MediaManager.spacingheight, MediaManager.flagwidth, MediaManager.flagheight),
                new Rectangle(4*MediaManager.spacingwidth, 8*MediaManager.spacingheight, MediaManager.flagwidth, MediaManager.flagheight),
                new Rectangle(2*MediaManager.spacingwidth, 9*MediaManager.spacingheight, MediaManager.flagwidth, MediaManager.flagheight),
                new Rectangle(0*MediaManager.spacingwidth, 7*MediaManager.spacingheight, MediaManager.flagwidth, MediaManager.flagheight),
                new Rectangle(0*MediaManager.spacingwidth, 6*MediaManager.spacingheight, MediaManager.flagwidth, MediaManager.flagheight),
                new Rectangle(2*MediaManager.spacingwidth, 6*MediaManager.spacingheight, MediaManager.flagwidth, MediaManager.flagheight),
                new Rectangle(2*MediaManager.spacingwidth, 8*MediaManager.spacingheight, MediaManager.flagwidth, MediaManager.flagheight),
                new Rectangle(1*MediaManager.spacingwidth, 8*MediaManager.spacingheight, MediaManager.flagwidth, MediaManager.flagheight),
                new Rectangle(4*MediaManager.spacingwidth, 9*MediaManager.spacingheight, MediaManager.flagwidth, MediaManager.flagheight),
                new Rectangle(3*MediaManager.spacingwidth, 9*MediaManager.spacingheight, MediaManager.flagwidth, MediaManager.flagheight),
            };

            baseTexture = MediaManager.content.Load<Texture2D>("content/hw_us_geo_hw1");
            answerTexture = MediaManager.textures["state flags"];
            incorrectOverlay = MediaManager.textures["flag incorrect"];

        }
    }
}
