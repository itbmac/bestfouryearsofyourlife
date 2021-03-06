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
using tbfyoyl.TAGame;

namespace tbfyoyl
{

    //*************************************************************************
    public static class MediaManager
    {
        public static ContentManager content;

        public static Dictionary<String, Texture2D> textures;

        private static ArrayList arrayArt = new ArrayList();
        private static ArrayList arraySound = new ArrayList();

        public static SoundEffectInstance emilia;
        public static SoundEffectInstance dark_beat;


        public static int flagwidth = 115;
        public static int flagheight = 73;
        public static int spacingwidth = 133;
        public static int spacingheight = 92;
        public static int chemAnswerHeight = 30;
        public static int chemAnswerWidth = 30;

        private static SpriteFont smallFont;
        private static SpriteFont mediumFont;
        public static SpriteFont mediumFont2;
        public static SpriteFont mediumFont3;
        private static SpriteFont bigFont;

        public static SpriteFont latexFont;

        //public static Answer[][][] allAnswers;
        //public static Answer nullAnswer;

        public static Texture2D pixel;

        private static Random randomNumberGenerator = new Random();

        public static int screenW, screenH;

        private static bool isAdvanced;

        public static SoundEffect curSoundEffect;

        public static float MasterVolume { get; set; }

        public static Camera2d cam;

        public static GraphicsDevice gd;

