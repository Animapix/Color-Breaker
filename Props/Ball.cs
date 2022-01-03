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
            SpriteNode sprite = new SpriteNode(assets.GetAsset<Texture2D>("Ball"), Layer.props);
            SpriteNode spriteShadow = new SpriteNode(assets.GetAsset<Texture2D>("BallShadow"), Layer.Shadows);
            sprite.Centered = true;
            spriteShadow.Centered = true;
            AddChild(sprite);
            sprite.AddChild(spriteShadow);
        }

        

    }
}
