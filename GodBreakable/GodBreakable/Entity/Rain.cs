using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodBreakable.Entity
{
    public class Rain : Sprite
    {
        private int nbDot;
        private Texture2D textureDot;
        private List<RainDot> lstRainDot;

        public Rain(Rectangle pScreen, Texture2D pTexture, int speed) : base(pScreen, pTexture)
        {
            Position = new Vector2(0-Width, 0);
            nbDot = 10;
            textureDot = pTexture;
            lstRainDot = new List<RainDot>();
        }

        public override void Update()
        {
            base.Update();
            GenerateRainDot();
            for (int i = lstRainDot.Count - 1; i >= 0; i--)
            {
                RainDot raindot = lstRainDot[i];

                raindot.Update();
                if (raindot.Position.X < 0 || raindot.Position.Y > Screen.Width)
                {
                    lstRainDot.Remove(raindot);
                }
            }
        }

        public void GenerateRainDot()
        {
            for (int i = 0; i < nbDot; i++)
            {
                RainDot newRainDot = new RainDot(Screen, textureDot);
                Random r = new Random();
                int posY = r.Next(-1000, Screen.Height);
                int divisRand = r.Next(1,4);
                int randSpeed = r.Next(-15,-6);
                newRainDot.SetPosition(Screen.Width, posY);
                newRainDot.Speed = new Vector2(randSpeed, 4);
                newRainDot.Rotation = -5f;
                newRainDot.Scale = new Vector2(1/divisRand,1/divisRand);
                lstRainDot.Add(newRainDot);
            }
        }

        public int GetRain()
        {
            return lstRainDot.Count;
        }

        public override void Draw(SpriteBatch pBatch)
        {
            base.Draw(pBatch);
            foreach (RainDot raindot in lstRainDot)
            {
                raindot.Draw(pBatch);
            }
        }
    }
}
