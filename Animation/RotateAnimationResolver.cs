using System;
using Microsoft.Xna.Framework;
using TheGame.Enums;
using TheGame.Utilities;

namespace TheGame.Animation
{
    public class RotateAnimationResolver : IAnimationResolver
    {
        private readonly TextureInfo textureInfo;

        public RotateAnimationResolver(TextureInfo textureInfo)
        {
            this.textureInfo = textureInfo;
        }

        public Rectangle GetAnimation(int x, MoveDirectionType moveDirection)
        {
            return new Rectangle(0, 0, textureInfo.Width, textureInfo.Height);
        }
    }
}
