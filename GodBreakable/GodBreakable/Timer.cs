using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodBreakable
{
    public class Timer
    {
        public double Time;
        public double TimeDefault;
        public bool CanDo;
        public Timer(double time) 
        {
            TimeDefault = time;
            Time = time;
            CanDo = false;
        }

        public void Update(GameTime gameTime) 
        {
            if (Time > 0)
            {
                Time -= gameTime.ElapsedGameTime.TotalSeconds;
                CanDo = false;
            }
            else
            {
                CanDo = true;
                Time = TimeDefault;
            }
            
        }
    }
}
