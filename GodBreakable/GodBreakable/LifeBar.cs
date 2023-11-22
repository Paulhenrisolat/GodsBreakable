using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodBreakable
{
    public class LifeBar : Sprite
    {
        private Texture2D textLifeBar;
        private Texture2D textLifeBarEmpty;
        private string entityName;
        private float lifeValue;
        private float lifeMax;

        public LifeBar(Rectangle pScreen, Texture2D pTexture, Game game) : base(pScreen, pTexture)
        {
            textLifeBarEmpty = game.Content.Load<Texture2D>("img/barempty");
        }

        public void LifeManager(float entityLife)
        {
            lifeValue = entityLife / 100;
            Scale = new Vector2(lifeValue, 0.50f);
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Draw(SpriteBatch pBatch)
        {
            pBatch.Draw(textLifeBarEmpty, Position, null, Color.White, Rotation, Vector2.Zero, new Vector2(1f, 0.50f), SpriteEffects.None, 0f);
            base.Draw(pBatch);
            
        }
    }
}
