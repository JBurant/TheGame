using Microsoft.Xna.Framework;
using TheGame.Enums;
using TheGame.Utilities;

namespace TheGame.Animation
{
    public class CharacterAnimationResolver : IAnimationResolver
    {
        private int column;
        private const int spacer = 1;

        private readonly int[] animateLeftColumns;
        private readonly int[] animateRightColumns;

        public TextureInfo TextureInfo { get; set; }

        public CharacterAnimationResolver(TextureInfo textureInfo, int[] animateLeftColumns, int[] animateRightColumns)
        {
            this.animateLeftColumns = animateLeftColumns;
            this.animateRightColumns = animateRightColumns;

            TextureInfo = textureInfo;
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

                case MoveDirectionType.Up:
                    column = 3;
                    break;

                case MoveDirectionType.Down:
                    column = 4;
                    break;
            }

            return new Rectangle((TextureInfo.Width + spacer) * column, 0, TextureInfo.Width, TextureInfo.Height);
        }
    }
}
