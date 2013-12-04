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

namespace tbfyoyl.TAGame
{
    class HomeworkStack : DecoratorObject
    {

        private Stack<Homework> homeworks;
        private GameObject baseObject;

        public HomeworkStack(Texture2D t, Vector2 p)
        {
            homeworks = new Stack<Homework>();
            homeworks.Push(new Homework(EmptyObject.Instance, EmptyObject.Instance, new Paper[] { new Paper(t, p, new Answer[] { new Answer(), new Answer() }) }));
            parent = homeworks.Peek();
            baseObject = new TextureObject(t, p);
        }

        public int numHomeworks()
        {
            return homeworks.Count - 1;
        }

        public void clear()
        {
            while (homeworks.Count > 1)
            {
                homeworks.Pop();
            }
            parent = homeworks.Peek();
        }

        public Homework getPaper()
        {
            if (homeworks.Count > 1)
            {
                Homework ret = homeworks.Pop();
                parent = homeworks.Peek();
                return ret;
            }
            parent = homeworks.Peek();
            return null;
        }

        public Homework peekPaper()
        {
            if (homeworks.Count > 1)
            {
                return (Homework) parent;
            }
            return null;
        }

        public void addHomework(Homework p)
        {
            p.Position = Position;
            homeworks.Push(p);
            parent = p;
        }

        public override void Drag(Vector2 start, Vector2 end) {}

        public override void Draw(SpriteBatch b)
        {
            baseObject.Draw(b);
            base.Draw(b);
        }

    }
}