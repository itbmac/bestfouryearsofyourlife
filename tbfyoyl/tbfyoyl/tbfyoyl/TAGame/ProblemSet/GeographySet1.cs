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
                new Vector2(0, 0),
                new Vector2(0, 200),
                new Vector2(0, 400),
            };

            textures = new Texture2D[] {
                MediaManager.textures["BLAH"],
                MediaManager.textures["books"],
                MediaManager.textures["cart"],
            };

            baseTexture = MediaManager.textures["paper"];

        }
    }
}
