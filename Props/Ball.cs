using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Color_Breaker
{
    public sealed class Ball : SpriteNode
    {

        private Vector2 _velocity = new Vector2(100, 100);
        public const float Speed = 400;
        public const float Radius = 10;
        private Rectangle _bounds;
        private Sides _boundsSides;


        public Ball(Rectangle bounds, Sides boundsSides) : this(0,0,bounds, boundsSides) { }
        public Ball(float x, float y, Rectangle bounds, Sides boundsSides) : base(Services.Get<IAssets>().GetAsset<Texture2D>("Ball"), x, y, Layers.Props,Color.White)
        {
            Centered = true;
            SpriteNode spriteShadow = new SpriteNode(Services.Get<IAssets>().GetAsset<Texture2D>("BallShadow"), Layers.Shadows);
            spriteShadow.Centered = true;
            AddChild(spriteShadow);
            _bounds = bounds;
            _boundsSides = boundsSides;
        }

        public void Launch(Vector2 dir)
        {
            _velocity = Vector2.Normalize(dir) * Speed;
        }

        public override void UpdatePhysics(float deltaTime)
        {
            if (IsFreezed) return;
            Position += _velocity * deltaTime;
            CheckBricksCollisions(Services.Get<INodeTree>().GetNodes<Brick>());
            CheckPadsCollisions(Services.Get<INodeTree>().GetNodes<Pad>());
            CheckingBounds(_bounds);
            base.UpdatePhysics(deltaTime);
        }

        public override void Update(float deltaTime)
        {
            if (IsFreezed) return;
            CheckOutOfScreen();
            base.Update(deltaTime);
        }

        public void CheckOutOfScreen()
        {
            IScreen screen = Services.Get<IScreen>();

            if (Position.X < screen.Bounds.Left ||
                    Position.X > screen.Bounds.Right ||
                    Position.Y < screen.Bounds.Top ||
                    Position.Y > screen.Bounds.Bottom)
            {

                Free = true;
                ParticleEmiterNode emiter = new ParticleEmiterNode(Services.Get<IAssets>().GetAsset<Texture2D>("Brick"), Color);
                emiter.Position = Center;
                Services.Get<INodeTree>().Add(emiter);
                Services.Get<IAssets>().GetAsset<SoundEffect>("Explosion").Play();
            }
        }

        public void CheckPadsCollisions(List<Pad> pads)
        {
            for (int i = 0; i < pads.Count; i++)
            {
                Pad pad = pads[i];
                if (IsIntersect(pad.Position, pad.Width, pad.Height))
                {
                    Sides collisionSide = getSideCollision(pad.Position, pad.Width, pad.Height);
                    Position -= Vector2.Normalize(_velocity);
                    float value = 0;

                    switch (collisionSide)
                    {
                        case Sides.Left:
                        case Sides.Right:
                            value = (Position.Y - pad.Position.Y) / pad.Height - 0.5f;
                            _velocity.X = -_velocity.X;
                            break;
                        case Sides.Bottom:
                        case Sides.Top:
                            value = (Position.X - pad.Position.X) / pad.Width - 0.5f;
                            _velocity.Y = -_velocity.Y;
                            break;
                    }

                    // Check collision influence
                    float offsetAngle = (float)Math.PI / 2 * value;
                    float collisionAngle = Util.VectorToAngle(_velocity);

                    if (pad.Side == Sides.Left && collisionSide == Sides.Right)
                    {
                        collisionAngle += offsetAngle;
                        //collisionAngle = (float)Math.Clamp(collisionAngle, Math.PI / 4, 3 * Math.PI / 4);
                        _velocity = Vector2.Normalize(Util.AngleToVector(collisionAngle)) * Speed;
                    }
                    else if (pad.Side == Sides.Right && collisionSide == Sides.Left)
                    {
                        collisionAngle -= offsetAngle;
                        //collisionAngle = (float)Math.Clamp(collisionAngle, 3* Math.PI / 4, 5 * Math.PI / 4); Check clamp wirh negative and positive value -180 180
                        _velocity = Vector2.Normalize(Util.AngleToVector(collisionAngle)) * Speed;
                    }
                    else if (pad.Side == Sides.Top && collisionSide == Sides.Bottom)
                    {
                        collisionAngle -= offsetAngle;
                        collisionAngle = (float)Math.Clamp(collisionAngle, Math.PI / 4, 3 * Math.PI / 4 );
                        _velocity = Vector2.Normalize(Util.AngleToVector(collisionAngle)) * Speed;
                    }
                    else if (pad.Side == Sides.Bottom && collisionSide == Sides.Top)
                    {
                        collisionAngle += offsetAngle;
                        collisionAngle = (float)Math.Clamp(collisionAngle, -3*Math.PI/4, -Math.PI / 4);
                        _velocity = Vector2.Normalize(Util.AngleToVector(collisionAngle)) * Speed;
                    }


                    Services.Get<IAssets>().GetAsset<SoundEffect>("Bip2").Play();
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
                    Position -= Vector2.Normalize(_velocity);
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
            if (Position.Y - Radius > rectPosition.Y + rectHeight)
            {
                return false;
            }
            if (Position.Y + Radius < rectPosition.Y)
            {
                return false;
            }
            if (Position.X - Radius > rectPosition.X + rectWidth)
            {
                return false;
            }
            if (Position.X + Radius < rectPosition.X)
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
                    else if (cy / cx > slope)
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
            if ((_boundsSides & Sides.Left) == Sides.Left){
                if (Position.X - Radius < bounds.Left)
                {
                    _velocity.X = -_velocity.X;
                    Position.X = bounds.Left + Radius;
                    Services.Get<IAssets>().GetAsset<SoundEffect>("Bip1").Play();
                }
            }


            if ((_boundsSides & Sides.Right) == Sides.Right)
            {
                if (Position.X + Radius > bounds.Right)
                {
                    _velocity.X = -_velocity.X;
                    Position.X = bounds.Right - Radius;
                    Services.Get<IAssets>().GetAsset<SoundEffect>("Bip1").Play();
                }
            }

            if ((_boundsSides & Sides.Top) == Sides.Top)
            {
                if (Position.Y - Radius < bounds.Top)
                {
                    _velocity.Y = -_velocity.Y;
                    Position.Y = bounds.Top + Radius;
                    Services.Get<IAssets>().GetAsset<SoundEffect>("Bip1").Play();
                }
            }


            if ((_boundsSides & Sides.Bottom) == Sides.Bottom){
                if (Position.Y + Radius > bounds.Bottom)
                {
                    _velocity.Y = -_velocity.Y;
                    Position.Y = bounds.Bottom - Radius;
                    Services.Get<IAssets>().GetAsset<SoundEffect>("Bip1").Play();
                }
            }
        }
    }
}
