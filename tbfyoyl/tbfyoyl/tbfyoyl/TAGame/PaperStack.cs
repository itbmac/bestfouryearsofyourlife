﻿using System;
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
    class PaperStack : DecoratorObject
    {

        private Stack<Paper> papers;
        private GameObject baseObject;

        public PaperStack(Texture2D t, Vector2 p)
        {
            papers = new Stack<Paper>();
            papers.Push(new Paper(t, p, new Answer[] {new Answer(), new Answer()}));
            parent = papers.Peek();
            baseObject = new TextureObject(t, p);
        }

        public int numPapers()
        {
            return papers.Count - 1;
        }

        public void clear()
        {
            while (papers.Count > 1)
            {
                papers.Pop();
            }
            parent = papers.Peek();
        }

        public Paper getPaper()
        {
            if (papers.Count > 1)
            {
                Paper ret = papers.Pop();
                parent = papers.Peek();
                return ret;
            }
            parent = papers.Peek();
            return null;
        }

        public Paper peekPaper()
        {
            if (papers.Count > 1)
            {
                return (Paper) parent;
            }
            return null;
        }

        public void addPaper(Paper p)
        {
            p.Position = Position;
            papers.Push(p);
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