using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Color_Breaker
{
    public sealed class SceneManager
    {
        private Scene _currentScene;
        private Dictionary<SceneType, Scene> scenes = new Dictionary<SceneType, Scene>();

        public void Register(SceneType sceneType, Scene scene)
        {
            scenes[sceneType] = scene;
        }

        public void Load(SceneType sceneType)
        {
            if (_currentScene != null)
            {
                _currentScene.Unload();
                _currentScene = null;
            }
            _currentScene = scenes[sceneType];
            _currentScene.Load();
        }


        public void Update(float deltaTime)
        {
            if (_currentScene == null) return;
            _currentScene.Update(deltaTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (_currentScene == null) return;
            _currentScene.Draw(spriteBatch);
        }

    }
}
