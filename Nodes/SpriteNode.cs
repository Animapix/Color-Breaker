using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Color_Breaker
{
    public class SpriteNode : Node
    {
        public Color Color;
        public int Frame;
        public bool Centered = false;
        public Vector2 Scale = new Vector2(1, 1);
        protected Texture2D _texture;
        private (int horizontal, int vertical) _split;

        public int Width      { get => _texture.Width  / _split.horizontal; }
        public int Height     { get => _texture.Height / _split.vertical;   }
        
        public Vector2 Center
        {
            get
            {
                if (Centered)
                    return GlobalPosition;
                else
                    return GlobalPosition + new Vector2(Width / 2, Height / 2);
            }
        }

        public float Left {
            get
            {
                if (Centered)
                    return GlobalPosition.X - Width/2;
                else
                    return GlobalPosition.X;
            }
        }

        public float Top
        {
            get
            {
                if (Centered)
                    return GlobalPosition.Y - Height / 2;
                else
                    return GlobalPosition.Y;
            }
        }

        public float Right
        {
            get
            {
                if (Centered)
                    return GlobalPosition.X + Width / 2;
                else
                    return GlobalPosition.X + Width;
            }
        }

        public float Bottom
        {
            get
            {
                if (Centered)
                    return GlobalPosition.Y + Height / 2;
                else
                    return GlobalPosition.Y + Height;
            }
        }
        public SpriteNode(Texture2D texture,Layers layer = Layers.None) : this(texture, 0,0,layer,Color.White) { }
        public SpriteNode(Texture2D texture, float x, float y,Layers layer, Color color)
        {
            Position = new Vector2(x,y);
            Frame = 0;
            Color = color;
            Layer = layer;
            _texture = texture;
            _split = (1, 1);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Vector2 pos = GlobalPosition;
            if (Centered)
                pos -= new Vector2(Width / 2, Height / 2);
            
            spriteBatch.Draw(_texture, pos, GetCurrentFrameRectangle(Frame), Color, 0, Vector2.Zero, Scale, SpriteEffects.None, 0);
            base.Draw(spriteBatch);
        }

        private Rectangle GetCurrentFrameRectangle(int frame)
        {
            double column = frame % _split.horizontal;
            double row = frame / _split.vertical;

            double x = column * Width;
            double y = row * Height;

            return new Rectangle((int)x, (int)y, Width, Height);
        }
    }
}
