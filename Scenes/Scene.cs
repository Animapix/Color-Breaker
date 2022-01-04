using Microsoft.Xna.Framework.Graphics;

namespace Color_Breaker
{
    public abstract class Scene
    {
        protected NodeTree _nodeTree;

        public virtual void Load()
        {
            _nodeTree = new NodeTree();
        }

        public virtual void Update(float deltaTime)
        {
            if (_nodeTree == null)
                return;
            _nodeTree.Update(deltaTime);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (_nodeTree == null)
                return;
            _nodeTree.Draw(spriteBatch);
        }

        public virtual void Unload()
        {
            _nodeTree = null;
        }

    }
}
