using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using TheGame.Animation;
using TheGame.Enums;
using TheGame.Utilities;

namespace TheGame.Objects
{
    public class ObjectInGame
    {
        public Texture2D Texture { get; set; }
        protected readonly float scale;
        public string TextureFile { get; set; }

        public Point Position;
        public Point Size { get; set; }

        protected IAnimationResolver animationResolver;

        public ObjectInGame(int x, int y, TextureInfo textureInfo, float scale)
        {
            Position = new Point(x, y);
            Size = new Point((int)Math.Floor(textureInfo.Width * scale), (int)Math.Floor(textureInfo.Height * scale));
            TextureFile = textureInfo.TextureFile;
            this.scale = scale;

            Initialize(textureInfo);
        }

        protected virtual void Initialize(TextureInfo textureInfo)
        {
            animationResolver = new StaticAnimationResolver(textureInfo);
        }

        public virtual void Draw(SpriteBatch spriteBatch, int worldPosition)
        {
        }
    }
}