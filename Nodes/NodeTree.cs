using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Animapix
{
    public sealed class NodeTree : INodeTree
    {
        private List<Node> _nodes = new List<Node>();

        public NodeTree()
        {

        }

        public void Add(Node node)
        {
            _nodes.Add(node);
        }

        public void Update(GameTime gameTime)
        {
            foreach (Node node in _nodes)
            {
                node.Update(gameTime);
            }
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            foreach (Node node in _nodes)
            {
                node.Draw(spriteBatch);
            }
            spriteBatch.End();
        }
    }
}
