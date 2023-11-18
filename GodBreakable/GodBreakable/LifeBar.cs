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
        private Texture2D textBarEmpty;
        private Texture2D textBarFull;
        private string entityName;
        private int lifeValue;
        private int lifeMax;
        SpriteFont fontLife;
        public Rectangle lifeRect { get; set; }

        public LifeBar(Rectangle pScreen, Texture2D pTexture, Texture2D pTextureSup) : base(pScreen, pTexture, pTextureSup)
        {
            //fontLife = Content.Load<SpriteFont>("Default");
            lifeRect = new Rectangle(0, 0, pTextureSup.Width, pTextureSup.Height);
        }

        public void LooseLife()
        {

        }
        public void GainLife()
        {

        }

        public override void Update()
        {
            base.Update();
        }
    }
}
