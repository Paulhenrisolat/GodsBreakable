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
    public class SceneBoss : Scene
    {
        Boss newBoss;
        private List<Boss> lstBoss;
        private string SelectedBoss;
        private Rectangle GameScreen;
        SpriteFont Arial;
        private readonly ServiceScreen ServiceScreen = new ServiceScreen();

        public SceneBoss(Game pGame) : base(pGame)
        {
            lstBoss = new List<Boss>();
            newBoss = new Boss("AB", 100);
            lstBoss.Add(newBoss);

            Arial = game.Content.Load<SpriteFont>("Default");

            IServiceScreen servScreen = ServiceLocator.GetService<IServiceScreen>();
            GameScreen = servScreen.GetScreen(pGame);
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

            foreach (var boss in lstBoss)
            {
                pBatch.DrawString(Arial, "Boss: " + boss.Name, new Vector2(GameScreen.Width/2, 10), Color.White);
                pBatch.DrawString(Arial, "HP: " + boss.Life  + " / " +  boss.MaxLife, new Vector2(GameScreen.Width/2, 30), Color.White);
                pBatch.DrawString(Arial, "IsDead: " + boss.IsDead.ToString(), new Vector2(GameScreen.Width/2, 50), Color.White);
            }

            pBatch.End();
        }
    }
}
