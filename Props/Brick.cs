using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace Color_Breaker
{
    public class Brick : SpriteNode
    {
        public Brick(float x = 0, float y = 0) : base(Services.Get<IAssets>().GetAsset<Texture2D>("Brick"), Layer.props)
        {
            SpriteNode spriteShadow = new SpriteNode(Services.Get<IAssets>().GetAsset<Texture2D>("BrickShadow"), Layer.Shadows);
            spriteShadow.Centered = true;
            spriteShadow.Position = Center;
            AddChild(spriteShadow);
        }

        public void Hit(Ball ball, Side collisionSide)
        {
            Free = true;
        }
    }
}
