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
        private Button playButton;

        public SceneMenu(Game pGame, string sceneName) : base(pGame, sceneName)
        {
            fontMenu = game.Content.Load<SpriteFont>("Default");
            base.textBackground = game.Content.Load<Texture2D>("img/bgTitleV2");

            serviceSound.PlayMusic("OpenYourEyes-part1");

            playButton = new Button(serviceScreen.GetScreen(), serviceSprite.NewSprite("img/play"), "");
            playButton.SetPosition(serviceScreen.GetScreen().Width/2 - playButton.Width / 2, serviceScreen.GetScreen().Height / 2 - playButton.Height / 2);
        }

        public override void Update(GameTime gameTime)
        {
            playButton.Update();
            if(playButton.IsClicked)
            {
                SceneManager.ChangeScene("SelectorBoss");
            }
        }

        public override void Draw(SpriteBatch pBatch)
        {
            base.Draw(pBatch);
            pBatch.Begin();
            //pBatch.DrawString(fontMenu, "Scene Menu", new Vector2(10, 10), Color.White);
            serviceFont.Print("Scene Menu", "", new Vector2(10, 10), pBatch);
            playButton.Draw(pBatch);
            pBatch.End();
        }
    }
}
