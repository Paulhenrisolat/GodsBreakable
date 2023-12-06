using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodBreakable
{
    public class Ball : Sprite
    {
        public bool isPaused { get; set; }
        public bool canChangeState { get; set; }
        public Vector2 lastSpeed { get; set; }
        public float constSpeed { get; private set; }

        public Ball(Rectangle pScreen, Texture2D pTexture) : base(pScreen, pTexture)
        {
            isPaused = false;
            canChangeState = true;
            constSpeed = Speed.X;
        }

        public void BallManager()
        {
            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                 //canMove();
            }
        }

        public bool canMove()
        {
            if (isPaused == true)
            {
                lastSpeed = Speed;
                Speed = new Vector2(0f, 0f);
                Debug.WriteLine("Pause ball s: " + lastSpeed.X + " " + lastSpeed.Y);
                return false;
            }
            if (isPaused == false)
            {
                Speed = lastSpeed;
                Debug.WriteLine("active ball");
                return true;
            }
            else
            {
                return false;
            }
        }

        public override void Update()
        {
            BallManager();
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
