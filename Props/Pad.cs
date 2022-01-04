using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace Color_Breaker
{
    public sealed class Pad : SpriteNode
    {
        private Vector2 _velocity = new Vector2(0,0);
        private Sides _side;
        private const float _speed = 800;

        public Pad(Sides side) : base(Services.Get<IAssets>().GetAsset<Texture2D>("PadH"), Layers.Props)
        {
            _side = side;

            Texture2D shadowTexture = Services.Get<IAssets>().GetAsset<Texture2D>("PadShadowH");
            switch (_side)
            {
                case Sides.Left:
                    _texture = Services.Get<IAssets>().GetAsset<Texture2D>("PadV");
                    shadowTexture = Services.Get<IAssets>().GetAsset<Texture2D>("PadShadowV");
                    Position.X = 80;
                    break;
                case Sides.Top:
                    Position.Y = 80;
                    break;
                case Sides.Right:
                    _texture = Services.Get<IAssets>().GetAsset<Texture2D>("PadV");
                    shadowTexture = Services.Get<IAssets>().GetAsset<Texture2D>("PadShadowV");
                    Position.X = 700;
                    break;
                case Sides.Bottom:
                    Position.Y = 700;
                    break;
            }
            SpriteNode spriteShadow = new SpriteNode(shadowTexture, Layers.Shadows);
            spriteShadow.Centered = true;
            spriteShadow.Position = Center - Position;
            AddChild(spriteShadow);
        }

        public override void UpdatePhysics(float deltaTime)
        {
            if (_side == Sides.Left || _side == Sides.Right)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Up))
                {
                    Position.Y -= _speed * deltaTime;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Down))
                {
                    Position.Y += _speed * deltaTime;
                }
            }
            else
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Left))
                {
                    Position.X -= _speed * deltaTime;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Right))
                {
                    Position.X += _speed * deltaTime;
                }
            }

            base.UpdatePhysics(deltaTime);
        }

    }
}
