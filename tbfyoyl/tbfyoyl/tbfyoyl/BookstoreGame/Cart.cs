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
        Texture2D texture_1;
        Texture2D texture_2;
        bool flipTexture = false;

        public Cart(Texture2D t1, Texture2D t2, Vector2 p)
            : base(t1, p)
        {
            texture_1 = t1;
            texture_2 = t2;
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

        public void SetTextureFlip(bool isFlipped)
        {
            flipTexture = isFlipped;

            if (isFlipped)
                Position = new Vector2(400, Position.Y);
            else
                Position = new Vector2(600, Position.Y);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!flipTexture)
                spriteBatch.Draw(texture_1, Position, Color.White);
            else
                spriteBatch.Draw(texture_2, Position, Color.White);
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
