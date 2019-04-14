namespace TheGame.Utilities
{
    public class ObjectSpecification
    {
        public string Name { get; }
        public float X { get; set; }
        public float Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public ObjectSpecification(string name, float positionX, float positionY, int width, int height)
        {
            Name = name;
            X = positionX;
            Y = positionY;
            Width = width;
            Height = height;
        }
    }
}
