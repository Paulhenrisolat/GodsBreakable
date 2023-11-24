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
    public class SceneMenu : Scene
    {
        SpriteFont fontMenu;
        Song MySong;
        public SceneMenu(Game pGame) : base(pGame)
        {
            fontMenu = game.Content.Load<SpriteFont>("Default");
            base.SceneSong = game.Content.Load<Song>("music/OpenYourEyes-part1");
            //MediaPlayer.IsRepeating = true;
            //MediaPlayer.Volume = 0.2f; //1ismax
            //MediaPlayer.Play(MySong);
            base.textBackground = game.Content.Load<Texture2D>("img/bgTitle");
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
