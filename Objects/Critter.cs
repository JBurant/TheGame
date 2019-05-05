using Microsoft.Xna.Framework;
using TheGame.Animation;
using TheGame.Enums;
using TheGame.Utilities;

namespace TheGame.Objects
{
    public class Critter : ForegroundObject
    {
        protected int speed;

        public Critter(int x, int y, TextureInfo textureInfo, float scale, int speed, MoveDirectionType initialMoveDirection) : base(x, y, textureInfo, scale, initialMoveDirection)
        {
            this.speed = speed;
        }

        protected override void Initialize(TextureInfo textureInfo)
        {
            animationResolver = new MovingAnimationResolver(textureInfo, new int[] { 0, 2 }, new int[] { 1, 3 });
        }

        public override void Move(float deltaTime)
        {
            if(MoveDirection == MoveDirectionType.Left)
            {
                Position.X -= (int)(speed * deltaTime);
            }

            if(MoveDirection == MoveDirectionType.Right)
            {
                Position.X += (int)(speed * deltaTime);
            }

            if (Position.X < 0)
            {
                Position.X = 0;
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
