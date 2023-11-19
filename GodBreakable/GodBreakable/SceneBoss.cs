﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
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
            if (Keyboard.GetState().IsKeyDown(Keys.E))
            {
                foreach (var boss in lstBoss)
                {
                    boss.LooseHp(3);
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
