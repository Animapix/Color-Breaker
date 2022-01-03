using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Color_Breaker
{
    public class Ball : Node
    {

        private Vector2 _velocity = new Vector2(0, 0);
        
        public Ball(float x, float y) : base(new Vector2(x,y))
        {
            IAssets assets = Services.Get<IAssets>();
            SpriteNode sprite = new SpriteNode(0, 0, Color.White, assets.GetAsset<Texture2D>("Ball"));
            SpriteNode spriteShadow = new SpriteNode(0, 0, Color.White, assets.GetAsset<Texture2D>("BallShadow"));
            sprite.Centered = true;
            spriteShadow.Centered = true;
            AddChild(spriteShadow);
            AddChild(sprite);
        }


    }
}
