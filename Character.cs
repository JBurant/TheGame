using Microsoft.Xna.Framework;
using TheGame.Enums;
using TheGame.Utilities;

namespace TheGame
{
    public class Character : ObjectInGame
    {
        public bool isJumping;
        private float jumpTime;
        private readonly float jumpSpeed;
        private readonly float fallSpeed;
        private const float maxJumpTime = 0.5f;

        public Character(int x, int y, TextureInfo textureInfo) : base(x, y, textureInfo, 2)
        {
            Speed = 100f;
            jumpSpeed = 150f;
            fallSpeed = 180f;
        }

        public void MoveVertically(bool isUp, float deltaTime)
        {
            if(isUp)
            {
                isJumping = true;
            }

            if (isJumping)
            {

                Y -= (int)(jumpSpeed * deltaTime);
                jumpTime += deltaTime;

                if (jumpTime > maxJumpTime)
                {
                    isJumping = false;
                    jumpTime = 0;
                }

                MoveDirection = MoveDirectionType.Up;
            }
            else
            {
                Fall(deltaTime);
            }
        }

        protected virtual void Fall(float deltaTime)
        {
            Y += (int)(fallSpeed * deltaTime);
        }

        public void LethalCollision(ObjectInGame enemy, int frameWhenDied)
        {
            if(!(LastPosition.Y < Y))
            {
                Die(frameWhenDied);
            }
            else
            {
                enemy.Die(frameWhenDied);
            }
        }

        public void MoveHorizontally(bool right, bool left, float deltaTime)
        {
            if(right)
            {
                MoveRight(deltaTime);
                MoveDirection = MoveDirectionType.Right;
            }

            if(left)
            {
                MoveLeft(deltaTime);
                MoveDirection = MoveDirectionType.Left;
            }
        }

        protected override Rectangle GetAnimation()
        {
            if (MoveDirection == MoveDirectionType.None) column = 0;
            if (MoveDirection == MoveDirectionType.Left) column = 1;
            if (MoveDirection == MoveDirectionType.Right) column = 2;
            if (MoveDirection == MoveDirectionType.Up) column = 3;
            if (MoveDirection == MoveDirectionType.Down) column = 4;

            return new Rectangle(TextureInfo.Width * column, 0, TextureInfo.Width, TextureInfo.Height);
        }
    }
}
