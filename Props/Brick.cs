using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace Color_Breaker
{
    public class Brick : SpriteNode
    {
        public Brick(float x = 0, float y = 0) : base(Services.Get<IAssets>().GetAsset<Texture2D>("Brick"), Layers.Props)
        {
            SpriteNode spriteShadow = new SpriteNode(Services.Get<IAssets>().GetAsset<Texture2D>("BrickShadow"), Layers.Shadows);
            spriteShadow.Centered = true;
            spriteShadow.Position = Center;
            AddChild(spriteShadow);
        }

        public virtual void Hit(Ball ball, Sides collisionSide)
        {
            if (ball.Color == Color || Color == Color.White)
            {
                ParticleEmiterNode emiter = new ParticleEmiterNode(Services.Get<IAssets>().GetAsset<Texture2D>("Brick"));
                emiter.Position = Center;
                Services.Get<INodeTree>().Add(emiter);
                Free = true;
            }
            else
            {
                ball.Color = Color;
            }
        }
    }

}
