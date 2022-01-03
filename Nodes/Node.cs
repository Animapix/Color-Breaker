using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Color_Breaker
{
    public class Node
    {
        public Node Parent { get; private set; }
        public List<Node> Children { get; private set; }
        public Layer Layer = Layer.none;

        public Vector2 Position;
        public Vector2 GlobalPosition
        {
            get
            {
                if (Parent != null)
                    return Position + Parent.GlobalPosition;
                else
                    return Position;
            }

            
        }

        public Node() : this(Vector2.Zero) { }
        public Node(Layer layer) : this(Vector2.Zero, layer) { }
        public Node(Vector2 position, Layer layer = Layer.none)
        {
            Position = position;
            Children = new List<Node>();
            Layer = layer;
        }

        public void AddChild(Node child)
        {
            child.Parent = this;
            Children.Add(child);
        }

        public List<Node> GetAllChildren()
        {
            List<Node> result = new List<Node>();

            foreach (Node child in Children)
            {
                result.Add(child);
                result.AddRange(child.GetAllChildren());
            }

            return result;
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
        }
    }
}
