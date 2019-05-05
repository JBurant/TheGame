using Microsoft.Xna.Framework;
using TheGame.Enums;
using TheGame.Utilities;

namespace TheGame.Animation
{
    public class MovingAnimationResolver : IAnimationResolver
    {
        private int column;
        private readonly int[] animateLeftColumns;
        private readonly int[] animateRightColumns;

        private readonly TextureInfo textureInfo;

        public MovingAnimationResolver(TextureInfo textureInfo, int[] animateLeftColumns, int[] animateRightColumns)
        {
            this.textureInfo = textureInfo;
            this.animateLeftColumns = animateLeftColumns;
            this.animateRightColumns = animateRightColumns;
        }

        private int GetAnimationColumn(int x, int[] animationColumns)
        {
            if ((x / 20) % 2 == 0)
            {
                return animationColumns[0];
            }
            else
            {
                return animationColumns[1];
            }
        }

        public Rectangle GetAnimation(int x, MoveDirectionType moveDirection)
        {
            switch (moveDirection)
            {
                case MoveDirectionType.None:
                    column = 0;
                    break;

                case MoveDirectionType.Left:
                    column = GetAnimationColumn(x, animateLeftColumns);
                    break;

                case MoveDirectionType.Right:
                    column = GetAnimationColumn(x, animateRightColumns);
                    break;
            }

            return new Rectangle((textureInfo.Width) * column, 0, textureInfo.Width, textureInfo.Height);
        }
    }
}
