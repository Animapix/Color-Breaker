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
            SpriteNode sprite = new SpriteNode(100, 100, Color.White, assets.GetAsset<Texture2D>("Brick"));
            _nodeTree.Add(sprite);

        }

    }
}
