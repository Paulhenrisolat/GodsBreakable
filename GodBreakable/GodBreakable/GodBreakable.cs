using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GodBreakable
{
    public class GodBreakable : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        //Scenes
        Scene MyActualScene;
        SceneMenu MySceneMenu;
        SceneGameplay MySceneGameplay;
        SceneBoss MySceneBoss;

        public GodBreakable()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            //screen size
            _graphics.PreferredBackBufferWidth = 1100;
            _graphics.PreferredBackBufferHeight = 800;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            MySceneMenu = new SceneMenu(this);
            MySceneGameplay = new SceneGameplay(this);
            MySceneBoss = new SceneBoss(this);

            MyActualScene = MySceneMenu;
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            //change scene
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                MyActualScene = MySceneGameplay;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                MyActualScene = MySceneBoss;
            }

            MyActualScene.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            //Draw scene
            MyActualScene.Draw(_spriteBatch);

            base.Draw(gameTime);
        }
    }
}