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
    class GradingStamp : TextureObject
    {
        
        public GradingStamp(Texture2D t, Vector2 p)
            : base(t, p)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        public override void ClickDown(Vector2 pos)
        {
        }

        public override void Drag(Vector2 start, Vector2 end)
        {
            Position = Position - start + end;
        }
    }
}

