using Microsoft.Xna.Framework;

namespace Color_Breaker
{
    public sealed class ScreenManager : IScreen
    {
        GraphicsDeviceManager graphicsDevice;

        public float Width
        {
            get => graphicsDevice.PreferredBackBufferWidth;
        }

        public float Height
        {
            get => graphicsDevice.PreferredBackBufferHeight;
        }

        public Rectangle Bounds
        {
            get => graphicsDevice.GraphicsDevice.Viewport.Bounds;
        }

        public Vector2 Center
        {
            get => new Vector2(Width / 2, Height / 2);
        }

        public ScreenManager(GraphicsDeviceManager device, int width, int height)
        {
            graphicsDevice = device;

            graphicsDevice.PreferredBackBufferWidth = width;
            graphicsDevice.PreferredBackBufferHeight = height;
            graphicsDevice.ApplyChanges();

            Services.RegisterService<IScreen>(this);
        }
    }
}
