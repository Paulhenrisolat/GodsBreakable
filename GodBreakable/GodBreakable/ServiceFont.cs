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
        void Print(string text,Game thisGame, SpriteBatch pBatch);
    }

    public class ServiceFont : IServiceFont
    {
        //private SpriteBatch pBatch;
        SpriteFont fontArial;

        public ServiceFont() 
        {
            ServiceLocator.RegisterService<IServiceFont>(this);
        }

        public void Print(string text, Game thisGame, SpriteBatch pBatch)
        {
            pBatch.Begin();
            fontArial = thisGame.Content.Load<SpriteFont>("Default");
            pBatch.DrawString(fontArial, text, new Vector2(10, 10), Color.White);
            pBatch.End();
        }
    }
}
