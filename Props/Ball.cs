using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Color_Breaker
{
    public sealed class Ball : Node
    {

        private Vector2 _velocity = new Vector2(-600, 400);
        private const float _speed = 400;
        private const float _radius = 10;
        private Rectangle _bounds;

        public Ball(float x, float y, Rectangle bounds) : base(new Vector2(x,y))
        {
            IAssets assets = Services.Get<IAssets>();
            SpriteNode sprite = new SpriteNode(assets.GetAsset<Texture2D>("Ball"), Layer.props);
            SpriteNode spriteShadow = new SpriteNode(assets.GetAsset<Texture2D>("BallShadow"), Layer.Shadows);
            sprite.Centered = true;
            spriteShadow.Centered = true;
            AddChild(sprite);
            sprite.AddChild(spriteShadow);
            _bounds = bounds;
        }


        public override void Update(GameTime gameTime)
        {
            Position += _velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            CheckingBounds(_bounds);
            base.Update(gameTime);
        }

        private void CheckingBounds(Rectangle bounds)
        {
            if (Position.X - _radius < bounds.Left)
            {
                _velocity.X = -_velocity.X;
                Position.X = bounds.Left + _radius;
            }

            if (Position.X + _radius > bounds.Right)
            {
                _velocity.X = -_velocity.X;
                Position.X = bounds.Right - _radius;
            }

            if (Position.Y - _radius < bounds.Top)
            {
                _velocity.Y = -_velocity.Y;
                Position.Y = bounds.Top + _radius;
            }

            if (Position.Y + _radius > bounds.Bottom)
            {
                _velocity.Y = -_velocity.Y;
                Position.Y = bounds.Bottom - _radius;
            }
        }
    }
}
