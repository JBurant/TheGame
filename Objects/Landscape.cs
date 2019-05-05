using Microsoft.Xna.Framework;
using System;
using TheGame.Utilities;

namespace TheGame.Objects
{
    public class Landscape : ForegroundObject
    {
        private const float landscapeScale = 3f;

        public Landscape(int x, int y, TextureInfo textureInfo) : base(x, y, textureInfo, landscapeScale)
        {
            Hitbox = new Rectangle(x, y, textureInfo.Width, textureInfo.Height);
        }
    }
}