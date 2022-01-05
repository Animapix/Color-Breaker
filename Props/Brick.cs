using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

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
            Services.Get<IAssets>().GetAsset<SoundEffect>("Bip5").Play();
            if (ball.Color == Color || Color == Color.White)
            {
                ParticleEmiterNode emiter = new ParticleEmiterNode(Services.Get<IAssets>().GetAsset<Texture2D>("Brick"), Color);
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
