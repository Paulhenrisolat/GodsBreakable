using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodBreakable
{
    public interface IServiceInput
    {
        bool GetInputPressed(string inputID);
    }

    public class ServiceInput : IServiceInput
    {
        public ServiceInput() 
        {
            ServiceLocator.RegisterService<IServiceInput>(this);
        }

        public bool GetInputPressed(string inputID)
        {
            Keys pressedKeys;
            switch (inputID)
            {
                case "enter":
                    pressedKeys = Keys.Enter;
                    break;
                case "space":
                    pressedKeys = Keys.Space;
                    break;
                case "escape":
                    pressedKeys = Keys.Escape;
                    break;
                case "a":
                    pressedKeys = Keys.A;
                    break;
                case "b":
                    pressedKeys = Keys.B;
                break;
                case "c":
                    pressedKeys = Keys.C;
                    break;
                case "d":
                    pressedKeys = Keys.D;
                    break;
                case "e":
                    pressedKeys = Keys.E;
                    break;
                case "f":
                    pressedKeys = Keys.F;
                    break;
                case "g":
                    pressedKeys = Keys.G;
                    break;
                case "h":
                    pressedKeys = Keys.H;
                    break;
                case "i":
                    pressedKeys = Keys.I;
                    break;
                case "j":
                    pressedKeys = Keys.J;
                    break;
                case "k":
                    pressedKeys = Keys.K;
                    break;
                case "l":
                    pressedKeys = Keys.L;
                    break;
                case "m":
                    pressedKeys = Keys.M;
                    break;
                case "n":
                    pressedKeys = Keys.N;
                    break;
                case "o":
                    pressedKeys = Keys.O;
                    break;
                case "p":
                    pressedKeys = Keys.P;
                    break;
                case "q":
                    pressedKeys = Keys.Q;
                    break;
                case "r":
                    pressedKeys = Keys.R;
                    break;
                case "s":
                    pressedKeys = Keys.S;
                    break;
                case "t":
                    pressedKeys = Keys.T;
                    break;
                case "u":
                    pressedKeys = Keys.U;
                    break;
                case "v":
                    pressedKeys = Keys.V;
                    break;
                case "w":
                    pressedKeys = Keys.W;
                    break;
                case "x":
                    pressedKeys = Keys.X;
                    break;
                case "y":
                    pressedKeys = Keys.Y;
                    break;
                case "z":
                    pressedKeys = Keys.Z;
                    break;
                default:
                    break;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.None))
            {
                return true;
            }
            else 
            {  
                return false; 
            }
            
        }
    }
}
