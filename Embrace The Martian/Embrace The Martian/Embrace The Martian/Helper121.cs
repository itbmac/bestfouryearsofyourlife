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

//*************************************************************************
public class Helper121
{
    ContentManager content;

    ArrayList arrayArt   = new ArrayList();
    ArrayList arraySound = new ArrayList();

    SpriteFont smallFont;
    SpriteFont mediumFont;
    public SpriteFont mediumFont2;
    public SpriteFont mediumFont3;
    SpriteFont bigFont;

    public Texture2D pixel;

    Random randomNumberGenerator = new Random();

    public int screenW, screenH;

    bool isAdvanced;

    public static float MasterVolume { get; set; }

    //*************************************************************************
    public void Setup(IServiceProvider Services, GraphicsDeviceManager g, bool advancedFlag = false)
    {
        content = new ContentManager(Services);

        isAdvanced = advancedFlag;
        if (advancedFlag)
        {
            smallFont = content.Load<SpriteFont>("Content/SmallFont");
            mediumFont = content.Load<SpriteFont>("Content/MediumFont");
            bigFont = content.Load<SpriteFont>("Content/BigFont");
        }
        else
            mediumFont = content.Load<SpriteFont>("Content/ETM SpriteFont1");
            mediumFont2 = content.Load<SpriteFont>("Content/ETM SpriteFont2");
            mediumFont3 = content.Load<SpriteFont>("Content/ETM SpriteFont3");

        Color[] sffColor = new Color[1];
        sffColor[0] = Color.White;

        pixel = new Texture2D(g.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
        pixel.SetData<Color>(sffColor);

        screenW = g.PreferredBackBufferWidth;
        screenH = g.PreferredBackBufferHeight;

    }

    //*************************************************************************
    public Texture2D GetArt(string artFileName)
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
    public void DrawArt(SpriteBatch sb, string artFileName, int x, int y)
    {
        DrawArt(sb, artFileName, x, y, Color.White);
    }

    //*************************************************************************
    public void DrawArt(SpriteBatch sb, string artFileName, int x, int y, Color c)
    {
        Vector2 pos;
        pos.X = x;
        pos.Y = y;

        Texture2D retVal = GetArt(artFileName);
        if (retVal != null)
            sb.Draw(retVal, pos, c);
    }

    //*************************************************************************
    public void DrawPartOfTexture(SpriteBatch sb, Texture2D texture, Rectangle sourceRect, Rectangle destRect, Color c)
    {
        sb.Draw(texture, destRect, sourceRect, Color.White);
    }

    //*************************************************************************
    public SoundEffect GetSound(string soundFileName)
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
    public void PlaySound(string soundFileName)
    {
        SoundEffect retVal = GetSound(soundFileName);
        if (retVal != null)
        {
            if (soundFileName == "Content/Embrace The Martian" || soundFileName == "Content/The Arrival")
            {
                retVal.Play( .5f, 0f, 0f); 
            }
            else
            {
                retVal.Play(.7f, 0f, 0f);
            }
        }
    }

    //*************************************************************************
    public void DrawText(SpriteBatch sb, string text, int x, int y, Color c)
    {
        Vector2 pos;
        pos.X = x;
        pos.Y = y;

        sb.DrawString(mediumFont, text, pos, c);
    }

    //*************************************************************************
    public void DrawTextCentered(SpriteBatch sb, string text, int y, Color c)
    {
        Vector2 textSize = mediumFont.MeasureString(text);
        Vector2 pos = new Vector2(screenW / 2 - textSize.X/2, y); 

        sb.DrawString(mediumFont, text, pos, c);
    }

    //*************************************************************************
    public Boolean IfKeyIsDown(Keys key)
    {
        KeyboardState ks = Keyboard.GetState();
        if (ks.IsKeyDown(key))
            return true;

        return false;
    }


    //*************************************************************************
    public void DrawPoint(SpriteBatch sb, Vector2 point, Color color)
    {
        sb.Draw(pixel, point, color);
    }

    //*************************************************************************
    public void DrawLine(SpriteBatch sb, Vector2 p1, Vector2 p2, Color color)
    {

        float angle = 0f;
        Vector2 diff = p2 - p1;  

        // calculate rotation angle
        angle = (float)Math.Atan2(p2.Y - p1.Y, p2.X - p1.X);
        // build destrect
        Rectangle destrect = new Rectangle((int)p1.X, (int)p1.Y, (int)diff.Length(), 1);
//        Rectangle sourcerect = new Rectangle(0, 0, 1, 1);

        float dist = Vector2.Distance(p1,p2);

        sb.Draw(pixel, p1, null, color, angle, Vector2.Zero, new Vector2(dist,1), SpriteEffects.None, 0);
    }

    //*************************************************************************
    public void DrawRectangle(SpriteBatch sb, Rectangle rectangle, Color color)
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
    public void DrawFilledRectangle(SpriteBatch sb, Rectangle rectangle, Color color)
    {
        sb.Draw(pixel, rectangle, color);
    }

    //*************************************************************************
    public int GetRandomInt(int min, int max)
    {
        return randomNumberGenerator.Next(max - min) + min;
    }

    //*************************************************************************
    public float GetRandomFloat(double min, double max)
    {
        return (float) (randomNumberGenerator.NextDouble() * (max - min) + min);
    }









}



