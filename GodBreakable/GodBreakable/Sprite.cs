using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public float center { get { return Position.X + Width / 2; } }

        private Texture2D Texture;
        private Texture2D TextureSup;
        public int Height { get { return Texture.Height; } }
        public int Width { get { return Texture.Width; } }

        public bool doubleSprite { get; set; }

        public Sprite(Rectangle pScreen, Texture2D pTexture)
        {
            Screen = pScreen;
            Texture = pTexture;
        }

        public Sprite(Rectangle pScreen, Texture2D pTexture, Texture2D pTextureSup)
        {
            Screen = pScreen;
            Texture = pTexture;
            TextureSup = pTextureSup;
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
        public virtual void Update()
        {
            //Position = new Vector2(Position.X + Speed.X, Position.Y + Speed.Y);
            //plus simple
            Position += Speed;
            CollideBox = new Rectangle((int)Position.X, (int)Position.Y, Width, Height);
        }

        public virtual void Draw(SpriteBatch pBatch)
        {
            pBatch.Draw(Texture, Position, Color.White);
            if (doubleSprite)
            {
                pBatch.Draw(TextureSup, Position, Color.White);
            }
        }
    }
}
