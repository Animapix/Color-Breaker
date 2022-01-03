using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Color_Breaker
{
    public sealed class Wall : Node
    {

        public Wall(float x, float y, Side side) : base(new Vector2(x,y))
        {
            IAssets assets = Services.Get<IAssets>();

            Texture2D texture = assets.GetAsset<Texture2D>("Wall_V");
            Texture2D shadowTexture = assets.GetAsset<Texture2D>("Wall_V_Shadow");
            if (side == Side.Bottom || side == Side.Top)
            {
                texture = assets.GetAsset<Texture2D>("Wall_H");
                shadowTexture = assets.GetAsset<Texture2D>("Wall_H_Shadow");
            }

            SpriteNode wallSprite = new SpriteNode(texture, Layer.props);
            SpriteNode shadowSprite = new SpriteNode(shadowTexture, Layer.Shadows);
            wallSprite.Centered = true;
            shadowSprite.Centered = true;
            
            AddChild(wallSprite);
            AddChild(shadowSprite);

        }


    }
}
