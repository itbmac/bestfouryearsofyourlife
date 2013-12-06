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

namespace tbfyoyl
{
    class AnimatedObject : TextureObject
    {

        private Texture2D[] textures;
        private int fps;

        public AnimatedObject(Texture2D[] textures, Vector2 pos, int fps)
            : base(textures[0], pos)
        {
            this.textures = new Texture2D[textures.Length];
            Array.Copy(textures, this.textures, textures.Length);
            this.fps = fps;
        }

        public override void Update(GameTime gameTime)
        {
            base.texture = textures[(int)((gameTime.TotalGameTime.TotalSeconds * fps) % textures.Length)];
        }

    }
}
