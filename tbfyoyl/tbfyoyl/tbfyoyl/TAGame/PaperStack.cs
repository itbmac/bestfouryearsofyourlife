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

namespace tbfyoyl
{
    class PaperStack : TextureObject
    {
        public PaperStack(Texture2D t, Vector2 p)
            : base(t, p)
        {
        }

        public Paper getPaper()
        {
            return null;
        }

        public void addPaper(Paper p)
        {
        }

    }
}