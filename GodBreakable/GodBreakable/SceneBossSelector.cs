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
        private List<string> lstBossTitle;
        private string BossTitleSelected;
        private int indexBossSelected;

        public SceneBossSelector(Game pGame) : base(pGame)
        {
            //Button
            BtnSelectLeft = new Button(serviceScreen.GetScreen(), serviceSprite.NewSprite("img/leftArrow"), "");
            BtnSelectLeft.SetPosition(serviceScreen.GetScreen().Width / 2 - BtnSelectLeft.Width / 2, serviceScreen.GetScreen().Height / 2 - BtnSelectLeft.Height / 2);
            BtnSelectRight = new Button(serviceScreen.GetScreen(), serviceSprite.NewSprite("img/rightArrow"), "");
            BtnSelectRight.SetPosition(serviceScreen.GetScreen().Width / 2 - BtnSelectRight.Width / 2, serviceScreen.GetScreen().Height / 2 - BtnSelectRight.Height / 2);

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
                new Boss("OVA", "img/brick5v2", 500f, new int[,]
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

            indexBossSelected = 1;
            BossTitleSelected = lstBossTitle[indexBossSelected];
        }

        public override void Update(GameTime gameTime)
        {
            BtnSelectLeft.Update();
            BtnSelectLeft.SetPosition(serviceScreen.GetScreen().Width / 2 - BtnSelectLeft.Width / 2 - BossTitleSelected.Length * 20, serviceScreen.GetScreen().Height / 2 - BtnSelectLeft.Height / 2);
            BtnSelectRight.Update();
            BtnSelectRight.SetPosition(serviceScreen.GetScreen().Width / 2 - BtnSelectRight.Width / 2 + BossTitleSelected.Length * 20, serviceScreen.GetScreen().Height / 2 - BtnSelectRight.Height / 2);
            
            if (BtnSelectRight.IsClicked && indexBossSelected < lstBossTitle.Count - 1 || Keyboard.GetState().IsKeyDown(Keys.Right) && indexBossSelected < lstBossTitle.Count - 1)
            {
                indexBossSelected = indexBossSelected + 1;

            }
            if (BtnSelectLeft.IsClicked && indexBossSelected > 0 || Keyboard.GetState().IsKeyDown(Keys.Left) && indexBossSelected > 0)
            {
                indexBossSelected = indexBossSelected - 1;
            }
            //if (Keyboard.GetState().IsKeyDown(Keys.Left) && indexBossSelected > 0)
            //{
            //    indexBossSelected = indexBossSelected - 1;
            //}
            BossTitleSelected = lstBossTitle[indexBossSelected];
        }

        public override void Draw(SpriteBatch pBatch)
        {
            base.Draw(pBatch);
            pBatch.Begin();

            serviceFont.Print(BossTitleSelected, "", new Vector2(serviceScreen.GetScreen().Width/2 - BossTitleSelected.Length * 5, serviceScreen.GetScreen().Height/2), pBatch);
            serviceFont.Print("Boss Selected : "+ indexBossSelected, "", new Vector2(serviceScreen.GetScreen().Width/2, serviceScreen.GetScreen().Height/2 - 50), pBatch);

            BtnSelectLeft.Draw(pBatch);
            BtnSelectRight.Draw(pBatch);

            pBatch.End();
        }
    }
}
