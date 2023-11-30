using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace GodBreakable
{
    public class SceneGameplay : Scene
    {
        Racket spRaquette;
        Ball spBall;
        Boss newBoss;
        LifeBar bossLifebar;
        private bool ballStick;
        const int NbColonnes = 11;
        const int NbLines = 10;
        private int[,] Level;
        private List<Brick> lstBrick;
        SpriteFont fontMenu;
        private readonly Score Score = new Score();

        public SceneGameplay(Game pGame, string sceneName) : base(pGame, sceneName)
        {
            //gameActual = pGame;

            //Contexte.life = 5;
            Rectangle Screen = ScreenSize;
            fontMenu = game.Content.Load<SpriteFont>("Default");

            //raquette
            spRaquette = new Racket(Screen, serviceSprite.NewSprite("img/racket"));//game.Content.Load<Texture2D>("racket"));
            spRaquette.SetPosition(Screen.Width / 2 - spRaquette.Width / 2, Screen.Height - spRaquette.Height);
            //spRaquette.Speed = new Vector2(2,0);

            //ball
            spBall = new Ball(Screen, serviceSprite.NewSprite("img/ball"));
            spBall.SetPosition(spRaquette.Position.X + spBall.Width / 2, spRaquette.Position.Y - spBall.Width);
            spBall.Speed = new Vector2(6, -6);
            ballStick = true;

            //Boss
            //newBoss = new Boss("AB", 100);

            //LifeBar
            //bossLifebar = new LifeBar(Screen, serviceSprite.NewSprite("img/barfull"), game);
            //bossLifebar.SetPosition(Screen.Width / 2 - bossLifebar.Width/2, Screen.Height / 2);

            Level = new int[,]
            {
                {1,1,1,1,1,1,1,1,1,1,1 },
                {1,1,1,1,1,1,1,1,1,1,1 },
                {1,1,1,1,1,1,1,1,1,1,1 },
                {1,1,1,1,1,1,1,1,1,1,1 },
                {1,1,1,1,1,1,1,1,1,1,1 },
                {1,1,1,1,1,1,1,1,0,0,1 },
                {1,1,1,1,1,1,0,0,0,0,1 },
                {1,1,1,1,1,0,0,0,0,0,1 },
                {1,1,1,1,1,1,1,0,0,0,1 },
                {1,1,1,1,1,1,1,0,0,0,1 }
            };

            lstBrick = new List<Brick>();
            Texture2D texBrick = serviceSprite.NewSprite("img/brick1");
            for (int l = 0; l < Level.GetLength(0); l++)
            {
                for (int c = 0; c < Level.GetLength(1); c++)
                {
                    if (Level[l, c] == 1)
                    {
                        Brick myBrick = new Brick(ScreenSize, texBrick, "Normal", 1);
                        myBrick.SetPosition(c * texBrick.Width, l * texBrick.Height);
                        lstBrick.Add(myBrick);
                    }
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            spRaquette.Update();

            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                if (ballStick)
                {
                    ballStick = false;
                }
            }

            if (ballStick)
            {
                spBall.SetPosition(spRaquette.center - spBall.Width / 2, spRaquette.Position.Y - spRaquette.Height / 2 - spRaquette.Height);
            }
            else
            {
                spBall.Update();
            }
            if (spRaquette.CollideBox.Intersects(spBall.NextPositionY()))
            {
                if (spBall.Position.X <= spRaquette.Position.X + spRaquette.CollideBox.Width/2)
                {
                    Debug.WriteLine("Hit Left !");
                }
                if (spBall.Position.X >= spRaquette.Position.X + spRaquette.CollideBox.Width / 2)
                {
                    Debug.WriteLine("Hit Right !");
                }
                if (spBall.Position.X == spRaquette.Position.X + spRaquette.CollideBox.Width / 2)
                {
                    Debug.WriteLine("Hit Center !");
                }

                Debug.WriteLine("BallX: "+spBall.Position.X + " RacketX: "+spRaquette.Position.X + " RacketW: "+spRaquette.Width+ " TestRXW: "+(spRaquette.Position.X + spRaquette.Width));
                spBall.InverseSpeedY();
            }
            if (spRaquette.CollideBox.Intersects(spBall.NextPositionX()))
            {
                //spBall.InverseSpeedX();
                ballStick = true;
            }
            if (spBall.Position.Y >= ScreenSize.Height)
            {
                ballStick = true;
            }

            for (int i = lstBrick.Count - 1; i >= 0; i--)
            {
                bool colision = false;
                Brick myBrick = lstBrick[i];
                myBrick.Update();
                if (myBrick.BrickIsFalling == false)
                {
                    if (myBrick.CollideBox.Intersects(spBall.NextPositionX()))
                    {
                        spBall.InverseSpeedX();
                        colision = true;
                    }
                    if (myBrick.CollideBox.Intersects(spBall.NextPositionY()))
                    {
                        spBall.InverseSpeedY();
                        colision = true;
                    }
                    if (colision)
                    {
                        myBrick.Fall();
                        CamShake = 30;
                    }
                }
                if (myBrick.Position.Y >= ScreenSize.Height)
                {
                    lstBrick.Remove(myBrick);
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Z))
            {
                //bossLifebar.lifeRect.Width -= 1;
            }
        }

        public override void Draw(SpriteBatch pBatch)
        {
            base.Draw(pBatch);
            pBatch.Begin();

            foreach (var brick in lstBrick)
            {
                brick.Draw(pBatch);
            }
            pBatch.DrawString(fontMenu, "Bricks Left: " + lstBrick.Count, new Vector2(10, 10), Color.White);

            spRaquette.Draw(pBatch);
            spBall.Draw(pBatch);
            //bossLifebar.Draw(pBatch);

            IServiceScore servScore = ServiceLocator.GetService<IServiceScore>();
            if (servScore != null)
            {
                pBatch.DrawString(fontMenu, "Score : " + servScore.Get(), new Vector2(10, 30), Color.White);
            }
            //IServiceFont servFont = ServiceLocator.GetService<IServiceFont>();
            //servFont.Print("Test",gameActual, pBatch);

            pBatch.End();
        }
    }
}
