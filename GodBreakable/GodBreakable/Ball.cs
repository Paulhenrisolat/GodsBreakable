using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodBreakable
{
    public class Ball : Sprite
    {
        public bool isPaused {  get; set; }

        public Ball(Rectangle pScreen, Texture2D pTexture) : base(pScreen, pTexture)
        {
            isPaused = false;
        }

        public override void Update()
        {
            //collide with screen
            if (Position.X > Screen.Width - Width)
            {
                SetPosition(Screen.Width - Width, Position.Y);
                InverseSpeedX();
            }
            if (Position.X < 0)
            {
                SetPosition(0, Position.Y);
                InverseSpeedX();
            }
            if (Position.Y < 0)
            {
                SetPosition(Position.X, 0);
                InverseSpeedY();
            }
            if (Position.Y > Screen.Height - Height)
            {
                //SetPosition(Position.X, Screen.Height - Height);
                //InverseSpeedY();
            }
            base.Update();
        }
    }
}
