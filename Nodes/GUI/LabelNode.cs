using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Color_Breaker
{
    public class LabelNode : Node
    {
        public string Text;
        public SpriteFont Font;
        public Color Color;
        public Rectangle Bounds;
        public Alignment Align;

        public LabelNode(string text, Rectangle bounds, SpriteFont font, Color color, Layers layer) : base(layer)
        {
            Text = text;
            Font = font;
            Color = color;
            Align = Alignment.Center;
            Bounds = bounds;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Vector2 size = Font.MeasureString(Text);
            Vector2 origin = size * 0.5f;

            if (Align.HasFlag(Alignment.Left))
                origin.X += Bounds.Width / 2 - size.X / 2;

            if (Align.HasFlag(Alignment.Right))
                origin.X -= Bounds.Width / 2 - size.X / 2;

            if (Align.HasFlag(Alignment.Top))
                origin.Y += Bounds.Height / 2 - size.Y / 2;

            if (Align.HasFlag(Alignment.Bottom))
                origin.Y -= Bounds.Height / 2 - size.Y / 2;

            spriteBatch.DrawString(Font, Text, Position, Color, 0, origin, 1, SpriteEffects.None, 0);
            base.Draw(spriteBatch);
        }
    }
}
