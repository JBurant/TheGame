using Microsoft.Xna.Framework;
using TheGame.Utilities;

namespace TheGame.Objects
{
    public class Landscape : ObjectInGame
    {
        private const float landscapeScale = 3f;

        public Landscape(int x, int y, TextureInfo textureInfo) : base(x, y, textureInfo, landscapeScale)
        {
            Hitbox = new Rectangle(X, Y, textureInfo.Width, textureInfo.Height);
        }
    }
}