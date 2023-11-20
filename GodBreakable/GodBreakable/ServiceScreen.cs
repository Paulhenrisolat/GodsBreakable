using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodBreakable
{
    public interface IServiceScreen
    {
        Rectangle GetScreen(Game pGame);
    }

    public class ServiceScreen : IServiceScreen
    {
        Rectangle GameScreen;

        public ServiceScreen()
        {
            ServiceLocator.RegisterService<IServiceScreen>(this);
        }

        public Rectangle GetScreen(Game pGame)
        {
            GameScreen = pGame.Window.ClientBounds;
            return GameScreen;
        }
    }
}
