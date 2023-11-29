using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GodBreakable
{
    public class SceneManager
    {
        //Scenes
        public Scene MyActualScene { get; set; }
        public SceneMenu MySceneMenu;
        SceneGameplay MySceneGameplay;
        SceneBoss MySceneBoss;
        SceneBossSelector MySceneBossSelector;

        public SceneManager(Game pGame)
        {
            MySceneMenu = new SceneMenu(pGame);
            MySceneGameplay = new SceneGameplay(pGame);
            MySceneBoss = new SceneBoss(pGame);
            MySceneBossSelector = new SceneBossSelector(pGame);
        }

        public void LoadScene() 
        {
            MyActualScene = MySceneMenu;
        }

        public void UpdateScene(GameTime gameTime)
        {
            MyActualScene.Update(gameTime);

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                ChangeScene(MySceneGameplay);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                ChangeScene(MySceneBoss);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.M))
            {
                ChangeScene(MySceneMenu);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                ChangeScene(MySceneBossSelector);
            }
        }

        public void ChangeScene(Scene selectedScene)
        {
            MyActualScene = selectedScene;
        }

        public void DrawScene(SpriteBatch spBatch) 
        {
            MyActualScene.Draw(spBatch);
        }
    }
}
