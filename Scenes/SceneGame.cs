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
            IScreen screen = Services.Get<IScreen>();

            SpriteNode background = new SpriteNode(assets.GetAsset<Texture2D>("Background"), Layer.Background);
            _nodeTree.Add(background);


            Wall wallTop = new Wall(screen.Center.X, 90, Side.Top);
            Wall wallLeft = new Wall(190, screen.Center.Y, Side.Left);
            Wall wallBottom = new Wall(screen.Center.X, screen.Height - 90, Side.Bottom);
            Wall wallRight = new Wall(screen.Width - 190, screen.Center.Y, Side.Right);

            _nodeTree.Add(wallLeft);
            _nodeTree.Add(wallRight);
            _nodeTree.Add(wallBottom);
            _nodeTree.Add(wallTop);

            Ball ball = new Ball(100, 200, new Rectangle(200,100,1200,700));
            _nodeTree.Add(ball);
        }
    }
}
