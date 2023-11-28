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
        private IServiceSprite serviceSprite;
        private IServiceScreen serviceScreen;
        SpriteFont fontMenu;
        private Button playButton;

        public SceneMenu(Game pGame) : base(pGame)
        {
            fontMenu = game.Content.Load<SpriteFont>("Default");
            base.textBackground = game.Content.Load<Texture2D>("img/bgTitle");

            IServiceSound servSound = ServiceLocator.GetService<IServiceSound>();
            servSound.PlayMusic("OpenYourEyes-part1");

            serviceSprite = ServiceLocator.GetService<IServiceSprite>();
            serviceScreen = ServiceLocator.GetService<IServiceScreen>();
            playButton = new Button(serviceScreen.GetScreen(), serviceSprite.NewSprite("img/playbtn"), "");
            playButton.SetPosition(serviceScreen.GetScreen().Width/2 - playButton.Width / 2, serviceScreen.GetScreen().Height / 2 - playButton.Height / 2);
        }

        public override void Update(GameTime gameTime)
        {
            playButton.Update();
        }

        public override void Draw(SpriteBatch pBatch)
        {
            base.Draw(pBatch);
            pBatch.Begin();
            pBatch.DrawString(fontMenu, "Scene Menu", new Vector2(10, 10), Color.White);
            playButton.Draw(pBatch);
            pBatch.End();
        }
    }
}
