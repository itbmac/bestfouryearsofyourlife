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
    class EmptyObject : GameObject
    {
        /*
         * An empty GameObject that we can use instead of setting all objects
         * to null
         */

        private static EmptyObject instance;
        private EmptyObject() {}
        public static EmptyObject Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new EmptyObject();
                }
                return instance;
            }
        }

        public Vector2 Position { get; set; }

        public void Draw(SpriteBatch spriteBatch) { }
        public void Click(Vector2 pos) { }
        public void Drag(Vector2 start, Vector2 end) { }
        public bool Contains(Vector2 pos)
        {
            return true;
        }


    }
}
