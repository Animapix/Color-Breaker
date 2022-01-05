using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace Color_Breaker
{
    public class SceneLevelSelection : Scene
    {
        private SpriteNode _selectionRect;
        private int _selectedItem = 0;
        private int _itemsCount = 0;
        private Tween tween = new Tween();

        public override void Load()
        {
            
            base.Load();

            _selectedItem = 0;

            IAssets assets = Services.Get<IAssets>();
            IScreen screen = Services.Get<IScreen>();
            
            // Add Background sprite
            SpriteNode background = new SpriteNode(assets.GetAsset<Texture2D>("Background"), Layers.Background);
            background.Centered = true;
            background.Position = screen.Center;
            _nodeTree.Add(background);


            ILevels levelsData = Services.Get<ILevels>();
            int Y = 50;
            _itemsCount = levelsData.GetLevels().Count;
            foreach (LevelData level in levelsData.GetLevels())
            {
                LabelNode label = new LabelNode(level.name, new Rectangle(0, 0, 800, 20), assets.GetAsset<SpriteFont>("MainFont12"), Color.White, Layers.GUI);
                Y += 50;
                label.Position.Y = Y;
                label.Position.X = screen.Width/2;
                _nodeTree.Add(label);
            }

            LabelNode backLabel = new LabelNode("Back", new Rectangle(0, 0, 800, 20), assets.GetAsset<SpriteFont>("MainFont12"), Color.White, Layers.GUI);
            Y += 50;
            backLabel.Position.Y = Y;
            backLabel.Position.X = screen.Width / 2;
            _nodeTree.Add(backLabel);

            _selectionRect = new SpriteNode(assets.GetAsset<Texture2D>("SelectionRect2"), Layers.GUI);
            _selectionRect.Centered = true;
            _selectionRect.Position.X = screen.Width / 2;
            _selectionRect.Position.Y = 100;
            _nodeTree.Add(_selectionRect);
            tween.Start(0, 100, 1.0f);
        }

        public override void Update(float deltaTime)
        {
            IInputs inputs = Services.Get<IInputs>();

            if (inputs.IsJustPressed(Keys.Up))
            {
                _selectedItem--;
                if (_selectedItem < 0)
                {
                    _selectedItem = _itemsCount ;
                }
                tween.Start(_selectionRect.Position.Y, 100 + _selectedItem * 50, 0.3f);
            }
            else if (inputs.IsJustPressed(Keys.Down))
            {
                _selectedItem++;
                if (_selectedItem > _itemsCount)
                {
                    _selectedItem = 0;
                }
                tween.Start(_selectionRect.Position.Y, 100 + _selectedItem * 50, 0.3f);
            }
            else if (inputs.IsJustPressed(Keys.Enter))
            {
                if (_selectedItem == _itemsCount)
                {
                    Services.Get<ISceneManager>().Load(Scenes.Menu);
                }
                else
                {
                    Services.Get<ILevels>().CurrentLevel = _selectedItem;
                    Services.Get<ISceneManager>().Load(Scenes.Game);
                }
            }

            //_selectionRect.Position.Y = 100 + _selectedItem * 50;
            _selectionRect.Position.Y = tween.easeInOutSine(deltaTime);
            base.Update(deltaTime);
        }

    }
}
