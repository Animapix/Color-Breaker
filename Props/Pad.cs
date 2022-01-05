using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Color_Breaker
{
    public sealed class Pad : SpriteNode
    {
        private Vector2 _velocity = new Vector2(0,0);
        private const float _speed = 800;
        private Ball _stickedBall;
        private Rectangle _bounds;

        public Sides Side;
        public Pad(Rectangle bounds, Sides side) : base(Services.Get<IAssets>().GetAsset<Texture2D>("PadH"), Layers.Props)
        {
            Side = side;
            _bounds = bounds;
            IScreen screen = Services.Get<IScreen>();
            Texture2D shadowTexture = Services.Get<IAssets>().GetAsset<Texture2D>("PadShadowH");
            switch (Side)
            {
                case Sides.Left:
                    _texture = Services.Get<IAssets>().GetAsset<Texture2D>("PadV");
                    shadowTexture = Services.Get<IAssets>().GetAsset<Texture2D>("PadShadowV");
                    Position.X = 80;
                    Position.Y = screen.Height / 2;
                    break;
                case Sides.Top:
                    Position.Y = 80;
                    Position.X = screen.Width / 2 - Width/2;
                    break;
                case Sides.Right:
                    _texture = Services.Get<IAssets>().GetAsset<Texture2D>("PadV");
                    shadowTexture = Services.Get<IAssets>().GetAsset<Texture2D>("PadShadowV");
                    Position.X = 700;
                    Position.Y = screen.Height / 2;
                    break;
                case Sides.Bottom:
                    Position.Y = 700;
                    Position.X = screen.Width / 2 - Width / 2;
                    break;
            }
            SpriteNode spriteShadow = new SpriteNode(shadowTexture, Layers.Shadows);
            spriteShadow.Centered = true;
            spriteShadow.Position = Center - Position;
            AddChild(spriteShadow);
        }

        public override void UpdatePhysics(float deltaTime)
        {
            if (IsFreezed) return;
            if (Side == Sides.Left || Side == Sides.Right)
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

            CheckingBounds(_bounds);

            if (_stickedBall != null)
            {
                Vector2 offset = new Vector2(0, 0);
                switch (Side)
                {
                    case Sides.Left:
                        offset.X = Width / 2 + Ball.Radius + 1;
                        break;
                    case Sides.Right:
                        offset.X = -Width / 2 - Ball.Radius - 1;
                        break;
                    case Sides.Top:
                        offset.Y = Height / 2 + Ball.Radius + 1;
                        break;
                    case Sides.Bottom:
                        offset.Y = -Height / 2 - Ball.Radius - 1;
                        break;
                }
                _stickedBall.Position = Center + offset;
            }
            

            base.UpdatePhysics(deltaTime);
        }


        public override void Update(float deltaTime)
        {
            if (IsFreezed) return;

            if (Services.Get<IInputs>().IsJustPressed(Keys.Space) && _stickedBall != null)
            {
                float value = 0;
                float angle = 0;

                switch (Side)
                {
                    case Sides.Left:
                    case Sides.Right:
                        value = (Position.Y - _bounds.Top) / (_bounds.Height - Height) - 0.5f;
                        angle = ((float)Math.PI / 180) * (90 * value);
                        break;
                    case Sides.Top:
                    case Sides.Bottom:
                        value = (Position.X - _bounds.Left) / (_bounds.Width - Width) - 0.5f;
                        angle = ((float)Math.PI / 180) * (90 * value - 90);
                        break;
                }

                Vector2 dir = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
                _stickedBall.Launch(dir);
                _stickedBall = null;
            }

            base.Update(deltaTime);
        }

        private void CheckingBounds(Rectangle bounds)
        {
            if (Side  == Sides.Left || Side == Sides.Right)
            {
                if (Position.Y < bounds.Top)
                {
                    Position.Y = bounds.Top;
                }
                else if(Position.Y + Height > bounds.Bottom)
                {
                    Position.Y = bounds.Bottom - Height;
                }
            }
            else if (Side == Sides.Bottom || Side == Sides.Top)
            {
                if (Position.X < bounds.Left)
                {
                    Position.X = bounds.Left;
                }
                else if (Position.X + Width > bounds.Right)
                {
                    Position.X = bounds.Right - Width;
                }
            }

        }

        public void StickBall(Ball ball)
        {
            _stickedBall = ball;
        }

    }
}
