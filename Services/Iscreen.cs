using Microsoft.Xna.Framework;

namespace Color_Breaker
{
    public interface IScreen
    {
        float Width { get; }
        float Height { get; }
        Rectangle Bounds { get; }
        Vector2 Center { get; }
        void Quit();
    }
}
