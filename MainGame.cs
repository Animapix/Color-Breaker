using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace Color_Breaker
{
    

    public class MainGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private AssetsManager _assetsManager;
        private SceneManager _sceneManager;
        private ScreenManager _screenManager;

        public MainGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            _sceneManager = new SceneManager();
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            IsFixedTimeStep = false;
        }

        protected override void Initialize()
        {
            _sceneManager.Register(SceneType.Game, new SceneGame());
            _sceneManager.Register(SceneType.Menu, new SceneMenu());

            _screenManager = new ScreenManager(_graphics, 800, 800);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _assetsManager = new AssetsManager(Content);
            _assetsManager.LoadAsset<Texture2D>("Brick");
            _assetsManager.LoadAsset<Texture2D>("BrickShadow");
            _assetsManager.LoadAsset<Texture2D>("Ball");
            _assetsManager.LoadAsset<Texture2D>("BallShadow");
            _assetsManager.LoadAsset<Texture2D>("Background");

            _assetsManager.LoadAsset<Texture2D>("WallV");
            _assetsManager.LoadAsset<Texture2D>("WallShadowV");
            _assetsManager.LoadAsset<Texture2D>("WallH");
            _assetsManager.LoadAsset<Texture2D>("WallShadowH");

            _sceneManager.Load(SceneType.Game);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            _sceneManager.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _sceneManager.Draw(_spriteBatch);
            base.Draw(gameTime);
        }
    }
}
