﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Color_Breaker

{
    public abstract class Scene
    {
        protected NodeTree _nodeTree;

        public Scene()
        {
            _nodeTree = new NodeTree();
        }

        public virtual void Load()
        {

        }

        public virtual void Update(GameTime gameTime)
        {
            _nodeTree.Update(gameTime);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            _nodeTree.Draw(spriteBatch);
        }

        public virtual void Unload()
        {

        }

    }
}
