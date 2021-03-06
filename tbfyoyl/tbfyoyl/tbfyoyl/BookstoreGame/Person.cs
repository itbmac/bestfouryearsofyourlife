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
using tbfyoyl;
using Spine;

namespace tbfyoyl
{
    public class Person : TextureObject
    {
        SkeletonRenderer skeletonRenderer;
		Skeleton skeleton;
		Slot headSlot;
		AnimationState state;
		SkeletonBounds bounds = new SkeletonBounds();
        int speed = 1;

        protected void LoadContent(GraphicsDevice gD)
        {
            skeletonRenderer = new SkeletonRenderer(gD);
            skeletonRenderer.PremultipliedAlpha = true;

            String name = "spineboy"; // "goblins";

            Atlas atlas = new Atlas("data/" + name + ".atlas", new XnaTextureLoader(gD));
            SkeletonJson json = new SkeletonJson(atlas);
            skeleton = new Skeleton(json.ReadSkeletonData("data/" + name + ".json"));
            if (name == "goblins") skeleton.SetSkin("goblingirl");
            skeleton.SetSlotsToSetupPose(); // Without this the skin attachments won't be attached. See SetSkin.

            // Define mixing between animations.
            AnimationStateData stateData = new AnimationStateData(skeleton.Data);
            if (name == "spineboy")
            {
                stateData.SetMix("walk", "jump", 0.2f);
                stateData.SetMix("jump", "walk", 0.4f);
            }

            state = new AnimationState(stateData);

            if (false)
            {
                // Event handling for all animations.
                state.Start += Start;
                state.End += End;
                state.Complete += Complete;
                state.Event += Event;

                state.SetAnimation(0, "drawOrder", true);
            }
            else
            {
                state.SetAnimation(0, "walk", false);
                TrackEntry entry = state.AddAnimation(0, "jump", false, 0);
                entry.End += new EventHandler<StartEndArgs>(End); // Event handling for queued animations.
                state.AddAnimation(0, "walk", true, 0);
            }

            skeleton.X = 320;
            skeleton.Y = 440;
            skeleton.UpdateWorldTransform();

            headSlot = skeleton.FindSlot("head");
        }
        
        public Person(Texture2D t, Vector2 pos, GraphicsDevice gD)
            : base(t, pos, false)
        {
            LoadContent(gD);
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Update(GameTime gameTime)
        {
            // TODO: Add your update code here
            state.Update(gameTime.ElapsedGameTime.Milliseconds / 1000f);
            state.Apply(skeleton);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            if (skeleton.X > 640 || skeleton.X < 0)
            {
                speed = -1 * speed;
                skeleton.FlipX = !skeleton.FlipX;
            }

            skeleton.X += speed;

            skeleton.UpdateWorldTransform();
            skeletonRenderer.Begin();
            skeletonRenderer.Draw(skeleton);
            skeletonRenderer.End();

            bounds.Update(skeleton, true);
            MouseState mouse = Mouse.GetState();
            headSlot.G = 1;
            headSlot.B = 1;
            if (bounds.AabbContainsPoint(mouse.X, mouse.Y))
            {
                BoundingBoxAttachment hit = bounds.ContainsPoint(mouse.X, mouse.Y);
                if (hit != null)
                {
                    headSlot.G = 0;
                    headSlot.B = 0;
                }
            }
        }

        public void Start(object sender, StartEndArgs e)
        {
            Console.WriteLine(e.TrackIndex + " " + state.GetCurrent(e.TrackIndex) + ": start");
        }

        public void End(object sender, StartEndArgs e)
        {
            Console.WriteLine(e.TrackIndex + " " + state.GetCurrent(e.TrackIndex) + ": end");
        }

        public void Complete(object sender, CompleteArgs e)
        {
            Console.WriteLine(e.TrackIndex + " " + state.GetCurrent(e.TrackIndex) + ": complete " + e.LoopCount);
        }

        public void Event(object sender, EventTriggeredArgs e)
        {
            Console.WriteLine(e.TrackIndex + " " + state.GetCurrent(e.TrackIndex) + ": event " + e.Event);
        }

        public new bool ClickDown(Vector2 pos)
        {
            if (base.Contains(pos))
            {
                
                return true;
            }
            return false;
        }
    }
}
