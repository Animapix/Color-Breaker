using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
            SpriteNode background = new SpriteNode(assets.GetAsset<Texture2D>("Background"), Layer.Background);
            background.Centered = true;
            background.Position = screen.Center;
            _nodeTree.Add(background);

            // Add wall sprites
            Wall wallTop = new Wall(screen.Center.X, 90, Side.Top);
            Wall wallLeft = new Wall(90, screen.Center.Y, Side.Left);
            Wall wallBottom = new Wall(screen.Center.X, screen.Height - 90, Side.Bottom);
            Wall wallRight = new Wall(screen.Width - 90, screen.Center.Y, Side.Right);
            _nodeTree.Add(wallLeft);
            _nodeTree.Add(wallRight);
            _nodeTree.Add(wallBottom);
            _nodeTree.Add(wallTop);

            LoadLevel();

            // Add ball
            Ball ball = new Ball(150, 150, new Rectangle(100, 100, 600, 600));
            _nodeTree.Add(ball);
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
