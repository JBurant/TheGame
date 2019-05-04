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

        public ObjectInGame GetCow(int x, int y, TextureInfo textureInfo)
        {
            return new Critter(x, y, textureInfo, 20f, MoveDirectionType.Left);
        }

        public BackgroundObject GetBackground(TextureInfo textureInfo)
        {
            return new BackgroundObject(0, 0, textureInfo, 1);
        }

        public Landscape GetLandscape(int x, int y, TextureInfo textureInfo)
        {
            return new Landscape(x, y, textureInfo);
        }

        public ObjectInGame GetTree(int x, int y, TextureInfo textureInfo)
        {
            return new ObjectInGame(x, y, textureInfo, 3);
        }

        public BackgroundObject GetCloud(int x, int y, TextureInfo textureInfo)
        {
            return new BackgroundObject(x, y, textureInfo, 1);
        }

        public BackgroundObject GetCloud2(int x, int y, TextureInfo textureInfo)
        {
            return new BackgroundObject(x, y, textureInfo, 1);
        }
    }
}
