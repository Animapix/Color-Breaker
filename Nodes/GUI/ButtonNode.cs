using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Color_Breaker
{
    public class ButtonNode : LabelNode
    {
        private Keys _key;
        private Texture2D _texture;
        private KeyboardState _oldState;

        public ButtonNode(Keys key, Texture2D texture, SpriteFont font, string text,Color color, Layers layer) : base(text, texture.Bounds, font, color, layer)
        {
            _key = key;
            _texture = texture;
        }

        public override void Update(float deltaTime)
        {

            if (Keyboard.GetState().IsKeyDown(_key) && !_oldState.IsKeyDown(_key))
            {
                Debug.WriteLine("btn pressed");
            }

            base.Update(deltaTime);
            _oldState = Keyboard.GetState();
        }

    }

}
