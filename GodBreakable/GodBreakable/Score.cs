using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodBreakable
{
    public interface IServiceScore
    {
        void Add(int pPoints);
        int Get();
    }

    class Score : IServiceScore
    {
        private int Value;
        //SpriteFont fontMenu;
        public Score()
        {
            Value = 0;
            ServiceLocator.RegisterService<IServiceScore>(this);
        }

        public void Add(int pPoints)
        {
            Value += pPoints;
        }

        public int Get()
        {
            return Value;
        }

        public void Display()//SpriteBatch pBatch)
        {
            Debug.WriteLine(Value);
            //pBatch.DrawString(fontMenu, "Score : " + Get(), new Vector2(10, 30), Color.White);
        }
    }
}
