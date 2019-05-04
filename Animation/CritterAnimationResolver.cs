using Microsoft.Xna.Framework;
using TheGame.Enums;
using TheGame.Utilities;

namespace TheGame.Animation
{
    public class CritterAnimationResolver : IAnimationResolver
    {
        public TextureInfo TextureInfo { get; set; }

        private int column;

        public CritterAnimationResolver(TextureInfo textureInfo, int[] animateLeftColumns, int[] animateRightColumns)
        {
            TextureInfo = textureInfo;
        }

        public Rectangle GetAnimation(int x, MoveDirectionType moveDirection)
        {
            if (moveDirection == MoveDirectionType.None) column = 0;
            if (moveDirection == MoveDirectionType.Left) column = 0;
            if (moveDirection == MoveDirectionType.Right) column = 1;

            return new Rectangle(TextureInfo.Width * column, 0, TextureInfo.Width, TextureInfo.Height);
        }
    }
}
