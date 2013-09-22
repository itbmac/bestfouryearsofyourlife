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

namespace Embrace_The_Martian
{
    public class SpaceObject
    {
        static public Helper121 helper;
        static public List<SpaceObject> asteroidList;
        static public List<SpaceObject> playerList;
        static public List<SpaceObject> bulletList;

        public enum SOType { NOTHING, PLAYER, ASTEROID, BULLET };
        //public enum AsteriodSize { SMALL, MEDIUM, LARGE };
        public int asteroidSize = 1;

        public SOType type;
        public Vector2 position;
        public Vector2 velocity;
        public Vector2 asteroidDriftVelocity;
        public float   angle, angleVelocity;
        public bool    isDead, isHit, isNewHit;
        public static int health = 100;
        public static int asteroidSizeHit, asteroidHits = 0;
        public float lifeCounter;
        static public int bulletLimit = 30; // Originally 20
        static public float bulletAsteroidXDistance, bulletAsteroidYDistance, asteroidPlayerXDistance, asteroidPlayerYDistance;
        static public float shootDelay = 0;

        static public Random lifeCounterRand = new Random();


        //*************************************************************************
        public SpaceObject(SOType myType)
        {
            type = myType;
            velocity = new Vector2(0, 0); // no moving
            angle = 0; // facing = 0
            angleVelocity = 0; // no turning
            isDead = false;
            isHit = false;
            isNewHit = false;
            lifeCounter = 20;
            asteroidSize = 1;

            position = new Vector2(100, 100); // start near the upper-left corner

        }


