using Microsoft.Xna.Framework;

namespace Color_Breaker
{
    public sealed class ScreenManager : IScreen
    {
        private GraphicsDeviceManager _graphicsDevice;
        private Game _game;

        public float Width
        {
            get => _graphicsDevice.PreferredBackBufferWidth;
        }

        public float Height
        {
            get => _graphicsDevice.PreferredBackBufferHeight;
        }

        public Rectangle Bounds
        {
            get => _graphicsDevice.GraphicsDevice.Viewport.Bounds;
        }

        public Vector2 Center
        {
            get => new Vector2(Width / 2, Height / 2);
        }

        public ScreenManager(GraphicsDeviceManager device, int width, int height, Game game)
        {
            _graphicsDevice = device;
            _game = game;
            _graphicsDevice.PreferredBackBufferWidth = width;
            _graphicsDevice.PreferredBackBufferHeight = height;
            _graphicsDevice.ApplyChanges();

            Services.RegisterService<IScreen>(this);
        }

        public void Quit()
        {
            _game.Exit();
        }
    }
}
