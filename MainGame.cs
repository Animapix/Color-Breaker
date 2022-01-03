using Animapix;
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


        private NodeTree _nodeTree;

        public MainGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            IsFixedTimeStep = false;

            _nodeTree = new NodeTree();
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _assetsManager = new AssetsManager(Content);
            _assetsManager.LoadAsset<Texture2D>("Brick");
            _assetsManager.LoadAsset<Texture2D>("BrickShadow");

            SpriteNode sprite = new SpriteNode(100, 100, Color.White, _assetsManager.GetAsset<Texture2D>("Brick"));
            _nodeTree.Add(sprite);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _nodeTree.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _nodeTree.Draw(_spriteBatch);

            base.Draw(gameTime);
        }
    }
}
