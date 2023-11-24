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
        Rectangle GetScreen();
    }

    public class ServiceScreen : IServiceScreen
    {
        Rectangle GameScreen;
        protected Game game;

        public ServiceScreen(Game pGame)
        {
            game = pGame;
            ServiceLocator.RegisterService<IServiceScreen>(this);
        }

        public Rectangle GetScreen()
        {
            GameScreen = game.Window.ClientBounds;
            return GameScreen;
        }
    }
}
