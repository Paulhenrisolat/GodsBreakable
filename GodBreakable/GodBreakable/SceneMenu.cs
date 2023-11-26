using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Input;

namespace GodBreakable
{
    public class SceneMenu : Scene
    {
        SpriteFont fontMenu;
        public SceneMenu(Game pGame) : base(pGame)
        {
            fontMenu = game.Content.Load<SpriteFont>("Default");
            base.textBackground = game.Content.Load<Texture2D>("img/bgTitle");

            IServiceSound servSound = ServiceLocator.GetService<IServiceSound>();
            servSound.PlayMusic("OpenYourEyes-part1");
        }

        public override void Update(GameTime gameTime)
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
