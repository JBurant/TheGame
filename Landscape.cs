using Microsoft.Xna.Framework;
using TheGame.Utilities;

namespace TheGame
{
    public class Landscape : ObjectInGame
    {
        private static readonly string textureFile = "Game/Land";

        public Landscape(int x, int y, string textureFile) : base(x, y, textureFile)
        {
        }

        public override void SetHitbox()
        {
            Hitbox = new Rectangle((int)X, (int)Y, Texture.Width, Texture.Height);
        }
    }
}