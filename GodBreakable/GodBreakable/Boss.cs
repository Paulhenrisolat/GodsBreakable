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
        private string name;

        public Boss(string bossName, int bossHp) 
        {
            name = bossName;

        }

        public void LooseHp(int damage)
        {

        }
    }
}
