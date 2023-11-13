using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodBreakable
{
    public class SceneMenu : Scene
    {
        SpriteFont fontMenu;

        public SceneMenu(Game pGame) : base(pGame)
        {
            fontMenu = game.Content.Load<SpriteFont>("Default");
        }

        public override void Update()
        {

        }

        public override void Draw(SpriteBatch pBatch)
        {
            base.Draw(pBatch);
            pBatch.Begin();
            pBatch.DrawString(fontMenu, "Scene Menu", new Vector2(10, 10), Color.White);
            pBatch.End();
        }
    }
}
