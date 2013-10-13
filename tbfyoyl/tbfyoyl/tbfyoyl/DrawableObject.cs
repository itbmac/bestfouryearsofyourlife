﻿using System;
using System.Collections.Generic;
using System.Collections;
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
    public class DrawableObject : GameObject
    {

        private Texture2D texture;
        private Vector2 position;

        public DrawableObject(Texture2D t, Vector2 p)
        {
            texture = t;
            position = p;
        }

        public override Rectangle BoundingBox()
        {
            Rectangle bounds = texture.Bounds;
            bounds.Offset((int)position.X, (int)position.Y);
            return bounds;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //assumes spriteBatch.begin() has already been called
            spriteBatch.Draw(texture, position, Color.White);
        }

    }
}