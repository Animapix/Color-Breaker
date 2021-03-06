using Microsoft.Xna.Framework.Graphics;
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

        public List<T> GetNodes<T>()
        {
            List<T> nodes = new List<T>();

            foreach (Object node in _nodes)
            {
                if (node.GetType() == typeof(T))
                    nodes.Add((T)node);

                foreach (Object child in ((Node)node).GetAllChildren())
                {
                    if (child.GetType() == typeof(T))
                        nodes.Add((T)child);
                }
            }
            return nodes;
        }

        public void Update(float deltaTime)
        {
            float dtDiv = deltaTime / 50;
            for (int i = 0; i < 50; i++)
            {
                for (int j = 0; j < _nodes.Count; j++)
                {
                    _nodes[j].UpdatePhysics(dtDiv);
                }
            }

            for (int i = 0; i < _nodes.Count; i++)
            {
                _nodes[i].Update(deltaTime);
            }


            for (int i = _nodes.Count - 1 ; i >= 0; i--)
            {
                if (_nodes[i].Free)
                    _nodes.RemoveAt(i);
            }
        }


        public void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Begin();

            Dictionary<Layers, List<Node>> layers = new Dictionary<Layers, List<Node>>();
            foreach (Layers layerKey in Enum.GetValues(typeof(Layers)))
            {
                layers[layerKey] = new List<Node>();
            }

            foreach (Node node in _nodes)
            {
                if (node.Layer != Layers.None)
                    layers[node.Layer].Add(node);

                foreach (Node child in node.GetAllChildren())
                {
                    if (child.Layer != Layers.None)
                        layers[child.Layer].Add(child);
                }
            }

            foreach (Layers layerKey in Enum.GetValues(typeof(Layers)))
            {
                foreach (Node node in layers[layerKey])
                {
                    node.Draw(spriteBatch);
                }
            }


            spriteBatch.End();
        }
    }
}
