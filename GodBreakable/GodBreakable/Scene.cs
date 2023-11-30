using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Media;
using System.Net;

namespace GodBreakable
{
    public class Scene
    {
        protected Game game;
        public Texture2D textBackground {  get; set; }
        public string SceneSong { get; set; }
        public bool CanChangeMusic {  get; set; }
        public Rectangle ScreenSize { get; private set; }
        protected int CamShake;
        private Random rnd;
        //Declare Services
        public readonly ServiceScreen serviceScreen;
        public readonly ServiceSprite serviceSprite;
        public readonly ServiceInput serviceInput = new ServiceInput();
        public readonly ServiceFont serviceFont;
        public readonly ServiceSound serviceSound;

        public string SceneName;
        public Scene(Game pGame, string sceneName)
        {
            game = pGame;
            SceneName = sceneName;
            rnd = new Random();

            serviceFont = new ServiceFont(game);
            serviceSound = new ServiceSound(game);
            serviceScreen = new ServiceScreen(game);
            serviceSprite = new ServiceSprite(game);

            ScreenSize = game.Window.ClientBounds;
            textBackground = serviceSprite.NewSprite("img/fondAi1");
        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Draw(SpriteBatch pBatch)
        {
            if (CamShake > 0)
            {
                int decal = rnd.Next(-4, 5);
                pBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, Matrix.CreateTranslation(decal, decal, 0));
                CamShake--;
            }
            else
            {
                pBatch.Begin();
            }

            pBatch.Draw(textBackground, new Vector2(ScreenSize.Width/2-textBackground.Width/2, 0), Color.White);
            serviceFont.Print("Music: " + serviceSound.MusicPlaying(),"", new Vector2(20, 70), pBatch);
            pBatch.End();
        }
    }
}
