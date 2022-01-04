using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Color_Breaker
{
    public class SceneGame : Scene
    {
        private const int columns = 16;
        private const int rows = 16;

        public override void Load()
        {
            base.Load();
            IAssets assets = Services.Get<IAssets>();
            IScreen screen = Services.Get<IScreen>();

            // Add Background sprite
            SpriteNode background = new SpriteNode(assets.GetAsset<Texture2D>("Background"), Layers.Background);
            background.Centered = true;
            background.Position = screen.Center;
            _nodeTree.Add(background);

            // Add wall sprites
            Wall wallTop = new Wall(screen.Center.X, 90, Sides.Top);
            Wall wallLeft = new Wall(90, screen.Center.Y, Sides.Left);
            Wall wallBottom = new Wall(screen.Center.X, screen.Height - 90, Sides.Bottom);
            Wall wallRight = new Wall(screen.Width - 90, screen.Center.Y, Sides.Right);
            _nodeTree.Add(wallLeft);
            _nodeTree.Add(wallRight);
            //_nodeTree.Add(wallBottom);
            _nodeTree.Add(wallTop);

            LoadLevel();

            _nodeTree.Add(new Pad(Sides.Bottom));
            //_nodeTree.Add(new Pad(Sides.Top));
            //_nodeTree.Add(new Pad(Sides.Left));
            //_nodeTree.Add(new Pad(Sides.Right));

            // Add ball
            Ball ball = new Ball(150, 150, new Rectangle(100, 100, 600, 600));
            _nodeTree.Add(ball);

            LabelNode label = new LabelNode("100000",new Rectangle(0,0,900,100), assets.GetAsset<SpriteFont>("MainFont24"), Color.White, Layers.GUI);
            label.Position = new Vector2(screen.Width/2, 50);
            _nodeTree.Add(label);

        }


        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            IScreen screen = Services.Get<IScreen>();

            foreach (Ball ball in _nodeTree.GetNodes<Ball>())
            {
                if( ball.Position.X < screen.Bounds.Left  || 
                    ball.Position.X > screen.Bounds.Right ||
                    ball.Position.Y < screen.Bounds.Top   ||
                    ball.Position.Y > screen.Bounds.Bottom)
                {

                    ball.Free = true;
                    _nodeTree.Add(new Ball(150, 150, new Rectangle(100, 100, 600, 600)));
                }
            }

        }

        private void LoadLevel()
        {
            LevelData level = Services.Get<ILevels>().GetLevel(0);
            for (int column = 0; column < level.bricks.Count; column++)
            {
                for (int row = 0; row < level.bricks[column].Count; row++)
                {
                    if(level.bricks[column][row] != 0)
                        AddNewBrick(column, row, 4, Color.White);
                }
            }
        }

        private void AddNewBrick(int column, int row,float margin, Color color)
        {
            IScreen screen = Services.Get<IScreen>();
            Brick brick = new Brick();

            // Offset to center all bricks in screen
            float x = (screen.Width - columns * (brick.Width + margin)) / 2 + margin/2;
            float y = (screen.Height - rows * (brick.Height + margin)) / 2 + margin/2;

            // Set Position and color
            brick.Position = new Vector2((brick.Width + margin) * column + x, (brick.Height + margin) * row + y);
            brick.Color = color;

            _nodeTree.Add(brick);
        }
    }
}
