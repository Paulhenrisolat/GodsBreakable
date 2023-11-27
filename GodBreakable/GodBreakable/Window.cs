using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct2D1.Effects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodBreakable
{
    public class Window
    {
        private IServiceSprite serviceSprite;
        private IServiceScreen serviceScreen;
        private IServiceFont serviceFont;
        private Vector2 windowPosition;
        private Texture2D textWindow;
        private Button closeButton;
        private Button mainMenuButton;

        private SceneManager sceneManager;
        public string windowName { get; set; }
        public bool windowIsOpen { get; set; }

        public Window(string WindowName) 
        {
            windowName = WindowName;
            windowIsOpen = false;

            serviceSprite = ServiceLocator.GetService<IServiceSprite>();
            serviceScreen = ServiceLocator.GetService<IServiceScreen>();
            serviceFont = ServiceLocator.GetService<IServiceFont>();
            textWindow = serviceSprite.NewSprite("img/window");
            windowPosition = new Vector2(serviceScreen.GetScreen().Width / 2 - textWindow.Width / 2, serviceScreen.GetScreen().Height / 2 - textWindow.Height / 2);
            //Buttons
            closeButton = new Button(serviceScreen.GetScreen(),serviceSprite.NewSprite("img/closebtn"),"");
            closeButton.SetPosition((windowPosition.X+textWindow.Width) - closeButton.Width, windowPosition.Y);
            mainMenuButton = new Button(serviceScreen.GetScreen(), serviceSprite.NewSprite("img/btnbg"), "Main Menu");
            mainMenuButton.SetPosition((windowPosition.X + textWindow.Width/2) - mainMenuButton.Width/2, windowPosition.Y + textWindow.Height/2 + mainMenuButton.Height/2);
        }

        public void OpenWindow()
        {
            //if (windowIsOpen == true)
            //{
            //    windowIsOpen = false;
            //}
            if (windowIsOpen == false)
            {
                windowIsOpen = true;
            }
        }

        public virtual void Update() 
        { 
            closeButton.Update();
            mainMenuButton.Update();
            if (closeButton.IsClicked == true)
            {
                Debug.WriteLine("Close Window");
                windowIsOpen = false;
            }
            if(mainMenuButton.IsClicked == true)
            {
                //sceneManager.ChangeScene();
            }
        }

        public virtual void Draw(SpriteBatch pBatch)
        {
            if (windowIsOpen == true)
            {
                pBatch.Draw(textWindow, windowPosition, Color.White);
                serviceFont.Print(windowName, "", new Vector2(windowPosition.X + textWindow.Width / 2, windowPosition.Y), pBatch);
                closeButton.Draw(pBatch);
                mainMenuButton.Draw(pBatch);
            }
        }
    }
}
