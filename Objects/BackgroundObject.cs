using TheGame.Utilities;

namespace TheGame.Objects
{
    public class BackgroundObject : ObjectInGame
    {
        public BackgroundObject(int x, int y, TextureInfo textureInfo, float scale) : base(x, y, textureInfo, scale)
        {
            Speed = 0;
        }
    }
}
