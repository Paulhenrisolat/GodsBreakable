using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodBreakable
{
    class Boss
    {
        public bool secondPhase { get; set; }
        public string Name { get; set; }
        public int Life { get; set; }
        public int MaxLife { get; set; }
        public bool IsDead { get { if (Life <= 0) { return true; } else { return false; } } set { } }

        public Boss(string bossName, int bossHp) 
        {
            Name = bossName;
            Life = bossHp;
            MaxLife = Life;
        }

        public void LooseHp(int damage)
        {
            Life -= damage;
        }
    }
}
