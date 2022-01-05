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
        private const int _columns = 16;
        private const int _rows = 16;
        private Sides _levelSides = 0;
        private readonly Rectangle _bounds = new Rectangle(100, 100, 600, 600);

        private enum GameState
        {
            play,
            pause
        }

        private GameState _currenteState = GameState.play;

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

            

            LoadLevel();


            // Add ball
            Ball ball = new Ball(150, 150, _bounds, _levelSides);
            _nodeTree.Add(ball);

            LabelNode label = new LabelNode("100000",new Rectangle(0,0,900,100), assets.GetAsset<SpriteFont>("MainFont24"), Color.White, Layers.GUI);
            label.Position = new Vector2(screen.Width/2, 50);
            _nodeTree.Add(label);

        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            IScreen screen = Services.Get<IScreen>();


            if (_currenteState == GameState.pause)
            {
                Pause(true);
            }else
            {
                Pause(false);
            }

        }

        private void Pause(bool arg)
        {
            foreach (Ball ball in _nodeTree.GetNodes<Ball>())
            {
                ball.IsFreezed = arg;
            }
            foreach (Pad pad in _nodeTree.GetNodes<Pad>())
            {
                pad.IsFreezed = arg;
            }
        }

        private void LoadLevel()
        {
            IScreen screen = Services.Get<IScreen>();
            ILevels levelsData = Services.Get<ILevels>();

            LevelData level = levelsData.GetLevel(levelsData.CurrentLevel);

            for (int column = 0; column < level.bricks.Count; column++)
            {
                for (int row = 0; row < level.bricks[column].Count; row++)
                {
                    if(level.bricks[column][row] != 0)
                        AddNewBrick(column, row, 4, Color.White);
                }
            }

            // Check walls
            if (level.sides[0])// If side left
            { 
                _nodeTree.Add(new Wall(90, screen.Center.Y, Sides.Left));
            }
            else
            {
                _nodeTree.Add(new Pad(Sides.Left));
            }

            if (level.sides[1])// If side top
            {
                _nodeTree.Add(new Wall(screen.Center.X, 90, Sides.Top));
            }
            else
            {
                _nodeTree.Add(new Pad(Sides.Top));
            }

            if (level.sides[2])// If side right
            {
                _nodeTree.Add(new Wall(screen.Width - 90, screen.Center.Y, Sides.Right));
            }
            else
            {
                _nodeTree.Add(new Pad(Sides.Right));
            }

            if (level.sides[3])// If side bottom
            { 
                _nodeTree.Add(new Wall(screen.Center.X, screen.Height - 90, Sides.Bottom)); 
            }
            else
            {
                _nodeTree.Add(new Pad(Sides.Bottom));
            }
            _levelSides = level.GetSidesFlag();
        }

        private void AddNewBrick(int column, int row,float margin, Color color)
        {
            IScreen screen = Services.Get<IScreen>();
            Brick brick = new Brick();

            // Offset to center all bricks in screen
            float x = (screen.Width - _columns * (brick.Width + margin)) / 2 + margin/2;
            float y = (screen.Height - _rows * (brick.Height + margin)) / 2 + margin/2;

            // Set Position and color
            brick.Position = new Vector2((brick.Width + margin) * column + x, (brick.Height + margin) * row + y);
            brick.Color = color;

            _nodeTree.Add(brick);
        }
    }
}
