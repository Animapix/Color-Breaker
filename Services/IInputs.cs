using Microsoft.Xna.Framework.Input;

namespace Color_Breaker
{
    public interface IInputs
    {
        bool IsJustPressed(Keys key);
        bool IsPressed(Keys key);
    }
}