        //*************************************************************************
        public void Update(GameTime gameTime)
        {
            //move
            position = position + asteroidDriftVelocity + (velocity * gameTime.ElapsedGameTime.Milliseconds / 100.0f);

            // wrap around screen edges
            while (position.X > helper.screenW)
                position.X -= helper.screenW;
            while (position.X < 0)
                position.X += helper.screenW;
            while (position.Y > helper.screenH)
                position.Y -= helper.screenH;
            while (position.Y < 0)
                position.Y += helper.screenH;

            
            if (SOType.ASTEROID == type)
            {
                //--------------------------New Collison Code ---------------------

                foreach (SpaceObject bullet in bulletList)
                {
                    Vector2 bulletAsteroidDistance = position - bullet.position;
                    float bulletAsteroidDistanceValue = bulletAsteroidDistance.Length();

                    if (bulletAsteroidDistanceValue <= (Game1.asteroidWidth/asteroidSize) && bullet.isDead != true)
                    {
                        AsteroidHit();
                        helper.PlaySound("Content/Asteroid Hit");
                        bullet.isDead = true;
                    }

                }

                /*
                foreach (SpaceObject player in playerList)
                {
                    Vector2 playerAsteroidDistance = position - player.position;
                    float playerAsteroidDistanceValue = playerAsteroidDistance.Length();

                    if (playerAsteroidDistanceValue <= (Game1.asteroidWidth / asteroidSize) && player.isDead != true)
                    {
                        Game1.livesTaken = Game1.livesTaken + 1;
                        player.isHit = true;
                    }

                }
                */
                /*--------------------------Old Collison Code ---------------------
                for (int i = bulletList.Count - 1; i >= 0; i--)
                {
                    bulletAsteroidXDistance = Math.Abs(position.X - bulletList[i].position.X);
                    bulletAsteroidYDistance = Math.Abs(position.Y - bulletList[i].position.Y);

                    if (Game1.asteriodWidth >= bulletAsteroidXDistance && Game1.asteriodWidth >= bulletAsteroidYDistance)
                    {
                        isDead = true;
                        bulletList[i].isDead = true;

                    }
                }
                
                for (int i = SpaceObject.playerList.Count - 1; i >= 0; i--)
                {
                    asteroidPlayerXDistance = Math.Abs(position.X - playerList[i].position.X);
                    asteroidPlayerYDistance = Math.Abs(position.Y - playerList[i].position.Y);

                    if (((Game1.asteroidWidth / asteroidSize) + Game1.playerWidth) >= asteroidPlayerXDistance && ((Game1.asteroidWidth / asteroidSize) + Game1.playerHeight) >= asteroidPlayerYDistance)
                    {
                        Game1.livesTaken = Game1.livesTaken + 1;
                        //playerList[i].isDead = true;

                    }
                }
                */
                return;
            }

            if (SOType.PLAYER == type)
            {
                KeyboardState ks = Keyboard.GetState();
                float thrust = .5f;
                float frictionCoefficent = .98f;
                float bulletSpeed = 25.0f;

                if (ks.IsKeyDown(Keys.Right))
                {
                    velocity.X += thrust;
                }

                if (ks.IsKeyDown(Keys.Left))
                {
                    velocity.X -= thrust;
                }

                if (ks.IsKeyDown(Keys.Up))
                {
                    velocity.Y -= thrust;
                }

                if (ks.IsKeyDown(Keys.Down))
                {
                    velocity.Y += thrust;
                }
                //------------------Developer Cheat Codes---------------
                if (ks.IsKeyDown(Keys.W) && Game1.oldks2.IsKeyUp(Keys.W))
                {
                    Game1.wave = Game1.wave + 1;
                    Game1.setUpNewWave = true;
                }
                Game1.oldks2 = ks;
                //--------------------------------------------------------

                velocity.X *= frictionCoefficent;
                velocity.Y *= frictionCoefficent;

                shootDelay = shootDelay + 1 * gameTime.ElapsedGameTime.Milliseconds / 100.0f;

                MouseState ms = Mouse.GetState();
                if (ms.LeftButton == ButtonState.Pressed && bulletLimit > 0 && shootDelay > 2.5)
                {
                    SpaceObject bullet = new SpaceObject(SpaceObject.SOType.BULLET);
                    bullet.position = position;
                    bullet.lifeCounter = (15 + lifeCounterRand.Next(1, 9));

                    Vector2 relativePosition = new Vector2((float)ms.X, (float)ms.Y);
                    relativePosition = relativePosition - position;
                    relativePosition = Vector2.Normalize(relativePosition);
                    relativePosition *= bulletSpeed;
                    bullet.velocity = relativePosition;
                    bullet.isDead = false;
                    SpaceObject.bulletList.Add(bullet);
                    bulletLimit = bulletLimit - 1;
                    shootDelay = 0;
                }
                //----------------------Asteroid Collision -----------------------------
                asteroidHits = 0;
                asteroidSizeHit = 3;

                for (int i = SpaceObject.asteroidList.Count - 1; i >= 0; i--)
                {
                    asteroidPlayerXDistance = Math.Abs(position.X - asteroidList[i].position.X);
                    asteroidPlayerYDistance = Math.Abs(position.Y - asteroidList[i].position.Y);

                    if (((Game1.asteroidWidth / asteroidList[i].asteroidSize) + Game1.playerWidth) >= asteroidPlayerXDistance && ((Game1.asteroidWidth / asteroidList[i].asteroidSize) + Game1.playerHeight) >= asteroidPlayerYDistance)
                    {
                        isHit = true;
                        asteroidHits = asteroidHits + 1;

                        if (asteroidList[i].asteroidSize == 1)
                        {
                            asteroidSizeHit = 1;
                        }
                        else if (asteroidList[i].asteroidSize == 2 && asteroidSizeHit > 1)
                        {
                            asteroidSizeHit = 2;
                        }
                        else if (asteroidList[i].asteroidSize == 3 && asteroidSizeHit > 2)
                        {
                            asteroidSizeHit = 3;
                        }
                    }
                }

                if (isNewHit == false && isHit == true && asteroidHits > 0)
                {
                    isNewHit = true;
                    helper.PlaySound("Content/Player Injury");
                    //Game1.livesTaken = Game1.livesTaken + 1;
                    //playerList[i].isDead = true;
                    if (asteroidSizeHit == 1)
                    {
                        SpaceObject.health = SpaceObject.health - 20;
                    }
                    else if (asteroidSizeHit == 2)
                    {
                        SpaceObject.health = SpaceObject.health - 10;
                    }
                    else if (asteroidSizeHit == 3)
                    {
                        SpaceObject.health = SpaceObject.health - 5;
                    }
                }

                else if (asteroidHits == 0)
                {
                    isHit = false;
                    isNewHit = false;
                }
                return;
            } 

            if (SOType.BULLET == type)
            {
                lifeCounter -= 1 * gameTime.ElapsedGameTime.Milliseconds / 100.0f;

                if (lifeCounter <= 0)
                {
                    isDead = true;
                    //lifeCounter = 20;
                }

                /*
                for (int i = SpaceObject.bulletList.Count - 1; i >= 0; i--)
                {
                    SpaceObject.bulletList[i].Update(gameTime);
                    if (SpaceObject.bulletList[i].lifeCounter > 0)
                    {
                        SpaceObject.bulletList[i].lifeCounter -= 1 * gameTime.ElapsedGameTime.Milliseconds / 100.0f;
                    }

                    if (SpaceObject.bulletList[i].lifeCounter <= 0)
                    {
                        SpaceObject.bulletList[i].isDead = true;
                        bulletLimit = bulletLimit + 1;
                    }
                }
                */

                return;
            }
        }

