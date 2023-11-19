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
        void Print(string text, string fontName, Vector2 PosXY, SpriteBatch pBatch);
    }

    public class ServiceFont : IServiceFont
    {
        protected Game game;
        SpriteFont font;

        public ServiceFont(Game pGame)
        {
            game = pGame;
            ServiceLocator.RegisterService<IServiceFont>(this);
        }

        public void Print(string text, string fontName, Vector2 PosXY, SpriteBatch pBatch)
        {   
            if (fontName == "Arial")
            {
                font = game.Content.Load<SpriteFont>("Default");
            }
            else
            {
                font = game.Content.Load<SpriteFont>("Default");
            }

            pBatch.DrawString(font, text, PosXY, Color.White);
        }
    }
}
