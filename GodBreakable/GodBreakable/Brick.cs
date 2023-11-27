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
        public string BrickType;
        public int BrickHP {  get; set; }
        private Texture2D BrickReflect;
        private bool IsHit;
        public Brick(Rectangle pScreen, Texture2D pTexture, string brickType, int brickHP) : base(pScreen, pTexture)
        {
            brickFalling = false;
            BrickType = brickType;
            BrickHP = brickHP;

            IsHit = false;
            IServiceSprite servSprite = ServiceLocator.GetService<IServiceSprite>();
            BrickReflect = servSprite.NewSprite("img/bricke");
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

        public void TakeHit()
        {
            BrickHP -= 1;
            IsHit = true;
        }

        public override void Draw(SpriteBatch pBatch)
        {
            base.Draw(pBatch);
            if (IsHit == true) 
            {
                pBatch.Draw(BrickReflect, Position, null, Color.White, Rotation, Vector2.Zero, Scale, SpriteEffects.None, 0f);
            }
            
        }
    }
}
