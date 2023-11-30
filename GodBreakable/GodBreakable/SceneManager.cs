using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace GodBreakable
{
    public class SceneManager
    {
        //Scenes
        static private Scene MyActualScene;
        private SceneMenu MySceneMenu;
        private SceneGameplay MySceneGameplay;
        static private SceneBoss MySceneBoss;
        private SceneBossSelector MySceneBossSelector;

        private static List<Scene> myScenes;

        private static Game actualGame;

        public SceneManager(Game pGame)
        {
            actualGame = pGame;

            MySceneMenu = new SceneMenu(pGame, "Menu");
            MySceneGameplay = new SceneGameplay(pGame, "Gameplay");
            MySceneBossSelector = new SceneBossSelector(pGame, "SelectorBoss");
            

            myScenes = new List<Scene>
            {
                MySceneMenu, 
                MySceneGameplay,
                MySceneBossSelector,
            };

            ChangeScene("Menu");
        }

        public void LoadScene() 
        {
          
        }

        public void UpdateScene(GameTime gameTime)
        {
            //MyActualScene = serviceScene.SceneSelected();
            MyActualScene.Update(gameTime);

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                ChangeScene("Gameplay");
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                ChangeScene("Boss");
            }
            if (Keyboard.GetState().IsKeyDown(Keys.M))
            {
                ChangeScene("Menu");
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                ChangeScene("SelectorBoss");
            }
        }

        public static void ChangeScene(string scene)
        {
            foreach(Scene myScene in myScenes)
            {
                if(myScene.SceneName == scene)
                {
                    MyActualScene = myScene;
                }
            }
        }

        public static void ChargeBoss(Boss selectedBoss)
        {
            MySceneBoss = new SceneBoss(actualGame, "Boss", selectedBoss);
            myScenes.Add(MySceneBoss);
            ChangeScene(MySceneBoss.SceneName);
        }

        public void DrawScene(SpriteBatch spBatch) 
        {
            MyActualScene.Draw(spBatch);
        }
    }
}