        //*************************************************************************
        public void Draw(SpriteBatch sb)
        {
            if (SOType.ASTEROID == type)
            {
                
                Rectangle r = new Rectangle((int)(position.X - (Game1.asteroidWidth / asteroidSize)),
                                            (int)(position.Y - (Game1.asteroidWidth / asteroidSize)),
                                            ((Game1.asteroidWidth / asteroidSize) + (Game1.asteroidWidth / asteroidSize)),
                                            ((Game1.asteroidWidth / asteroidSize)) + (Game1.asteroidWidth / asteroidSize));
                
                Texture2D retVal = helper.GetArt("Content/AsteroidSmall");
                if (retVal != null)
                {
                    sb.Draw(retVal, r, Color.White);

                    //draw side echos

                    r.X += helper.screenW;
                    sb.Draw(retVal, r, Color.White);

                    r.X -= helper.screenW * 2;
                    sb.Draw(retVal, r, Color.White);

                    r.X += helper.screenW;
                    r.Y += helper.screenH;
                    sb.Draw(retVal, r, Color.White);

                    r.Y -= helper.screenH;
                    sb.Draw(retVal, r, Color.White);
                }

                //helper.DrawArt(sb, "Content/asteroid", (int)position.X - Game1.asteroidWidth, (int)position.Y - Game1.asteroidWidth);

                return;
            }

            if (SOType.BULLET == type)
            {
                helper.DrawArt(sb, "Content/bullet4", (int)position.X - Game1.bulletWidth, (int)position.Y - Game1.bulletWidth);

                return;
            }

            if (SOType.PLAYER == type)
            {
                if (Game1.wave > 0 && Game1.wave < 21)
                {
                    if (isHit == true)
                    {
                        helper.DrawArt(sb, "Content/SpaceshipFix", (int)position.X - Game1.playerWidth, (int)position.Y - Game1.playerHeight, Color.Red);
                        isHit = false;
                    }
                    else
                    {
                        helper.DrawArt(sb, "Content/SpaceshipFix", (int)position.X - Game1.playerWidth, (int)position.Y - Game1.playerHeight);
                    }
                }
                else if (Game1.wave <= 0 || Game1.wave > 20)
                {
                    Rectangle r = new Rectangle((int)(position.X - Game1.martianWidth),
                                            (int)(position.Y - Game1.martianHeight),
                                            (Game1.martianWidth * 2),
                                            (Game1.martianHeight * 2));

                    Texture2D retVal = helper.GetArt("Content/Martian");
                    if (retVal != null)
                    {
                        sb.Draw(retVal, r, Color.White);

                        //draw side echos

                        r.X += helper.screenW;
                        sb.Draw(retVal, r, Color.White);

                        r.X -= helper.screenW * 2;
                        sb.Draw(retVal, r, Color.White);

                        r.X += helper.screenW;
                        r.Y += helper.screenH;
                        sb.Draw(retVal, r, Color.White);

                        r.Y -= helper.screenH;
                        sb.Draw(retVal, r, Color.White);

                        //helper.DrawArt(sb, "Content/Martian", (int)position.X - Game1.martianWidth, (int)position.Y - Game1.martianHeight);
                    }
                }

                return;
            }
        }


        //*************************************************************************
        public void AsteroidHit()
        {
            asteroidSize += 1;
            if (asteroidSize > 3)
            {
                isDead = true;
                return;
            }

            float direction = SpaceObject.helper.GetRandomFloat(-Math.PI, Math.PI);
            //velocity.X = ((float)Math.Sin(direction) * asteroidSize/3) + asteroidDriftVelocity.X;
            //velocity.Y = (float)Math.Cos(direction) * asteroidSize / 3;

            SpaceObject newAsteroid = new SpaceObject(SOType.ASTEROID);
            newAsteroid.position = position;
            newAsteroid.asteroidSize = asteroidSize;
            newAsteroid.asteroidDriftVelocity.X = -3.0f;
            newAsteroid.velocity.X = (SpaceObject.helper.GetRandomFloat(-1, 0) * asteroidSize / 3) + asteroidDriftVelocity.X;
            newAsteroid.velocity.Y = SpaceObject.helper.GetRandomFloat(-2, 2) * (asteroidSize / 3) * asteroidSize;
            asteroidList.Add(newAsteroid);
        }

    }
}
