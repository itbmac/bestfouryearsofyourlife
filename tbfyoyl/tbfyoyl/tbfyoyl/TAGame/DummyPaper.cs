using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace tbfyoyl.TAGame
{
    class DummyPaper : Paper
    {
        public DummyPaper()
            : base(MediaManager.textures["empty"], new Vector2(0, 0), new Answer[] {new Answer()})
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            spriteBatch.DrawString(MediaManager.mediumFont2, "Drag this to finish this set", Position, Color.Black);
        }

    }
}
