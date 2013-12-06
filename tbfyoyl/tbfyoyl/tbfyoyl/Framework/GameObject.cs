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
    public interface GameObject
    {
        /*
         * Interface showing all functionalities an object in the game
         * should support. In theory, all non-trivial objects in the
         * game will implement this interface in some form or another.
         */

        Vector2 Position { get; set; }

        void Draw(SpriteBatch spriteBatch);
        void ClickDown(Vector2 pos);
        void ClickUp(Vector2 pos);
        void Drag(Vector2 start, Vector2 end);
        bool Contains(Vector2 pos);
        void Update(GameTime gameTime);
    }
}
