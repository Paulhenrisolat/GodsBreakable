using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodBreakable
{
    public class Brick : Sprite
    {
        private bool brickFalling;
        public bool BrickIsFalling { get { return brickFalling; } }
        public Brick(Texture2D pTexture, Rectangle pScreen) : base(pTexture, pScreen)
        {
            brickFalling = false;
        }
        public override void Update()
        {
            if (brickFalling)
            {
                Speed = new Vector2(Speed.X, Speed.Y * 1.10f);
            }
            base.Update();
        }

        public void Fall()
        {
            brickFalling = true;
            Speed = new Vector2(Speed.X, 1);
            IServiceScore servScore = ServiceLocator.GetService<IServiceScore>();
            if (servScore != null)
            {
                servScore.Add(10);
            }
        }
    }
}
