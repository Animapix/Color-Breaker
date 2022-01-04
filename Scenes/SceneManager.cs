using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Color_Breaker
{
    public sealed class SceneManager : ISceneManager
    {
        private Scene _currentScene;
        private Dictionary<Scenes, Scene> scenes = new Dictionary<Scenes, Scene>();

        public SceneManager()
        {
            Services.RegisterService<ISceneManager>(this);
        }

        public void Register(Scenes sceneType, Scene scene)
        {
            scenes[sceneType] = scene;
        }

        public void Load(Scenes sceneType)
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
