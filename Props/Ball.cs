using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Color_Breaker
{
    public sealed class Ball : SpriteNode
    {

        private Vector2 _velocity = new Vector2(-400, 600);
        private const float _speed = 400;
        private const float _radius = 10;
        private Rectangle _bounds;

        public Ball(float x, float y, Rectangle bounds) : base(Services.Get<IAssets>().GetAsset<Texture2D>("Ball"), x, y, Layer.props,Color.White)
        {
            Centered = true;
            SpriteNode spriteShadow = new SpriteNode(Services.Get<IAssets>().GetAsset<Texture2D>("BallShadow"), Layer.Shadows);
            spriteShadow.Centered = true;
            AddChild(spriteShadow);
            _bounds = bounds;
        }

        public override void UpdatePhysics(float deltaTime)
        {
            Position += _velocity * deltaTime;
            CheckBricksCollisions(Services.Get<INodeTree>().GetNodes<Brick>());
            CheckingBounds(_bounds);
            base.UpdatePhysics(deltaTime);
        }

        public void CheckBricksCollisions(List<Brick> bricks)
        {
            for (int i = 0; i < bricks.Count; i++)
            {
                Brick testedBrick = bricks[i];

                if (IsIntersect(testedBrick.Position, testedBrick.Width, testedBrick.Height))
                {
                    Side collisionSide = getSideCollision(testedBrick.Position, testedBrick.Width, testedBrick.Height);
                    switch (collisionSide)
                    {
                        case Side.Left:
                            _velocity.X = -_velocity.X;
                            //Position.X = testedBrick.Sides.Left - Radius - 1;
                            break;
                        case Side.Right:
                            _velocity.X = -_velocity.X;
                            //Position.X = testedBrick.Sides.Right + Radius + 1;
                            break;
                        case Side.Bottom:
                            _velocity.Y = -_velocity.Y;
                            //Position.Y = testedBrick.Sides.Bottom + Radius + 1;
                            break;
                        case Side.Top:
                            _velocity.Y = -_velocity.Y;
                            //Position.Y = testedBrick.Sides.Top - Radius - 1;
                            break;
                    }

                    testedBrick.Hit(this, collisionSide);

                    return;
                }
            }
        }

        private bool IsIntersect(Vector2 rectPosition, float rectWidth, float rectHeight)
        {
            if (Position.Y - _radius > rectPosition.Y + rectHeight)
            {
                return false;
            }
            if (Position.Y + _radius < rectPosition.Y)
            {
                return false;
            }
            if (Position.X - _radius > rectPosition.X + rectWidth)
            {
                return false;
            }
            if (Position.X + _radius < rectPosition.X)
            {
                return false;
            }

            return true;
        }

        private Side getSideCollision(Vector2 rectPosition, float rectWidth, float rectHeight)
        {
            if (_velocity.X == 0)
            {
                if (_velocity.Y > 0)
                    return Side.Top;
                else
                    return Side.Bottom;
            }
            else if (_velocity.Y == 0)
            {
                if (_velocity.X > 0)
                    return Side.Left;
                else
                    return Side.Right;
            }
            else
            {
                float slope = _velocity.Y / _velocity.X;
                float cx, cy;

                if (slope > 0 && _velocity.X > 0) // Ball moving down right
                {
                    cx = rectPosition.X - Position.X;
                    cy = rectPosition.Y - Position.Y;
                    if (cx <= 0)
                        return Side.Top;
                    else if (cy / cx < slope)
                        return Side.Left;
                    else
                        return Side.Top;
                }
                else if (slope < 0 && _velocity.X > 0) // Ball moving up right
                {
                    cx = rectPosition.X - Position.X;
                    cy = rectPosition.Y + rectHeight - Position.Y;
                    if (cx <= 0)
                        return Side.Bottom;
                    else if (cy / cx < slope)
                        return Side.Bottom;
                    else
                        return Side.Left;
                }
                else if (slope > 0 && _velocity.X < 0) // Ball moving up left
                {
                    cx = rectPosition.X + rectWidth - Position.X;
                    cy = rectPosition.Y + rectHeight - Position.Y;
                    if (cx >= 0)
                        return Side.Bottom;
                    else if (cy / cx < slope)
                        return Side.Bottom;
                    else
                        return Side.Right;
                }
                else // Ball moving down left
                {
                    cx = rectPosition.X + rectWidth - Position.X;
                    cy = rectPosition.Y - Position.Y;
                    if (cx >= 0)
                        return Side.Top;
                    else if (cy / cx < slope)
                        return Side.Top;
                    else
                        return Side.Right;
                }

            }
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
