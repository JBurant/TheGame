using TheGame.Enums;
using TheGame.Objects;

namespace TheGame
{
    public class ObjectsFactory
    {
        public Character GetCharacter(int x, int y)
        {
            return new Character(x, y, "Game/Character");
        }

        public ObjectInGame GetCow(int x, int y)
        {
            return new Critter(x, y, "Game/Cow", 20f, MoveDirectionType.Left);
        }

        public BackgroundObject GetBackground()
        {
            return new BackgroundObject(0, 0, "Game/Sky");
        }

        public Landscape GetLandscape(int x, int y)
        {
            return new Landscape(x, y, "Game/Land");
        }

        public ObjectInGame GetTree(int x, int y)
        {
            return new ObjectInGame(x, y, "Game/Tree");
        }

        public BackgroundObject GetCloud(int x, int y)
        {
            return new BackgroundObject(x, y, "Game/Cloud");
        }

        public BackgroundObject GetCloud2(int x, int y)
        {
            return new BackgroundObject(x, y, "Game/Cloud");
        }
    }
}
