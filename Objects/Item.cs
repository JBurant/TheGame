using TheGame.Utilities;

namespace TheGame.Objects
{
    public class Item : ForegroundObject
    {
        public Item(int x, int y, TextureInfo textureInfo) : base(x, y, textureInfo, 3)
        {
        }

        public virtual void PickUp(int frame)
        {
            Die(frame);
        }
    }
}
