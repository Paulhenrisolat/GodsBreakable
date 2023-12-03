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
    public class Button : Sprite
    {
        public string Name;
        public bool IsClicked {  get; set; }

        public Button(Rectangle pScreen, Texture2D pTexture, string btnName) : base(pScreen, pTexture)
        {
            Name = btnName;
            IsClicked = false;
        }

        public override void Update()
        {
            CheckMouse();
        }

        public void CheckMouse()
        {
            if (Mouse.GetState().X >= Position.X && Mouse.GetState().X <= Position.X + Width && Mouse.GetState().Y >= Position.Y && Mouse.GetState().Y <= Position.Y + Height)
            {
                Scale = new Vector2(1.1f,1.1f);
                if(Mouse.GetState().LeftButton == ButtonState.Pressed)
                {
                    IsClicked = true;
                }
            }
            else
            {
                Scale = new Vector2(1f,1f);
                IsClicked = false;
            }
        }

        public override void Draw(SpriteBatch pBatch)
        {
            IServiceFont servFont = ServiceLocator.GetService<IServiceFont>();
            base.Draw(pBatch);
            servFont.Print(Name, "", new Vector2((Position.X + Width/2) - Name.Length*2,Position.Y + Height/2), pBatch);
        }
    }
}
