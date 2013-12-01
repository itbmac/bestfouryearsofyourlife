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
    class ProblemSet
    {
        /*
         * A problem set that can generate answers based on a correctness criterion
         */

        protected Vector2[] positions;
        protected Texture2D[] textures;
        protected Texture2D baseTexture;

        protected ProblemSet () { }

        public Paper generatePaper(double threshold)
        {
            Answer[] ret = new Answer[positions.Length];
            Vector2[] newPositions = new Vector2[positions.Length];
            System.Array.Copy(positions, newPositions, positions.Length);

            int numSwaps = (int)Math.Round(positions.Length * threshold);
            System.Diagnostics.Debug.WriteLine(positions.Length * threshold);
            System.Diagnostics.Debug.WriteLine(numSwaps);
            for (int i = 0; i < numSwaps; i++)
            {
                int x = MediaManager.GetRandomInt(0, positions.Length);
                int y = MediaManager.GetRandomInt(0, positions.Length);
                Vector2 tmp = newPositions[x];
                newPositions[x] = newPositions[y];
                newPositions[y] = tmp;
            }

            for (int i = 0; i < positions.Length; i++)
            {
                ret[i] = new Answer(textures[i], newPositions[i], positions[i] == newPositions[i], false);
            }

            return new Paper(baseTexture, new Vector2(0, 0), ret);
        }

    }
}