        //*************************************************************************
        public static void Setup(IServiceProvider Services, GraphicsDeviceManager g, bool advancedFlag = false)
        {

            content = new ContentManager(Services);
            gd = g.GraphicsDevice;
            textures = new Dictionary<string, Texture2D>();

            emilia = (content.Load<SoundEffect>("Content/emilia")).CreateInstance();
            dark_beat = (content.Load<SoundEffect>("Content/dark_beat")).CreateInstance();

            // UI'S ASSETS
            textures.Add("BLAH", content.Load<Texture2D>("content/BLAH"));
            textures.Add("MENU_TOP", content.Load<Texture2D>("content/UI_Top_150p"));
            textures.Add("MENU_MIDDLE", content.Load<Texture2D>("content/UI_Middle_150p"));
            textures.Add("MENU_BOTTOM", content.Load<Texture2D>("content/UI_Bottom_150p"));
            
            // TA GAME'S ASSETS
            textures.Add("paper", content.Load<Texture2D>("content/main_paper"));
            textures.Add("answer", content.Load<Texture2D>("content/BLAH"));
            textures.Add("pen_incorrect", content.Load<Texture2D>("content/main_pen_red"));
            textures.Add("pen_cheater", content.Load<Texture2D>("content/main_pen"));
            Texture2D empty = new Texture2D(g.GraphicsDevice, 400, 250);
            Color[] empty_color = new Color[400 * 250];
            for (int i = 0; i < 400 * 250; i++)
                empty_color[i] = Color.Transparent;
            empty.SetData(empty_color);
            textures.Add("empty", empty);
            textures.Add("graded stack", empty);
            textures.Add("ungraded stack", content.Load<Texture2D>("content/hw_stack"));
            textures.Add("state flags", content.Load<Texture2D>("content/state_flags_small"));
            textures.Add("flag incorrect", content.Load<Texture2D>("content/crossout_2"));
            textures.Add("next button", content.Load<Texture2D>("content/BLAH"));
            textures.Add("prev button", content.Load<Texture2D>("content/BLAH"));
            textures.Add("chem answers", content.Load<Texture2D>("content/ABCDE"));
            textures.Add("chem incorrect", content.Load<Texture2D>("content/crossout_2"));
            textures.Add("hw done", content.Load<Texture2D>("content/hw_set_completion_paper"));
            textures.Add("geo results", content.Load<Texture2D>("content/hw_stats_paper_us_geo"));
            textures.Add("chem results", content.Load<Texture2D>("content/hw_stats_paper_chem"));
            textures.Add("math results", content.Load<Texture2D>("content/hw_stats_paper_us_geo"));

            // BOOKSTORE GAME'S ASSETS
            textures.Add("books", content.Load<Texture2D>("content/books"));
            textures.Add("cart", content.Load<Texture2D>("content/shopping_cart_200"));
            textures.Add("cart_h", content.Load<Texture2D>("content/shopping_cart_200_h"));
            textures.Add("a", content.Load<Texture2D>("content/partay/implied_partay0_00000"));


            /*
            Texture2D answerTexture = textures["BLAH"];
            //allAnswers[problemSet#][paper#][answer#]
            allAnswers = new Answer[][][] {
                new Answer[][] {
                    new Answer[] {
                        new Answer(answerTexture, new Vector2(0, 0), true, true),
                        new Answer(answerTexture, new Vector2(0, 0), true, true)
                    },
                    new Answer[] {
                        new Answer(answerTexture, new Vector2(0, 0), true, true),
                        new Answer(answerTexture, new Vector2(0, 0), true, true)
                    },
                    new Answer[] {
                        new Answer(answerTexture, new Vector2(0, 0), true, true),
                        new Answer(answerTexture, new Vector2(0, 0), true, true)
                    }
                },
                new Answer[][] {
                    new Answer[] {
                        new Answer(answerTexture, new Vector2(0, 0), true, true),
                        new Answer(answerTexture, new Vector2(0, 0), true, true)
                    },
                    new Answer[] {
                        new Answer(answerTexture, new Vector2(0, 0), true, true),
                        new Answer(answerTexture, new Vector2(0, 0), true, true)
                    },
                    new Answer[] {
                        new Answer(answerTexture, new Vector2(0, 0), true, true),
                        new Answer(answerTexture, new Vector2(0, 0), true, true)
                    },
                    new Answer[] {
                        new Answer(answerTexture, new Vector2(0, 0), true, true),
                        new Answer(answerTexture, new Vector2(0, 0), true, true)
                    }
                }
            };
             */

            latexFont = content.Load<SpriteFont>("Content/latex_font");

            isAdvanced = advancedFlag;
            if (advancedFlag)
            {
                //smallFont = content.Load<SpriteFont>("Content/SmallFont");
                // mediumFont = content.Load<SpriteFont>("Content/MediumFont");
                // bigFont = content.Load<SpriteFont>("Content/BigFont");
            }
            else
            {
                //  mediumFont = content.Load<SpriteFont>("Content/ETM SpriteFont1");
                //  mediumFont2 = content.Load<SpriteFont>("Content/ETM SpriteFont2");
                //  mediumFont3 = content.Load<SpriteFont>("Content/ETM SpriteFont3");
            }

            mediumFont2 = content.Load<SpriteFont>("Content/MainSpriteFont1");

            Color[] sffColor = new Color[1];
            sffColor[0] = Color.White;

            pixel = new Texture2D(g.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            pixel.SetData<Color>(sffColor);

            screenW = g.PreferredBackBufferWidth;
            screenH = g.PreferredBackBufferHeight;

        }

        //*************************************************************************

        public static Vector2 GetMousePos(MouseState state)
        {
            return Vector2.Transform(new Vector2(state.X, state.Y),
                   Matrix.Invert(MediaManager.cam.get_transformation(gd)));
        }

        public static Vector2 GetCurMousePos()
        {
            return GetMousePos(Mouse.GetState());
        }

        public static Texture2D GetArt(string artFileName)
        {
            Texture2D retVal;

            for (int i = 0; i < arrayArt.Count; ++i)
            {
                retVal = (Texture2D)arrayArt[i];
                if (retVal.Name == artFileName)
                    return retVal;
            }

            retVal = content.Load<Texture2D>(artFileName);
            if (retVal != null)
            {
                retVal.Name = artFileName;
                arrayArt.Add(retVal);
                return retVal;
            }
            return null;
        }

        //*************************************************************************
        public static void DrawArt(SpriteBatch sb, string artFileName, int x, int y)
        {
            DrawArt(sb, artFileName, x, y, Color.White);
        }

        //*************************************************************************
        public static void DrawArt(SpriteBatch sb, string artFileName, int x, int y, Color c)
        {
            Vector2 pos;
            pos.X = x;
            pos.Y = y;

            Texture2D retVal = GetArt(artFileName);
            if (retVal != null)
                sb.Draw(retVal, pos, c);
        }

        //*************************************************************************
        public static void DrawPartOfTexture(SpriteBatch sb, Texture2D texture, Rectangle sourceRect, Rectangle destRect, Color c)
        {
            sb.Draw(texture, destRect, sourceRect, Color.White);
        }

        //*************************************************************************
        public static SoundEffect GetSound(string soundFileName)
        {
            SoundEffect retVal;

            for (int i = 0; i < arraySound.Count; ++i)
            {
                retVal = (SoundEffect)arraySound[i];
                if (retVal.Name == soundFileName)
                    return retVal;
            }

            retVal = content.Load<SoundEffect>(soundFileName);
            if (retVal != null)
            {
                retVal.Name = soundFileName;
                arraySound.Add(retVal);
                return retVal;
            }
            return null;
        }

        //*************************************************************************
        public static void PlaySound(string soundFileName)
        {
            SoundEffect retVal = GetSound(soundFileName);
            if ((curSoundEffect != null) && (curSoundEffect.IsDisposed))
                curSoundEffect.CreateInstance();
            if (retVal != null)
            {
                if (true) //soundFileName == "Content/Embrace The Martian" || soundFileName == "Content/The Arrival")
                {
                    retVal.Play(.5f, 0f, 0f);
                }
                else
                {
                    retVal.Play(.7f, 0f, 0f);
                }
            }
            curSoundEffect = retVal;
        }

        //*************************************************************************
        public static void StopSound()
        {
            //curSoundEffect.Duration = 0;
        }

        //*************************************************************************
        public static void DrawText(SpriteBatch sb, string text, int x, int y, Color c)
        {
            Vector2 pos;
            pos.X = x;
            pos.Y = y;

            sb.DrawString(mediumFont2, text, pos, c);
        }

        //*************************************************************************
        public static void DrawTextCentered(SpriteBatch sb, string text, int y, Color c)
        {
            Vector2 textSize = mediumFont.MeasureString(text);
            Vector2 pos = new Vector2(screenW / 2 - textSize.X / 2, y);

            sb.DrawString(mediumFont, text, pos, c);
        }

        //*************************************************************************
        public static void DrawPoint(SpriteBatch sb, Vector2 point, Color color)
        {
            sb.Draw(pixel, point, color);
        }

        //*************************************************************************
        public static void DrawLine(SpriteBatch sb, Vector2 p1, Vector2 p2, Color color)
        {

            float angle = 0f;
            Vector2 diff = p2 - p1;

            // calculate rotation angle
            angle = (float)Math.Atan2(p2.Y - p1.Y, p2.X - p1.X);
            // build destrect
            Rectangle destrect = new Rectangle((int)p1.X, (int)p1.Y, (int)diff.Length(), 1);
            //        Rectangle sourcerect = new Rectangle(0, 0, 1, 1);

            float dist = Vector2.Distance(p1, p2);

            sb.Draw(pixel, p1, null, color, angle, Vector2.Zero, new Vector2(dist, 1), SpriteEffects.None, 0);
        }

        //*************************************************************************
        public static void DrawRectangle(SpriteBatch sb, Rectangle rectangle, Color color)
        {
            Vector2 TopLeft = new Vector2(rectangle.Left, rectangle.Top);
            Vector2 TopRight = new Vector2(rectangle.Right, rectangle.Top);
            Vector2 BottomLeft = new Vector2(rectangle.Left, rectangle.Bottom);
            Vector2 BottomRight = new Vector2(rectangle.Right, rectangle.Bottom);

            DrawLine(sb, TopLeft, TopRight, color);
            DrawLine(sb, TopLeft, BottomLeft, color);
            DrawLine(sb, TopRight, BottomRight, color);
            DrawLine(sb, BottomLeft, BottomRight, color);
        }

        //*************************************************************************
        public static int GetRandomInt(int min, int max)
        {
            return randomNumberGenerator.Next(max - min) + min;
        }

        //*************************************************************************
        public static float GetRandomFloat(double min, double max)
        {
            return (float)(randomNumberGenerator.NextDouble() * (max - min) + min);
        }

    }

}
