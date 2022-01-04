﻿using Microsoft.Xna.Framework;
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

        public Ball(float x, float y, Rectangle bounds) : base(Services.Get<IAssets>().GetAsset<Texture2D>("Ball"), x, y, Layers.Props,Color.White)
        {
            Centered = true;
            SpriteNode spriteShadow = new SpriteNode(Services.Get<IAssets>().GetAsset<Texture2D>("BallShadow"), Layers.Shadows);
            spriteShadow.Centered = true;
            AddChild(spriteShadow);
            _bounds = bounds;
        }

        public override void UpdatePhysics(float deltaTime)
        {
            Position += _velocity * deltaTime;
            CheckBricksCollisions(Services.Get<INodeTree>().GetNodes<Brick>());
            CheckPadsCollisions(Services.Get<INodeTree>().GetNodes<Pad>());
            CheckingBounds(_bounds);
            base.UpdatePhysics(deltaTime);
        }

        public void CheckPadsCollisions(List<Pad> pads)
        {
            for (int i = 0; i < pads.Count; i++)
            {
                Pad pad = pads[i];
                if (IsIntersect(pad.Position, pad.Width, pad.Height))
                {
                    Sides collisionSide = getSideCollision(pad.Position, pad.Width, pad.Height);
                    switch (collisionSide)
                    {
                        case Sides.Left:
                            _velocity.X = -_velocity.X;
                            Position.X = pad.Left - _radius;
                            break;
                        case Sides.Right:
                            _velocity.X = -_velocity.X;
                            Position.X = pad.Right + _radius;
                            break;
                        case Sides.Bottom:
                            _velocity.Y = -_velocity.Y;
                            Position.Y = pad.Bottom + _radius;
                            break;
                        case Sides.Top:
                            _velocity.Y = -_velocity.Y;
                            Position.Y = pad.Top - _radius;
                            break;
                    }
                    //pad.Hit(this, collisionSide);
                    return;
                }
            }
        }

        public void CheckBricksCollisions(List<Brick> bricks)
        {
            for (int i = 0; i < bricks.Count; i++)
            {
                Brick testedBrick = bricks[i];

                if (IsIntersect(testedBrick.Position, testedBrick.Width, testedBrick.Height))
                {
                    Sides collisionSide = getSideCollision(testedBrick.Position, testedBrick.Width, testedBrick.Height);
                    switch (collisionSide)
                    {
                        case Sides.Left:
                        case Sides.Right:
                            _velocity.X = -_velocity.X;
                            break;
                        case Sides.Bottom:
                        case Sides.Top:
                            _velocity.Y = -_velocity.Y;
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

        private Sides getSideCollision(Vector2 rectPosition, float rectWidth, float rectHeight)
        {
            if (_velocity.X == 0)
            {
                if (_velocity.Y > 0)
                    return Sides.Top;
                else
                    return Sides.Bottom;
            }
            else if (_velocity.Y == 0)
            {
                if (_velocity.X > 0)
                    return Sides.Left;
                else
                    return Sides.Right;
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
                        return Sides.Top;
                    else if (cy / cx < slope)
                        return Sides.Left;
                    else
                        return Sides.Top;
                }
                else if (slope < 0 && _velocity.X > 0) // Ball moving up right
                {
                    cx = rectPosition.X - Position.X;
                    cy = rectPosition.Y + rectHeight - Position.Y;
                    if (cx <= 0)
                        return Sides.Bottom;
                    else if (cy / cx < slope)
                        return Sides.Bottom;
                    else
                        return Sides.Left;
                }
                else if (slope > 0 && _velocity.X < 0) // Ball moving up left
                {
                    cx = rectPosition.X + rectWidth - Position.X;
                    cy = rectPosition.Y + rectHeight - Position.Y;
                    if (cx >= 0)
                        return Sides.Bottom;
                    else if (cy / cx < slope)
                        return Sides.Bottom;
                    else
                        return Sides.Right;
                }
                else // Ball moving down left
                {
                    cx = rectPosition.X + rectWidth - Position.X;
                    cy = rectPosition.Y - Position.Y;
                    if (cx >= 0)
                        return Sides.Top;
                    else if (cy / cx < slope)
                        return Sides.Top;
                    else
                        return Sides.Right;
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

            /*if (Position.Y - _radius < bounds.Top)
            {
                _velocity.Y = -_velocity.Y;
                Position.Y = bounds.Top + _radius;
            }*/

            /*if (Position.Y + _radius > bounds.Bottom)
            {
                _velocity.Y = -_velocity.Y;
                Position.Y = bounds.Bottom - _radius;
            }*/
        }
    }
}
