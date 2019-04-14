namespace TheGame.Utilities
{
    public class ObjectSpecification
    {
        public string Name { get; }
        public float X { get; set; }
        public float Y { get; set; }

        public ObjectSpecification(string name, float positionX, float positionY)
        {
            Name = name;
            X = positionX;
            Y = positionY;
        }
    }
}
