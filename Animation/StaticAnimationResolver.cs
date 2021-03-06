﻿using Microsoft.Xna.Framework;
using TheGame.Enums;
using TheGame.Utilities;

namespace TheGame.Animation
{
    public class StaticAnimationResolver : IAnimationResolver
    {
        private TextureInfo textureInfo;

        public StaticAnimationResolver(TextureInfo textureInfo)
        {
            this.textureInfo = textureInfo;
        }

        public Rectangle GetAnimation(int x, MoveDirectionType moveDirection)
        {
            return new Rectangle(0, 0, textureInfo.Width, textureInfo.Height);
        }
    }
}
