namespace TheGame.Utilities
{
    public class TextureInfo
    {
        public string TextureFile { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public TextureInfo(string textureFile, int width, int height)
        {
            TextureFile = textureFile;
            Width = width;
            Height = height;
        }
    }
}
