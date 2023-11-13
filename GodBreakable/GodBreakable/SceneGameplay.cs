using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace GodBreakable
{
    public class SceneGameplay : Scene
    {
        Racket spRaquette;
        Ball spBall;
        private bool ballStick;
        const int NbColonnes = 11;
        const int NbLines = 10;
        private int[,] Level;
        private List<Brick> lstBrick;
        SpriteFont fontMenu;
        private readonly Score Score = new Score();

        public SceneGameplay(Game pGame) : base(pGame)
        {
            //Contexte.life = 5;
            Rectangle Screen = ScreenSize;
            fontMenu = game.Content.Load<SpriteFont>("Default");
            //raquette
            spRaquette = new Racket(game.Content.Load<Texture2D>("racket"), Screen);
            spRaquette.SetPosition(Screen.Width / 2 - spRaquette.Width / 2, Screen.Height - spRaquette.Height);
            //spRaquette.Speed = new Vector2(2,0);

            //ball
            spBall = new Ball(game.Content.Load<Texture2D>("ball"), Screen);
            spBall.SetPosition(spRaquette.Position.X + spBall.Width / 2, spRaquette.Position.Y - spBall.Width);
            spBall.Speed = new Vector2(6, -6);
            ballStick = true;

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
            Texture2D texBrick = game.Content.Load<Texture2D>("brick1");
            for (int l = 0; l < Level.GetLength(0); l++)
            {
                for (int c = 0; c < Level.GetLength(1); c++)
                {
                    if (Level[l, c] == 1)
                    {
                        Brick myBrick = new Brick(texBrick, ScreenSize);
                        myBrick.SetPosition(c * texBrick.Width, l * texBrick.Height);
                        lstBrick.Add(myBrick);
                    }
                }
            }
        }

        public override void Update()
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
                spBall.InverseSpeedY();
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

            IServiceScore servScore = ServiceLocator.GetService<IServiceScore>();
            if (servScore != null)
            {
                pBatch.DrawString(fontMenu, "Score : " + servScore.Get(), new Vector2(10, 30), Color.White);
            }

            pBatch.End();
        }
    }
}
