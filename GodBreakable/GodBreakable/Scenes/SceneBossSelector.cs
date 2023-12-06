using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodBreakable
{
    public class SceneBossSelector : Scene
    {
        private List<Boss> lstBoss;
        private Button BtnSelectLeft;
        private Button BtnSelectRight;
        private Button BtnPlay;
        private List<string> lstBossTitle;
        private string BossTitleSelected;
        private int indexBossSelected;
        private KeyboardState oldstate;

        public SceneBossSelector(Game pGame, string sceneName) : base(pGame, sceneName)
        {
            //Button
            BtnSelectLeft = new Button(serviceScreen.GetScreen(), serviceSprite.NewSprite("img/leftArrow"), "");
            BtnSelectLeft.SetPosition(serviceScreen.GetScreen().Width / 2 - BtnSelectLeft.Width / 2, serviceScreen.GetScreen().Height / 2 - BtnSelectLeft.Height / 2);
            BtnSelectRight = new Button(serviceScreen.GetScreen(), serviceSprite.NewSprite("img/rightArrow"), "");
            BtnSelectRight.SetPosition(serviceScreen.GetScreen().Width / 2 - BtnSelectRight.Width / 2, serviceScreen.GetScreen().Height / 2 - BtnSelectRight.Height / 2);
            BtnPlay = new Button(serviceScreen.GetScreen(), serviceSprite.NewSprite("img/play"), "");
            BtnPlay.SetPosition(serviceScreen.GetScreen().Width / 2 - BtnPlay.Width / 2, serviceScreen.GetScreen().Height / 2 - BtnPlay.Height / 2 + 180);

            //Background
            base.textBackground = serviceSprite.NewSprite("img/selector");

            //Boss
            lstBoss = new List<Boss>
            {
                new Boss("AB", "img/brick5v2", 100f, new int[,]
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
                }),
                new Boss("SECRET", "img/brick5v2", 100000f, new int[,]
                {
                {0,0,0,0,0,0,0,0,0,0,0 },
                {0,0,0,0,0,0,0,0,0,0,0 },
                {0,0,0,0,0,0,0,0,0,0,0 },
                {4,4,4,4,4,4,4,4,4,4,4 },
                {0,1,1,1,2,4,2,1,1,1,0 },
                {0,1,1,1,0,0,0,1,1,1,0 },
                {4,2,2,0,0,5,0,0,2,2,4 },
                {0,1,1,1,0,0,0,1,1,1,0 },
                {0,1,1,1,1,0,1,1,1,1,0 },
                {0,1,3,1,1,1,1,1,3,1,0 },
                {0,1,1,1,2,2,2,1,1,1,0 },
                {0,1,1,1,2,1,2,1,1,1,0 },
                {0,0,1,1,2,1,2,1,1,0,0 },
                }),
                new Boss("OVA", "img/ova", 1000f, new int[,]
                {
                {0,0,0,0,0,0,0,0,0,0,0 },
                {0,0,0,0,0,0,0,0,0,0,0 },
                {0,0,0,0,0,0,0,0,0,0,0 },
                {0,0,0,1,3,3,3,1,0,0,0 },
                {0,1,0,1,2,1,2,1,0,1,0 },
                {0,1,0,0,0,1,0,0,0,1,0 },
                {0,2,2,0,1,5,1,0,2,2,0 },
                {0,1,1,1,0,1,0,1,1,1,0 },
                {0,1,1,1,1,0,1,1,1,1,0 },
                {0,1,3,1,1,1,1,1,3,1,0 },
                {0,1,1,1,2,2,2,1,1,1,0 },
                {0,1,1,1,2,0,2,1,1,1,0 },
                {0,0,1,1,0,0,0,1,1,0,0 },
                })
            };

            //BossTitle
            lstBossTitle = new List<string>();

            foreach (Boss boss in lstBoss)
            {
                lstBossTitle.Add(boss.Name);
            }

            indexBossSelected = 0;
            BossTitleSelected = lstBossTitle[indexBossSelected];
        }

        public override void Update(GameTime gameTime)
        {
            BtnSelectLeft.Update();
            BtnSelectLeft.SetPosition(serviceScreen.GetScreen().Width / 2 - BtnSelectLeft.Width / 2 - BossTitleSelected.Length * 20, serviceScreen.GetScreen().Height / 2 - BtnSelectLeft.Height / 2);
            BtnSelectRight.Update();
            BtnSelectRight.SetPosition(serviceScreen.GetScreen().Width / 2 - BtnSelectRight.Width / 2 + BossTitleSelected.Length * 20, serviceScreen.GetScreen().Height / 2 - BtnSelectRight.Height / 2);
            BtnPlay.Update();

            KeyboardState newState = Keyboard.GetState();

            if (BtnSelectRight.IsClicked || oldstate.IsKeyUp(Keys.Right) && newState.IsKeyDown(Keys.Right))
            {
                indexBossSelected = indexBossSelected + 1;
                if(indexBossSelected > lstBoss.Count-1) 
                {
                    indexBossSelected = 0;
                }
            }
            if (BtnSelectLeft.IsClicked || oldstate.IsKeyUp(Keys.Left) && newState.IsKeyDown(Keys.Left))
            {
                indexBossSelected = indexBossSelected - 1;
                if (indexBossSelected < 0)
                {
                    indexBossSelected = lstBoss.Count-1;
                }
            }

            oldstate = newState;
            //if (BtnSelectLeft.IsClicked && indexBossSelected > 0 || Keyboard.GetState().IsKeyDown(Keys.Left) && indexBossSelected > 0)
            //{
            //    indexBossSelected = indexBossSelected - 1;
            //}
            if (BtnPlay.IsClicked || Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                SceneManager.ChargeBoss(lstBoss[indexBossSelected]);
            }

            BossTitleSelected = lstBossTitle[indexBossSelected];
        }

        public override void Draw(SpriteBatch pBatch)
        {
            base.Draw(pBatch);
            pBatch.Begin();

            serviceFont.Print(BossTitleSelected, "Aldot", new Vector2(serviceScreen.GetScreen().Width/2 - BossTitleSelected.Length * 5, serviceScreen.GetScreen().Height/2), pBatch);
            //serviceFont.Print("Boss Selected : "+ indexBossSelected, "", new Vector2(serviceScreen.GetScreen().Width/2 - BossTitleSelected.Length * 5, serviceScreen.GetScreen().Height/2 - 50), pBatch);
            pBatch.Draw(serviceSprite.NewSprite(lstBoss[indexBossSelected].BossCore), new Vector2(serviceScreen.GetScreen().Width / 2 - 50, serviceScreen.GetScreen().Height / 2 - 160), Color.White);

            BtnSelectLeft.Draw(pBatch);
            BtnSelectRight.Draw(pBatch);
            BtnPlay.Draw(pBatch);

            pBatch.End();
        }
    }
}
