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
        private string BossCore { get; set; }
        public float Life { get; private set; }
        public float MaxLife { get; private set; }
        public bool SecondPhase { get { if (Life <= MaxLife/2) { return true; } else { return false; } } set { } }
        public bool IsDead { get { if (Life <= 0) { return true; } else { return false; } } set { } }
        public int[,] Level { get; set; }
        public List<Brick> ListBrick { get; private set; }
        public bool CanChangeMusic { get; set; }
        public bool isSelected { get; set; }

        public Boss(string bossName,string bossCore, float bossHp, int[,] bossLevel) 
        {
            //Boss Property
            Name = bossName;
            BossCore = bossCore;
            MaxLife = bossHp;
            Life = MaxLife;
            Level = bossLevel;
            CanChangeMusic = true;
            isSelected = false;

            //Generate Boss
            ListBrick = new List<Brick>();
            IServiceSprite servSprite = ServiceLocator.GetService<IServiceSprite>();
            IServiceScreen servScreen = ServiceLocator.GetService<IServiceScreen>();
            Texture2D texBrickNormal = servSprite.NewSprite("img/brick1");
            Texture2D texBrickHard = servSprite.NewSprite("img/brick2");
            Texture2D texBrickWeak = servSprite.NewSprite("img/brick3");
            Texture2D texBrickWeapon = servSprite.NewSprite("img/brick4");
            Texture2D texBricCore = servSprite.NewSprite(BossCore);
            for (int l = 0; l < Level.GetLength(0); l++)
            {
                for (int c = 0; c < Level.GetLength(1); c++)
                {
                    if (Level[l, c] == 1)
                    {
                        Brick myBrick = new Brick(servScreen.GetScreen(), texBrickNormal, "Normal",1);
                        myBrick.SetPosition(c * texBrickNormal.Width, l * texBrickNormal.Height);
                        ListBrick.Add(myBrick);
                    }
                    if (Level[l, c] == 2)
                    {
                        Brick myBrick = new Brick(servScreen.GetScreen(), texBrickHard, "Hard", 2);
                        myBrick.SetPosition(c * texBrickHard.Width, l * texBrickHard.Height);
                        ListBrick.Add(myBrick);
                    }
                    if (Level[l, c] == 3)
                    {
                        Brick myBrick = new Brick(servScreen.GetScreen(), texBrickWeak, "Weak", 1);
                        myBrick.SetPosition(c * texBrickWeak.Width, l * texBrickWeak.Height);
                        ListBrick.Add(myBrick);
                    }
                    if (Level[l, c] == 4)
                    {
                        Brick myBrick = new Brick(servScreen.GetScreen(), texBrickWeapon, "Weapon",2);
                        myBrick.SetPosition(c * texBrickWeapon.Width, l * texBrickWeapon.Height);
                        ListBrick.Add(myBrick);
                    }
                    if (Level[l, c] == 5)
                    {
                        Brick myBrick = new Brick(servScreen.GetScreen(), texBricCore, "Core",1000);
                        myBrick.SetPosition(c * texBricCore.Width, l * texBricCore.Height);
                        ListBrick.Add(myBrick);
                    }
                }
            }
        }

        public void LoseHp(int damage)
        {
            Life -= damage;
        }
    }
}
