using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodBreakable
{
    class Player
    {
        public int PlayerHp { get; set; }
        public int PlayerMaxHp { get; set; }
        public bool IsDead { get { if (PlayerHp <= 0) { return true; } else { return false; } } set { } }

        public Player(int playerHp)
        {
            PlayerHp = playerHp;
            PlayerMaxHp = PlayerHp;
        }

        public void LoseHp(int damage)
        {
            PlayerHp -= damage;
        }
    }
}
