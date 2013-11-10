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
    class PaperStack : DecoratorObject
    {
        int last_paper;
        public PaperStack(Texture2D t, Vector2 p)
        {
            last_paper = -1;
            base.parent = new TextureObject(t, p);
        }


        public Paper getPaper()
        {
            last_paper++;
            return new Paper(MediaManager.textures["paper"], Position, MediaManager.allAnswers[last_paper]);
        }

        public void addPaper(Paper p)
        {
        }

    }
}