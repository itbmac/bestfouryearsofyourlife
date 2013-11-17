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
    class Cart : TextureObject
    {
        public Cart(Texture2D t, Vector2 p)
            : base(t, p)
        {

        }

        public override Vector2 Position
        {
            get
            {
                return base.Position;
            }
            set
            {
                Vector2 diff = value - base.Position;
                base.Position = value;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        public bool TryInsert(Vector2 pos)
        {
            if (this.Contains(pos))
            {
                return true;
            }
            else
            {
                return false;
            }
            System.Diagnostics.Debug.Write("TryInsert Object\n");
        }
    }
}
