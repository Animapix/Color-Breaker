using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Color_Breaker
{
    public class SceneMenu : Scene
    {
        private SpriteNode _selectionRect;
        private int _selectedItem = 0;

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

            LabelNode titleLabel = new LabelNode("COLOR BREAKER", new Rectangle(0, 0, 800, 50), assets.GetAsset<SpriteFont>("MainFont24"), Color.White, Layers.GUI);
            titleLabel.Position = new Vector2(400, 200);
            _nodeTree.Add(titleLabel);

            LabelNode playLabel = new LabelNode("PLAY", new Rectangle(0, 0, 800, 50), assets.GetAsset<SpriteFont>("MainFont24"), Color.White, Layers.GUI);
            playLabel.Position = new Vector2(400, 500);
            _nodeTree.Add(playLabel);

            LabelNode quitLabel = new LabelNode("QUIT", new Rectangle(0, 0, 800, 50), assets.GetAsset<SpriteFont>("MainFont24"), Color.White, Layers.GUI);
            quitLabel.Position = new Vector2(400, 600);
            _nodeTree.Add(quitLabel);


            _selectionRect = new SpriteNode(assets.GetAsset<Texture2D>("SelectionRect"), Layers.GUI);
            _selectionRect.Centered = true;
            _selectionRect.Position.X = 400;
            _selectionRect.Position.Y = 500;
            _nodeTree.Add(_selectionRect);
        }

        public override void Update(float deltaTime)
        {
            IInputs inputs = Services.Get<IInputs>();

            if (inputs.IsJustPressed(Keys.Up))
            {
                _selectedItem--;
                if (_selectedItem < 0)
                {
                    _selectedItem = 1;
                }
            }
            else if (inputs.IsJustPressed(Keys.Down))
            {
                _selectedItem++;
                if (_selectedItem > 1)
                {
                    _selectedItem = 0;
                }
            }
            else if (inputs.IsJustPressed(Keys.Enter))
            {
                switch (_selectedItem)
                {
                    case 0:
                        Services.Get<ISceneManager>().Load(Scenes.LevelSelection);
                        break;
                    default:
                        Services.Get<IScreen>().Quit();
                        break;
                }
            }

            _selectionRect.Position.Y = 500 + _selectedItem * 100;

            base.Update(deltaTime);
        }

    }
}
