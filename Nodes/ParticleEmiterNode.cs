using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Color_Breaker
{
    public class ParticleEmiterNode : Node
    {

        public int Amount = 300;
        public Color Color;
        private Random rnd;
        private Texture2D _texture;
        private float _lifeTime;


        public ParticleEmiterNode(Texture2D texture, Color color)
        {
            Color = color;
            _texture = texture;
            rnd = new Random(DateTime.Now.Millisecond);
            _lifeTime = 0.2f;
        }

        public override void Update(float deltaTime)
        {
            if (IsFreezed) return;
            _lifeTime -= deltaTime;
            if (_lifeTime <= 0) Free = true;

            for (int i = 0; i < Amount * deltaTime; i++)
            {

                Vector2 dir = new Vector2((float)Math.Cos(rnd.NextDouble() * Math.PI * 2), (float)Math.Sin(rnd.NextDouble() * Math.PI * 2)) * rnd.Next(100,150);

                ParticleNode particle = new ParticleNode(_texture, dir, (float)rnd.NextDouble() * 0.8f, Color);
                particle.Position = GlobalPosition;
                particle.Scale = new Vector2((float)rnd.NextDouble() * 0.2f);
                Services.Get<INodeTree>().Add(particle);
            }

            base.Update(deltaTime);
        }

    }

    public class ParticleNode : SpriteNode
    {
        private Vector2 _velocity;
        private float _lifeTime;

        public ParticleNode(Texture2D texture, Vector2 velocity, float lifeTime, Color color) : base(texture,0,0, Layers.Particles,color)
        {
            _velocity = velocity;
            _lifeTime = lifeTime;
        }

        public override void Update(float deltaTime)
        {
            if (IsFreezed) return;
            _lifeTime -= deltaTime;
            Position += _velocity * deltaTime;

            if (_lifeTime <= 0) Free = true;

            base.Update(deltaTime);
        }
    }
}
