using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheGame.Animation;
using TheGame.Utilities;

namespace TheGame.Objects
{
    public class BackgroundObject : ObjectInGame
    {
        public BackgroundObject(int x, int y, TextureInfo textureInfo, float scale) : base(x, y, textureInfo, scale)
        {
            Initialize(textureInfo);
        }

        protected override void Initialize(TextureInfo textureInfo)
        {
            animationResolver = new StaticAnimationResolver(textureInfo);
        }

        public override void Draw(SpriteBatch spriteBatch, int minX)
        {
            var sourceAnimation = animationResolver.GetAnimation(Position.X, Enums.MoveDirectionType.None);
            spriteBatch.Draw(Texture, new Vector2(Position.X - minX, Position.Y), sourceAnimation, Color.White, 0, new Vector2(0, 0), scale, SpriteEffects.None, 0);
        }
    }
}
