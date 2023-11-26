using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Media;

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
        private readonly ServiceScreen ServiceScreen;
        private readonly ServiceSprite ServiceSprite;
        private readonly ServiceInput ServiceInput = new ServiceInput();
        private readonly ServiceFont ServiceFont;
        public readonly ServiceSound ServiceSound;

        public Scene(Game pGame)
        {
            game = pGame;
            ScreenSize = game.Window.ClientBounds;
            textBackground = game.Content.Load<Texture2D>("img/fondAi1");

            rnd = new Random();

            ServiceFont = new ServiceFont(game);
            ServiceSound = new ServiceSound(game);
            ServiceScreen = new ServiceScreen(game);
            ServiceSprite = new ServiceSprite(game);
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
            ServiceFont.Print("Music: " + ServiceSound.MusicPlaying(),"", new Vector2(20, 70), pBatch);
            pBatch.End();
        }
    }
}
