using Microsoft.Xna.Framework;
using TheGame.Utilities;

namespace TheGame
{
    public class Landscape : ObjectInGame
    {
        public Landscape(int x, int y, TextureInfo textureInfo) : base(x, y, textureInfo)
        {
        }

        public override void SetHitbox()
        {
            Hitbox = new Rectangle(X, Y, TextureInfo.Width, TextureInfo.Height);
        }
    }
}