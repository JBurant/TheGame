namespace TheGame.Utilities
{
    public class ObjectSpecification
    {
        public TextureInfo TextureInfo { get; }
        public float X { get; set; }
        public float Y { get; set; }

        public ObjectSpecification(TextureInfo textureInfo, float x, float y)
        {
            TextureInfo = textureInfo;
            X = x;
            Y = y;
        }
    }
}
