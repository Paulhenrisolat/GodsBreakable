using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodBreakable
{
    public class SceneBoss : Scene
    {
        Boss newBoss;
        private List<Boss> lstBoss;
        private string SelectedBoss;
        private Rectangle GameScreen;
        Racket newRacket;
        Ball newBall;
        bool ballStick;

        public SceneBoss(Game pGame) : base(pGame)
        {
            //Services
            IServiceSprite servSprite = ServiceLocator.GetService<IServiceSprite>();
            IServiceScreen servScreen = ServiceLocator.GetService<IServiceScreen>();
            GameScreen = servScreen.GetScreen(pGame);

            //Racket
            newRacket = new Racket(GameScreen, servSprite.NewSprite("racket", pGame));
            newRacket.Position = new Vector2(GameScreen.Width/2 - newRacket.Width/2,GameScreen.Height - newRacket.Height);

            //ball
            newBall = new Ball(GameScreen, servSprite.NewSprite("ball", pGame));
            newBall.SetPosition(newRacket.center - newBall.Width / 2, newRacket.Position.Y - newRacket.Height / 2 - newRacket.Height/2);
            newBall.Speed = new Vector2(6, -6);
            ballStick = true;

            //Boss
            lstBoss = new List<Boss>();

            newBoss = new Boss(pGame,"AB", 100, new int[,]
            {
                {0,0,0,0,0,0,0,0,0,0,0 },
                {0,1,1,1,1,1,1,1,1,1,0 },
                {0,1,1,1,1,1,1,1,1,1,0 },
                {0,1,1,1,1,0,1,1,1,1,0 },
                {0,1,1,1,0,0,0,1,1,1,0 },
                {0,1,1,0,0,2,0,0,1,1,0 },
                {0,1,1,1,0,0,0,1,1,1,0 },
                {0,1,1,1,1,0,1,1,1,1,0 },
                {0,1,1,1,1,1,1,1,1,1,0 },
                {0,1,1,1,1,1,1,1,1,1,0 }
            });
            
            
            lstBoss.Add(newBoss);
        }

        public override void Update()
        {
            Input();
            newRacket.Update();
            BallManager();
            BrickManager();
        }

        private void Input()
        {
            //IServiceInput servInput = ServiceLocator.GetService<IServiceInput>();
            //if (servInput.GetInputPressed(Keys.E))

            if (Keyboard.GetState().IsKeyDown(Keys.E))
            {
                foreach (var boss in lstBoss)
                {
                    boss.LooseHp(3);
                }
            }
        }

        private void BallManager()
        {
            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                if (ballStick)
                {
                    ballStick = false;
                }
            }

            if (ballStick)
            {
                newBall.SetPosition(newRacket.center - newBall.Width / 2, newRacket.Position.Y - newRacket.Height / 2 - newRacket.Height);
            }
            else
            {
                newBall.Update();
            }
            if (newRacket.CollideBox.Intersects(newBall.NextPositionY()))
            {
                if (newBall.Position.X <= newRacket.Position.X + newRacket.CollideBox.Width / 2)
                {
                    Debug.WriteLine("Hit Left !");
                }
                if (newBall.Position.X >= newRacket.Position.X + newRacket.CollideBox.Width / 2)
                {
                    Debug.WriteLine("Hit Right !");
                }
                if (newBall.Position.X == newRacket.Position.X + newRacket.CollideBox.Width / 2)
                {
                    Debug.WriteLine("Hit Center !");
                }

                Debug.WriteLine("BallX: " + newBall.Position.X + " RacketX: " + newRacket.Position.X + " RacketW: " + newRacket.Width + " TestRXW: " + (newRacket.Position.X + newRacket.Width));
                newBall.InverseSpeedY();
            }
            if (newRacket.CollideBox.Intersects(newBall.NextPositionX()))
            {
                //spBall.InverseSpeedX();
                ballStick = true;
            }
            if (newBall.Position.Y >= ScreenSize.Height)
            {
                ballStick = true;
            }
        }

        private void BrickManager()
        {
            foreach (var boss in lstBoss)
            {
                for (int i = boss.ListBrick.Count - 1; i >= 0; i--)
                {
                    bool colision = false;
                    Brick myBrick = boss.ListBrick[i];
                    myBrick.Update();
                    if (myBrick.BrickIsFalling == false)
                    {
                        if (myBrick.CollideBox.Intersects(newBall.NextPositionX()))
                        {
                            newBall.InverseSpeedX();
                            colision = true;
                        }
                        if (myBrick.CollideBox.Intersects(newBall.NextPositionY()))
                        {
                            newBall.InverseSpeedY();
                            colision = true;
                        }
                        if (colision)
                        {
                            boss.LooseHp(4);
                            myBrick.Fall();
                            CamShake = 30;
                        }
                    }
                    if (myBrick.Position.Y >= ScreenSize.Height)
                    {
                        boss.ListBrick.Remove(myBrick);
                    }
                }
            }
        }

        public override void Draw(SpriteBatch pBatch)
        {
            base.Draw(pBatch);
            pBatch.Begin();
            IServiceFont servFont = ServiceLocator.GetService<IServiceFont>();

            newRacket.Draw(pBatch);
            newBall.Draw(pBatch);

            foreach (var boss in lstBoss)
            {
                foreach (var brick in boss.ListBrick)
                {
                    brick.Draw(pBatch);
                }

                servFont.Print("Boss" + boss.Name, "Arial", new Vector2(GameScreen.Width / 2, 10), pBatch);
                servFont.Print("HP: " + boss.Life + " / " + boss.MaxLife, "", new Vector2(GameScreen.Width / 2, 30), pBatch);
                servFont.Print("IsDead: " + boss.IsDead.ToString(), "", new Vector2(GameScreen.Width / 2, 50), pBatch);
            }

            //pBatch.DrawString(fontMenu, "Score : " + servScore.Get(), new Vector2(10, 30), Color.White);

            pBatch.End();
        }
    }
}
