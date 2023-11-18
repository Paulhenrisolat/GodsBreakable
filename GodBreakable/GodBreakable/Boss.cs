using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodBreakable
{
    class Boss
    {
        public string Name { get; set; }
        public int Life { get; set; }
        public int MaxLife { get; private set; }
        public bool SecondPhase { get { if (Life <= MaxLife/2) { return true; } else { return false; } } set { } }
        public bool IsDead { get { if (Life <= 0) { return true; } else { return false; } } set { } }
        public int[,] Level { get; set; }
        public List<Brick> ListBrick { get; private set; }

        public Boss(Game pGame, string bossName, int bossHp, int[,] bossLevel) 
        {
            //Boss Property
            Name = bossName;
            Life = bossHp;
            MaxLife = Life;
            Level = bossLevel;

            //Generate Boss
            ListBrick = new List<Brick>();
            IServiceSprite servSprite = ServiceLocator.GetService<IServiceSprite>();
            IServiceScreen servScreen = ServiceLocator.GetService<IServiceScreen>();
            Texture2D texBrick = servSprite.NewSprite("brick1", pGame);
            Texture2D texBrickB = servSprite.NewSprite("brick2", pGame);
            for (int l = 0; l < Level.GetLength(0); l++)
            {
                for (int c = 0; c < Level.GetLength(1); c++)
                {
                    if (Level[l, c] == 1)
                    {
                        Brick myBrick = new Brick(servScreen.GetScreen(pGame), texBrick);
                        myBrick.SetPosition(c * texBrick.Width, l * texBrick.Height);
                        ListBrick.Add(myBrick);
                    }
                    if (Level[l, c] == 2)
                    {
                        Brick myBrick = new Brick(servScreen.GetScreen(pGame), texBrickB);
                        myBrick.SetPosition(c * texBrickB.Width, l * texBrickB.Height);
                        ListBrick.Add(myBrick);
                    }
                }
            }
        }

        public void LooseHp(int damage)
        {
            Life -= damage;
        }
    }
}
