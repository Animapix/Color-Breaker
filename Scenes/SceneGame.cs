using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Color_Breaker
{
    public class SceneGame : Scene
    {
        private const int _columns = 16;
        private const int _rows = 16;
        private Sides _levelSides = 0;
        private readonly Rectangle _bounds = new Rectangle(100, 100, 600, 600);
        private int _lifes;

        private float _startDelay = 0f;

        private enum GameState
        {
            start,
            play,
            pause,
            gameOver,
            win
        }

        private GameState _currenteState = GameState.start;

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


            _lifes = 3;
            _currenteState = GameState.start;
            _startDelay = 0f;
            Pause(true);
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            IScreen screen = Services.Get<IScreen>();
            IInputs inputs = Services.Get<IInputs>();

            switch (_currenteState)
            {
                case GameState.start: //----------------------------------------- Start ---------------------------
                    _startDelay += deltaTime;
                    if (_startDelay >= _nodeTree.GetNodes<Brick>().Count * 0.01f + 0.5f)
                    {
                        _currenteState = GameState.play;
                        Pause(false);
                    }
                    break;
                case GameState.play: // ----------------------------------------- GamePlay -------------------------

                    // If no more balls in arena
                    if (_nodeTree.GetNodes<Ball>().Count == 0)
                    {
                        _lifes--;

                        if (_lifes >= 0)
                        {
                            // Spawn new Ball
                            List<Pad> pads = _nodeTree.GetNodes<Pad>();
                            Ball ball = new Ball(_bounds, _levelSides);
                            _nodeTree.Add(ball);
                            pads[0].StickBall(ball);
                        }
                        else
                        {
                            // GameOver
                            IAssets assets = Services.Get<IAssets>();

                            

                            LabelNode label = new LabelNode("GAMEOVER", new Rectangle(0, 0, 900, 900), assets.GetAsset<SpriteFont>("MainFont24"), Color.White, Layers.GUI);
                            label.Position = screen.Center;
                            _nodeTree.Add(label);

                            LabelNode infos = new LabelNode("Press enter", new Rectangle(0, 0, 900, 900), assets.GetAsset<SpriteFont>("MainFont12"), Color.White, Layers.GUI);
                            infos.Position.X = screen.Center.X;
                            infos.Position.Y = screen.Bounds.Bottom - 50;
                            _nodeTree.Add(infos);

                            _currenteState = GameState.gameOver;
                            Pause(true);
                        }

                    }

                    if (_nodeTree.GetNodes<Brick>().Count == 0)
                    {
                        // Win
                        IAssets assets = Services.Get<IAssets>();

                        SpriteNode softBalck = new SpriteNode(assets.GetAsset<Texture2D>("SoftBlack"), Layers.Background);
                        softBalck.Centered = true;
                        softBalck.Position = screen.Center;
                        _nodeTree.Add(softBalck);

                        LabelNode label = new LabelNode("WINNER", new Rectangle(0, 0, 900, 900), assets.GetAsset<SpriteFont>("MainFont24"), Color.White, Layers.GUI);
                        label.Position = screen.Center;
                        _nodeTree.Add(label);

                        LabelNode infos = new LabelNode("Press enter", new Rectangle(0, 0, 900, 900), assets.GetAsset<SpriteFont>("MainFont12"), Color.White, Layers.GUI);
                        infos.Position.X = screen.Center.X;
                        infos.Position.Y = screen.Bounds.Bottom - 50;
                        _nodeTree.Add(infos);

                        _currenteState = GameState.win;
                        Pause(true);
                    }

                    if (inputs.IsJustPressed(Keys.Escape))
                    {
                        Services.Get<ISceneManager>().Load(Scenes.Menu);
                    }

                    break;
                case GameState.pause: // ----------------------------------------- Pause -------------------------

                    if (inputs.IsJustPressed(Keys.Escape))
                    {
                        _currenteState = GameState.play;
                        Pause(false);
                    }

                    break;
                case GameState.gameOver: // ----------------------------------------- GameOver -------------------------

                    if (inputs.IsJustPressed(Keys.Enter))
                    {
                        Services.Get<ISceneManager>().Load(Scenes.Menu);
                    }

                    break;
                case GameState.win: // ----------------------------------------- Win -------------------------

                    if (inputs.IsJustPressed(Keys.Enter))
                    {
                        Services.Get<ISceneManager>().Load(Scenes.Menu);
                    }

                    break;
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
                    Color brickColor;
                    switch (level.bricks[column][row])
                    {
                        case 2:
                            brickColor = Color.Red;
                            break;
                        case 3:
                            brickColor = Color.Green;
                            break;
                        case 4:
                            brickColor = Color.Blue;
                            break;
                        default:
                            brickColor = Color.White;
                            break;
                    }
                    if (level.bricks[column][row] > 0)
                        AddNewBrick(column, row, 4, brickColor, level.bricks[column][row]);
                }
            }
            _levelSides = level.GetSidesFlag();

            // Check walls
            if (level.sides[0])// If side left
                _nodeTree.Add(new Wall(90, screen.Center.Y, Sides.Left));
            else
                _nodeTree.Add(new Pad(_bounds, Sides.Left));

            if (level.sides[1])// If side top
                _nodeTree.Add(new Wall(screen.Center.X, 90, Sides.Top));
            else
                _nodeTree.Add(new Pad(_bounds,Sides.Top));

            if (level.sides[2])// If side right
                _nodeTree.Add(new Wall(screen.Width - 90, screen.Center.Y, Sides.Right));
            else
                _nodeTree.Add(new Pad(_bounds,Sides.Right));

            if (level.sides[3])// If side bottom
                _nodeTree.Add(new Wall(screen.Center.X, screen.Height - 90, Sides.Bottom)); 
            else
                _nodeTree.Add(new Pad(_bounds,Sides.Bottom));

        }

        private void AddNewBrick(int column, int row,float margin, Color color, int type)
        {
            IScreen screen = Services.Get<IScreen>();
            Brick brick = new Brick();

            // Offset to center all bricks in screen
            float x = (screen.Width - _columns * (brick.Width + margin)) / 2 + margin/2;
            float y = (screen.Height - _rows * (brick.Height + margin)) / 2 + margin/2;

            // Set Position and color
            brick.Position = new Vector2((brick.Width + margin) * column + x, -200);
            brick.Color = color;
            brick.Tween.Start(-200,(brick.Height + margin) * row + y,0.5f, _nodeTree.GetNodes<Brick>().Count * 0.01f);
            _nodeTree.Add(brick);
        }

    }
}
