using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodBreakable
{
    public interface IServiceFont
    {
        void Print(string text, Vector2 PosXY, SpriteBatch pBatch);
    }

    public class ServiceFont : IServiceFont
    {
        protected Game game;
        SpriteFont fontArial;

        public ServiceFont(Game pGame)
        {
            game = pGame;
            ServiceLocator.RegisterService<IServiceFont>(this);
        }

        public void Print(string text, Vector2 PosXY, SpriteBatch pBatch)
        {
            fontArial = game.Content.Load<SpriteFont>("Default");
            pBatch.DrawString(fontArial, text, PosXY, Color.White);
        }
    }
}
