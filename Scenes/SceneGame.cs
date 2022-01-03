using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Color_Breaker
{
    public class SceneGame : Scene
    {

        public override void Load()
        {
            base.Load();
            IAssets assets = Services.Get<IAssets>();

            SpriteNode background = new SpriteNode(assets.GetAsset<Texture2D>("Background"));
            _nodeTree.Add(background);

            Ball ball = new Ball(100, 200);
            _nodeTree.Add(ball);
        }

    }
}
