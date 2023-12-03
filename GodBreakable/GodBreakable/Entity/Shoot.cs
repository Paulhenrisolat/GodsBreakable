using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodBreakable
{
    public class Shoot : Sprite
    {
        public int Damage {  get; set; }

        public Shoot(Rectangle pScreen, Texture2D pTexture, Vector2 NewPosition, Vector2 NewSpeed, int NewDamage) : base(pScreen, pTexture)
        {
            Position = NewPosition;
            Speed = NewSpeed;
            Damage = NewDamage;
        }
    }
}
