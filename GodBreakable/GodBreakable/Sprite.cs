using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace GodBreakable
{
    public class Sprite
    {
        protected Rectangle Screen;
        public Rectangle CollideBox { get; private set; }
        public Vector2 Position { get; set; }
        public Vector2 Speed { get; set; }

        private Texture2D Texture;

        public int Height { get { return Texture.Height; } }
        public int Width { get { return Texture.Width; } }
        public float center { get { return Position.X + Width / 2; } }

        public float Rotation;
        public Vector2 Scale;

        public Sprite(Rectangle pScreen, Texture2D pTexture)
        {
            Screen = pScreen;
            Texture = pTexture;
            Rotation = 0f;
            Scale = new Vector2(1f, 1f);
        }

        public void SetPosition(Vector2 pPosition)
        {
            Position = pPosition;
        }
        public void SetPosition(float pX, float pY)
        {
            Position = new Vector2(pX, pY);
        }

        public Rectangle NextPositionX()
        {
            Rectangle nextPosition = CollideBox;
            nextPosition.Offset(new Point((int)Speed.X, 0));
            return nextPosition;
        }
        public Rectangle NextPositionY()
        {
            Rectangle nextPosition = CollideBox;
            nextPosition.Offset(new Point(0, (int)Speed.Y));
            return nextPosition;
        }

        public void InverseSpeedX()
        {
            Speed = new Vector2(-Speed.X, Speed.Y);
        }
        public void InverseSpeedY()
        {
            Speed = new Vector2(Speed.X, -Speed.Y);
        }

        public void ChangeSize(int value)
        {
            //Texture.Width += value;
        }

        public virtual void Update()
        {
            //Position = new Vector2(Position.X + Speed.X, Position.Y + Speed.Y);
            //plus simple
            Position += Speed;
            CollideBox = new Rectangle((int)Position.X, (int)Position.Y, Width, Height);
        }

        public virtual void Draw(SpriteBatch pBatch)
        {
            pBatch.Draw(Texture, Position, null, Color.White,Rotation,Vector2.Zero, Scale, SpriteEffects.None, 0f);
        }
    }
}
