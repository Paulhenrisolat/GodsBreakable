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
        Texture2D NewSprite(string spriteName, Game thisGame);
    }

    public class ServiceSprite : IServiceSprite
    {

        public ServiceSprite()
        {
            ServiceLocator.RegisterService<IServiceSprite>(this);
        }

        public Texture2D NewSprite(string spriteName, Game thisGame)
        {
            return thisGame.Content.Load<Texture2D>(spriteName);
        }
    }
}
