using Microsoft.Xna.Framework;
using TheGame.Animation;
using TheGame.Enums;
using TheGame.Utilities;

namespace TheGame.Objects
{
    public class Critter : ObjectInGame
    {
        public Critter(int x, int y, TextureInfo textureInfo, float speed, MoveDirectionType initialMoveDirection) : base(x, y, textureInfo, true, initialMoveDirection)
        {
            Speed = speed;
            animationResolver = new CritterAnimationResolver(textureInfo, new int[] { 1, 3 }, new int[] { 2, 4 });
        }

        public override void Move(float deltaTime)
        {
            if(MoveDirection == MoveDirectionType.Left)
            {
                MoveLeft(deltaTime);
            }

            if(MoveDirection == MoveDirectionType.Right)
            {
                MoveRight(deltaTime);
            }
        }

        public override void NonLethalCollision(Rectangle hitbox)
        {
            if (MoveDirection == MoveDirectionType.Left)
            {
                MoveDirection = MoveDirectionType.Right;
            }
            else if(MoveDirection == MoveDirectionType.Right)
            {
                MoveDirection = MoveDirectionType.Left;
            }
        }
    }
}
