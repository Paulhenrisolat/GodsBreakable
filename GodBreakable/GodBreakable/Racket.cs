using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodBreakable
{
    public class Racket : Sprite
    {
        public Racket(Texture2D pTexture, Rectangle pScreen) : base(pTexture, pScreen)
        {

        }
        public override void Update()
        {
            if (Mouse.GetState().Y < Screen.Height && Mouse.GetState().Y > 0)
            {
                SetPosition(Mouse.GetState().X - Width / 2, Position.Y);
            }
            if (Mouse.GetState().X > Screen.Width)
            {
                SetPosition(Screen.Width - Width, Position.Y);
            }
            if (Mouse.GetState().X < 0)
            {
                SetPosition(0, Position.Y);
            }
            base.Update();
        }
    }
}
