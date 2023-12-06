using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodBreakable.Services
{
    public interface IServiceDebug
    {
        bool DebugIsOn();
        void ActivateDebug();
    }

    public class ServiceDebug : IServiceDebug
    {
        bool debugIsOn;

        public ServiceDebug()
        {
            debugIsOn = false;
            ServiceLocator.RegisterService<IServiceDebug>(this);
        }

        public bool DebugIsOn()
        {
            return debugIsOn;
        }

        public void ActivateDebug()
        {
            if (debugIsOn == false)
            {
                debugIsOn = true;
            }
            //else
            //{
            //    debugIsOn = false;
            //}
        }
    }
}
