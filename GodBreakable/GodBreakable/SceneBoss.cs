using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
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
        Player player;
        Timer timer;
        LifeBar bossLifebar;
        private List<Shoot> lstShoot;
        Game ActualGame;
        private Texture2D textPlayerUI;

        public SceneBoss(Game pGame) : base(pGame)
        {
            //base.SceneSong = game.Content.Load<Song>("music/OpenYourEyes-part2");
            //Game
            ActualGame = pGame;

            //Services
            IServiceSprite servSprite = ServiceLocator.GetService<IServiceSprite>();
            IServiceScreen servScreen = ServiceLocator.GetService<IServiceScreen>();
            GameScreen = servScreen.GetScreen(pGame);

            //Player
            player = new Player(100);
            textPlayerUI = game.Content.Load<Texture2D>("img/playerUI");

            //Racket
            newRacket = new Racket(GameScreen, servSprite.NewSprite("img/racket", pGame));
            newRacket.Position = new Vector2(GameScreen.Width/2 - newRacket.Width/2,GameScreen.Height - newRacket.Height - textPlayerUI.Height / 5);

            //ball
            newBall = new Ball(GameScreen, servSprite.NewSprite("img/ball", pGame));
            newBall.SetPosition(newRacket.center - newBall.Width / 2, newRacket.Position.Y - newRacket.Height / 2 - newRacket.Height/2);
            newBall.Speed = new Vector2(6, -6);
            ballStick = true;

            //Timer
            timer = new Timer(10);

            //LifeBar
            bossLifebar = new LifeBar(GameScreen, servSprite.NewSprite("img/barfull", pGame),pGame);
            bossLifebar.SetPosition(GameScreen.Width / 2 - bossLifebar.Width / 2,10);

            //Shoot
            lstShoot = new List<Shoot>();

            //Boss
            lstBoss = new List<Boss>();

            newBoss = new Boss(pGame,"AB","img/brick5v2", 100f, new int[,]
            {
                {0,0,0,0,0,0,0,0,0,0,0 },
                {0,0,0,0,0,0,0,0,0,0,0 },
                {0,0,0,0,0,0,0,0,0,0,0 },
                {0,0,1,1,1,1,1,1,1,0,0 },
                {0,1,1,1,2,4,2,1,1,1,0 },
                {0,1,1,1,0,0,0,1,1,1,0 },
                {4,2,2,0,0,5,0,0,2,2,4 },
                {0,1,1,1,0,0,0,1,1,1,0 },
                {0,1,1,1,1,0,1,1,1,1,0 },
                {0,1,3,1,1,1,1,1,3,1,0 },
                {0,1,1,1,2,2,2,1,1,1,0 },
                {0,1,1,1,2,1,2,1,1,1,0 },
                {0,0,1,1,2,1,2,1,1,0,0 },
            });
            
            
            lstBoss.Add(newBoss);
        }

        public override void Update(GameTime gameTime)
        {
            //base.SceneSong = ActualGame.Content.Load<Song>("music/OpenYourEyes-part2");
            timer.Update(gameTime);
            Input();
            newRacket.Update();
            BallManager();
            BrickManager();
            bossLifebar.LifeManager(newBoss.Life);
            ProjectileManager();
        }

        private void Input()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.E))
            {
                foreach (var boss in lstBoss)
                {
                    boss.LoseHp(3);
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
            if (ballStick == false) 
            {
                newBall.Update();
                //if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                //{
                //    if (ballStick)
                //    {
                //        ballStick = false;
                //    }
                //}
            }
            else
            {
                
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
            if (newBall.Position.Y >= ScreenSize.Height - textPlayerUI.Height / 5 - newBall.Height)
            {
                ballStick = true;
            }
        }

        private void BrickManager()
        {
            IServiceSprite servSprite = ServiceLocator.GetService<IServiceSprite>();
            foreach (var boss in lstBoss)
            {
                for (int i = boss.ListBrick.Count - 1; i >= 0; i--)
                {
                    bool colision = false;
                    Brick myBrick = boss.ListBrick[i];
                    myBrick.Update();
                    //myBrick.Speed = new Vector2(1f, 0);
                    if ( myBrick.BrickType=="Weapon" && timer.CanDo == true)
                    {
                        Shoot("img/blastv2", new Vector2(myBrick.Position.X + myBrick.Width/2, myBrick.Position.Y), new Vector2(0f,5f), 10);
                    }

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
                            if (myBrick.BrickType == "Weak")
                            {
                                boss.LoseHp(4);
                            }
                            if (myBrick.BrickType == "Normal")
                            {
                                boss.LoseHp(2);
                            }
                            if (myBrick.BrickType == "Hard")
                            {
                                boss.LoseHp(1);
                            }
                            if (myBrick.BrickType == "Core")
                            {
                                boss.LoseHp(8);
                                CamShake = 100;
                            }

                            if (myBrick.BrickType != "Core")
                            {
                                myBrick.Fall();
                                CamShake = 30;
                            }
                        }
                    }
                    if (myBrick.BrickType == "Core" && boss.IsDead)
                    {
                        myBrick.Fall();
                    }
                    if (myBrick.Position.Y >= ScreenSize.Height - textPlayerUI.Height / 5)
                    {
                        boss.ListBrick.Remove(myBrick);
                    }
                }
            }
        }

        private void Shoot(string TextName, Vector2 Position, Vector2 Speed, int Damage)
        {
            IServiceSprite servSprite = ServiceLocator.GetService<IServiceSprite>();

            Shoot newProjectile = new Shoot(GameScreen, servSprite.NewSprite(TextName, ActualGame), Position, Speed, Damage);
            lstShoot.Add(newProjectile);
        }

        private void ProjectileManager()
        {
            for (int i = lstShoot.Count - 1; i >= 0; i--)
            {
                Shoot theProjectile = lstShoot[i];
                if (theProjectile.Position.Y >= ScreenSize.Height - textPlayerUI.Height / 5 - theProjectile.Height)
                {
                    lstShoot.Remove(theProjectile);
                    player.LoseHp(theProjectile.Damage);
                }
                if (newRacket.CollideBox.Intersects(theProjectile.NextPositionY()))
                {
                    lstShoot.Remove(theProjectile);
                }
                if (newRacket.CollideBox.Intersects(theProjectile.NextPositionX()))
                {
                    lstShoot.Remove(theProjectile);
                }
                else
                {
                    theProjectile.Update();
                }
            }
        }

        public override void Draw(SpriteBatch pBatch)
        {
            IServiceFont servFont = ServiceLocator.GetService<IServiceFont>();
            base.Draw(pBatch);
            pBatch.Begin();

            pBatch.Draw(textPlayerUI, new Vector2(0, GameScreen.Height - textPlayerUI.Height/5), Color.White);

            bossLifebar.Draw(pBatch);
            newRacket.Draw(pBatch);
            newBall.Draw(pBatch);

            foreach (var boss in lstBoss)
            {
                foreach (var brick in boss.ListBrick)
                {
                    brick.Draw(pBatch);
                    if (brick.BrickType == "Weapon")
                    {
                        servFont.Print(Math.Floor(timer.Time).ToString(), "", new Vector2(brick.Position.X + brick.Width/ 2, brick.Position.Y - brick.Height), pBatch);
                    }
                }
                servFont.Print("Boss" + boss.Name, "Arial", new Vector2(GameScreen.Width / 2, 10), pBatch);
                servFont.Print(boss.Life + " / " + boss.MaxLife +" %"+boss.Life/100, "", new Vector2(bossLifebar.Position.X + bossLifebar.Width/2, bossLifebar.Position.Y +20), pBatch);
                //servFont.Print("IsDead: " + boss.IsDead.ToString(), "", new Vector2(GameScreen.Width / 2, 50), pBatch);
            }

            foreach (var projectile in lstShoot)
            {
                projectile.Draw(pBatch);
            }
            
            servFont.Print("Player HP: " + player.PlayerHp + " / " + player.PlayerMaxHp, "", new Vector2(GameScreen.Width / 2, GameScreen.Height - 20), pBatch);

            pBatch.End();
        }
    }
}
