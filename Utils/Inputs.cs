using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Color_Breaker
{
    public sealed class Inputs : IInputs
    {
        private KeyboardState _oldState;

        public Inputs()
        {
            Services.RegisterService<IInputs>(this);
        }

        public void UpdateState()
        {
            _oldState = Keyboard.GetState();
        }

        public bool IsJustPressed(Keys key)
        {
            return Keyboard.GetState().IsKeyDown(key) && !_oldState.IsKeyDown(key);
        }

        public bool IsPressed(Keys key)
        {
            return Keyboard.GetState().IsKeyDown(key);
        }

    }
}
