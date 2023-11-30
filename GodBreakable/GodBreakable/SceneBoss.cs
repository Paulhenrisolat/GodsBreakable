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
        private Boss newBoss;
        private List<Boss> lstBoss;
        private string SelectedBoss;
        private Rectangle GameScreen;
        private Racket newRacket;
        private Ball newBall;
        bool ballStick;
        private Player player;
        private Timer timer;
        private LifeBar bossLifebar;
        private List<Shoot> lstShoot;
        private Game ActualGame;
        private Texture2D textPlayerUI;
        private Window PauseWindow;
        private bool BossAsBeenSelected;

        public SceneBoss(Game pGame, string sceneName, Boss SelectedBoss) : base(pGame, sceneName)
        {
            //Game
            ActualGame = pGame;
            BossAsBeenSelected = false;

            //Button
            

            //Music
            //servSound.PlayMusic("OpenYourEyes-part2");

            GameScreen = serviceScreen.GetScreen();

            //Player
            player = new Player(100);
            textPlayerUI = game.Content.Load<Texture2D>("img/playerUI");

            //Racket
            newRacket = new Racket(GameScreen, serviceSprite.NewSprite("img/racket"));
            newRacket.Position = new Vector2(GameScreen.Width/2 - newRacket.Width/2,GameScreen.Height - newRacket.Height - textPlayerUI.Height / 5);

            //ball
            newBall = new Ball(GameScreen, serviceSprite.NewSprite("img/ball"));
            newBall.SetPosition(newRacket.center - newBall.Width / 2, newRacket.Position.Y - newRacket.Height / 2 - newRacket.Height/2);
            newBall.Speed = new Vector2(6, -6);
            ballStick = true;

            //Timer
            timer = new Timer(10);

            //Shoot
            lstShoot = new List<Shoot>();

            //Pause
            PauseWindow = new Window("Pause");

            //Boss
            lstBoss = new List<Boss>();

            newBoss = SelectedBoss;
            newBoss.isSelected = true;
            lstBoss.Add(newBoss);

            //LifeBar
            bossLifebar = new LifeBar(GameScreen, serviceSprite.NewSprite("img/barfull"), pGame);
            bossLifebar.SetPosition(GameScreen.Width / 2 - bossLifebar.Width / 2, 10);
        }

        public override void Update(GameTime gameTime)
        {
            Input();
            timer.Update(gameTime);
            newRacket.Update();
            BallManager();
            BrickManager();
            bossLifebar.LifeManager(newBoss.Life, newBoss.MaxLife);
            ProjectileManager();

            if(PauseWindow.windowIsOpen == true)
            {
                PauseWindow.Update();
            }
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

            if (Keyboard.GetState().IsKeyDown(Keys.I))
            {
                serviceSound.PlaySound("bump");
                PauseWindow.OpenWindow(PauseWindow.windowIsOpen);
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
            PauseWindow.Update();
            foreach (var boss in lstBoss)
            {
                if(boss.isSelected == true)
                {
                    for (int i = boss.ListBrick.Count - 1; i >= 0; i--)
                    {
                        if (boss.SecondPhase == true && boss.CanChangeMusic == true)
                        {
                            serviceSound.StopMusic();
                            serviceSound.PlayMusic("OpenYourEyes-part2");
                            timer = new Timer(2);
                            boss.CanChangeMusic = false;
                        }
                        bool colision = false;
                        Brick myBrick = boss.ListBrick[i];
                        myBrick.Update();
                        //myBrick.Speed = new Vector2(1f, 0);
                        if (myBrick.BrickType == "Weapon" && timer.CanDo == true)
                        {
                            Shoot("img/blastv2", new Vector2(myBrick.Position.X + myBrick.Width / 2, myBrick.Position.Y), new Vector2(0f, 5f), 10);
                            serviceSound.PlaySound("lightning");
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
                                serviceSound.PlaySound("bump");
                                myBrick.TakeHit();
                                if (myBrick.BrickHP <= 0 && myBrick.BrickType != "Core")
                                {
                                    //myBrick.Fall();
                                    serviceSound.PlaySound("brickxplode");
                                    boss.ListBrick.Remove(myBrick);
                                }
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
        }

        private void Shoot(string TextName, Vector2 Position, Vector2 Speed, int Damage)
        {
            Shoot newProjectile = new Shoot(GameScreen, serviceSprite.NewSprite(TextName), Position, Speed, Damage);
            lstShoot.Add(newProjectile);
            newProjectile.SetPosition(new Vector2(newProjectile.Position.X - newProjectile.Width /2, Position.Y));
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
            base.Draw(pBatch);
            pBatch.Begin();

            pBatch.Draw(textPlayerUI, new Vector2(0, GameScreen.Height - textPlayerUI.Height/5), Color.White);

            bossLifebar.Draw(pBatch);
            newRacket.Draw(pBatch);
            newBall.Draw(pBatch);
            //newBtn.Draw(pBatch);

            foreach (var boss in lstBoss)
            {
                if(boss.isSelected == true)
                {
                    foreach (var brick in boss.ListBrick)
                    {
                        brick.Draw(pBatch);
                        if (brick.BrickType == "Weapon")
                        {
                            serviceFont.Print(Math.Floor(timer.Time).ToString(), "", new Vector2(brick.Position.X + brick.Width / 2, brick.Position.Y - brick.Height), pBatch);
                        }
                    }
                    serviceFont.Print("Boss" + boss.Name, "Arial", new Vector2(GameScreen.Width / 2, 10), pBatch);
                    serviceFont.Print(boss.Life + " / " + boss.MaxLife, "", new Vector2(bossLifebar.Position.X + bossLifebar.Width / 2, bossLifebar.Position.Y + 20), pBatch);
                }
            }

            foreach (var projectile in lstShoot)
            {
                projectile.Draw(pBatch);
            }

            serviceFont.Print("Player HP: " + player.PlayerHp + " / " + player.PlayerMaxHp, "", new Vector2(GameScreen.Width / 2, GameScreen.Height - 20), pBatch);
            
            PauseWindow.Draw(pBatch);
            
            pBatch.End();
        }
    }
}
