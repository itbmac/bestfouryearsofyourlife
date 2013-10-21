using System;
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
        /*
         * The concrete implementation of the GameObject class. In this project
         * all game objects should be drawable.
         */

        // The texture???? (consider creating a content class) that this drawable 
        // object should show
        private Texture2D texture;

        // The position of this object
        private Vector2 position;

        public DrawableObject(Texture2D t, Vector2 p)
        {
            texture = t;
            position = p;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //assumes spriteBatch.begin() has already been called
            spriteBatch.Draw(texture, position, Color.White);
        }

        public override void Click(int x, int y) { }

        public override void Drag(int x1, int y1, int x2, int y2) { }

        public override Rectangle BoundingBox()
        {
            Rectangle bounds = texture.Bounds;
            bounds.Offset((int)position.X, (int)position.Y);
            return bounds;
        }

        public override void SetPosition(Vector2 newP)
        {
            System.Diagnostics.Debug.WriteLine("SET POS");
            position = newP;
        }

    }
}
