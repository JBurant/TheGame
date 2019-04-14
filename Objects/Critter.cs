using Microsoft.Xna.Framework;
using TheGame.Enums;
using TheGame.Utilities;

namespace TheGame.Objects
{
    public class Critter : ObjectInGame
    {
        public Critter(int x, int y, TextureInfo textureInfo, float speed, MoveDirectionType initialMoveDirection) : base(x, y, textureInfo, true, initialMoveDirection)
        {
            Speed = speed;
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
