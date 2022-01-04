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
            float dtDiv = deltaTime / 100;
            for (int i = 0; i < 100; i++)
            {
                foreach (Node node in _nodes)
                {
                    node.UpdatePhysics(dtDiv);
                }
            }
            

            foreach (Node node in _nodes)
            {
                node.Update(deltaTime);
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
