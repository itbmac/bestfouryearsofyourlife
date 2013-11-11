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
using tbfyoyl;
using Spine;

namespace tbfyoyl
{
    public class Player : TextureObject
    {
        SkeletonRenderer skeletonRenderer;
        Skeleton skeleton;
        Slot headSlot;
        AnimationState state;
        SkeletonBounds bounds = new SkeletonBounds();
        int direction = 1;
        float speed = 1;
        float distStartToTarget = 10;
        float targetX = 600;
        GraphicsDevice graphics;

        protected void LoadContent(GraphicsDevice gD)
        {
            graphics = gD;
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

            skeleton.X = 600;
            skeleton.Y = 600;
            skeleton.UpdateWorldTransform();

            headSlot = skeleton.FindSlot("head");
        }

        public Player(Texture2D t, Vector2 pos, GraphicsDevice gD)
            : base(t, pos, false)
        {
            LoadContent(gD);

            skeleton.X = pos.X;
            skeleton.Y = pos.Y;
        }

        public void setCurX(float X)
        {
            skeleton.X = X;
            distStartToTarget = 0;
        }

        public void setX(float X)
        {
            if (X > skeleton.X)
            {
                skeleton.FlipX = false;
                direction = 1;
            }
            else
            {
                skeleton.FlipX = true;
                direction = -1;
            }
            targetX = X;
            distStartToTarget = Math.Abs(skeleton.X - X);
        }

        public void setDirection(Boolean isFlipped)
        {
            skeleton.FlipX = isFlipped;
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Update(GameTime gameTime)
        {
            // TODO: Add your update code here
            if (Math.Abs(skeleton.X - targetX) > 10)
            {
                float one = 1.0f;
                float dist = Math.Abs(targetX - skeleton.X) / (distStartToTarget / 10);
                speed = Math.Max(one, dist);
                skeleton.X += direction * speed;
                //state.SetAnimation(0, "walk", true);
            }
            else
            {
                speed = 0;
                //state.SetAnimation(0, "stand", false);
            }

            state.Update(gameTime.ElapsedGameTime.Milliseconds / 1000f);
            state.Apply(skeleton);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            skeleton.UpdateWorldTransform();

            spriteBatch.End();

            skeletonRenderer.Begin();
            skeletonRenderer.Draw(skeleton);
            skeletonRenderer.End();

            spriteBatch.Begin(SpriteSortMode.Immediate,
                        BlendState.AlphaBlend,
                        null,
                        null,
                        null,
                        null,
                        MediaManager.cam.get_transformation(graphics));

            bounds.Update(skeleton, true);
           /* MouseState mouse = Mouse.GetState();
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
            } */
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
