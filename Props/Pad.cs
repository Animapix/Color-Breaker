using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace Color_Breaker
{
    public sealed class Pad : SpriteNode
    {
        private Vector2 _velocity = new Vector2(0,0);

        public Sides Side;

        public Pad(Sides side) : base(Services.Get<IAssets>().GetAsset<Texture2D>("Pad"), Layers.Props)
        {
            Side = side;

            switch (side)
            {
                case Sides.Left:
                    break;
                case Sides.Top:
                    Position.Y = 80;
                    break;
                case Sides.Right:
                    break;
                case Sides.Bottom:
                    Position.Y = 700;
                    break;
            }


            SpriteNode spriteShadow = new SpriteNode(Services.Get<IAssets>().GetAsset<Texture2D>("PadShadow"), Layers.Shadows);
            spriteShadow.Centered = true;
            spriteShadow.Position = Center - Position;

            Debug.WriteLine(spriteShadow.Position);
            AddChild(spriteShadow);
        }

        public override void UpdatePhysics(float deltaTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                Position.X -= 800 * deltaTime;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                Position.X += 800 * deltaTime;
            }

            base.UpdatePhysics(deltaTime);
        }

    }
}
