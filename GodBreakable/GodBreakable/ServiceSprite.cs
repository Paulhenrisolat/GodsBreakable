using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodBreakable
{
    public interface IServiceSprite
    {
        Texture2D NewSprite(string spriteName);
    }

    public class ServiceSprite : IServiceSprite
    {
        protected Game game;

        public ServiceSprite(Game pGame)
        {
            game = pGame;
            ServiceLocator.RegisterService<IServiceSprite>(this);
        }

        public Texture2D NewSprite(string spriteName)
        {
            return game.Content.Load<Texture2D>(spriteName);
        }
    }
}
