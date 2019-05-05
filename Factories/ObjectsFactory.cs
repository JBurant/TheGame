using TheGame.Enums;
using TheGame.Objects;
using TheGame.Utilities;

namespace TheGame.Factories
{
    public class ObjectsFactory
    {
        public Character GetCharacter(int x, int y, TextureInfo textureInfo)
        {
            return new Character(x, y, textureInfo);
        }

        public Critter GetCow(int x, int y, TextureInfo textureInfo)
        {
            return new Critter(x, y, textureInfo, 3f, 60, MoveDirectionType.Left);
        }

        public BackgroundObject GetBackground(TextureInfo textureInfo)
        {
            return new BackgroundObject(0, 0, textureInfo, 2);
        }

        public Landscape GetLandscape(int x, int y, TextureInfo textureInfo)
        {
            return new Landscape(x, y, textureInfo);
        }

        public Landscape GetTree(int x, int y, TextureInfo textureInfo)
        {
            return new Landscape(x, y, textureInfo);
        }

        public BackgroundObject GetBackgroundObject(int x, int y, TextureInfo textureInfo)
        {
            return new BackgroundObject(x, y, textureInfo, 1);
        }

        public BackgroundObject GetBackgroundObject(int x, int y, TextureInfo textureInfo, float scale)
        {
            return new BackgroundObject(x, y, textureInfo, scale);
        }

        public RotatingObject GetRotatingObject(int x, int y, TextureInfo textureInfo)
        {
            return new RotatingObject(x, y, textureInfo);
        }

        public Tulip GetTulip(int x, int y, TextureInfo textureInfo)
        {
            return new Tulip(x, y, textureInfo);
        }
    }
}
