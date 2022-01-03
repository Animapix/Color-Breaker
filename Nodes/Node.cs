using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Animapix.Nodes
{
    public class Node
    {

        public Node Parent { get; private set; }
        public List<Node> Children { get; private set; }

        public Vector2 Position;
        public Vector2 GlobalPosition
        {
            get
            {
                if (Parent != null)
                    return Position + Parent.Position;
                else
                    return Position;
            }
        }

        public Node() : this(Vector2.Zero) { }
        public Node(Vector2 position)
        {
            Position = position;
            Children = new List<Node>();
        }

        public void AddChild(Node child)
        {
            child.Parent = this;
            Children.Add(child);
        }

        public virtual void Update(GameTime gameTime)
        {
            foreach (Node child in Children)
            {
                child.Update(gameTime);
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            foreach (Node child in Children)
            {
                child.Draw(spriteBatch);
            }
        }

    }
}
