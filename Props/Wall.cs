using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Color_Breaker
{
    public sealed class Wall : Node
    {

        public Wall(float x, float y, Sides side) : base(new Vector2(x,y))
        {
            IAssets assets = Services.Get<IAssets>();

            Texture2D texture = assets.GetAsset<Texture2D>("WallV");
            Texture2D shadowTexture = assets.GetAsset<Texture2D>("WallShadowV");
            if (side == Sides.Bottom || side == Sides.Top)
            {
                texture = assets.GetAsset<Texture2D>("WallH");
                shadowTexture = assets.GetAsset<Texture2D>("WallShadowH");
            }

            SpriteNode wallSprite = new SpriteNode(texture, Layers.Props);
            SpriteNode shadowSprite = new SpriteNode(shadowTexture, Layers.Shadows);
            wallSprite.Centered = true;
            shadowSprite.Centered = true;
            
            AddChild(wallSprite);
            AddChild(shadowSprite);

        }


    }
}
