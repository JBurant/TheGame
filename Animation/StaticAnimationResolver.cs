using Microsoft.Xna.Framework;
using TheGame.Enums;
using TheGame.Utilities;

namespace TheGame.Animation
{
    public class StaticAnimationResolver : IAnimationResolver
    {
        public TextureInfo TextureInfo { get; set; }

        public StaticAnimationResolver(TextureInfo textureInfo)
        {
            TextureInfo = textureInfo;
        }

        public Rectangle GetAnimation(int x, MoveDirectionType moveDirection)
        {
            return new Rectangle(0, 0, TextureInfo.Width, TextureInfo.Height);
        }
    }
}
