using TheGame.Utilities;

namespace TheGame.Objects
{
    public class Item : ForegroundObject
    {
        public Item(int x, int y, TextureInfo textureInfo, int score) : base(x, y, textureInfo, 3, score)
        {
        }
    }
}
