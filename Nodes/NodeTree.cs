using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;
using System.Collections.Generic;
using System;

namespace Color_Breaker
{
    public sealed class NodeTree : INodeTree
    {
        private List<Node> _nodes = new List<Node>();

        public NodeTree()
        {
            Services.RegisterService<INodeTree>(this);
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

            

            Dictionary<Layer, List<Node>> layers = new Dictionary<Layer, List<Node>>();
            foreach (Layer layerKey in Enum.GetValues(typeof(Layer)))
            {
                layers[layerKey] = new List<Node>();
            }

            foreach (Node node in _nodes)
            {
                if (node.Layer != Layer.none)
                    layers[node.Layer].Add(node);

                foreach (Node child in node.GetAllChildren())
                {
                    if (child.Layer != Layer.none)
                        layers[child.Layer].Add(child);
                }
            }

            foreach (Layer layerKey in Enum.GetValues(typeof(Layer)))
            {
                foreach (Node node in layers[layerKey])
                {
                    node.Draw(spriteBatch);
                }
            }
            /*
            foreach (Node node in _nodes)
            {
                node.Draw(spriteBatch);
                foreach (Node child in node.Children)
                {
                    child.Draw(spriteBatch);
                }
            }*/

            spriteBatch.End();
        }
    }
}
