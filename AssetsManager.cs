using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;

namespace Animapix
{
    public sealed class AssetsManager : IAssets
    {
        private ContentManager _contentManager;
        private Dictionary<String, object> _assets = new Dictionary<string, object>();

        public AssetsManager(ContentManager contentManager)
        {
            _contentManager = contentManager;
            Services.RegisterService<IAssets>(this);
        }

        public void LoadAsset<T>(string name)
        { 
            T asset = this._contentManager.Load<T>(name);
            this._assets[name] = asset;
        }

        public T GetAsset<T>(string name)
        {
            return (T)_assets[name];
        }
    }
}
