using Microsoft.Xna.Framework;
using TheGame.Enums;
using TheGame.Utilities;

namespace TheGame.Animation
{
    public interface IAnimationResolver
    {
        TextureInfo TextureInfo { get; set; }

        Rectangle GetAnimation(int x, MoveDirectionType moveDirection);
    }
}
